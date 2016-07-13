#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: Delegates.cs
// Date - created:2016.07.10 - 16:08
// Date - current: 2016.07.13 - 19:17

#endregion

#region Usings

using System.Windows.Controls;

#endregion

namespace FileSearch.SearchingAlgorithms
{
    public delegate TreeViewItem[] FileGathering(
        string directory, FileContains searchAlgorithm, string textToSearchFor, string searchcrets = "*.*",
        bool subfolder = true);

    public delegate bool FileContains(string file, string content);
}