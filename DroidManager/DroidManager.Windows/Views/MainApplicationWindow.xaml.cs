using NanoMvvm.Pagination;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DroidManager.Windows.Views
{
    /// <summary>
    /// Interaction logic for MainApplicationWindow.xaml
    /// </summary>
    public partial class MainApplicationWindow : IPaginatingView
    {
        public MainApplicationWindow()
        {
            InitializeComponent();
            (DataContext as PaginatingViewModel).View = this;
            WirePageEvents();
        }

        private void WirePageEvents()
        {
        }

        public Window WindowHandle => this;
        public ContentControl PageHost => pageHost;

        public event EventHandler<string> PageChanged;

        private void overviewTab_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PageChanged?.Invoke(this, "Overview");
        }

        private void applicationsTab_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PageChanged?.Invoke(this, "Applications");
        }

        private void backupTab_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PageChanged?.Invoke(this, "Backup");
        }

        private void fileTransferTab_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PageChanged?.Invoke(this, "File Transfer");
        }

        private void batteryTab_mouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PageChanged?.Invoke(this, "Battery");
        }

        private void advancedBoot_mouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PageChanged?.Invoke(this, "Advanced Boot");
        }        
    }
}