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