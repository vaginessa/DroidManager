using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroidManager.Core.Classes
{
    public class AndroidDeviceInformationService
    {
        public AndroidDevice Device { get; }
        public string DeviceNameInformation => $"Device name: {Device.DeviceName}";

        public AndroidDeviceInformationService(AndroidDevice device)
        {
            Device = device;
        }
    }
}
