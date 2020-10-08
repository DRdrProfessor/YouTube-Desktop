﻿using System;
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

namespace YouTube_Desktop.Control
{
    /// <summary>
    /// Interaction logic for MenuControlButton.xaml
    /// </summary>
    public partial class MenuControlButton : UserControl
    {
        // Control properties
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(MenuControlButton), new PropertyMetadata());

        public MenuControlButton()
        {
            InitializeComponent();
        }

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
    }
}