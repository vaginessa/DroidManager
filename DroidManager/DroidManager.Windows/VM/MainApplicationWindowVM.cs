using DroidManager.Windows.Views.Pages;
using NanoMvvm;
using NanoMvvm.Pagination;
using System.Windows.Input;
using System;
using DroidManager.Core;

namespace DroidManager.Windows.VM
{
    internal class MainApplicationWindowVM : PaginatingViewModel
    {
        private PageSwitcherService _pageSwitcher;
        public PaginationMessage PageSwitchMessage;

        public ICommand ViewLoadedCommand => new DelegateCommand(ViewDidLoad);

        public bool DeviceAvailable => AndroidDeviceConnection.OverviewState?.CurrentDevice != null;

        private void DeviceSelected(object obj)
        {
            throw new NotImplementedException();
        }

        private void ViewDidLoad(object obj)
        {
            View.PageChanged += OnViewPageChanged;
            _pageSwitcher = new PageSwitcherService(View.PageHost);
            LoadOverviewPage();
            OnPropertyChanged(nameof(DeviceAvailable));
        }

        public MainApplicationWindowVM()
        {
            PageSwitchMessage = new PaginationMessage();
        }

        #region Page Loaders
        private void OnViewPageChanged(object sender, string pageIdentifier)
        {
            switch (pageIdentifier)
            {
                case "Overview":
                    LoadOverviewPage();
                    break;

                case "Applications":
                    LoadApplicationsPage();
                    break;

                case "Backup":
                    LoadBackupPage();
                    break;

                case "File Transfer":
                    LoadFileTransferPage();
                    break;

                case "Battery":
                    LoadBatteryPage();
                    break;

                case "Advanced Boot":
                    LoadAdvancedBootPage();
                    break;
            }
        }

        private void LoadAdvancedBootPage()
        {
            throw new NotImplementedException();
        }

        private void LoadBatteryPage()
        {
            _pageSwitcher.LoadPage<BatteryPage>();
        }

        private void LoadFileTransferPage()
        {
            throw new NotImplementedException();
        }

        private void LoadBackupPage()
        {
            _pageSwitcher.LoadPage<BackupPage>();
        }

        private void LoadApplicationsPage()
        {
            _pageSwitcher.LoadPage<ApplicationsPage>();
        }

        private void LoadOverviewPage()
        {
            _pageSwitcher.LoadPage<OverviewPage>();
        }
        #endregion
    }
}