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

#endregion Copyright and License Header

using DroidManager.Core.Classes;
using SharpAdbClient;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DroidManager.Core.States.Pages
{
    public class RootDevicePageState
    {
        private AndroidDevice _currentDevice = AndroidDeviceConnection.OverviewState.CurrentDevice;

        public string DeviceState => _currentDevice.DeviceStateString;

        private string _ssuzippath;

        public string SuperSUPath => "/sdcard/SuperSU-root-BETA-v2.67.zip";

        public int SuperSUZipPermissions => 0777;

        private string _ssudownloadurl = "https://github.com/IridiumIon/DroidManager-Assets/releases/download/supersu-1/SuperSU-root-BETA-v2.67.zip";

        public async Task DownloadSuperSU()
        {
            _ssuzippath = Path.GetTempFileName();
            var wc = new WebClient();
            await wc.DownloadFileTaskAsync(_ssudownloadurl, _ssuzippath);
        }

        public async Task<bool> PushSuperSU()
        {
            var ret = false;
            await Task.Run(() =>
            {
                try
                {
                    using (SyncService service = new SyncService(AndroidDeviceConnection.OverviewState.DefaultAdbSocket, _currentDevice.DeviceMetadata))
                    using (Stream stream = File.OpenRead(_ssuzippath))
                    {
                        service.Push(stream, SuperSUPath, SuperSUZipPermissions, DateTime.Now, CancellationToken.None);
                    }
                    ret = true;
                }
                catch (Exception)
                {
                    ret = false;
                }
            });
            return ret;
        }

        public void RebootRecovery()
        {
            _currentDevice.DeviceConnection.Reboot("recovery");
        }
    }
}