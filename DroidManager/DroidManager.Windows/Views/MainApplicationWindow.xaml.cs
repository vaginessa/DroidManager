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


using NanoMvvm.Pagination;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DroidManager.Windows.Views
{
    /// <summary>
    /// Interaction logic for MainApplicationWindow.xaml
    /// </summary>
    public partial class MainApplicationWindow : IPaginatingView
    {
        public MainApplicationWindow()
        {
            InitializeComponent();
            (DataContext as PaginatingViewModel).View = this;
            WirePageEvents();
        }

        private void WirePageEvents()
        {
        }

        public Window WindowHandle => this;
        public ContentControl PageHost => pageHost;

        public event EventHandler<string> PageChanged;

        private void overviewTab_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PageChanged?.Invoke(this, "Overview");
        }

        private void applicationsTab_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PageChanged?.Invoke(this, "Applications");
        }

        private void backupTab_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PageChanged?.Invoke(this, "Backup");
        }

        private void fileTransferTab_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PageChanged?.Invoke(this, "File Transfer");
        }

        private void batteryTab_mouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PageChanged?.Invoke(this, "Battery");
        }

        private void advancedBootTab_mouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PageChanged?.Invoke(this, "Advanced Boot");
        }

        private void terminalTab_mouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PageChanged?.Invoke(this, "Terminal");
        }
    }
}