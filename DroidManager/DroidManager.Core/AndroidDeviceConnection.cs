using DroidManager.Core.States.Pages;
using System;

namespace DroidManager.Core
{
    public static class AndroidDeviceConnection
    {
        private static OverviewPageState overviewState;

        /// <summary>
        /// Stores the current OverviewPageState instance, because it has all the device metadata
        /// </summary>
        public static OverviewPageState OverviewState
        {
            get
            {
                return overviewState;
            }

            set
            {
                overviewState = value;
                StateChanged?.Invoke(null, null);
            }
        }

        public static event EventHandler StateChanged;
    }
}