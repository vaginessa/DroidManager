using NanoMvvm;
using System.Windows;

namespace DroidManager.Windows.Views
{
    /// <summary>
    /// Interaction logic for IntroWindow.xaml
    /// </summary>
    public partial class IntroWindow : IView
    {
        public IntroWindow()
        {
            InitializeComponent();
            (DataContext as ViewModelBase).View = this as IView;
        }

        public Window WindowHandle => this;
    }
}