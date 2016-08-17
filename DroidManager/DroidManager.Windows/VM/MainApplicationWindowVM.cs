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

using DroidManager.Core;
using DroidManager.Windows.Views.Pages;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NanoMvvm;
using NanoMvvm.Pagination;
using System;
using System.Windows.Input;

namespace DroidManager.Windows.VM
{
    internal class MainApplicationWindowVM : PaginatingViewModel
    {
        private PageSwitcherService _pageSwitcher;
        public PaginationMessage PageSwitchMessage;

        public ICommand ViewLoadedCommand => new DelegateCommand(ViewDidLoad);

        public bool DeviceAvailable => AndroidDeviceConnection.OverviewState?.CurrentDevice != null;

        public bool HackerMode => Properties.Settings.Default.hackerMode;

        private void DeviceSelected(object obj)
        {
            throw new NotImplementedException();
        }

        private async void ViewDidLoad(object obj)
        {
            View.PageChanged += OnViewPageChanged;
            _pageSwitcher = new PageSwitcherService(View.PageHost);
            LoadOverviewPage();
            OnPropertyChanged(nameof(DeviceAvailable));
            if (Properties.Settings.Default.debugMode)
            {
                await (View as MetroWindow).ShowMessageAsync("Warning: Debug Mode", "Debug mode is currently enabled. This will very likely result in application crashes, as many of the protection features are disabled. Please turn it off from the settings menu on the launcher window.");
            }
        }

        public MainApplicationWindowVM()
        {
            PageSwitchMessage = new PaginationMessage();
        }

        #region Page Loaders

        private async void OnViewPageChanged(object sender, string pageIdentifier)
        {
            if (AndroidDeviceConnection.OverviewState.CurrentDevice != null || Properties.Settings.Default.debugMode)
            {
                try
                {
                    switch (pageIdentifier)
                    {
                        case "Overview":
                            LoadOverviewPage();
                            break;

                        case "Applications":
                            LoadApplicationsPage();
                            break;

                        case "Backup":
                            LoadBackupPage();
                            break;

                        case "File Transfer":
                            LoadFileTransferPage();
                            break;

                        case "Battery":
                            LoadBatteryPage();
                            break;

                        case "Advanced Boot":
                            LoadAdvancedBootPage();
                            break;

                        case "Terminal":
                            LoadTerminalPage();
                            break;

                        case "Sideload":
                            LoadSideloadPage();
                            break;

                        case "Image Flasher":
                            LoadImageFlasherPage();
                            break;

                        case "Root Device":
                            LoadRootDevicePage();
                            break;

                        default:
                            throw new NotImplementedException("Tab identifier not found.");
                    }
                }
                catch (NotImplementedException)
                {
                    await (View as MetroWindow).ShowMessageAsync("Feature not implemented", "This feature has not yet been implemented, but it's probably coming soon!");
                }
            }
            else
            {
                await (View as MetroWindow).ShowMessageAsync("Device not connected", "DroidManager could not find any devices. Please check your connection.");
                //It is null. Refresh devices now.
                //Never mind    
                //AndroidDeviceConnection.OverviewState.RefreshDevices();
            }
        }

        private void LoadRootDevicePage()
        {
            _pageSwitcher.LoadPage<RootDevicePage>();
        }

        private void LoadImageFlasherPage()
        {
            throw new NotImplementedException();
        }

        private void LoadSideloadPage()
        {
            throw new NotImplementedException();
        }

        private void LoadTerminalPage()
        {
            _pageSwitcher.LoadPage<TerminalPage>();
        }

        private void LoadAdvancedBootPage()
        {
            _pageSwitcher.LoadPage<AdvancedBootPage>();
        }

        private void LoadBatteryPage()
        {
            _pageSwitcher.LoadPage<BatteryPage>();
        }

        private void LoadFileTransferPage()
        {
            _pageSwitcher.LoadPage<FileTransferPage>();
        }

        private void LoadBackupPage()
        {
            _pageSwitcher.LoadPage<BackupPage>();
        }

        private void LoadApplicationsPage()
        {
            _pageSwitcher.LoadPage<ApplicationsPage>();
        }

        private void LoadOverviewPage()
        {
            _pageSwitcher.LoadPage<OverviewPage>();
        }

        #endregion Page Loaders
    }
}