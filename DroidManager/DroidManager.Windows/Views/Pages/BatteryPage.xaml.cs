using NanoMvvm.Pagination;
using System.Windows;
using System.Windows.Controls;

namespace DroidManager.Windows.Views.Pages
{
    /// <summary>
    /// Interaction logic for BatteryPage.xaml
    /// </summary>
    public partial class BatteryPage : UserControl, ISwitchablePage
    {
        public BatteryPage()
        {
            InitializeComponent();
            (DataContext as SwitchablePageViewModel).PageView = this;
        }

        #region ISwitchablePage members

        public void UtilizeState(object state)
        {
        }

        public Window HostView => Window.GetWindow(this);

        public PageSwitcherService SwitcherService { get; set; }

        #endregion ISwitchablePage members
    }
}