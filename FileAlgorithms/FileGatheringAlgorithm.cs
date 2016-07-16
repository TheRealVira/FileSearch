#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileAlgorithms
// Filename: FileGatheringAlgorithm.cs
// Date - created:2016.07.15 - 17:47
// Date - current: 2016.07.16 - 19:02

#endregion

#region Usings

using System.Collections.Generic;

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

        protected abstract IEnumerable<string> MySearchAlgo(string directory,
            string textToSearchFor,
            string searchcrets = "*", bool subfolder = true);
    }
}