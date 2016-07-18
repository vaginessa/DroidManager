using NanoMvvm;
using DroidManager.Core.States.Pages;

namespace DroidManager.Windows.VM.Pages
{
    class OverviewPageVM : ViewModelBase
    {
        private OverviewPageState _pageState = new OverviewPageState();

        public string ConnectionStatusString => _pageState.ConnectionStatusString;
    }
}
