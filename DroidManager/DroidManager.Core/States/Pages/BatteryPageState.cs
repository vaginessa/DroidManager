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


using SharpAdbClient;

namespace DroidManager.Core.States.Pages
{
    public class BatteryPageState
    {
        public BatteryPageState()
        {
            RefreshBatteryInformation();
        }

        public void RefreshBatteryInformation()
        {
            //Get Battery info
            var currentDevice = AndroidDeviceConnection.OverviewState.CurrentDevice;
            BatteryInformation = currentDevice.DeviceConnection.GetBatteryInfo();
        }

        public BatteryInfo BatteryInformation { get; private set; }

        public int BatteryPercentage => BatteryInformation.Level;

        public int BatteryScale => BatteryInformation.Scale;

        public string BatteryStatus => BatteryInformation.Status.ToString().ToLowerInvariant();

        public int BatteryVoltage => BatteryInformation.Voltage; //milliVolts

        public double BatteryTemperature => BatteryInformation.Temperature / 10; //Celsius

        public string BatteryType => BatteryInformation.Type; //Battery type (ex: Li-ion)
    }
}