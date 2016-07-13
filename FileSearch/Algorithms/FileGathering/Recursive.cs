#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: Recursive.cs
// Date - created:2016.07.13 - 17:40
// Date - current: 2016.07.13 - 19:22

#endregion

#region Usings

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;

#endregion

namespace FileSearch.SearchingAlgorithms
{
    internal class Recursive : FileGatheringAlgorithm
    {
        protected override TreeViewItem[] MySearchAlgo(string directory, FileContains searchAlgorithm,
            string textToSearchFor,
            string searchcrets = "*", bool subfolder = true)
        {
            var toRet = new List<TreeViewItem>(); // Masternodes

            // If the querry is set to nothing, we'll set it to universal.
            var searchCrets = searchcrets.Equals(string.Empty) ? "*" : searchcrets;

            foreach (var file in Directory.GetFiles(directory).Where(x => searchCrets == "*" || x.EndsWith(searchcrets))
                ) // Add each file from the current directory as a masternode
            {
                if (searchAlgorithm(file, textToSearchFor))
                {
                    var toAdd = new TreeViewItem();
                    var stack = new StackPanel();

                    var textLbl = new Label {Content = file};

                    var copy = new Image
                    {
                        Source = SingletonContentFactory.Icons["File"],
                        Width = SingletonContentFactory.ICON_WIDTH,
                        Height = SingletonContentFactory.ICON_HEIGHT
                    };

                    stack.Orientation = Orientation.Horizontal;
                    stack.Children.Add(copy);
                    stack.Children.Add(textLbl);
                    toAdd.Header = stack;

                    toRet.Add(toAdd);
                }
            }

            if (subfolder)
                // If the subfolder checkbox is checked, than iterrate through all sub directories and add them as masternodes
            {
                foreach (var dir in Directory.GetDirectories(directory))
                {
                    var stack = new StackPanel();

                    var textLBL = new Label {Content = dir};

                    var copy = new Image
                    {
                        Source = SingletonContentFactory.Icons["Folder"],
                        Width = SingletonContentFactory.ICON_WIDTH,
                        Height = SingletonContentFactory.ICON_HEIGHT
                    };

                    stack.Orientation = Orientation.Horizontal;
                    stack.Children.Add(copy);
                    stack.Children.Add(textLBL);

                    var subNode = new TreeViewItem();

                    foreach (
                        var item in Algorithm(dir, searchAlgorithm, textToSearchFor, searchCrets) ?? new TreeViewItem[0]
                        )
                    {
                        subNode.Items.Add(item);
                    }

                    if (subNode.Items.Count == 0) continue;

                    subNode.Header = stack;

                    toRet.Add(subNode);
                }
            }

            // Return all nodes
            return toRet.ToArray();
        }
    }
}