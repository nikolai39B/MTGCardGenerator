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

namespace MTGSetGenerator
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

        //----------------//
        // Initialization //
        //----------------//
        private void LoadCardJson()
        {

        }

        //----------------//
        // Event Handlers //
        //----------------//
        private void b_AddNewSet_Click(object sender, RoutedEventArgs e)
        {
            Window addNewSetWindow = new AddNewSetWindow();
            bool? result = addNewSetWindow.ShowDialog();
            if (result == true)
            {
                Window.GetWindow(this).Content = new ViewSet();
            }
        }

        private void b_EditExistingSet_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new ViewSet();
        }

        private void b_Options_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_Help_Click(object sender, RoutedEventArgs e)
        {
            // NOTE: temporary for debugging purposes
            Window.GetWindow(this).Content = new GenerateCard(this);
        }

        private void b_Exit_Click(object sender, RoutedEventArgs e)
        {
            Window parent = Window.GetWindow(this);
            parent.Close();
        }
    }
}
