using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
