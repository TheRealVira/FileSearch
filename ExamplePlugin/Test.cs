#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: ExamplePlugin
// Filename: Test.cs
// Date - created:2016.07.15 - 21:49
// Date - current: 2016.07.16 - 19:02

#endregion

#region Usings

using System.Collections.Generic;
using FileAlgorithms;

#endregion

namespace ExamplePlugin
{
    [TestingPurpose]
    public class Test : FileGatheringAlgorithm
    {
        protected override IEnumerable<string> MySearchAlgo(string directory, string textToSearchFor,
            string searchcrets = "*",
            bool subfolder = true)
        {
            return new[] {"Test"};
        }
    }
}