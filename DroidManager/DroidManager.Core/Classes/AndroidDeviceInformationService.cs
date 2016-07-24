namespace DroidManager.Core.Classes
{
    public class AndroidDeviceInformationService
    {
        public AndroidDevice Device { get; }
        public string DeviceNameInformation => $"Device name: {Device.DeviceName}";
        public string DeviceSerialInformation => $"Device serial identifier: {Device.DeviceMetadata.Serial}";
        public string ProductInformation => $"Product information: {Device.DeviceMetadata.Product}";
        public string DeviceFeatureInformation => $"Device features: {Device.DeviceMetadata.Features}";
        public string DeviceModelInformation => $"Device model: {Device.DeviceMetadata.Model}";

        public AndroidDeviceInformationService(AndroidDevice device)
        {
            Device = device;
        }
    }
}