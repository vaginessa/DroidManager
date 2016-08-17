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
using System.Threading.Tasks;
using System.Windows.Input;

namespace DroidManager.Windows.VM.Pages
{
    internal class AdvancedBootPageVM : SwitchablePageViewModel
    {
        private AdvancedBootPageState _pageState = new AdvancedBootPageState();

        public string CurrentModeInfoText => _pageState.CurrentModeInfoText;

        public ICommand RefreshCommand => new DelegateCommand(Refresh);
        public ICommand RebootNormalCommand => new DelegateCommand(RebootNormal);

        private async void RebootNormal(object obj)
        {
            await RebootDevice(AdvancedBootBootState.Online);
        }

        public ICommand RebootRecoveryCommand => new DelegateCommand(RebootRecovery);

        private MetroWindow HostWindow => (PageView.HostView as MetroWindow);

        private async void RebootRecovery(object obj)
        {
            await RebootDevice(AdvancedBootBootState.Recovery);
        }

        public ICommand RebootBootloaderCommand => new DelegateCommand(RebootBootloader);

        private async void RebootBootloader(object obj)
        {
            await RebootDevice(AdvancedBootBootState.Bootloader);
        }

        public ICommand RebootSideloadCommand => new DelegateCommand(RebootSideload);

        private async void RebootSideload(object obj)
        {
            await RebootDevice(AdvancedBootBootState.Sideload);
        }

        private void Refresh(object obj)
        {
            _pageState.RefreshInformation();
        }

        public async Task RebootDevice(string mode)
        {
            var result = await HostWindow.ShowMessageAsync("Reboot device?", $"Are you sure you want to reboot your device to {mode} mode?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                var progressController = await HostWindow.ShowProgressAsync("Rebooting", $"Rebooting device into {mode} mode");
                progressController.SetIndeterminate();
                _pageState.RebootIntoMode(mode);
                await progressController.CloseAsync();
            }
        }


    }
}