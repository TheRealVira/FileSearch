#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: ContentFactory.cs
// Date - created:2016.07.10 - 13:49
// Date - current: 2016.07.13 - 18:44

#endregion

#region Usings

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

#endregion

namespace FileSearch
{
    internal static class ContentFactory
    {
        public static Dictionary<string, Image> Icons;

        public static void LoadContent()
        {
            if (Icons != null) return;

            Icons = new Dictionary<string, Image>();

            foreach (var file in Directory.GetFiles("Content\\Icons", "*.ico"))
            {
                var icon = Icon.ExtractAssociatedIcon(file);
                Stream stream = new MemoryStream();
                icon.Save(stream);
                var decoder = BitmapDecoder.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.None);
                BitmapSource src = decoder.Frames[0];

                var image = new Image();
                image.Source = src;
                image.Width = 16;
                image.Height = 16;
                Icons.Add(Path.GetFileNameWithoutExtension(file), image);
            }
        }
    }
}