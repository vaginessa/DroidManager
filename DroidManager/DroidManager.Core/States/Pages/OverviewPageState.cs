using DroidManager.Core.Classes;
using SharpAdbClient;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DroidManager.Core.States.Pages
{
    public class OverviewPageState
    {
        public string ConnectionStatusString => CurrentDevice?.StatusText ?? "Device not detected.";

        public ObservableCollection<AndroidDevice> ConnectedDevices { get; } = new ObservableCollection<AndroidDevice>();
        private AndroidDevice _currentDevice;

        public AndroidDevice CurrentDevice
        {
            get
            {
                return _currentDevice;
            }

            set
            {
                _currentDevice = value;
                //Set ADB target to latest device
                AdbClient.Instance.SetDevice(new AdbSocket(AdbServer.Instance.EndPoint), _currentDevice.DeviceMetadata);
                CurrentDeviceInformationService = new AndroidDeviceInformationService(CurrentDevice);
            }
        }

        public AndroidDeviceInformationService CurrentDeviceInformationService { get; set; }

        public OverviewPageState(string adbExecutablePath)
        {
            InitializeAdbConnection(adbExecutablePath);
        }

        private void InitializeAdbConnection(string adbExecutablePath)
        {
            //Start ADB server (on task because it can block a bit)
            var result = AdbServer.Instance.StartServer(adbExecutablePath, true);
            //var result = await Task.Run(() => AdbServer.Instance.StartServer(adbExecutablePath, true));

            //Start ADB monitor

            var monitor = new DeviceMonitor(new AdbSocket(AdbServer.Instance.EndPoint));
            //Subscribe for more events
            monitor.DeviceConnected += Monitor_DeviceConnected;
            monitor.DeviceDisconnected += Monitor_DeviceDisconnected;
            monitor.Start();

            //The events should take care of this:
            /*
            var currentlyConnectedDevices = monitor.Devices;
            ConnectedDevices.AddRange(currentlyConnectedDevices.Select(deviceData => new AndroidDevice(deviceData))); //Initialize connected device collection
            */
        }

        private void Monitor_DeviceDisconnected(object sender, DeviceDataEventArgs e)
        {
            string disconnectedDeviceSerial = e.Device.Serial;
            //Match by serial rather than identity
            var currentDevice = ConnectedDevices.Where(connectedDevice => connectedDevice.DeviceMetadata.Serial == disconnectedDeviceSerial).ToArray()[0];
            ConnectedDevices.Remove(currentDevice);
            currentDevice = null; //Dereference

            //Dereference current device if IDs match
            if (CurrentDevice.DeviceMetadata.Serial == disconnectedDeviceSerial)
            {
                CurrentDevice = null;
            }
            if (ConnectedDevices.Count == 0)
            {
                NoDevicesAvailable?.Invoke(this, null);
            }
        }

        internal void TriggerNoDevicesAvailable()
        {
            NoDevicesAvailable?.Invoke(this, null);
        }

        private void Monitor_DeviceConnected(object sender, DeviceDataEventArgs e)
        {
            var allDevices = AdbClient.Instance.GetDevices();
            //Since name isn't in eventargs, get name from the alldevices by matching serial number
            var newDeviceData = allDevices.Where(device => device.Serial == e.Device.Serial).ToArray()[0];
            var newDevice = new AndroidDevice(newDeviceData);
            ConnectedDevices.Add(newDevice);
            CurrentDevice = newDevice;
        }

        public event EventHandler NoDevicesAvailable;
    }
}