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

        public int BatteryScale => BatteryInformation.Scale;

        public string BatteryStatus => BatteryInformation.Status.ToString().ToLowerInvariant();

        public int BatteryVoltage => BatteryInformation.Voltage; //milliVolts

        public double BatteryTemperature => BatteryInformation.Temperature / 10; //Celsius

        public string BatteryType => BatteryInformation.Type; //Battery type (ex: Li-ion)
    }
}