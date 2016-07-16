#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: ModeManager.cs
// Date - created:2016.07.16 - 17:31
// Date - current: 2016.07.16 - 18:41

#endregion

#region Usings

using System;

#endregion

namespace FileSearch.Modes
{
    internal static class ModeManager
    {
        private static RunningMode Mode = RunningMode.Stop;

        public static RunningMode CurrentMode
        {
            get { return Mode; }
            set
            {
                if (Mode == value) return;

                Mode = value;
                switch (value)
                {
                    case RunningMode.Pause:
                        OnPausing(new RunningModeEventArgs(Mode));
                        return;
                    case RunningMode.Run:
                        OnResuming(new RunningModeEventArgs(Mode));
                        return;
                    case RunningMode.Stop:
                        OnStopping(new RunningModeEventArgs(Mode));
                        return;
                }
            }
        }

        private static void OnPausing(RunningModeEventArgs args)
        {
            PausingEvent?.Invoke(null, args);
        }

        private static void OnStopping(RunningModeEventArgs args)
        {
            StoppingEvent?.Invoke(null, args);
        }

        private static void OnResuming(RunningModeEventArgs args)
        {
            RunningEvent?.Invoke(null, args);
        }

        public static event EventHandler PausingEvent;
        public static event EventHandler RunningEvent;
        public static event EventHandler StoppingEvent;
    }
}