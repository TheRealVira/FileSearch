#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: ContentSearchAlgorithm.cs
// Date - created:2016.07.13 - 17:46
// Date - current: 2016.07.13 - 18:44

#endregion

#region Usings

using FileSearch.SearchingAlgorithms;

#endregion

namespace FileSearch.Algorithms.SearchAlgorithm
{
    /// <summary>
    ///     This class is not only used, to get some variable protection, it's also used to automatically initialise every
    ///     class, which inherit this abstract class and represents
    ///     a searching algorithm. (The name of the class (which inherit this abstract one) will be used as key in the
    ///     dictionary in the SearchAlgorithmFactory.)
    /// </summary>
    public abstract class ContentSearchAlgorithm
    {
        public readonly FileContains Algorithm;

        protected ContentSearchAlgorithm()
        {
            Algorithm = MySearchAlgo;
        }

        protected abstract bool MySearchAlgo(string file, string content);
    }
}