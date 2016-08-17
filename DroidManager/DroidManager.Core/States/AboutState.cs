#region Copyright and License Header
/*

	DroidManager

	Copyright (c) 2016 0xFireball, IridiumIon Software

	Author(s): 0xFireball
	

	This file is part of DroidManager.

	DroidManager is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	DroidManager is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with DroidManager.  If not, see <http://www.gnu.org/licenses/>.

*/
#endregion


using System;
using System.Diagnostics;

namespace DroidManager.Core.States
{
    public class AboutState
    {
        public string AboutHeader { get; } = "About DroidManager";
        public string AboutContent { get; } = "DroidManager is an application developed by 0xFireball of IridiumIon Software that allows you to manage your Android device.";
        public string AboutCopyright { get; } = "Copyright (©) 0xFireball, IridiumIon Software 2015-2016.";

        public void VisitGitHub()
        {
            Process.Start("https://github.com/IridiumIon/DroidManager");
        }

        public void VisitXda()
        {
            Process.Start("http://forum.xda-developers.com/android/software/tool-droidmanager-noob-friendly-t3441124");
        }
    }
}