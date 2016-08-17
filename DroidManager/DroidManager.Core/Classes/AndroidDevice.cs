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
using SharpAdbClient.DeviceCommands;
using System.Collections.Generic;

namespace DroidManager.Core.Classes
{
    public class AndroidDevice
    {
        public DeviceData DeviceMetadata { get; }
        public Device DeviceConnection { get; }
        public DeviceState DeviceState { get; }
        public string StatusText { get; private set; }
        public string DeviceName { get; private set; }
        public Dictionary<string, string> AdditionalProperties { get; }

        public AndroidDevice(DeviceData deviceHandle)
        {
            DeviceMetadata = deviceHandle;
            DeviceState = deviceHandle.State;
            StatusText = $"Device connected in {DeviceState.ToString().ToLowerInvariant()} mode.";
            DeviceConnection = new Device(DeviceMetadata);
            //Set properties
            DeviceName = DeviceMetadata.Name;
            AdditionalProperties = deviceHandle.GetProperties();
        }

        public override string ToString()
        {
            return $"{DeviceMetadata.Name} ({DeviceMetadata.Serial})";
        }
    }
}