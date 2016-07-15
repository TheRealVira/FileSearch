#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: SingletonContentFactory.cs
// Date - created:2016.07.10 - 13:49
// Date - current: 2016.07.15 - 21:54

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

#endregion

namespace FileSearch
{
    internal static class SingletonContentFactory
    {
        public const byte ICON_WIDTH = 16;
        public const byte ICON_HEIGHT = 16;
        public static Dictionary<string, BitmapSource> Icons;

        public static void LoadContent()
        {
            if (Icons != null) return;

            Icons = new Dictionary<string, BitmapSource>();

            foreach (var file in Directory.GetFiles("Content\\Icons", "*.ico"))
            {
                var icon = Icon.ExtractAssociatedIcon(file);
                Stream stream = new MemoryStream();
                icon.Save(stream);
                var decoder = BitmapDecoder.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.None);

                Icons.Add(Path.GetFileNameWithoutExtension(file), decoder.Frames[0]);

                //var image = new Image
                //{
                //    Source = src,
                //    Width = 16,
                //    Height = 16
                //};
            }
        }

        /// <summary>
        ///     Will dispose all icons.
        /// </summary>
        public static void Dispose()
        {
            if (Icons == null) return;

            Icons.Clear();
            Icons = null;
            GC.Collect();
        }
    }
}