#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: MainWindow.xaml.cs
// Date - created:2016.07.10 - 11:19
// Date - current: 2016.08.31 - 00:50

#endregion

#region Usings

using System;

#endregion

namespace FileSearch
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     For good practice:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            SingletonContentFactory.Dispose();
        }
    }
}