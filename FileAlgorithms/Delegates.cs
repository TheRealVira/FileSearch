#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileAlgorithms
// Filename: Delegates.cs
// Date - created:2016.07.15 - 17:48
// Date - current: 2016.07.15 - 21:54

#endregion

#region Usings

using System.Windows.Controls;

#endregion

namespace FileAlgorithms
{
    public delegate TreeViewItem[] FileGathering(
        string directory, FileContains searchAlgorithm, string textToSearchFor, string searchcrets = "*.*",
        bool subfolder = true);

    public delegate bool FileContains(string file, string content);
}