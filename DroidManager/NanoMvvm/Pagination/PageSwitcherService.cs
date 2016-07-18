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

        public ISwitchablePage CurrentPage { get; private set; }

        public void LoadPage<T>() where T : UserControl, ISwitchablePage, new()
        {
            LoadPage<T>(null);
        }

        public void LoadPage<T>(bool useCache = true) where T : UserControl, ISwitchablePage, new()
        {
            LoadPage<T>(null, useCache);
        }

        public void LoadPage<T>(object state, bool useCache = true) where T : UserControl, new()
        {
            var pageTypeKey = typeof(T);
            if (!PageCache.ContainsKey(pageTypeKey) || !useCache)
            {
                var nextPage = new T();
                PageCache.Add(pageTypeKey, nextPage);
                LoadPageFromInstance(nextPage);
                (nextPage as ISwitchablePage)?.UtilizeState(state);
            }
            else
            {
                //Page is cached, do not recreate
                var nextPage = PageCache[pageTypeKey];
                LoadPageFromInstance((UserControl)nextPage);
            }
        }

        private void LoadPageFromInstance(UserControl nextPage)
        {
            hostControl.Content = nextPage;
            var switchablePage = nextPage as ISwitchablePage;
            CurrentPage = switchablePage;
            switchablePage.SwitcherService = this;
        }

        public void ReloadCurrentPage()
        {
            ReloadCurrentPage(null);
        }

        public void ReloadCurrentPage(object state)
        {
            var newPageInstance = Activator.CreateInstance(CurrentPage.GetType());
            LoadPageFromInstance((UserControl)newPageInstance);
            (newPageInstance as ISwitchablePage).UtilizeState(state);
        }
    }
}