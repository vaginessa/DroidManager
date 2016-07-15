using System.Windows;
using System.Windows.Controls;
using NanoMvvm.Pagination;

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
        }

        public Window WindowHandle => this;
        public ContentControl PageHost => pageHost;
    }
}