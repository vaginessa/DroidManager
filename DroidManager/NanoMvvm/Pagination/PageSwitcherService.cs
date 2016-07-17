using System;
using System.Collections.Generic;
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

        private Dictionary<Type, object> PageCache = new Dictionary<Type, object>();

        public void LoadPage<T>() where T : UserControl, ISwitchablePage, new()
        {
            LoadPage<T>(null);
        }

        public void LoadPage<T>(object state) where T : UserControl, new()
        {
            var pageTypeKey = typeof(T);
            if (!PageCache.ContainsKey(pageTypeKey))
            {
                var nextPage = new T();
                PageCache.Add(pageTypeKey, nextPage);
                hostControl.Content = nextPage;
                (nextPage as ISwitchablePage)?.UtilizeState(state);
            }
            else
            {
                //Page is cached, do not recreate
                hostControl.Content = PageCache[pageTypeKey];
            }
        }
    }
}