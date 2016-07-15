using NanoMvvm;
using System.Windows.Input;
using DroidManager.Core.States;
using System;
using DroidManager.Windows.Views;

namespace DroidManager.Windows.VM
{
    class IntroWindowVM : ViewModelBase
    {
        private IntroState _introStateModel = new IntroState();

        public ICommand VisitIridiumIonSiteCommand => new DelegateCommand(VisitIridiumIonSite);

        public ICommand ViewAboutCommand => new DelegateCommand(ViewAbout);

        public ICommand LaunchMainApplicationCommand => new DelegateCommand(LaunchMainApplication);

        private void LaunchMainApplication(object obj)
        {
            View.WindowHandle.Hide();
            DroidManagerContext.WindowService.ShowWindowDialog<MainApplicationWindow>(View.WindowHandle);
            View.WindowHandle.Show();
        }

        private void ViewAbout(object obj)
        {
            DroidManagerContext.WindowService.ShowWindow<AboutWindow>(View.WindowHandle);
        }

        private void VisitIridiumIonSite(object obj)
        {
            _introStateModel.VisitIridiumIonSite();
        }
    }
}
