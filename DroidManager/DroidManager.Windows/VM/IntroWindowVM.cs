﻿#region Copyright and License Header
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


using DroidManager.Core.States;
using DroidManager.Core.Utilities;
using DroidManager.Windows.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NanoMvvm;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DroidManager.Windows.VM
{
    internal class IntroWindowVM : ViewModelBase
    {
        public string ApplicationVersion => $"v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}";

        private IntroState _introStateModel = new IntroState();

        public ICommand VisitIridiumIonSiteCommand => new DelegateCommand(VisitIridiumIonSite);

        public ICommand ViewAboutCommand => new DelegateCommand(ViewAbout);

        public ICommand LaunchMainApplicationCommand => new DelegateCommand(LaunchMainApplication);

        public ICommand LaunchSettingsWindowCommand => new DelegateCommand(LaunchSettingsWindow);

        public ICommand ViewLoadedCommand => new DelegateCommand(ViewDidLoad);

        private async void LaunchMainApplication(object obj)
        {
            if (!await VerifyAdbPath())
            {
                //Adb could not be verified.
                var tryAgain = await (View as MetroWindow).ShowMessageAsync("Error", "Sorry, a valid ADB executable could not be found in that path. Verify and try again?");
                return;
            }
            View.WindowHandle.Hide();
            DroidManagerContext.WindowService.ShowWindowDialog<MainApplicationWindow>(View.WindowHandle);
            View.WindowHandle.Show();
        }

        private void LaunchSettingsWindow(object obj)
        {
            DroidManagerContext.WindowService.ShowWindowDialog<SettingsWindow>(View.WindowHandle);
        }

        private void ViewAbout(object obj)
        {
            DroidManagerContext.WindowService.ShowWindow<AboutWindow>(View.WindowHandle);
        }

        private void VisitIridiumIonSite(object obj)
        {
            _introStateModel.VisitIridiumIonSite();
        }

        private async void ViewDidLoad(object obj)
        {
            if (!await VerifyAdbPath())
            {
                //Adb could not be verified.
                var tryAgain = await (View as MetroWindow).ShowMessageAsync("Error", "Sorry, a valid ADB executable could not be found in that path. Verify and try again?");
            }
        }

        private async Task<bool> VerifyAdbPath()
        {
            if (!CheckAdbAvailability(null))
            {
                //Adb was not available. Ask now.
                string inputAdbPath = await (View as MetroWindow).ShowInputAsync("Please locate ADB", "Please enter the full path to or containing folder of the ADB executable. Alternatively, enter the path to the Android SDK. DroidManager uses ADB to communicate with your device.");
                //Verify ADB executable
                Tuple<bool, string> findResult;
                if (!String.IsNullOrWhiteSpace(inputAdbPath) && (findResult = await Task.Run(() => SmartFindAdb(inputAdbPath))).Item1 && await Task.Run(() => AdbChecker.VerifyAdbExecutable(findResult.Item2)))
                {
                    //Adb has been verified, save new settings
                    Properties.Settings.Default.adbExecutablePath = findResult.Item2;
                    Properties.Settings.Default.Save();
                    await (View as MetroWindow).ShowMessageAsync("Success", "We're all set! We found a working ADB executable!");
                    return true; //Success
                }
                else
                {
                    //await (View as MetroWindow).ShowMessageAsync("Error", "Sorry, we couldn't find a valid ADB executable in that path.");
                    return false; //Validation failure
                }
            }
            return true; //Adb is already available
        }

        /// <summary>
        /// Attempts to find the ADB executable even if the input string is not valid
        /// </summary>
        /// <param name="inputAdbPath"></param>
        /// <returns></returns>
        public static Tuple<bool, string> SmartFindAdb(string inputAdbPath)
        {
            string originalPath = inputAdbPath;
            if (CheckAdbAvailability(inputAdbPath))
            {
                return new Tuple<bool, string>(true, inputAdbPath); //It works right away! :D
            }
            else
            {
                //Some tweaking is required.

                //First try removing quotes!
                inputAdbPath = inputAdbPath.Replace("\"", ""); //Remove the double-quote character
                if (CheckAdbAvailability(inputAdbPath))
                {
                    return new Tuple<bool, string>(true, inputAdbPath); //Got it!
                }

                //Now try appending adb.exe
                inputAdbPath = Path.Combine(inputAdbPath, "adb.exe");
                if (CheckAdbAvailability(inputAdbPath))
                {
                    return new Tuple<bool, string>(true, inputAdbPath); //Got it!
                }

                //Now try platform-tools/adb.exe
                inputAdbPath = Path.Combine(Path.Combine(originalPath, "platform-tools"), "adb.exe");
                if (CheckAdbAvailability(inputAdbPath))
                {
                    return new Tuple<bool, string>(true, inputAdbPath); //Got it!
                }

                //Sorry, out of luck
            }

            return new Tuple<bool, string>(true, inputAdbPath); //Adb could not be found :(
        }

        public static bool CheckAdbAvailability(string testPath)
        {
            string adbPath = testPath ?? Properties.Settings.Default.adbExecutablePath;
            if (String.IsNullOrWhiteSpace(adbPath)) return false; //Setting is unset
            if (!File.Exists(adbPath)) return false; //Adb not found
            return true; //Adb is available
        }
    }
}