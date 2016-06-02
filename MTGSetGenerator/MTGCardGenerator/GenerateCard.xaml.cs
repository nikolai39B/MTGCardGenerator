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
        public GenerateCard()
        {
            InitializeComponent();
        }

        private void b_Help_Click(object sender, RoutedEventArgs e)
        {
            b_Help.Content = "Clicked";
        }

        private void b_Save_Click(object sender, RoutedEventArgs e)
        {

            //Make new card
            JsonCard newCard = new JsonCard();
            newCard.name = tb_CardName.Text;
            newCard.power = (int)tb_CardPower.Text;
            newCard.toughness = tb_CardToughness;
            newCard.rarity = cm_CardRarity;
            newCard.subtype = tb_CardSubtype;

            //Add card to collection manager
            CardCollectionManager.AddCard(newCard);
            
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
        private int TextToInt(string intText)
        {
            int j;
            if (Int32.TryParse("-105", out j))
                Console.WriteLine(j);
            else
                Console.WriteLine("String could not be parsed.");
        }
    }
}
