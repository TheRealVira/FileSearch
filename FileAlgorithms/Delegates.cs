#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileAlgorithms
// Filename: Delegates.cs
// Date - created:2016.07.15 - 17:48
// Date - current: 2016.07.16 - 18:41

#endregion

#region Usings

using System.Collections.Generic;

#endregion

namespace FileAlgorithms
{
    public delegate IEnumerable<string> FileGathering(
        string directory, string textToSearchFor, string searchcrets = "*.*",
        bool subfolder = true);

    public delegate bool FileContains(string file, string content);
}