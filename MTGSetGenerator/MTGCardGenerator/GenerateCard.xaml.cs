using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
using System.Runtime.Serialization.Json;

namespace MTGSetGenerator
{
    /// <summary>
    /// Interaction logic for GenerateCard.xaml
    /// </summary>
    public partial class GenerateCard : UserControl
    {
        /// <summary>
        /// Instantiates a GenerateCard user control.
        /// </summary>
        /// <param name="controlToReturnTo">The control to return to if the cancel or save buttons are pressed.</param>
        public GenerateCard(ContentControl controlToReturnTo)
        {
            this.controlToReturnTo = controlToReturnTo;
            InitializeComponent();

            intToRarity = new Dictionary<ComboBoxItem,JsonCard.Rarity>()
            {
                { cbi_Common, JsonCard.Rarity.COMMON},
                {1, JsonCard.Rarity.UNCOMMON},
                {2, JsonCard.Rarity.RARE},
                {3, JsonCard.Rarity.MYTHIC}
            };
        }


        //----------------//
        // Parent Objects //
        //----------------//

        private ContentControl controlToReturnTo;


        //----------------//
        // Event Handlers //
        //----------------//

        private void b_Help_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void b_Clear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Cancel card generation and discard changes?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Window.GetWindow(this).Content = controlToReturnTo;
            }
        }

        private void b_Save_Click(object sender, RoutedEventArgs e)
        {

            //Make new card
            JsonCard newCard = new JsonCard();
            List<string> invalidLog = new List<string>();

            newCard.name = tb_CardName.Text;

            newCard.power = tb_CardPower.Text;
            newCard.toughness = tb_CardToughness.Text;

            newCard.rarity = intToRarity[cm_CardRarity.SelectedIndex];

            newCard.subtype = tb_CardSubtype.Text;

            if (invalidLog.Count() > 0)
            {
                MessageBox.Show(string.Format(
                    "The following errors have occured in the card submission: \n-{0}", String.Join("\n-", invalidLog.ToArray())),
                    "Card Save Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //Add card to collection manager
                CardCollectionManager.AddCard(newCard);
            }
        }


        //--------------------//
        // Processing Helpers //
        //--------------------//

        /// <summary>
        /// Safely parses text which represents an integer. 
        /// </summary>
        /// <param name="intText"></param>
        /// <returns></returns>

        // TODO: Add print to 'save' error window
        // TODO: Load state from previously suspended application
        private int TextToInt(string intText, List<string> invalidLog)
        {
            int j;
            if (int.TryParse(intText, out j))
            {
                return j;
            }
            else
            {
                invalidLog.Add(String.Format("\"{0}\" cannot be converted to an integer", intText));
                return -1;
            }
                
        }


        //------------------------------//
        // Enum Conversion Dictionaries //
        //------------------------------//
        private Dictionary<ComboBoxItem, JsonCard.Rarity> intToRarity;

        private Dictionary<JsonCard.Rarity, int> rarityToInt = new Dictionary<JsonCard.Rarity, int>();
        {
            {JsonCard.Rarity.COMMON,   0},
            {JsonCard.Rarity.UNCOMMON, 1},
            {JsonCard.Rarity.RARE,     2},
            {JsonCard.Rarity.MYTHIC,   3}
        };
    }
}
