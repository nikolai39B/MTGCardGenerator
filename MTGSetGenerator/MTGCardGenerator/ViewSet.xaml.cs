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
    /// Interaction logic for ViewSet.xaml
    /// </summary>
    public partial class ViewSet : UserControl
    {
        /// <summary>
        /// Initializes a new ViewSet user control.
        /// </summary>
        /// <param name="setToView">The set to view.</param>
        public ViewSet(JsonSet setToView)
        {
            InitializeComponent();

            Set = setToView;
            ConfigureUIForSet();
        }


        //------//
        // Sets //
        //------//

        public JsonSet Set { get; private set; }

        /// <summary>
        /// Configures all UI elements for the given set, or for no set if the set is null.
        /// </summary>
        private void ConfigureUIForSet()
        {
            // If the set is null, disable everything that the user shouldn't use
            if (Set == null)
            {
                g_SetIcon.Visibility = System.Windows.Visibility.Hidden;
                tbl_SetName.Style = FindResource("tbl_Error") as Style;
                tbl_SetName.Text = "Please select a set.";

                b_AddCard.IsEnabled = false;

                tb_SearchCardName.IsEnabled = false;
                b_AdvancedSearch.IsEnabled = false;

                b_SetOptions.IsEnabled = false;
                b_SetStatistics.IsEnabled = false;
                b_GetCardSheet.IsEnabled = false;
            }
            
            // Otherwise, configure the UI for the set
            else
            {
                tbl_SetName.Text = Set.name;
                img_SetIcon.Source = Set.Icon;

                // TODO fill in the cards
            }
        }


        //----------------//
        // Event Handlers //
        //----------------//

        private void b_SwitchSet_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new SelectSet(this);
        }

        private void b_AddCard_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new GenerateCard(this, Set);
        }

        private void b_Back_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new Home();
        }

        private void tb_SearchCardName_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TODO implement search
        }
    }
}
