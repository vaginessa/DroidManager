using SharpAdbClient;

namespace DroidManager.Core.Classes
{
    public class AndroidDevice
    {
        public DeviceData DeviceHandle { get; }

        public AndroidDevice(DeviceData deviceHandle)
        {
            DeviceHandle = deviceHandle;
        }
    }
}