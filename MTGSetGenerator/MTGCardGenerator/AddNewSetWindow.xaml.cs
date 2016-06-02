using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

            tbl_SetIconFilename.Text = "Please choose an icon...";
            img_SetIcon.Visibility = System.Windows.Visibility.Hidden;
        }

        //------//
        // Data //
        //------//

        ErrorWindow issuesWindow = null;
        string setIconFilePath = "";
        bool setIconValid = false;


        //----------------//
        // Event Handlers //
        //----------------//

        private void b_Browse_Click(object sender, RoutedEventArgs e)
        {
            // Create the select file dialog
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG Files (.png)|*.png|GIF Files (*.gif)|*.gif";

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                // Set the fields on the UI
                setIconFilePath = dlg.FileName;
                tbl_SetIconFilename.Text = Path.GetFileName(setIconFilePath);

                // Set the icon
                img_SetIcon.Source = new BitmapImage(new Uri(setIconFilePath));
                img_SetIcon.Visibility = System.Windows.Visibility.Visible;

                setIconValid = true;
            }
        }

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

            // Check the set icon
            if (!setIconValid)
            {
                errorMessage += "Please supply a valid set icon.\n";
            }

            // If there's errors, display them and return
            if (errorMessage != "")
            {
                issuesWindow = new ErrorWindow(errorMessage.Trim());
                issuesWindow.Show();
                return;
            }

            // TODO: save the set icon image to a defined location in case the user changes the source image
            
            // Otherwise, create the set
            JsonSet newSet = new JsonSet()
            {
                setName = tb_SetName.Text,
                setDetails = tb_SetDetails.Text,
                setIconPath = setIconFilePath
            };
            CardCollectionManager.AddSet(newSet);

            DialogResult = true;
        }

        private void b_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
