#region Copyright and License Header
/*

	DroidManager

	Copyright (c) 2016 0xFireball, IridiumIon Software

	Author(s): 0xFireball
	

	This file is part of DroidManager.

	DroidManager is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	DroidManager is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with DroidManager.  If not, see <http://www.gnu.org/licenses/>.

*/
#endregion


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

        public async Task<Tuple<bool, string>> InstallApplicationFromFileAsync(string apkFile)
        {
            var ret = false;
            string installError = null;
            await Task.Run(() =>
            {
                try
                {
                    PackageManager.InstallPackage(apkFile, false);
                    ret = true;
                }
                catch (PackageInstallationException pinstex)
                {
                    installError = pinstex.Message;
                    //It will be handled in the caller
                    ret = false;
                }
            });
            return new Tuple<bool, string>(ret, installError);
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