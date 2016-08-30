#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: CountLines.xaml.cs
// Date - created:2016.08.30 - 21:16
// Date - current: 2016.08.31 - 00:50

#endregion

#region Usings

using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

#endregion

namespace FileSearch
{
    /// <summary>
    ///     Interaction logic for CountLines.xaml
    /// </summary>
    public partial class CountLines
    {
        /// <summary>
        ///     Indicates, if we are bussy or not (if we're currently working in other tasks)
        /// </summary>
        private bool _isBussy;

        public CountLines()
        {
            InitializeComponent();
        }

        private void CountBtn_OnClickBTN_Click(object sender, RoutedEventArgs e)
        {
            if (_isBussy) return;

            Task.Factory.StartNew(() =>
            {
                _isBussy = true;
                Counter.Dispatcher.Invoke(() => // Reseting the counter
                { Counter.Content = "0"; });

                OnlySelectedItemsCkBx.Dispatcher.Invoke(() =>
                {
                    if (OnlySelectedItemsCkBx.IsChecked.Value)
                    {
                        for (var i = 0; i < SearchFile.PublicFoundItems.SelectedItems.Count; i++)
                            // Going through every file
                        {
                            CountTheLines(SearchFile.PublicFoundItems.SelectedItems[i].ToString());
                        }
                    }
                    else
                    {
                        for (var i = 0; i < SearchFile.PublicFoundItems.Items.Count; i++) // Going through every file
                        {
                            CountTheLines(SearchFile.PublicFoundItems.Items[i].ToString());
                        }
                    }
                });

                _isBussy = false;
            });
        }

        private void CountTheLines(string file)
        {
            if (!File.Exists(file))
                return;
            // If file doesn't exist (which only happens, when the user changes stuff, after reading files in), than jump out

            EndOfLineIndecatorTb.Dispatcher.Invoke(() =>
            {
                Counter.Dispatcher.Invoke(() =>
                {
                    using (var reader = new StreamReader(file)) // Open the file.
                    {
                        try
                        {
                            while (!reader.EndOfStream) // Read, while there is stuff to read.
                            {
                                var readLine = reader.ReadLine();
                                if (EndOfLineIndecatorTb.Text == "\\n") // Basic
                                {
                                    AddOne();
                                    continue;
                                }

                                if (readLine != null && readLine.Contains(EndOfLineIndecatorTb.Text))
                                    // If current line contains the string to search (one or multiple times), then...
                                {
                                    var times =
                                        readLine.Select((c, i) => readLine.Substring(i))
                                            .Count(sub => sub.StartsWith(EndOfLineIndecatorTb.Text));
                                    // ... coiunt how many times.

                                    for (var i = 0; i < times; i++) // Add one per found.
                                    {
                                        AddOne();
                                    }
                                }
                            }
                        }
                        finally // Clean closing
                        {
                            reader.Close();
                            reader.Dispose();
                        }
                    }
                }, DispatcherPriority.Render);
            });
        }

        /// <summary>
        ///     This is a funny little adder, which adds one to an integer (as a string).
        /// </summary>
        public void AddOne()
        {
            var i = 0;
            var carry = true;
            var number = (string) Counter.Content;

            while (carry && i < number.Length)
            {
                i++;
                var current = byte.Parse(number[number.Length - i].ToString());

                var before = number.Substring(0, number.Length - i < 0 ? 0 : number.Length - i);
                var after = number.Substring(number.Length - (i - 1));

                if (current == 9)
                {
                    number = before + "0" + after;
                    continue;
                }

                number = before + (current + 1) + after;

                Counter.Content = number;
                return;
            }

            number = number.Insert(0, "1");
            Counter.Content = number;
        }

        /// <summary>
        ///     Also a funny little subtractor, which subtracts one of an integer (as a string).
        /// </summary>
        public void SubtractOne()
        {
            var i = 0;
            var carry = true;
            var number = (string) Counter.Content;

            while (carry && i < number.Length)
            {
                i++;
                var current = byte.Parse(number[number.Length - i].ToString());

                var before = number.Substring(0, number.Length - i < 0 ? 0 : number.Length - i);
                var after = number.Substring(number.Length - (i - 1));

                if (current == 0)
                {
                    number = before + "9" + after;
                    continue;
                }

                number = before + (current - 1) + after;

                number = number.TrimStart('0');
                Counter.Content = number;
                return;
            }

            number = number.TrimStart('0');
            number = number == string.Empty ? "0" : number;
            Counter.Content = number;
        }

        /// <summary>
        ///     Copys the count into the clipboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyBTN_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText((string) Counter.Content);
        }

        /// <summary>
        ///     Resets the current count to 0 (with animation).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetBtn_OnClickBTN_Click(object sender, RoutedEventArgs e)
        {
            if (_isBussy || (string) Counter.Content == "0") return;

            Task.Factory.StartNew(() =>
            {
                _isBussy = true;

                while (true)
                {
                    var shouldBreak = false;
                    Counter.Dispatcher.Invoke(() =>
                    {
                        SubtractOne();

                        if ((string) Counter.Content == "0")
                        {
                            shouldBreak = true;
                        }
                    }, DispatcherPriority.Render);

                    if (shouldBreak)
                    {
                        _isBussy = false;
                        break;
                    }
                }
            });
        }
    }
}