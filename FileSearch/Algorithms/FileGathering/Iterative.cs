#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: Iterative.cs
// Date - created:2016.07.13 - 17:40
// Date - current: 2016.08.31 - 00:50

#endregion

#region Usings

using System.Collections.Generic;
using FileAlgorithms;

#endregion

namespace FileSearch.Algorithms.FileGathering
{
    /// <summary>
    ///     May be done in the future :P
    /// </summary>
    [TestingPurpose]
    internal class Iterative : FileGatheringAlgorithm
    {
        protected override IEnumerable<string> MySearchAlgo(string directory,
            string textToSearchFor,
            string searchcrets = "*", bool subfolder = true)
        {
            // TODO: Don't be so lazy and think about a plan how to do this!
            return new string[0];
        }
    }
}