#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: SearchFile.xaml.cs
// Date - created:2016.08.30 - 21:04
// Date - current: 2016.08.31 - 00:50

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Threading;
using FileAlgorithms;
using FileSearch.Modes;
using FileSearch.SearchingAlgorithms;
using Application = System.Windows.Application;
using Clipboard = System.Windows.Clipboard;
using FileContains = FileAlgorithms.FileContains;
using FileGathering = FileAlgorithms.FileGathering;
using ListBox = System.Windows.Controls.ListBox;
using MessageBox = System.Windows.MessageBox;

#endregion

namespace FileSearch
{
    /// <summary>
    ///     Interaction logic for SearchFile.xaml
    /// </summary>
    public partial class SearchFile
    {
        /// <summary>
        ///     A bit hacky... :P
        /// </summary>
        public static ListBox PublicFoundItems;

        /// <summary>
        ///     Those algorithms will gather my files together (WITHOUT validations)
        /// </summary>
        private readonly Dictionary<string, FileGatheringAlgorithm> _fileGatheringAlgorithms;

        /// <summary>
        ///     Those algorithms will determine, if my gathered files are worth to be in my premium listbox (called 'FoundItems')
        /// </summary>
        private readonly Dictionary<string, ContentSearchAlgorithm> _fileSearchingAlgorithms;

        /// <summary>
        ///     The current algorithm to gather files.
        /// </summary>
        private FileGathering _thatsHowIGatherFiles;

        /// <summary>
        ///     The current algorithm to validate files.
        /// </summary>
        private FileContains _thatsHowISearch;

        public SearchFile()
        {
            InitializeComponent();
            SingletonContentFactory.LoadContent();

            PublicFoundItems = FoundItems;

            // Setup running mode events
            ModeManager.RunningEvent += (sender, args) =>
            {
                PauseBtn.Content = "Pause";
                PauseBtn.Visibility = Visibility.Visible;
                SearchBtn.Content = "Stop";
            };

            ModeManager.PausingEvent += (sender, args) => { PauseBtn.Content = "Resume"; };

            ModeManager.StoppingEvent += (sender, args) =>
            {
                PauseBtn.Visibility = Visibility.Hidden;
                SearchBtn.Content = "Search";
            };

            _fileGatheringAlgorithms = UltimateFactory<FileGatheringAlgorithm>.Compute(AppDomain.CurrentDomain);
            _fileSearchingAlgorithms = UltimateFactory<ContentSearchAlgorithm>.Compute(AppDomain.CurrentDomain);

            // Get all file-searching algorithms and put them into a combobox.
            _fileGatheringAlgorithms.GetKeys().ForEach(x => FileGatheringCmbbx.Items.Add(x));

            // Get all "string in File"-searching algorithms and put them into another combobox.
            _fileSearchingAlgorithms.GetKeys().ForEach(x => AlgorithmCmbbx.Items.Add(x));

            // Loads custom plugins (if there are any)
            Loadplugin();

            // If one of those dictionary is empty, than we have a problem...
            if (AlgorithmCmbbx.Items.Count == 0 || FileGatheringCmbbx.Items.Count == 0)
            {
                MessageBox.Show(
                    $"Someone's being lazy here!\n{(AlgorithmCmbbx.Items.Count == 0 ? "There are NO algorithms, to search for content, loaded!" : "")}\n{(FileGatheringCmbbx.Items.Count == 0 ? "There are NO file gathering methods loaded!" : "")}",
                    "Ehm", new MessageBoxButton(),
                    MessageBoxImage.Error);
                Application.Current.Shutdown(); // Rage quit
                return;
            }

            FileGatheringCmbbx.SelectedIndex = 0;
            AlgorithmCmbbx.SelectedIndex = 0;
        }

        // Is only true, when the path is set correctly
        private bool PathIsValid { get; set; }

        /// <summary>
        ///     Will load all *.dll in the Plugins directory and add the matching algorithms to their matching combobox.
        /// </summary>
        private void Loadplugin()
        {
            if (!Directory.Exists("Plugins")) return;

            // NOTE: .AsParallel().ForAll creates an endless break here! Please don't ask me why...

            foreach (var file in Directory.GetFiles("Plugins").Where(x => x.EndsWith(".dll")))
            {
                UltimateFactory<FileGatheringAlgorithm>.Compute(Assembly.LoadFrom(file)).ToList().ForEach(x =>
                {
                    _fileGatheringAlgorithms.TryAdd(x.Key, x.Value);

                    if (FileGatheringCmbbx.Dispatcher.CheckAccess())
                        // Check if the thread allows us to access the control
                    {
                        FileGatheringCmbbx.Items.Add(x.Key); // If so, than we just need to add the new item
                    }
                    else
                    {
                        // If not, than it gets a bit complicater:
                        // Now we have to invoke the dispatcher of the control from its current thread to access it.
                        FileGatheringCmbbx.Dispatcher.Invoke(DispatcherPriority.Normal,
                            new Action(() => FileGatheringCmbbx.Items.Add(x.Key)));
                    }
                });

                UltimateFactory<ContentSearchAlgorithm>.Compute(Assembly.LoadFrom(file)).ToList().ForEach(x =>
                {
                    _fileSearchingAlgorithms.TryAdd(x.Key, x.Value);

                    if (AlgorithmCmbbx.Dispatcher.CheckAccess()) // Check if the thread allows us to access the control
                    {
                        AlgorithmCmbbx.Items.Add(x.Key); // If so, than we just need to add the new item
                    }
                    else
                    {
                        // If not, than it gets a bit complicater:
                        // Now we have to invoke the dispatcher of the control from its current thread to access it.
                        AlgorithmCmbbx.Dispatcher.Invoke(DispatcherPriority.Normal,
                            new Action(() => AlgorithmCmbbx.Items.Add(x.Key)));
                    }
                });
            }
        }

        /// <summary>
        ///     When changing the path by hand, than you shall not pass if you enter something incorrect!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pathTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Load content
            PathIsValid = Directory.Exists(PathTb.Text);
        }

        /// <summary>
        ///     This button will call an dialog, which lets the user pick an directory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseBTN_Click(object sender, RoutedEventArgs e)
        {
            var dia = new FolderBrowserDialog();
            dia.ShowDialog();

            if (!Directory.Exists(dia.SelectedPath))
            {
                MessageBox.Show("Directory not found", "Error", new MessageBoxButton(), MessageBoxImage.Error);
                return;
            }

            PathTb.Text = dia.SelectedPath;
        }

        /// <summary>
        ///     This button is Search and Stop button, but never at the same time.
        ///     When the button's content is showing 'Search' than it'll start(over) the searching algorithms (in a
        ///     separate task).
        ///     When the button's content is showing 'Stop' than it'll stop the algorithms and jump out of the separate task.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchBTN_Click(object sender, RoutedEventArgs e)
        {
            if (ModeManager.CurrentMode != RunningMode.Stop)
                // If the current search button is in fact the 'Stop'-Button, we'll stop, when clicked again.
            {
                ModeManager.CurrentMode = RunningMode.Stop;
                return;
            }

            if (!PathIsValid && !Directory.Exists(PathTb.Text))
            {
                MessageBox.Show("Path not set", "Error", new MessageBoxButton(), MessageBoxImage.Error);
                return;
            }

            FoundItems.Items.Clear();

            var check = SubfolderCkbx.IsChecked ?? false;
            var path = PathTb.Text;
            var searchText = SearchForTb.Text;
            var extension = ExtensionTb.Text;
            ModeManager.CurrentMode = RunningMode.Run;
            Task.Factory.StartNew(() =>
            {
                //SearchBtn.Dispatcher.Invoke(() => SearchBtn.Content = "Stop");

                foreach (
                    var file in
                        _thatsHowIGatherFiles(path, searchText, extension,
                            check).Where(x => x != null))
                {
                    while (ModeManager.CurrentMode == RunningMode.Pause)
                    {
                    } // While we are paused, we'd stuck in an endless loop

                    if (ModeManager.CurrentMode == RunningMode.Stop)
                        return; // If we should stop, than we jump out of this Task

                    try
                    {
                        if (!_thatsHowISearch(file, searchText))
                            continue;
                        // If the file doesn't contain the content we wish to see, than we'll continue with other files
                    }
                    catch (Exception ex)
                    {
#if DEBUG
                        Console.WriteLine(ex.Message);
#endif
                    }

                    FoundItems.Dispatcher.Invoke(
                        () => // Just some invoking, because we're in an other task than the control
                        {
                            FoundItems.Items.Add(file);
                            FoundItems.SelectedIndex = FoundItems.Items.Count - 1;
                            FoundItems.ScrollIntoView(FoundItems.SelectedItem);
                        });
                }

                SearchBtn.Dispatcher.Invoke(
                    () =>
                        // Same invoke thingy going on here, which is because we'll change properties of the 'SearchBtn' and the 'PauseBtn'
                    { PauseBtn.Dispatcher.Invoke(() => { ModeManager.CurrentMode = RunningMode.Stop; }); });

                // Notify the user, that we are finished now:
                MessageBox.Show("Done searching");
            });
        }

        /// <summary>
        ///     When the selection of the ContentSearchingAlgorithms changes, the current algorithm changes too.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void algorithmCMBBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _thatsHowISearch = _fileSearchingAlgorithms[AlgorithmCmbbx.SelectedItem.ToString()].Algorithm;
        }

        /// <summary>
        ///     When the selection of the FileSearchingAlgorithms changes, the current algorithm changes too.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileGatheringCmbbx_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _thatsHowIGatherFiles = _fileGatheringAlgorithms[FileGatheringCmbbx.SelectedItem.ToString()].Algorithm;
        }

        /// <summary>
        ///     For good practice:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            FoundItems.Items.Clear();
        }

        /// <summary>
        ///     This button won't be visible, when the current mode is 'Stop' (you could say the program isn't in sleeping state,
        ///     when this button lights up :))
        ///     When the content of this button saies 'Pause', than it'll pause the algorithms (but won't stop them!!!)
        ///     When the content of this button saies 'Resum', than it'll resume the paused algorithms... (Who'd guessed it xD)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PauseBtn_OnClickBTN_Click(object sender, RoutedEventArgs e)
        {
            if (ModeManager.CurrentMode == RunningMode.Run)
            {
                ModeManager.CurrentMode = RunningMode.Pause;
                return;
            }

            // Because this button is only visible, when the current RunMode is equal to Run or Pause, we don't have to write down another if equation
            ModeManager.CurrentMode = RunningMode.Run;
        }

        /// <summary>
        ///     Copies the current selected item into teh clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyBTN_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(FoundItems.SelectedItems.Cast<object>()
                .Aggregate(string.Empty, (current, t) => current + Environment.NewLine + t));
        }

        private void FoundItems_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CopyBtn.Visibility = FoundItems.SelectedItem == null || ModeManager.CurrentMode != RunningMode.Stop
                ? Visibility.Hidden
                : Visibility.Visible;
        }
    }
}