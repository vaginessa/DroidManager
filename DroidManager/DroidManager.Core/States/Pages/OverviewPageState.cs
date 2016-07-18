using DroidManager.Core.Classes;
using SharpAdbClient;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DroidManager.Core.States.Pages
{
    public class OverviewPageState
    {
        public string ConnectionStatusString => "Device not detected.";

        public List<AndroidDevice> ConnectedDevices { get; } = new List<AndroidDevice>();

        public OverviewPageState(string adbExecutablePath)
        {
            //Start ADB server
            var result = AdbServer.Instance.StartServer(adbExecutablePath, true);

            //Start ADB monitor
            var monitor = new DeviceMonitor(new AdbSocket(new IPEndPoint(IPAddress.Parse("127.0.0.1"), AdbServer.AdbServerPort)));
            //Subscribe for more events
            monitor.DeviceConnected += Monitor_DeviceConnected;
            monitor.DeviceDisconnected += Monitor_DeviceDisconnected;
            ConnectedDevices.AddRange(monitor.Devices.Select(deviceData => new AndroidDevice(deviceData))); //Initialize connected device collection
        }

        private void Monitor_DeviceDisconnected(object sender, DeviceDataEventArgs e)
        {
            var currentDevice = ConnectedDevices.Where(connectedDevice => connectedDevice.DeviceHandle == e.Device);
        }

        private void Monitor_DeviceConnected(object sender, DeviceDataEventArgs e)
        {
            ConnectedDevices.Add(new AndroidDevice(e.Device));
        }
    }
}