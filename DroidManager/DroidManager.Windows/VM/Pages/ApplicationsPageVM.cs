using DroidManager.Core.States.Pages;
using NanoMvvm;
using NanoMvvm.Pagination;
using System.Collections.Generic;
using System.Windows.Input;
using System;

namespace DroidManager.Windows.VM.Pages
{
    internal class ApplicationsPageVM : SwitchablePageViewModel
    {
        private ApplicationsPageState _pageState = new ApplicationsPageState();

        public ICommand RefreshApplicationsCommand => new DelegateCommand(RefreshApplications);
        public ICommand ViewLoadedCommand => new DelegateCommand(ViewDidLoad);

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