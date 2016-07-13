#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: NotDoingAnything.cs
// Date - created:2016.07.13 - 17:40
// Date - current: 2016.07.13 - 18:39

#endregion

#region Usings

using System.Windows.Controls;

#endregion

namespace FileSearch.SearchingAlgorithms
{
    internal class Iterative : FileGatheringAlgorithm
    {
        protected override TreeViewItem[] MySearchAlgo(string directory, FileContains searchAlgorithm,
            string textToSearchFor,
            string searchcrets = "*", bool subfolder = true)
        {
            // TODO: Don't be so lazy and think about a plan how to do this!
            return new TreeViewItem[0];
        }
    }
}