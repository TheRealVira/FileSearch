#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: Top_To_Buttom.cs
// Date - created:2016.07.13 - 18:32
// Date - current: 2016.08.31 - 00:50

#endregion

#region Usings

using System.IO;
using FileAlgorithms;

#endregion

namespace FileSearch.Algorithms.SearchAlgorithm
{
    internal class Top_To_Bottom : ContentSearchAlgorithm
    {
        protected override bool MySearchAlgo(string file, string content)
        {
            using (var reader = new StreamReader(file))
            {
                try
                {
                    while (!reader.EndOfStream)
                    {
                        var readLine = reader.ReadLine();
                        if (readLine != null && readLine.Contains(content))
                        {
                            return true;
                        }
                    }
                }
                finally
                {
                    reader.Close();
                    reader.Dispose();
                }
            }

            return false;
        }
    }
}