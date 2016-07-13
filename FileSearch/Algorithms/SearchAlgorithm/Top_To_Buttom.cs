#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: Top_To_Buttom.cs
// Date - created:2016.07.13 - 18:32
// Date - current: 2016.07.13 - 18:44

#endregion

#region Usings

using System.IO;
using System.Linq;

#endregion

namespace FileSearch.Algorithms.SearchAlgorithm
{
    internal class Top_To_Buttom : ContentSearchAlgorithm
    {
        protected override bool MySearchAlgo(string file, string content)
        {
            var lines = File.ReadAllLines(file);

            for (var i = 0; i < lines.Count(); i++)
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