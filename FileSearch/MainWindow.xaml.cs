#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: MainWindow.xaml.cs
// Date - created:2016.07.10 - 11:19
// Date - current: 2016.07.13 - 19:01

#endregion

#region Usings

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using FileSearch.Algorithms.SearchAlgorithm;
using FileSearch.SearchingAlgorithms;
using MessageBox = System.Windows.MessageBox;

#endregion

namespace FileSearch
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Dictionary<string, FileGatheringAlgorithm> _fileGatheringAlgorithms;
        private readonly Dictionary<string, ContentSearchAlgorithm> _fileSearchingAlgorithms;

        private FileGathering _thatsHowIGatherFiles;
        private FileContains _thatsHowISearch;

        public MainWindow()
        {
            InitializeComponent();
            SingletonContentFactory.LoadContent();

            // Get all file-searching algorithms and put them into a combobox.
            _fileGatheringAlgorithms = UltimateFactory<FileGatheringAlgorithm>.Compute();
            _fileGatheringAlgorithms.GetKeys().ForEach(x => FileGatheringCmbbx.Items.Add(x));

            // Get all "string in File"-searching algorithms and put them into another combobox.
            _fileSearchingAlgorithms = UltimateFactory<ContentSearchAlgorithm>.Compute();
            _fileSearchingAlgorithms.GetKeys().ForEach(x => AlgorithmCmbbx.Items.Add(x));

            // If one of those dictionary is empty, than we have a problem...
            if (AlgorithmCmbbx.Items.Count == 0 || FileGatheringCmbbx.Items.Count == 0)
            {
                MessageBox.Show(
                    $"Someone's being lazy here!\n{(AlgorithmCmbbx.Items.Count == 0 ? "There are NO algorithms, to search for content, loaded!" : "")}\n{(FileGatheringCmbbx.Items.Count == 0 ? "There are NO file gathering methods loaded!" : "")}",
                    "Ehm", new MessageBoxButton(),
                    MessageBoxImage.Error);
                Close(); // Rage quit
                return;
            }

            FileGatheringCmbbx.SelectedIndex = FileGatheringCmbbx.Items.Count - 1;
            AlgorithmCmbbx.SelectedIndex = AlgorithmCmbbx.Items.Count - 1;
        }

        // Is only true, when the path is set correctly
        private bool Workz { get; set; }

        private void pathTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Load content
            Workz = Directory.Exists(PathTb.Text);
        }

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

        private void searchBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!Workz && !Directory.Exists(PathTb.Text))
            {
                MessageBox.Show("Path not set", "Error", new MessageBoxButton(), MessageBoxImage.Error);
                return;
            }

            TreeView.Items.Clear();

            // Select the correct search algorithm by the SelectedIndex; If it returns null -> create a temp array
            foreach (
                var node in
                    _thatsHowIGatherFiles(PathTb.Text, _thatsHowISearch, SearchForTb.Text, ExtensionTb.Text,
                        SubfolderCkbx.IsChecked ?? false))
            {
                TreeView.Items.Add(node);
            }

            TreeView.Items.OfType<TreeViewItem>().ToList().ForEach(x => x.ExpandSubtree());

            MessageBox.Show("Done searching");
        }

        private void algorithmCMBBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _thatsHowISearch = _fileSearchingAlgorithms[AlgorithmCmbbx.SelectedItem.ToString()].Algorithm;
        }

        private void FileGatheringCmbbx_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _thatsHowIGatherFiles = _fileGatheringAlgorithms[FileGatheringCmbbx.SelectedItem.ToString()].Algorithm;
        }
    }
}