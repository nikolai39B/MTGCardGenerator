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
        public ViewSet()
        {
            InitializeComponent();
        }

        //-------------//
        // Switch Sets //
        //-------------//

        /// <summary>
        /// Configures all UI elements for the given set, or for no set if the set is null.
        /// </summary>
        /// <param name="set">The set to configure for, or null for no set.</param>
        private void ConfigureUIForSet(JsonSet set)
        {

        }


        //----------------//
        // Event Handlers //
        //----------------//

        private void b_Back_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new Home();
        }
    }
}
