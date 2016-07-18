using DroidManager.Core.States.Pages;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NanoMvvm.Pagination;

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
    }
}