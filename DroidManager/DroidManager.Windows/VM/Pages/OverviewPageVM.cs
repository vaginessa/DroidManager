using DroidManager.Core.Classes;
using DroidManager.Core.States.Pages;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NanoMvvm.Pagination;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DroidManager.Windows.VM.Pages
{
    internal class OverviewPageVM : SwitchablePageViewModel
    {
        private OverviewPageState _pageState;

        public OverviewPageVM()
        {
            _pageState = new OverviewPageState(Properties.Settings.Default.adbExecutablePath);
        }

        public string ConnectionStatusString => _pageState.ConnectionStatusString;

        public List<AndroidDevice> AvailableDevices => _pageState.ConnectedDevices;

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