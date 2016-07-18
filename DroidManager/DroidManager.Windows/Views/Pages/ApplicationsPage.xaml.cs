﻿using NanoMvvm.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DroidManager.Windows.Views.Pages
{
    /// <summary>
    /// Interaction logic for ApplicationsPage.xaml
    /// </summary>
    public partial class ApplicationsPage : UserControl, ISwitchablePage
    {
        public ApplicationsPage()
        {
            InitializeComponent();
            (DataContext as SwitchablePageViewModel).PageView = this;
        }

        public void UtilizeState(object state)
        {
        }

        public Window HostView => Window.GetWindow(this);
    }
}