using System.Diagnostics;

namespace DroidManager.Core.States
{
    public class IntroState
    {
        public void VisitIridiumIonSite()
        {
            Process.Start("https://iridiumion.xyz");
        }
    }
}