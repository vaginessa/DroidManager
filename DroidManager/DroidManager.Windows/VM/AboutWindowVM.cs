using DroidManager.Core.States;
using NanoMvvm;

namespace DroidManager.Windows.VM
{
    class AboutWindowVM : ViewModelBase
    {
        private AboutState _aboutStateModel = new AboutState();

        public string Header => _aboutStateModel.AboutHeader;
    }
}
