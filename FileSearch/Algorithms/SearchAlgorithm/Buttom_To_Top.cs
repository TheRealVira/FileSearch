#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: Buttom_To_Top.cs
// Date - created:2016.07.13 - 18:38
// Date - current: 2016.07.15 - 21:54

#endregion

#region Usings

using System.IO;
using FileAlgorithms;

#endregion

namespace FileSearch.Algorithms.SearchAlgorithm
{
    internal class Buttom_To_Top : ContentSearchAlgorithm
    {
        protected override bool MySearchAlgo(string file, string content)
        {
            var lines = File.ReadAllLines(file);

            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(content))
                {
                    return true;
                }
            }

            return false;
        }
    }
}