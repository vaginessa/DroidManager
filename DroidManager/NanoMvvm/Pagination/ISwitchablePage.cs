using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
