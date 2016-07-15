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

        public ICommand ViewLoadedCommand => new DelegateCommand(ViewLoaded);

        private void ViewLoaded(object obj)
        {
            _pageSwitcher = new PageSwitcherService(View.PageHost);
            _pageSwitcher.LoadPage<OverviewPage>();
        }

        public MainApplicationWindowVM()
        {
            PageSwitchMessage = new PaginationMessage();
        }
    }
}