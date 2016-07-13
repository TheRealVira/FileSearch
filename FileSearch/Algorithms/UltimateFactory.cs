#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: UltimateFactory.cs
// Date - created:2016.07.10 - 15:05
// Date - current: 2016.07.13 - 19:01

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace FileSearch.SearchingAlgorithms
{
    internal static class UltimateFactory<T>
    {
        public static Dictionary<string, T> Compute()
        {
            var myType = typeof(T);
            var toRet = new Dictionary<string, T>();

            // The oneliner of hell explained:
            // 1) Get all Assemblies in the current domain
            // 2) Flatt out the resulting sequneces int one sequence
            // 3) Select only those Types, which are assigned from T
            // 3.1) And they shouldn't be an interface or an abstract class
            // 4) Convert it to a list, so we are able to use Linq to iterate through (via the "Foreach" method") without using any more lines of code
            // 5) Now we need to create an instance of all types, we got
            // 6) Add [the name of the type, the created instance] to our toRet dictionary
            // 7) Hope you'll don't get an error :3

            // Note: Well - it kind looks quite like a spell which summons satan if you wait long enough, but if you look closer...... hmmm yeah you're probably right. It'll summon satan.
            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => myType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList()
                .ForEach(x => toRet.Add(x.Name, (T) Activator.CreateInstance(x)));

            return toRet;
        }
    }
}