using SharpAdbClient.DeviceCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroidManager.Core.States.Pages
{
    public class ApplicationsPageState
    {
        public bool IncludeSystemApps { get; set; } = false;
        public List<string> InstalledPackageIds { get; private set; }
        public Dictionary<string, string> InstalledPackages { get; private set; }
        public PackageManager PackageManager { get; set; }

        public async Task RefreshApplicationsInformationAsync()
        {
            await Task.Run(() =>
            {
                RefreshApplicationsInformation();
            });
        }

        public void RefreshApplicationsInformation()
        {
            var currentDevice = AndroidDeviceConnection.OverviewState.CurrentDevice;
            PackageManager = new PackageManager(currentDevice.DeviceMetadata, !IncludeSystemApps);
            PackageManager.RefreshPackages();
            InstalledPackages = PackageManager.Packages;
            InstalledPackageIds = InstalledPackages.Keys.ToList();
        }

        public async Task<bool> UninstallApplicationByPackageIdAsync(string selectedPackageId)
        {
            var ret = false;
            await Task.Run(() =>
            {
                try
                {
                    PackageManager.UninstallPackage(selectedPackageId);
                    ret = true;
                }
                catch (Exception ex)
                {
                    //It will be handled in the caller
                    ret = false;
                }
            });
            return ret;
        }
    }
}