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
using System.Windows.Navigation;

namespace MTGSetGenerator
{
    /// <summary>
    /// Interaction logic for AddNewSet.xaml
    /// </summary>
    public partial class AddNewSet : UserControl
    {
        /// <summary>
        /// Initializes a new AddNewSet control.
        /// </summary>
        /// <param name="previousControl">The control to return to if the user fails to return a set.</param>
        public AddNewSet(UserControl previousControl)
        {
            InitializeComponent();

            this.previousControl = previousControl;

            tbl_SetIconFilename.Text = "Please choose an icon...";
            img_SetIcon.Visibility = System.Windows.Visibility.Hidden;
        }

        //------//
        // Data //
        //------//

        UserControl previousControl;
        
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

            // Otherwise, create the set
            int setId = CardCollectionManager.RequestUniqueSetId();
            JsonSet newSet = new JsonSet()
            {
                id = setId,
                name = tb_SetName.Text,
                details = tb_SetDetails.Text
            };
            CardCollectionManager.AddSet(newSet);

            // Save the set icon            
            CardCollectionManager.WriteSetIconImage(newSet.id, newSet.name, new BitmapImage(new Uri(setIconFilePath)));

            Window.GetWindow(this).Content = new ViewSet(newSet);
        }

        private void b_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = previousControl;
        }
    }
}
