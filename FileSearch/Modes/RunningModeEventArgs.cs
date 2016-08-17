#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: RunningModeEventArgs.cs
// Date - created:2016.07.16 - 17:32
// Date - current: 2016.08.17 - 16:28

#endregion

#region Usings

using System;

#endregion

namespace FileSearch.Modes
{
    internal class RunningModeEventArgs : EventArgs
    {
        public readonly RunningMode Mode;

        public RunningModeEventArgs(RunningMode mode)
        {
            Mode = mode;
        }
    }
}