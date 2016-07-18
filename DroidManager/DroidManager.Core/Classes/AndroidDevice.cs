using SharpAdbClient;

namespace DroidManager.Core.Classes
{
    public class AndroidDevice
    {
        public DeviceData DeviceHandle { get; }
        public string StatusText { get; private set; }

        public AndroidDevice(DeviceData deviceHandle)
        {
            DeviceHandle = deviceHandle;
            var state = deviceHandle.State;
            StatusText = $"Device connected in {state.ToString().ToLowerInvariant()} mode.";
        }

        public override string ToString()
        {
            return $"{DeviceHandle.Name} ({DeviceHandle.Serial})";
        }
    }
}