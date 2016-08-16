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


using DroidManager.Core.States.Pages;
using System;

namespace DroidManager.Core
{
    public static class AndroidDeviceConnection
    {
        private static OverviewPageState overviewState;

        /// <summary>
        /// Stores the current OverviewPageState instance, because it has all the device metadata
        /// </summary>
        public static OverviewPageState OverviewState
        {
            get
            {
                return overviewState;
            }

            set
            {
                overviewState = value;
                StateChanged?.Invoke(null, null);
            }
        }

        public static event EventHandler StateChanged;
    }
}