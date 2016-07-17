using System;
using System.Windows;
using System.Windows.Controls;

namespace NanoMvvm.Pagination
{
    public interface IPaginatingView
    {
        Window WindowHandle { get; }
        
        ContentControl PageHost { get; }

        event EventHandler<string> PageChanged;
    }
}