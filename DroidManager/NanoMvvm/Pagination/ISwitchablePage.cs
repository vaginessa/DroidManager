using System.Windows;

namespace NanoMvvm.Pagination
{
    public interface ISwitchablePage
    {
        void UtilizeState(object state);

        Window HostView { get; }
        PageSwitcherService SwitcherService { get; set; }
    }
}