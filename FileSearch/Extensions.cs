#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: Extensions.cs
// Date - created:2016.07.13 - 17:57
// Date - current: 2016.07.16 - 19:02

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
        /// <typeparam name="TKey">Keys</typeparam>
        /// <typeparam name="TValue">Values</typeparam>
        /// <param name="dictionary"></param>
        /// <returns>Returns a new list filled with the keys of the dictionary.</returns>
        public static List<TKey> GetKeys<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
            => dictionary == null ? null : new List<TKey>(dictionary.Keys);

        public static void TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key)) return;

            dictionary.Add(key, value);
        }
    }
}