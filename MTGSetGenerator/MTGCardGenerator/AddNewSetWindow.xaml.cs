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
using System.Windows.Shapes;

namespace MTGSetGenerator
{
    /// <summary>
    /// Interaction logic for AddNewSetWindow.xaml
    /// </summary>
    public partial class AddNewSetWindow : Window
    {
        public AddNewSetWindow()
        {
            InitializeComponent();
        }

        // TODO: improve dialog

        //----------------//
        // Event Handlers //
        //----------------//

        private void b_AddSet_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void b_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
