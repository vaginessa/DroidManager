﻿using DroidManager.Core.States;
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

        public ICommand ViewLoadedCommand => new DelegateCommand(ViewDidLoad);

        private void LaunchMainApplication(object obj)
        {
            View.WindowHandle.Hide();
            DroidManagerContext.WindowService.ShowWindowDialog<MainApplicationWindow>(View.WindowHandle);
            View.WindowHandle.Show();
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
                var tryAgain = await (View as MetroWindow).ShowMessageAsync("Error", "Sorry, ADB could not be found. Please restart the application and enter a valid ADB path.");
                View.WindowHandle.Close();
            }
        }

        private async Task<bool> VerifyAdbPath()
        {
            if (!CheckAdbAvailability(null))
            {
                //Adb was not available. Ask now.
                string inputAdbPath = await (View as MetroWindow).ShowInputAsync("Please enter the full path to or containing folder of the ADB executable", "DroidManager uses ADB to communicate with your device. Please enter the full path to the ADB executable.");
                //Verify ADB executable
                if (!String.IsNullOrWhiteSpace(inputAdbPath) && SmartFindAdb(ref inputAdbPath) && AdbChecker.VerifyAdbExecutable(inputAdbPath))
                {
                    //Adb has been verified, save new settings
                    Properties.Settings.Default.adbExecutablePath = inputAdbPath;
                    Properties.Settings.Default.Save();
                    await (View as MetroWindow).ShowMessageAsync("Success", "We're all set! We found a working ADB executable!");
                    return true; //Success
                }
                else
                {
                    await (View as MetroWindow).ShowMessageAsync("Error", "Sorry, we couldn't find a valid ADB executable in that path.");
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
        private bool SmartFindAdb(ref string inputAdbPath)
        {
            if (CheckAdbAvailability(inputAdbPath))
            {
                return true; //It works right away! :D
            }
            else
            {
                //Some tweaking is required.

                //First try removing quotes!
                inputAdbPath = inputAdbPath.Replace("\"", ""); //Remove the double-quote character
                if (CheckAdbAvailability(inputAdbPath))
                {
                    return true; //Got it!
                }

                //Now try appending adb.exe
                inputAdbPath = Path.Combine(inputAdbPath, "adb.exe");
                if (CheckAdbAvailability(inputAdbPath))
                {
                    return true; //Got it!
                }

                //Sorry, out of luck
            }

            return false; //Adb could not be found :(
        }

        private bool CheckAdbAvailability(string testPath)
        {
            string adbPath = testPath ?? Properties.Settings.Default.adbExecutablePath;
            if (String.IsNullOrWhiteSpace(adbPath)) return false; //Setting is unset
            if (!File.Exists(adbPath)) return false; //Adb not found
            return true; //Adb is available
        }
    }
}