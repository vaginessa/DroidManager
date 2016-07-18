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
    }
}