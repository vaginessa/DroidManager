using DroidManager.Core.States.Pages;
using NanoMvvm;
using NanoMvvm.Pagination;
using System.Collections.Generic;
using System.Windows.Input;

namespace DroidManager.Windows.VM.Pages
{
    internal class ApplicationsPageVM : SwitchablePageViewModel
    {
        private ApplicationsPageState _pageState = new ApplicationsPageState();

        public ICommand ReloadViewCommand => new DelegateCommand(ReloadView);
        public ICommand ViewLoadedCommand => new DelegateCommand(ViewDidLoad);

        private async void ViewDidLoad(object obj)
        {
            await _pageState.RefreshApplicationsInformationAsync();
            OnPropertyChanged(nameof(InstalledPackageIdentifiers));
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