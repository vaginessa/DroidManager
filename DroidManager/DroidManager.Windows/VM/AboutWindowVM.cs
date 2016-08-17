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


using DroidManager.Core.States;
using NanoMvvm;
using System.Windows.Input;
using System;

namespace DroidManager.Windows.VM
{
    internal class AboutWindowVM : ViewModelBase
    {
        private AboutState _aboutStateModel = new AboutState();

        public string Header => _aboutStateModel.AboutHeader;
        public string AboutContent => _aboutStateModel.AboutContent;
        public string Copyright => _aboutStateModel.AboutCopyright;

        public ICommand VisitGitHubCommand => new DelegateCommand(VisitGitHub);
        public ICommand VisitXdaCommand => new DelegateCommand(VisitXda);

        private void VisitXda(object obj)
        {
            _aboutStateModel.VisitXda();
        }

        private void VisitGitHub(object obj)
        {
            _aboutStateModel.VisitGitHub();
        }
    }
}