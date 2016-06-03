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
        /// <param name="currentSet">The current set for this card.</param>
        public GenerateCard(ContentControl controlToReturnTo, JsonSet currentSet)
        {

            InitializeComponent();

            this.controlToReturnTo = controlToReturnTo;
            this.currentSet = currentSet;

            InitConversionDictionaries();

            RestoreUIToDefault();
        }

        /// <summary>
        /// Initializes the conversion dictionaries.
        /// </summary>
        private void InitConversionDictionaries()
        {
            comboBoxItemToRarity = new Dictionary<ComboBoxItem, JsonCard.Rarity>()
            {
                { cbi_Common , JsonCard.Rarity.COMMON },
                { cbi_Uncommon, JsonCard.Rarity.UNCOMMON },
                { cbi_Rare, JsonCard.Rarity.RARE },
                { cbi_Mythic, JsonCard.Rarity.MYTHIC }
            };

            rarityToComboBoxItem = new Dictionary<JsonCard.Rarity, ComboBoxItem>()
            {
                { JsonCard.Rarity.COMMON, cbi_Common },
                { JsonCard.Rarity.UNCOMMON, cbi_Uncommon },
                { JsonCard.Rarity.RARE, cbi_Rare },
                { JsonCard.Rarity.MYTHIC, cbi_Mythic }
            };

            // TODO finish implementing conversion dictionaries
            comboBoxItemToType = new Dictionary<ComboBoxItem, JsonCard.Type>()
            {
                { cbi_Creature, JsonCard.Type.CREATURE },

            };

            typeToComboBoxItem = new Dictionary<JsonCard.Type, ComboBoxItem>()
            {
            
            };

            comboBoxItemToString = new Dictionary<ComboBoxItem, string>()
            {

            };

            stringToComboBoxItem = new Dictionary<string, ComboBoxItem>()
            {

            };

            comboBoxItemToColor = new Dictionary<ComboBoxItem, JsonCard.Color>()
            {

            };

            colorToComboBoxItem = new Dictionary<JsonCard.Color, ComboBoxItem>()
            {

            };
        }


        //----------------//
        // Parent Objects //
        //----------------//

        private ContentControl controlToReturnTo;
        private JsonSet currentSet;


        //---------------//
        // UI Management //
        //---------------//

        /// <summary>
        /// Resets all UI elements to their default values.
        /// </summary>
        private void RestoreUIToDefault()
        {
            tb_Name.Text = "";
            
            tb_Cost.Text = "";
            tb_Cmc.Text = "";
            tb_Cmc.IsEnabled = false;
            cb_InferCMC.IsChecked = true;

            tb_Pretype.Text = "";
            cmb_Type.SelectedIndex = 0;
            tb_Subtype.Text = "";

            cmb_Rarity.SelectedIndex = 0;

            tbl_ImageFilename.Text = "";

            cmb_Color.SelectedIndex = 0;
            cmb_Color.IsEnabled = false;

            RefreshPowerToughnessAndLoyalty();
            InferCmc();
            InferColor();

            RefreshCardImage();
        }


        /// <summary>
        /// Refreshes and enables the power, toughness, and loyalty text boxes as necessary.
        /// </summary>
        private void RefreshPowerToughnessAndLoyalty()
        {
            // Everything is on by default
            tb_Power.IsEnabled = true;
            tb_Toughness.IsEnabled = true;
            tb_Loyalty.IsEnabled = true;

            // If we're not a creature, turn off P/T
            if (cmb_Type.SelectedItem != cbi_Creature)
            {
                tb_Power.Text = "";
                tb_Toughness.Text = "";
                tb_Power.IsEnabled = false;
                tb_Toughness.IsEnabled = false;
            }

            // If we're not a planeswalker, turn off loyalty
            if (cmb_Type.SelectedItem != cbi_Planeswalker)
            {
                tb_Loyalty.Text = "";
                tb_Loyalty.IsEnabled = false;
            }
        }

        /// <summary>
        /// Infers the card's cmc and sets the value on the UI.
        /// </summary>
        /// <returns></returns>
        public int InferCmc()
        {
            // TODO implement InferCmc()
            return 0;
        }

        /// <summary>
        /// Infers the card's color and sets the value on the UI.
        /// </summary>
        /// <returns></returns>
        public ComboBoxItem InferColor()
        {
            // TODO implement InferColor()
            return cbi_Colorless;
        }


        //------------------//
        // Image Management //
        //------------------//

        private const int cardImageHeight = 125;
        private const int cardImageWidth = 170;

        /// <summary>
        /// Refreshes all elements that compose the card image.
        /// </summary>
        private void RefreshCardImage()
        {
            // TODO finish implementing RefreshCardImage()
            img_CardSetIcon.Source = currentSet.Icon;
            CropCardImage();
        }

        /// <summary>
        /// Crops the current card image to fit neatly in the allocated space.
        /// </summary>
        private void CropCardImage()
        {
            BitmapImage image = FindResource("img_Raime") as BitmapImage;
            
            double requiredHeightOverWidth = ((double)cardImageHeight) / cardImageWidth;
            double currentHeightOverWidth = image.PixelHeight / image.PixelWidth;

            int centerX = image.PixelWidth / 2;
            int centerY = image.PixelHeight / 2;

            int finalWidth = image.PixelWidth;
            int finalHeight = image.PixelHeight;

            // Too tall
            if (currentHeightOverWidth > requiredHeightOverWidth)
            {
                finalHeight = (int)Math.Ceiling(finalWidth * requiredHeightOverWidth);
            }

            // Too wide
            else if (currentHeightOverWidth < requiredHeightOverWidth)
            {
                finalWidth = (int)Math.Ceiling(finalHeight / requiredHeightOverWidth);
            }

            CroppedBitmap newImage = new CroppedBitmap(image, new Int32Rect(
                centerX - finalWidth / 2, 
                centerY - finalHeight / 2, 
                finalWidth, finalHeight));
            img_CardPicture.Source = newImage;
        }


        private void RefreshCardType()
        {

        }


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

            newCard.name = tb_Name.Text;

            newCard.power = tb_Power.Text;
            newCard.toughness = tb_Toughness.Text;

            newCard.rarity = comboBoxItemToRarity[cmb_Rarity.SelectedItem as ComboBoxItem];

            newCard.subtype = tb_Subtype.Text;

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

        // TODO Add print to 'save' error window
        // TODO Load state from previously suspended application
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


        //-------------------------//
        // Conversion Dictionaries //
        //-------------------------//

        private Dictionary<ComboBoxItem, JsonCard.Rarity> comboBoxItemToRarity;
        private Dictionary<JsonCard.Rarity, ComboBoxItem> rarityToComboBoxItem;

        private Dictionary<ComboBoxItem, JsonCard.Type> comboBoxItemToType;
        private Dictionary<JsonCard.Type, ComboBoxItem> typeToComboBoxItem;

        private Dictionary<ComboBoxItem, string> comboBoxItemToString;
        private Dictionary<string, ComboBoxItem> stringToComboBoxItem;

        private Dictionary<ComboBoxItem, JsonCard.Color> comboBoxItemToColor;
        private Dictionary<JsonCard.Color, ComboBoxItem> colorToComboBoxItem;

        //----------------//
        // Event Handlers //
        //----------------//

        private void b_Browse_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
