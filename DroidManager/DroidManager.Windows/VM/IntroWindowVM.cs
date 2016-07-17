using NanoMvvm;
using System.Windows.Input;
using DroidManager.Core.States;
using System;
using DroidManager.Windows.Views;
using System.Reflection;

namespace DroidManager.Windows.VM
{
    class IntroWindowVM : ViewModelBase
    {
        public string ApplicationVersion => $"v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}";

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
