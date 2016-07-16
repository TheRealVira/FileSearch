#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: Bottom_To_Top.cs
// Date - created:2016.07.13 - 18:38
// Date - current: 2016.07.16 - 18:41

#endregion

#region Usings

using FileAlgorithms;

#endregion

namespace FileSearch.Algorithms.SearchAlgorithm
{
    /// <summary>
    ///     May be implemented in the future :P
    /// </summary>
    [TestingPurpose]
    internal class Bottom_To_Top : ContentSearchAlgorithm
    {
        protected override bool MySearchAlgo(string file, string content)
        {
            //var lines = File.ReadLines(file).ToList();

            //for (var i = lines.Count - 1; i > 0; i--)
            //{
            //    if (lines[i].Contains(content))
            //    {
            //        return true;
            //    }

            //    lines.Remove(lines[i]);
            //}

            return false;
        }
    }
}