using DroidManager.Windows.Views.Pages;
using NanoMvvm;
using NanoMvvm.Pagination;
using System.Windows.Input;
using System;

namespace DroidManager.Windows.VM
{
    internal class MainApplicationWindowVM : PaginatingViewModel
    {
        private PageSwitcherService _pageSwitcher;
        public PaginationMessage PageSwitchMessage;

        public ICommand ViewLoadedCommand => new DelegateCommand(ViewDidLoad);

        private void DeviceSelected(object obj)
        {
            throw new NotImplementedException();
        }

        private void ViewDidLoad(object obj)
        {
            View.PageChanged += OnViewPageChanged;
            _pageSwitcher = new PageSwitcherService(View.PageHost);
            LoadOverviewPage();
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
            }
        }

        private void LoadBackupPage()
        {
            throw new NotImplementedException();
        }

        private void LoadApplicationsPage()
        {
            throw new NotImplementedException();
        }

        private void LoadOverviewPage()
        {
            _pageSwitcher.LoadPage<OverviewPage>();
        }
        #endregion
    }
}