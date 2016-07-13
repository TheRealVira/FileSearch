#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: FileGatheringAlgorithm.cs
// Date - created:2016.07.13 - 17:40
// Date - current: 2016.07.13 - 18:39

#endregion

#region Usings

using System.Windows.Controls;

#endregion

namespace FileSearch.SearchingAlgorithms
{
    /// <summary>
    ///     This class is not only used, to get some variable protection, it's also used to automatically initialise every
    ///     class, which inherit this abstract class and represents
    ///     a searching algorithm. (The name of the class (which inherit this abstract one) will be used as key in the
    ///     dictionary in the SearchAlgorithmFactory.)
    /// </summary>
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