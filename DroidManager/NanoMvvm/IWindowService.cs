using System.Windows;

namespace NanoMvvm
{
    interface IWindowService
    {
        void ShowWindow<T>() where T : Window, new();
        void ShowWindow<T>(Window owner) where T : Window, new();
        void ShowWindow<T>(object dataContext, Window owner) where T : Window, new();

        void ShowWindowDialog<T>(Window owner) where T : Window, new();
    }
}