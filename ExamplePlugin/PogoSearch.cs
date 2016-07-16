#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: ExamplePlugin
// Filename: PogoSearch.cs
// Date - created:2016.07.15 - 18:02
// Date - current: 2016.07.16 - 19:02

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileAlgorithms;

#endregion

namespace ExamplePlugin
{
    //[TestingPurpose]
    public class PogoSearch : ContentSearchAlgorithm
    {
        protected override bool MySearchAlgo(string file, string content)
        {
            var lines = File.ReadAllLines(file);
            var temp = new List<BooleanStringPair>();
            var rand = new Random(DateTime.Now.Millisecond);

            foreach (var line in lines)
            {
                temp.Add(new BooleanStringPair(line, false));
            }

            while (temp.Any(x => !x.Bool))
            {
                var current = temp[rand.Next(0, temp.Count)];
                if (current.Bool) continue;

                if (current.Text.Contains(content)) return true;

                current.Bool = true;
            }

            return false;
        }
    }

    internal struct BooleanStringPair
    {
        public BooleanStringPair(string text, bool bl)
        {
            Text = text;
            Bool = bl;
        }

        public string Text;
        public bool Bool;
    }
}