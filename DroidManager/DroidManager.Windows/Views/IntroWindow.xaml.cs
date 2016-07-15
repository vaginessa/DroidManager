﻿using NanoMvvm;
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
using System.Windows.Shapes;

namespace DroidManager.Windows.Views
{
    /// <summary>
    /// Interaction logic for IntroWindow.xaml
    /// </summary>
    public partial class IntroWindow : IView
    {
        public IntroWindow()
        {
            InitializeComponent();
            (DataContext as ViewModelBase).View = this as IView;
        }

        public Window WindowHandle => this;
    }
}
