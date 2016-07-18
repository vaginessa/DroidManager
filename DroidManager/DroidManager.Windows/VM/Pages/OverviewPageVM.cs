using DroidManager.Core.Classes;
using DroidManager.Core.States.Pages;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NanoMvvm;
using NanoMvvm.Pagination;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using DroidManager.Core;

namespace DroidManager.Windows.VM.Pages
{
    internal class OverviewPageVM : SwitchablePageViewModel
    {
        private OverviewPageState _pageState;

        public ICommand ReloadViewCommand => new DelegateCommand(ReloadView);

        private void ReloadView(object obj)
        {
            PageView.SwitcherService.ReloadCurrentPage();
        }

        public OverviewPageVM()
        {
            _pageState = new OverviewPageState(Properties.Settings.Default.adbExecutablePath);
            AndroidDeviceConnection.OverviewState = _pageState;
        }

        public string ConnectionStatusString => _pageState.ConnectionStatusString;

        public ObservableCollection<AndroidDevice> AvailableDevices => _pageState.ConnectedDevices;

        public AndroidDevice CurrentlySelectedDevice
        {
            get
            {
                return _pageState.CurrentDevice;
            }
            set
            {
                _pageState.CurrentDevice = value;
                OnPropertyChanged(nameof(CurrentlySelectedDevice));
            }
        }

        public AndroidDeviceInformationService CurrentDeviceInformationService => _pageState.CurrentDeviceInformationService;
    }
}