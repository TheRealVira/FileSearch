#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: ExamplePlugin
// Filename: Test.cs
// Date - created:2016.07.15 - 21:49
// Date - current: 2016.07.15 - 21:54

#endregion

#region Usings

using System.Windows.Controls;
using FileAlgorithms;

#endregion

namespace ExamplePlugin
{
    [TestingPurpose]
    internal class Test : FileGatheringAlgorithm
    {
        protected override TreeViewItem[] MySearchAlgo(string directory, FileContains searchAlgorithm,
            string textToSearchFor,
            string searchcrets = "*", bool subfolder = true)
        {
            return null;
        }
    }
}