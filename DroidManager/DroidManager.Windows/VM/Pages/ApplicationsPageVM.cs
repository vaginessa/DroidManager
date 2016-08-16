using DroidManager.Core.CLIAdb;
using DroidManager.Core.States.Pages;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NanoMvvm;
using NanoMvvm.Pagination;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace DroidManager.Windows.VM.Pages
{
    internal class ApplicationsPageVM : SwitchablePageViewModel
    {
        private ApplicationsPageState _pageState = new ApplicationsPageState();

        public ICommand RefreshApplicationsCommand => new DelegateCommand(RefreshApplications);
        public ICommand ViewLoadedCommand => new DelegateCommand(ViewDidLoad);
        public ICommand UninstallSelectedApplicationCommand => new DelegateCommand(UninstallSelectedApplication);
        public ICommand InstallApplicationFromFileCommand => new DelegateCommand(InstallApplicationFromFile);
        public ICommand BackupDataCommand => new DelegateCommand(BackupData);
        public ICommand BackupDataApkCommand => new DelegateCommand(BackupDataApk);
        public ICommand BackupDataApkAndObbCommand => new DelegateCommand(BackupDataApkAndObb);
        public ICommand RestoreDataCommand => new DelegateCommand(RestoreData);

        private void RestoreData(object obj)
        {
            var backupLocation = FileDialogUtil.BrowseForOpenFile("Android Backups (*.ab)|*.ab|All Files (*.*)|*.*", "Select an backup to restore.");
            if (backupLocation != null)
            {
                RunBackupRestore(backupLocation);
            }
        }

        private void BackupDataApkAndObb(object obj)
        {
            var backupLocation = FileDialogUtil.BrowseForSaveFile("Android Backups (*.ab)|*.ab|All Files (*.*)|*.*", "Select an output file for the backup.");
            if (backupLocation != null)
            {
                RunApplicationBackup(true, true, backupLocation);
            }
        }

        private void BackupDataApk(object obj)
        {
            var backupLocation = FileDialogUtil.BrowseForSaveFile("Android Backups (*.ab)|*.ab|All Files (*.*)|*.*", "Select an output file for the backup.");
            if (backupLocation != null)
            {
                RunApplicationBackup(true, false, backupLocation);
            }
        }

        private void BackupData(object obj)
        {
            var backupLocation = FileDialogUtil.BrowseForSaveFile("Android Backups (*.ab)|*.ab|All Files (*.*)|*.*", "Select an output file for the backup.");
            if (backupLocation != null)
            {
                RunApplicationBackup(false, false, backupLocation);
            }
        }

        private async void RunBackupRestore(string backupFile)
        {
            List<string> restoreArguments = new List<string>();
            restoreArguments.Add(backupFile);

            var progressController = await HostWindow.ShowProgressAsync("Restoring Backup", $"Please wait while the backup is restored.");
            progressController.SetIndeterminate();

            //Start backup process
            var restoreProcess = new CommandLineAdbExecutor(Properties.Settings.Default.adbExecutablePath);

            var processController = restoreProcess.ExecuteCommand("restore", restoreArguments.ToArray());
            int exitCode = await processController.WaitForCompletion();
            await progressController.CloseAsync();
            if (exitCode == 0)
            {
                await HostWindow.ShowMessageAsync("Success", "The operation completed successfully.");
            }
            else
            {
                await HostWindow.ShowMessageAsync("Error", $"The process exited with error code {exitCode}.");
            }
        }

        private async void RunApplicationBackup(bool apk, bool obb, string outputFile)
        {
            //Create arguments for backup
            List<string> backupArguments = new List<string>();
            if (apk)
            {
                backupArguments.Add("-apk");
            }
            else
            {
                backupArguments.Add("-noapk");
            }
            if (obb)
            {
                backupArguments.Add("-obb");
            }
            else
            {
                backupArguments.Add("-noobb");
            }
            backupArguments.Add("-f");
            backupArguments.Add(outputFile);
            backupArguments.Add(SelectedPackageId);

            var progressController = await HostWindow.ShowProgressAsync("Creating Backup", $"Please wait while {SelectedPackageId} is backed up.");
            progressController.SetIndeterminate();

            //Start backup process
            var backupProcess = new CommandLineAdbExecutor(Properties.Settings.Default.adbExecutablePath);

            var processController = backupProcess.ExecuteCommand("backup", backupArguments.ToArray());
            int exitCode = await processController.WaitForCompletion();
            await progressController.CloseAsync();
            if (exitCode == 0)
            {
                await HostWindow.ShowMessageAsync("Success", "The operation completed successfully.");
            }
            else
            {
                await HostWindow.ShowMessageAsync("Error", $"The process exited with error code {exitCode}.");
            }
        }

        private async void InstallApplicationFromFile(object obj)
        {
            var apkFile = FileDialogUtil.BrowseForOpenFile("Android Applications (*.apk)|*.apk|All Files (*.*)|*.*", "Select an APK");
            if (apkFile != null)
            {
                var progressController = await HostWindow.ShowProgressAsync("Installing", "Installing application");
                progressController.SetIndeterminate();
                Tuple<bool, string> installResult = await _pageState.InstallApplicationFromFileAsync(apkFile);
                var success = installResult.Item1;
                if (!success)
                {
                    await progressController.CloseAsync();
                    await HostWindow.ShowMessageAsync("Error", $"Application install failed with error: {installResult.Item2}");
                    return;
                }

                await progressController.CloseAsync();
                RefreshApplications(null);
            }
        }

        private MetroWindow HostWindow => (PageView.HostView as MetroWindow);

        private async void UninstallSelectedApplication(object obj)
        {
            var result = await HostWindow.ShowMessageAsync("Confirm action", $"Are you sure you want to uninstall {SelectedPackageId}?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                //Uninstall APs
                var progressController = await HostWindow.ShowProgressAsync("Uninstalling", "Uninstalling application");
                progressController.SetIndeterminate();
                bool success = await _pageState.UninstallApplicationByPackageIdAsync(SelectedPackageId);

                if (!success)
                {
                    await progressController.CloseAsync();
                    await HostWindow.ShowMessageAsync("Error", "Application uninstall failed");
                    return;
                }

                await progressController.CloseAsync();
                RefreshApplications(null);
            }
        }

        public bool IncludeSystemApps
        {
            get
            {
                return _pageState.IncludeSystemApps;
            }

            set
            {
                _pageState.IncludeSystemApps = value;
                RefreshApplications(null);
            }
        }

        private async void RefreshApplications(object obj)
        {
            await _pageState.RefreshApplicationsInformationAsync();
            OnPropertyChanged(nameof(InstalledPackageIdentifiers));
        }

        private async void ViewDidLoad(object obj)
        {
            RefreshApplications(null);
        }

        public List<string> InstalledPackageIdentifiers => _pageState.InstalledPackageIds;
        private string _selectedPackageId;

        public string SelectedPackageId
        {
            get
            {
                return _selectedPackageId;
            }

            set
            {
                _selectedPackageId = value;
                OnPropertyChanged(nameof(SelectedPackageId));
            }
        }

        private void ReloadView(object obj)
        {
            PageView.SwitcherService.ReloadCurrentPage();
        }
    }
}