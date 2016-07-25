﻿using MahApps.Metro.Controls;
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
            string restoreBackupPath = await (View as MetroWindow).ShowInputAsync("Select backup", "Enter the full file path to a backup to restore to your device");
            if (string.IsNullOrWhiteSpace(restoreBackupPath))
            {
                await (View as MetroWindow).ShowMessageAsync("Invalid parameters", "The existing backup path is invalid.");
                return;
            }
            var dialogResult = await (View as MetroWindow).ShowMessageAsync("Confirm action", "You are about to begin a restore. Are you absolutely sure you want to proceed?");
            if (dialogResult == MessageDialogResult.Affirmative)
            {
                //Run restore operation
                var progressController = await (View as MetroWindow).ShowProgressAsync("Restoring Backup", "Please wait while the backup is being restored to your device.");
                progressController.SetIndeterminate();
                //Start restore process
                string[] backupCommandArguments = { restoreBackupPath };
                await progressController.CloseAsync();
            }
        }

        public ICommand RunBackupCommand => new DelegateCommand(RunBackup);

        private async void RunBackup(object obj)
        {
            if (string.IsNullOrWhiteSpace(BackupLocation))
            {
                await (View as MetroWindow).ShowMessageAsync("Invalid parameters", "The backup output path is invalid.");
                return;
            }
            var dialogResult = await (View as MetroWindow).ShowMessageAsync("Confirm action", "You are about to begin a backup. Are you sure you want to proceed?");
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
                backupArguments.Add(BackupLocation);
                //Run backup operation
                var progressController = await (View as MetroWindow).ShowProgressAsync("Creating Backup", "Please wait while your device is being backed up.");
                progressController.SetIndeterminate();
                //Start backup process
                await progressController.CloseAsync();
            }
        }

        public ICommand BrowseBackupPathCommand => new DelegateCommand(BrowseBackupPath);

        private async void BrowseBackupPath(object obj)
        {
            BackupLocation = await (View as MetroWindow).ShowInputAsync("Save backup", "Enter a full file path to save the backup to.");
        }

        public string BackupLocation { get; set; }

        public bool BackupIncludeApk { get; set; }
        public bool BackupIncludeShared { get; set; }
        public bool BackupIncludeSystem { get; set; } = true;
    }
}