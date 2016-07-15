using System.Windows.Controls;

namespace NanoMvvm.Pagination
{
    public class PageSwitcherService
    {
        private ContentControl hostControl;

        public PageSwitcherService(ContentControl hostWindow)
        {
            this.hostControl = hostWindow;
        }

        public void LoadPage<T>() where T : UserControl, ISwitchablePage, new()
        {
            var nextPage = new T();
            hostControl.Content = nextPage;
        }

        public void LoadPage<T>(object state) where T : UserControl, new()
        {
            var nextPage = new T();
            hostControl.Content = nextPage;
            (nextPage as ISwitchablePage)?.UtilizeState(state);
        }
    }
}