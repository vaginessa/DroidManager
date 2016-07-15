using System.Windows.Controls;

namespace NanoMvvm.Pagination
{
    public interface IPaginatingView : IView
    {
        ContentControl PageHost { get; }
    }
}