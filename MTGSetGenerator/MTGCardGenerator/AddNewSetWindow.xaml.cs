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

        ErrorWindow issuesWindow = null;

        //----------------//
        // Event Handlers //
        //----------------//

        private void b_AddSet_Click(object sender, RoutedEventArgs e)
        {
            // Close any open issues window
            if (issuesWindow != null)
            {
                issuesWindow.Close();
            }

            string errorMessage = "";

            // Check the set name
            string setName = tb_SetName.Text;
            if (setName == "" || setName == null)
            {
                errorMessage += "Please supply a set name.\n";
            }

            // If there's errors, display them and return
            if (errorMessage != "")
            {
                issuesWindow = new ErrorWindow(errorMessage.Trim());
                issuesWindow.Show();
                return;
            }
            
            // Otherwise, create the set
            // TODO: add the set
            DialogResult = true;
        }

        private void b_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
