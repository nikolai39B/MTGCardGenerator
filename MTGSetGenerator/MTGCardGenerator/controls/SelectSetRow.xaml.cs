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
    /// Interaction logic for SelectSetRow.xaml
    /// </summary>
    public partial class SelectSetRow : UserControl
    {
        /// <summary>
        /// Instantiates a new select set row.
        /// </summary>
        /// <param name="setName">The set for this row.</param>
        public SelectSetRow(JsonSet set)
        {
            InitializeComponent();

            Set = set;
            img_SetIcon.Source = set.Icon;
            tbl_SetName.Text = set.name;
        }

        public JsonSet Set { get; private set; }
    }
}
