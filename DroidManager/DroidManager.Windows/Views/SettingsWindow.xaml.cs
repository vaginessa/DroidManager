using NanoMvvm;
using System.Windows;

namespace DroidManager.Windows.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : IView
    {
        public SettingsWindow()
        {
            InitializeComponent();
            (DataContext as ViewModelBase).View = this as IView;
        }

        public Window WindowHandle => this;
    }
}