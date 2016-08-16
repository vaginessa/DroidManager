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


using DroidManager.Core.Utilities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NanoMvvm;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DroidManager.Windows.VM
{
    internal class SettingsWindowVM : ViewModelBase
    {
        public string AdbPathText => $"ADB Path: {Properties.Settings.Default.adbExecutablePath}";

        public ICommand ChangeAdbPathCommand => new DelegateCommand(ChangeAdbPath);

        public ICommand ResetAllSettingsCommand => new DelegateCommand(ResetAllSettings);

        private async void ResetAllSettings(object obj)
        {
            var result = await (View as MetroWindow).ShowMessageAsync("Confirm action", "Are you absolutely sure you want to reset all settings to default? This action cannot be undone.", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.Save();
                await (View as MetroWindow).ShowMessageAsync("Success", "All settings have been reset to their defaults.");
                (View as MetroWindow).Close();
            }
        }

        public ICommand ViewClosingCommand => new DelegateCommand(ViewIsClosing);

        private void ViewIsClosing(object obj)
        {
            Properties.Settings.Default.Save();
        }

        public bool DebugModeOption
        {
            get { return Properties.Settings.Default.debugMode; }
            set { Properties.Settings.Default.debugMode = value; }
        }

        private async void ChangeAdbPath(object obj)
        {
            string inputAdbPath = await (View as MetroWindow).ShowInputAsync("Please locate ADB", "Please enter the full path to or containing folder of the ADB executable. Alternatively, enter the path to the Android SDK. DroidManager uses ADB to communicate with your device.");
            //Verify ADB executable
            Tuple<bool, string> findResult;
            if (!String.IsNullOrWhiteSpace(inputAdbPath) && (findResult = await Task.Run(() => IntroWindowVM.SmartFindAdb(inputAdbPath))).Item1 && await Task.Run(() => AdbChecker.VerifyAdbExecutable(findResult.Item2)))
            {
                //Adb has been verified, save new settings
                Properties.Settings.Default.adbExecutablePath = inputAdbPath;
                Properties.Settings.Default.Save();
                await (View as MetroWindow).ShowMessageAsync("Success", "We're all set! We found a working ADB executable!");
            }
            else
            {
                await (View as MetroWindow).ShowMessageAsync("Error", "Sorry, we couldn't find a valid ADB executable in that path. The ADB path setting remains unchanged.");
                //Validation failure
            }
        }
    }
}