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


namespace DroidManager.Core.Classes
{
    public class AndroidDeviceInformationService
    {
        public AndroidDevice Device { get; }
        public string DeviceNameInformation => $"Device name: {Device.DeviceName}";
        public string DeviceSerialInformation => $"Serial: {Device.DeviceMetadata.Serial}";
        public string ProductInformation => $"Product information: {Device.DeviceMetadata.Product}";
        public string DeviceFeatureInformation => $"Device features: {Device.DeviceMetadata.Features}";
        public string DeviceModelInformation => $"Device model: {Device.DeviceMetadata.Model}";

        public AndroidDeviceInformationService(AndroidDevice device)
        {
            Device = device;
        }
    }
}