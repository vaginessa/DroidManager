using DroidManager.Core.States;
using NanoMvvm;

namespace DroidManager.Windows.VM
{
    internal class AboutWindowVM : ViewModelBase
    {
        private AboutState _aboutStateModel = new AboutState();

        public string Header => _aboutStateModel.AboutHeader;
        public string AboutContent => _aboutStateModel.AboutContent;
        public string Copyright => _aboutStateModel.AboutCopyright;
    }
}