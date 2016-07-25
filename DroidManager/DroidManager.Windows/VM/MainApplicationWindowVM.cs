using DroidManager.Core;
using DroidManager.Windows.Views.Pages;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NanoMvvm;
using NanoMvvm.Pagination;
using System;
using System.Windows.Input;

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

        private async void OnViewPageChanged(object sender, string pageIdentifier)
        {
            if (AndroidDeviceConnection.OverviewState.CurrentDevice != null || Properties.Settings.Default.debugMode)
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

                    case "Terminal":
                        LoadTerminalPage();
                        break;
                }
            }
            else
            {
                await (View as MetroWindow).ShowMessageAsync("Device not connected", "DroidManager could not find any devices. Please check your connection.");
            }
        }

        private void LoadTerminalPage()
        {
            _pageSwitcher.LoadPage<TerminalPage>();
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

        #endregion Page Loaders
    }
}