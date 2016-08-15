using SharpAdbClient.DeviceCommands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroidManager.Core.States.Pages
{
    public class ApplicationsPageState
    {
        public List<string> InstalledPackageIds { get; private set; }
        public Dictionary<string, string> InstalledPackages { get; private set; }

        public async Task RefreshApplicationsInformationAsync()
        {
            await Task.Run(() =>
            {
                var currentDevice = AndroidDeviceConnection.OverviewState.CurrentDevice;
                PackageManager pm = new PackageManager(currentDevice.DeviceMetadata);
                pm.RefreshPackages();
                InstalledPackages = pm.Packages;
                InstalledPackageIds = InstalledPackages.Keys.ToList();
            });
        }
    }
}