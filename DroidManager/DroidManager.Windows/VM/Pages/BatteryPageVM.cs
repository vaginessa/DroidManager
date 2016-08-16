#region Copyright and License Header
/*

	DroidManager

	Copyright (c) 2016 0xFireball, IridiumIon Software

	Author(s): 0xFireball
	

	This file is part of DroidManager.

	DroidManager is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	DroidManager is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with DroidManager.  If not, see <http://www.gnu.org/licenses/>.

*/
#endregion


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