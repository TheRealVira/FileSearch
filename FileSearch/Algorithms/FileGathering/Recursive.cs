#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: Recursive.cs
// Date - created:2016.07.13 - 17:40
// Date - current: 2016.08.17 - 16:28

#endregion

#region Usings

using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileAlgorithms;

#endregion

namespace FileSearch.Algorithms.FileGathering
{
    internal class Recursive : FileGatheringAlgorithm
    {
        protected override IEnumerable<string> MySearchAlgo(string directory,
            string textToSearchFor,
            string searchcrets = "*", bool subfolder = true)
        {
            // If the querry is set to nothing, we'll set it to universal.
            var searchCrets = searchcrets.Equals(string.Empty) ? "*" : searchcrets;

            foreach (var file in Directory.GetFiles(directory).Where(x => searchCrets == "*" || x.EndsWith(searchcrets))
                ) // Add each file from the current directory as a masternode
            {
                yield return file;
            }

            if (subfolder)
                // If the subfolder checkbox is checked, than iterrate through all sub directories and add them as masternodes
            {
                foreach (var dir in Directory.GetDirectories(directory))
                {
                    foreach (
                        var item in Algorithm(dir, textToSearchFor, searchCrets)
                        )
                    {
                        yield return item;
                    }
                }
            }
        }
    }
}