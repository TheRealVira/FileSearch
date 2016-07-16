#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileAlgorithms
// Filename: ContentSearchAlgorithm.cs
// Date - created:2016.07.15 - 17:47
// Date - current: 2016.07.16 - 18:41

#endregion

#region Usings

#endregion

namespace FileAlgorithms
{
    public abstract class ContentSearchAlgorithm
    {
        public readonly FileContains Algorithm;

        protected ContentSearchAlgorithm()
        {
            Algorithm = MySearchAlgo;
        }

        protected abstract bool MySearchAlgo(string file, string content);
    }
}