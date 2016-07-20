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
    /// Interaction logic for PlaneswalkerAbilityRow.xaml
    /// </summary>
    public partial class PlaneswalkerAbilityRow : UserControl
    {
        public PlaneswalkerAbilityRow()
        {
            InitializeComponent();
        }


        //------//
        // Data //
        //------//

        public string AbilityCost { get { return tb_AbilityCost.Text; } }
        public string AbilityText { get { return tb_AbilityText.Text; } }


        //---------//
        // Utility //
        //---------//

        /// <summary>
        /// Resets the row to its default values.
        /// </summary>
        public void ResetToDefault()
        {
            tb_AbilityCost.Text = "";
            tb_AbilityText.Text = "";
        }


        //--------//
        // Events //
        //--------//

        public event EventHandler AbilityChanged;

        private void tb_UiElement_TextChanged(object sender, TextChangedEventArgs e)
        {
            EventHandler ev = AbilityChanged;
            if (ev != null)
            {
                ev.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
