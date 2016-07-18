using DroidManager.Core.States.Pages;
using NanoMvvm;
using NanoMvvm.Pagination;
using System.Windows.Input;

namespace DroidManager.Windows.VM.Pages
{
    internal class BatteryPageVM : SwitchablePageViewModel
    {
        private BatteryPageState _pageState = new BatteryPageState();

        public ICommand ReloadViewCommand => new DelegateCommand(ReloadView);

        private void ReloadView(object obj)
        {
            //Just refresh stats
            _pageState.RefreshBatteryInformation();
        }

        public string BatteryPercentageText => $"Battery Level: {_pageState.BatteryPercentage}%";

        public string BatteryScaleText => $"Battery Scale: {_pageState.BatteryScale}";

        public string BatteryVoltageText => $"Battery Voltage: {_pageState.BatteryVoltage} mV";

        public string BatteryTemperatureText => $"Battery Temperature: {_pageState.BatteryTemperature} °C";

        public string BatteryTypeText => $"Battery Type: {_pageState.BatteryType}";
    }
}