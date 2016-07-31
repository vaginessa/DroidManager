using System.Collections.Generic;
using SharpAdbClient.DeviceCommands;
using System.Linq;

namespace DroidManager.Core.States.Pages
{
    public class ApplicationsPageState
    {
        public ApplicationsPageState()
        {
            RefreshApplicationsInformation();
        }

        public List<string> InstalledPackageIds { get; private set; }
        public Dictionary<string, string> InstalledPackages { get; private set; }

        private void RefreshApplicationsInformation()
        {
            var currentDevice = AndroidDeviceConnection.OverviewState.CurrentDevice;
            PackageManager pm = new PackageManager(currentDevice.DeviceMetadata);
            pm.RefreshPackages();
            InstalledPackages = pm.Packages;
            InstalledPackageIds = InstalledPackages.Keys.ToList();
        }
    }
}