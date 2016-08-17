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

using System.Threading.Tasks;

namespace DroidManager.Core.States.Pages
{
    public class AdvancedBootBootState
    {
        public static string Sideload { get; } = "sideload";
        public static string Bootloader { get; } = "bootloader";
        public static string Recovery { get; } = "recovery";
        public static string Online { get; } = "online";
        public static string Offline { get; } = "offline";
        public static string Host { get; } = "host";
    }

    public class AdvancedBootPageState
    {
        private OverviewPageState _overviewState;

        public AdvancedBootPageState()
        {
            _overviewState = AndroidDeviceConnection.OverviewState;
            RefreshInformation();
        }

        public void RebootIntoMode(string mode)
        {
            _overviewState.CurrentDevice.DeviceConnection.Reboot(mode);
        }

        public string CurrentModeInfoText => $"Device in {_overviewState.CurrentDevice.DeviceState.ToString().ToLowerInvariant()} mode.";

        public async Task RefreshInformationAsync()
        {
            await Task.Run(() => RefreshInformation());
        }

        public void RefreshInformation()
        {
            _overviewState.RefreshCurrentDevice();
        }
    }
}