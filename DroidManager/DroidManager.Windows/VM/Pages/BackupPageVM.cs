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


using DroidManager.Core.CLIAdb;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NanoMvvm;
using NanoMvvm.Pagination;
using System.Collections.Generic;
using System.Windows.Input;

namespace DroidManager.Windows.VM.Pages
{
    internal class BackupPageVM : SwitchablePageViewModel
    {
        public ICommand RunRestoreCommand => new DelegateCommand(RunRestore);

        private async void RunRestore(object obj)
        {
            string restoreBackupPath = BackupLocation;
            if (string.IsNullOrWhiteSpace(restoreBackupPath))
            {
                restoreBackupPath = await HostViewWindow.ShowInputAsync("Select backup", "Enter the full file path to a backup to restore to your device");
                if (string.IsNullOrWhiteSpace(restoreBackupPath))
                {
                    await HostViewWindow.ShowMessageAsync("Invalid parameters", "The existing backup path is invalid.");
                    return;
                }
            }
            var dialogResult = await HostViewWindow.ShowMessageAsync("Confirm action", "You are about to begin a restore. Are you absolutely sure you want to proceed?", MessageDialogStyle.AffirmativeAndNegative);
            if (dialogResult == MessageDialogResult.Affirmative)
            {
                //Run restore operation
                var progressController = await HostViewWindow.ShowProgressAsync("Restoring Backup", "Please wait while the backup is being restored to your device.");
                progressController.SetIndeterminate();
                //Start restore process
                string[] restoreCommandArguments = { restoreBackupPath };
                var backupProcess = new CommandLineAdbExecutor(Properties.Settings.Default.adbExecutablePath);
                var processController = backupProcess.ExecuteCommand("restore", restoreCommandArguments);
                int exitCode = await processController.WaitForCompletion();
                await progressController.CloseAsync();
                if (exitCode == 0)
                {
                    await HostViewWindow.ShowMessageAsync("Success", "The operation completed successfully.");
                }
                else
                {
                    await HostViewWindow.ShowMessageAsync("Error", $"The process exited with error code {exitCode}.");
                }
            }
        }

        public ICommand RunBackupCommand => new DelegateCommand(RunBackup);

        private async void RunBackup(object obj)
        {
            if (string.IsNullOrWhiteSpace(BackupLocation))
            {
                await HostViewWindow.ShowMessageAsync("Invalid parameters", "The backup output path is invalid.");
                return;
            }
            var dialogResult = await HostViewWindow.ShowMessageAsync("Confirm action", "You are about to begin a backup. Are you sure you want to proceed?", MessageDialogStyle.AffirmativeAndNegative);
            if (dialogResult == MessageDialogResult.Affirmative)
            {
                //Create arguments for backup
                List<string> backupArguments = new List<string>();
                if (BackupIncludeApk)
                {
                    backupArguments.Add("-apk");
                }
                else
                {
                    backupArguments.Add("-noapk");
                }
                if (BackupIncludeShared)
                {
                    backupArguments.Add("-shared");
                }
                else
                {
                    backupArguments.Add("-noshared");
                }
                if (BackupIncludeSystem)
                {
                    backupArguments.Add("-system");
                }
                else
                {
                    backupArguments.Add("-nosystem");
                }
                backupArguments.Add("-all");
                backupArguments.Add("-f");
                backupArguments.Add(BackupLocation);
                //Run backup operation
                var progressController = await HostViewWindow.ShowProgressAsync("Creating Backup", "Please wait while your device is being backed up.");
                progressController.SetIndeterminate();
                //Start backup process
                var backupProcess = new CommandLineAdbExecutor(Properties.Settings.Default.adbExecutablePath);
                var processController = backupProcess.ExecuteCommand("backup", backupArguments.ToArray());
                int exitCode = await processController.WaitForCompletion();
                await progressController.CloseAsync();
                if (exitCode == 0)
                {
                    await HostViewWindow.ShowMessageAsync("Success", "The operation completed successfully.");
                }
                else
                {
                    await HostViewWindow.ShowMessageAsync("Error", $"The process exited with error code {exitCode}.");
                }
            }
        }

        private MetroWindow HostViewWindow => (PageView.HostView as MetroWindow);

        public ICommand BrowseBackupPathCommand => new DelegateCommand(BrowseBackupPath);

        private async void BrowseBackupPath(object obj)
        {
            BackupLocation = await HostViewWindow.ShowInputAsync("Select backup path", "Enter the full file path to a backup location.");
            OnPropertyChanged(nameof(BackupLocation));
        }

        public string BackupLocation { get; set; }

        private bool backupIncludeApk;

        public bool BackupIncludeApk
        {
            get
            {
                return backupIncludeApk;
            }

            set
            {
                backupIncludeApk = value;
                OnPropertyChanged(nameof(BackupIncludeApk));
            }
        }

        private bool backupIncludeShared;

        public bool BackupIncludeShared
        {
            get
            {
                return backupIncludeShared;
            }

            set
            {
                backupIncludeShared = value;
                OnPropertyChanged(nameof(BackupIncludeShared));
            }
        }

        private bool backupIncludeSystem;

        public bool BackupIncludeSystem
        {
            get
            {
                return backupIncludeSystem;
            }

            set
            {
                backupIncludeSystem = value;
                OnPropertyChanged(nameof(BackupIncludeSystem));
            }
        }
    }
}