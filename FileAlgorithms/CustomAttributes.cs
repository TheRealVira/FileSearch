#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileAlgorithms
// Filename: CustomAttributes.cs
// Date - created:2016.07.15 - 19:47
// Date - current: 2016.07.15 - 21:54

#endregion

#region Usings

using System;

#endregion

namespace FileAlgorithms
{
    /// <summary>
    ///     Algorithms which have the "TestingPurpose" as attribute, won't show up in Release-Mode
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TestingPurpose : Attribute
    {
    }
}