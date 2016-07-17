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

        private void LoadOverviewPage()
        {
            _pageSwitcher = new PageSwitcherService(View.PageHost);
            _pageSwitcher.LoadPage<OverviewPage>();
        }

        private void ViewDidLoad(object obj)
        {
            View.PageChanged += OnViewPageChanged;
            //_pageSwitcher = new PageSwitcherService(View.PageHost);
            //_pageSwitcher.LoadPage<OverviewPage>();
        }

        public MainApplicationWindowVM()
        {
            PageSwitchMessage = new PaginationMessage();
        }

        private void OnViewPageChanged(object sender, string e)
        {
            LoadOverviewPage();
        }
    }
}