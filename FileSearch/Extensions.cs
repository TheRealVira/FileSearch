#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: Extensions.cs
// Date - created:2016.07.13 - 17:57
// Date - current: 2016.07.13 - 18:39

#endregion

#region Usings

using System.Collections.Generic;

#endregion

namespace FileSearch
{
    internal static class Extensions
    {
        /// <summary>
        ///     Creates a new list, containing all keys (copied) from the dictionary.
        /// </summary>
        /// <typeparam name="TOne">Keys</typeparam>
        /// <typeparam name="TTwo">Values</typeparam>
        /// <param name="dictionary"></param>
        /// <returns>Returns a new list filled with the keys of the dictionary.</returns>
        public static List<TOne> GetKeys<TOne, TTwo>(this Dictionary<TOne, TTwo> dictionary)
            => dictionary == null ? null : new List<TOne>(dictionary.Keys);
    }
}