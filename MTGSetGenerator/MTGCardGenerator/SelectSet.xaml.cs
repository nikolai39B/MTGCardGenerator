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
    /// Interaction logic for SelectSet.xaml
    /// </summary>
    public partial class SelectSet : UserControl
    {
        /// <summary>
        /// Initializes a SelectSet user control.
        /// </summary>
        /// <param name="previousControl">The control to return to if the user presses cancel.</param>
        public SelectSet(ViewSet previousControl)
        {
            InitializeComponent();
            this.previousControl = previousControl;
            FillUIWithSets();
        }

        //------//
        // Data //
        //------//

        ViewSet previousControl;
        List<SelectSetRow> selectSetRows = new List<SelectSetRow>();

        /// <summary>
        /// Fills the scroll viewer with the collection's sets.
        /// </summary>
        private void FillUIWithSets()
        {
            sp_LeftSetsList.Children.Clear();
            sp_RightSetsList.Children.Clear();
            selectSetRows = new List<SelectSetRow>();
            
            foreach (var set in CardCollectionManager.Sets)
            {
                SelectSetRow currentRow = new SelectSetRow(set);
                selectSetRows.Add(currentRow);

                if (sp_LeftSetsList.Children.Count <= sp_RightSetsList.Children.Count)
                {
                    sp_LeftSetsList.Children.Add(currentRow);
                }
                else
                {
                    sp_RightSetsList.Children.Add(currentRow);
                }
            }

            // TODO: add sorting functionality
        }


        //----------------//
        // Event Handlers //
        //----------------//

        private void b_SelectSet_Click(object sender, RoutedEventArgs e)
        {
            foreach (var row in selectSetRows)
            {
                if (row.rb_Select.IsChecked == true)
                {
                    Window.GetWindow(this).Content = new ViewSet(row.Set);
                    return;
                }
            }

            // If we got here, no set is selected
            ErrorWindow window = new ErrorWindow("Please select a set to view.");
            window.ShowDialog();
        }

        private void b_AddNewSet_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new AddNewSet(this);
        }

        private void b_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = previousControl;
        }
    }
}
