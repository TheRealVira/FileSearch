#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: UltimateFactory.cs
// Date - created:2016.07.10 - 15:05
// Date - current: 2016.07.16 - 18:41

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using FileAlgorithms;

#endregion

namespace FileSearch.SearchingAlgorithms
{
    internal static class UltimateFactory<T>
    {
#if (DEBUG)
        private const bool DEBUG = true;
#else
        private const bool DEBUG = false;
#endif

        public static Dictionary<string, T> Compute(AppDomain domain)
        {
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
            domain.GetAssemblies()
                .AsParallel()
                .ForAll(assms => Compute(assms).AsParallel().ForAll(items => toRet.Add(items.Key, items.Value)));

            return toRet;
        }

        public static Dictionary<string, T> Compute(Assembly assm)
        {
            var myType = typeof(T);
            var toRet = new Dictionary<string, T>();

            //var M = Attribute.GetCustomAttribute(x, typeof(TestingPurpose)) != null;
            //var H_A = System.Diagnostics.Debugger.IsAttached;
            //var F1 = !(M && H_A);
            //var F2 = !x.IsInterface && !x.IsAbstract && myType.IsAssignableFrom(x);
            //var F3 = !F2 && !F1;

            //var expanded = !(!x.IsInterface && !x.IsAbstract && myType.IsAssignableFrom(x)) &&
            //               !(!((Attribute.GetCustomAttribute(x, typeof(TestingPurpose)) != null) &&
            //                   System.Diagnostics.Debugger.IsAttached));

            try
            {
                assm.GetTypes()
                    .Where(x => !x.IsInterface && !x.IsAbstract && myType.IsAssignableFrom(x) &&
                                (DEBUG
                                    ? true
                                    : !Attribute.IsDefined(x, typeof(TestingPurpose))))
                    .ToList()
                    .ForEach(x => { toRet.Add(x.Name, (T) Activator.CreateInstance(x)); });
            }
            catch (ReflectionTypeLoadException ex)
            {
                foreach (var item in ex.LoaderExceptions)
                {
                    MessageBox.Show(item.Message);
                }
            }

            return toRet;
        }
    }
}