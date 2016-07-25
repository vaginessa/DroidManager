using NanoMvvm;
using System.Windows;

namespace DroidManager.Windows.Views
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : IView
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        public Window WindowHandle => this;
    }
}