using DroidManager.Core.States.Pages;

namespace DroidManager.Core
{
    public static class AndroidDeviceConnection
    {
        /// <summary>
        /// Stores the current OverviewPageState instance, because it has all the device metadata
        /// </summary>
        public static OverviewPageState OverviewState { get; set; }
    }
}