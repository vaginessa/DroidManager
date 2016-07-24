using NanoMvvm.Pagination;
using System.Windows;
using System.Windows.Controls;

namespace DroidManager.Windows.Views.Pages
{
    /// <summary>
    /// Interaction logic for FileTransferPage.xaml
    /// </summary>
    public partial class FileTransferPage : UserControl, ISwitchablePage
    {
        public FileTransferPage()
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