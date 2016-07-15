using System.Windows;

namespace NanoMvvm
{
    public class WindowService : IWindowService
    {
        public void ShowWindow<T>() where T : Window, new()
        {
            var newWindow = new T();
            newWindow.Show();
        }

        public void ShowWindow<T>(Window owner) where T : Window, new()
        {
            var newWindow = new T();
            newWindow.Owner = owner;
            newWindow.Show();
        }

        public void ShowWindowDialog<T>(Window owner) where T : Window, new()
        {
            var newWindow = new T();
            newWindow.Owner = owner;
            newWindow.ShowDialog();
        }

        public void ShowWindow<T>(object dataContext, Window owner) where T : Window, new()
        {
            var newWindow = new T();
            newWindow.DataContext = dataContext;
            newWindow.Owner = owner;
            newWindow.Show();
        }
    }
}