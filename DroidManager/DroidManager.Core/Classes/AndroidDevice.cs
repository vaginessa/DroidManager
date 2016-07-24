using SharpAdbClient;
using SharpAdbClient.DeviceCommands;
using System.Collections.Generic;

namespace DroidManager.Core.Classes
{
    public class AndroidDevice
    {
        public DeviceData DeviceMetadata { get; }
        public Device DeviceConnection;
        public string StatusText { get; private set; }
        public string DeviceName { get; private set; }
        public Dictionary<string, string> AdditionalProperties { get; }

        public AndroidDevice(DeviceData deviceHandle)
        {
            DeviceMetadata = deviceHandle;
            var state = deviceHandle.State;
            StatusText = $"Device connected in {state.ToString().ToLowerInvariant()} mode.";
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