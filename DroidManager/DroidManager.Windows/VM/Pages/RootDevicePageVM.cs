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

using DroidManager.Core.States.Pages;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NanoMvvm;
using NanoMvvm.Pagination;
using System;
using System.Windows.Input;

namespace DroidManager.Windows.VM.Pages
{
    internal class RootDevicePageVM : SwitchablePageViewModel
    {
        private RootDevicePageState _pageState = new RootDevicePageState();

        public ICommand DownloadAndFlashSuperSUCommand => new DelegateCommand(DownloadAndFlashSuperSU);

        private MetroWindow HostWindow => (PageView.HostView as MetroWindow);

        private async void DownloadAndFlashSuperSU(object obj)
        {
            if (_pageState.DeviceState != AdvancedBootBootState.Online)
            {
                await HostWindow.ShowMessageAsync("Incorrect mode", "Please reboot your device into online mode to continue.");
                return;
            }

            var result = await HostWindow.ShowMessageAsync("Confirm", "SuperSU will be downloaded and pushed to your device, which will then be rebooted into recovery mode. Are you sure you wish to proceed?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                var progressController = await HostWindow.ShowProgressAsync("Downloading", "Downloading SuperSU");
                progressController.SetIndeterminate();
                try
                {
                    //Download SuperSU
                    await _pageState.DownloadSuperSU();
                    progressController.SetTitle("Pushing");
                    progressController.SetMessage($"Pushing SuperSU to {_pageState.SuperSUPath} ({_pageState.SuperSUZipPermissions})");
                    //Push SuperSU
                    if (!await _pageState.PushSuperSU())
                    {
                        throw new ApplicationException("Error pushing SuperSU");
                    }
                    progressController.SetTitle("Rebooting");
                    progressController.SetMessage("Rebooting into recovery mode");
                    _pageState.RebootRecovery();
                    await progressController.CloseAsync();
                }
                catch (Exception ex)
                {
                    await progressController.CloseAsync();
                    await HostWindow.ShowMessageAsync("Error", $"An error occured: {ex.Message}");
                }
            }
        }
    }
}