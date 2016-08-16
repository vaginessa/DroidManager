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


using DroidManager.Core;
using DroidManager.Core.Classes;
using DroidManager.Core.States.Pages;
using DroidManager.Windows.Views.Pages;
using NanoMvvm;
using NanoMvvm.Pagination;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DroidManager.Windows.VM.Pages
{
    internal class OverviewPageVM : SwitchablePageViewModel
    {
        private OverviewPageState _pageState;

        public ICommand ReloadViewCommand => new DelegateCommand(ReloadView);

        private void ReloadView(object obj)
        {
            PageView.SwitcherService.ReloadCurrentPage();
        }

        public OverviewPageVM()
        {
            _pageState = new OverviewPageState(Properties.Settings.Default.adbExecutablePath);
            AndroidDeviceConnection.OverviewState = _pageState;
            _pageState.NoDevicesAvailable += OnPageStateNoDevicesAvailable;
        }

        private void OnPageStateNoDevicesAvailable(object sender, System.EventArgs e)
        {
            PageView.SwitcherService.LoadPage<OverviewPage>(false);
        }

        public string ConnectionStatusString => _pageState.ConnectionStatusString;

        public ObservableCollection<AndroidDevice> AvailableDevices => _pageState.ConnectedDevices;

        public AndroidDevice CurrentlySelectedDevice
        {
            get
            {
                return _pageState.CurrentDevice;
            }
            set
            {
                _pageState.CurrentDevice = value;
                OnPropertyChanged(nameof(CurrentlySelectedDevice));
            }
        }

        public AndroidDeviceInformationService CurrentDeviceInformationService => _pageState.CurrentDeviceInformationService;

        public Dictionary<string, string> CurrentDeviceAdditionalProperties => CurrentDeviceInformationService?.Device.AdditionalProperties;
    }
}