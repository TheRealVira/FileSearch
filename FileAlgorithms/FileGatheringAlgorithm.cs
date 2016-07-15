#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileAlgorithms
// Filename: FileGatheringAlgorithm.cs
// Date - created:2016.07.15 - 17:47
// Date - current: 2016.07.15 - 21:54

#endregion

#region Usings

using System.Windows.Controls;

#endregion

namespace FileAlgorithms
{
    public abstract class FileGatheringAlgorithm
    {
        public readonly FileGathering Algorithm;

        protected FileGatheringAlgorithm()
        {
            Algorithm = MySearchAlgo;
        }

        protected abstract TreeViewItem[] MySearchAlgo(string directory, FileContains searchAlgorithm,
            string textToSearchFor,
            string searchcrets = "*", bool subfolder = true);
    }
}