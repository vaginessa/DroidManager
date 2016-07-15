using System.Windows.Controls;

namespace NanoMvvm.Pagination
{
    public class PageSwitcherService
    {
        private Frame hostFrame;

        public PageSwitcherService(Frame hostWindow)
        {
            this.hostFrame = hostWindow;
        }

        public void Navigate(UserControl nextPage)
        {
            hostFrame.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            hostFrame.Content = nextPage;
            (nextPage as ISwitchablePage)?.UtilizeState(state);
        }
    }
}