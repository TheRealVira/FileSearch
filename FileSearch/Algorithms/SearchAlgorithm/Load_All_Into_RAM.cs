#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: Load_All_Into_RAM.cs
// Date - created:2016.07.13 - 17:51
// Date - current: 2016.07.13 - 19:01

#endregion

#region Usings

using System.IO;

#endregion

namespace FileSearch.Algorithms.SearchAlgorithm
{
    internal class Load_All_Into_RAM : ContentSearchAlgorithm
    {
        protected override bool MySearchAlgo(string file, string content)
        {
            var reader = new StreamReader(file);

            try
            {
                if (reader.ReadToEnd().Contains(content))
                {
                    return true;
                }

                reader.Close();
                reader.Dispose();
                reader = null;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }

            return false;
        }
    }
}