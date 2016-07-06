//using Microsoft.Win32;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.IO;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Runtime.Serialization.Json;

//namespace MTGSetGenerator
//{
//    /// <summary>
//    /// Interaction logic for GenerateCard.xaml
//    /// </summary>
//    public partial class GenerateCard : UserControl
//    {
//        /// <summary>
//        /// Instantiates a GenerateCard user control.
//        /// </summary>
//        /// <param name="controlToReturnTo">The control to return to if the cancel or save buttons are pressed.</param>
//        /// <param name="currentSet">The current set for this card.</param>
//        public GenerateCard(ContentControl controlToReturnTo, JsonSet currentSet)
//        {
//            InitializeComponent();
//            componentsInitialized = true;

//            this.controlToReturnTo = controlToReturnTo;
//            this.currentSet = currentSet;

//            InitConversionDictionaries();

//            RestoreUIToDefault();
//        }

//        /// <summary>
//        /// Initializes the conversion dictionaries.
//        /// </summary>
//        private void InitConversionDictionaries()
//        {
//            rarityComboBoxMap = new MultiwayMap<ComboBoxItem, JsonCard.Rarity>(
//                new Dictionary<ComboBoxItem, JsonCard.Rarity>()
//                {
//                    { cbi_Common, JsonCard.Rarity.COMMON },
//                    { cbi_Uncommon, JsonCard.Rarity.UNCOMMON },
//                    { cbi_Rare, JsonCard.Rarity.RARE },
//                    { cbi_Mythic, JsonCard.Rarity.MYTHIC }
//                }
//            );

//            typeComboBoxMap = new MultiwayMap<ComboBoxItem, JsonCard.Type>(
//                new Dictionary<ComboBoxItem, JsonCard.Type>()
//                {
//                    { cbi_Creature, JsonCard.Type.CREATURE },
//                    { cbi_Instant, JsonCard.Type.INSTANT },
//                    { cbi_Sorcery, JsonCard.Type.SORCERY },
//                    { cbi_Planeswalker, JsonCard.Type.PLANESWALKER },
//                    { cbi_Enchantment, JsonCard.Type.ENCHANTMENT },
//                    { cbi_Artifact, JsonCard.Type.ARTIFACT },
//                    { cbi_Land, JsonCard.Type.LAND }
//                }
//            );

//            typeStringMap = new MultiwayMap<string, JsonCard.Type>(
//                new Dictionary<string, JsonCard.Type>()
//                {
//                    { "Creature", JsonCard.Type.CREATURE },
//                    { "Instant", JsonCard.Type.INSTANT },
//                    { "Sorcery", JsonCard.Type.SORCERY },
//                    { "Planeswalker", JsonCard.Type.PLANESWALKER },
//                    { "Enchantment", JsonCard.Type.ENCHANTMENT },
//                    { "Artifact", JsonCard.Type.ARTIFACT },
//                    { "Land", JsonCard.Type.LAND }
//                }
//            );

//            colorComboBoxMap = new MultiwayMap<ComboBoxItem, JsonCard.Color>(
//                new Dictionary<ComboBoxItem, JsonCard.Color>
//                {
//                    { cbi_Colorless, JsonCard.Color.COLORLESS },
//                    { cbi_White, JsonCard.Color.WHITE },
//                    { cbi_Blue, JsonCard.Color.BLUE },
//                    { cbi_Black, JsonCard.Color.BLACK },
//                    { cbi_Red, JsonCard.Color.RED },
//                    { cbi_Green, JsonCard.Color.GREEN },
//                    { cbi_Gold, JsonCard.Color.GOLD }
//                }
//            );
//        }


//        //----------------//
//        // Parent Objects //
//        //----------------//

//        private ContentControl controlToReturnTo;
//        private JsonSet currentSet;
//        private bool componentsInitialized = false;

//        //---------------//
//        // UI Management //
//        //---------------//

//        /// <summary>
//        /// Resets all UI elements to their default values.
//        /// </summary>
//        private void RestoreUIToDefault()
//        {
//            tb_Name.Text = "";
            
//            tb_Cost.Text = "";
//            tb_Cmc.Text = "";
//            tb_Cmc.IsEnabled = false;
//            cb_InferCMC.IsChecked = true;

//            tb_Supertype.Text = "";
//            cmb_Type.SelectedIndex = 0;
//            tb_Subtype.Text = "";

//            cmb_Rarity.SelectedIndex = 0;

//            tbl_ImageFilename.Text = "";

//            cmb_Color.SelectedIndex = 0;

//            ResetPowerToughnessAndLoyaltyTextBoxes();
//            InferCmc();
//            InferColor();

//            ResetCardPreview();
//        }


//        /// <summary>
//        /// Resets and enables the power, toughness, and loyalty text boxes as necessary.
//        /// </summary>
//        private void ResetPowerToughnessAndLoyaltyTextBoxes()
//        {
//            // Everything is on by default
//            tb_Power.IsEnabled = true;
//            tb_Toughness.IsEnabled = true;
//            tb_Loyalty.IsEnabled = true;

//            // If we're not a creature, turn off P/T
//            if (cmb_Type.SelectedItem != cbi_Creature)
//            {
//                tb_Power.Text = "";
//                tb_Toughness.Text = "";
//                tb_Power.IsEnabled = false;
//                tb_Toughness.IsEnabled = false;
//            }

//            // If we're not a planeswalker, turn off loyalty
//            if (cmb_Type.SelectedItem != cbi_Planeswalker)
//            {
//                tb_Loyalty.Text = "";
//                tb_Loyalty.IsEnabled = false;
//            }
//        }

//        /// <summary>
//        /// Infers the card's converted mana cost from its casting cost.
//        /// </summary>
//        /// <returns>The card's inferred cmc.</returns>
//        public int InferCmc()
//        {
//            // TODO implement InferCmc()
//            return 0;
//        }

//        /// <summary>
//        /// Infers the card's color from its casting cost.
//        /// </summary>
//        /// <returns>The card's inferred color.</returns>
//        public JsonCard.Color InferColor()
//        {
//            // TODO implement InferColor()
//            return JsonCard.Color.COLORLESS;
//        }

//        /// <summary>
//        /// Opens a dialog for the user to select a card image from.
//        /// </summary>
//        public void BrowseForCardImage()
//        {
//            // Create the select file dialog
//            OpenFileDialog dlg = new OpenFileDialog();
//            dlg.DefaultExt = ".png";
//            dlg.Filter = "PNG Files (.png)|*.png|GIF Files (*.gif)|*.gif|JPEG Files (*.jpg, *.jpeg)|*.jpg; *.jpeg";

//            bool? result = dlg.ShowDialog();
//            if (result == true)
//            {
//                // Set the image
//                BitmapImage image = new BitmapImage(new Uri(dlg.FileName));
//                cardImage = CropImageForCardPreview(image);
//                img_CardPicture.Source = cardImage;

//                // Set the filename on the UI
//                tbl_ImageFilename.Text = Path.GetFileName(dlg.FileName);
//            }
//        }


        

//        //----------------//
//        // Event Handlers //
//        //----------------//

//        private void tb_Supertype_TextChanged(object sender, TextChangedEventArgs e)
//        {
//            ResetCardPreviewType();
//        }

//        private void cmb_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            // Make sure the components have been initialized
//            if (!componentsInitialized)
//            {
//                return;
//            }

//            ResetPowerToughnessAndLoyaltyTextBoxes();
//            ResetCardPreviewType();
//        }

//        private void tb_Subtype_TextChanged(object sender, TextChangedEventArgs e)
//        {
//            ResetCardPreviewType();
//        }

//        private void tb_Text_TextChanged(object sender, TextChangedEventArgs e)
//        {
//            ResetCardText();
//        }

//        private void b_Browse_Click(object sender, RoutedEventArgs e)
//        {
//            BrowseForCardImage();
//        }

//        private void b_Help_Click(object sender, RoutedEventArgs e)
//        {
//            // Render the image
//            double imageScale = 200.0;
//            BitmapSource img = ImageUtilities.CaptureScreen(cnvs_CardPreview, imageScale, imageScale);

//            PngBitmapEncoder pngImage = new PngBitmapEncoder();
//            pngImage.Frames.Add(BitmapFrame.Create(img));
//            using (Stream fileStream = File.Create(@"C:\Users\ad3vfw\Documents\Visual Studio 2013\Projects\MTGSetGenerator\MTGSetGenerator\MTGCardGenerator\images\test\test.png"))
//            {
//                pngImage.Save(fileStream);
//            }
//        }

//        private void b_Clear_Click(object sender, RoutedEventArgs e)
//        {

//        }

//        private void b_Cancel_Click(object sender, RoutedEventArgs e)
//        {
//            MessageBoxResult result = MessageBox.Show("Cancel card generation and discard changes?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
//            if (result == MessageBoxResult.Yes)
//            {
//                Window.GetWindow(this).Content = controlToReturnTo;
//            }
//        }

//        private void b_Save_Click(object sender, RoutedEventArgs e)
//        {
//            // TODO move this logic into a separate function
//            // TODO check all fields before creating a new json card

//            // Make new card
//            JsonCard newCard = new JsonCard();
//            List<string> invalidLog = new List<string>();

//            newCard.name = tb_Name.Text;

//            newCard.power = tb_Power.Text;
//            newCard.toughness = tb_Toughness.Text;

//            newCard.rarity = rarityComboBoxMap[cmb_Rarity.SelectedItem as ComboBoxItem];

//            newCard.subtype = tb_Subtype.Text;

//            if (invalidLog.Count() > 0)
//            {
//                MessageBox.Show(string.Format(
//                    "The following errors have occured in the card submission: \n-{0}", String.Join("\n-", invalidLog.ToArray())),
//                    "Card Save Failed", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//            else
//            {
//                // Add card to collection manager
//                CardCollectionManager.AddCard(newCard);
//            }
//        }


//        //--------------------//
//        // Processing Helpers //
//        //--------------------//

//        /// <summary>
//        /// Safely parses text which represents an integer. 
//        /// </summary>
//        /// <param name="intText"></param>
//        /// <returns></returns>

//        // TODO Add print to 'save' error window
//        // TODO Load state from previously suspended application
//        private int TextToInt(string intText, List<string> invalidLog)
//        {
//            int j;
//            if (int.TryParse(intText, out j))
//            {
//                return j;
//            }
//            else
//            {
//                invalidLog.Add(String.Format("\"{0}\" cannot be converted to an integer", intText));
//                return -1;
//            }
                
//        }


//        //-------------------------//
//        // Conversion Dictionaries //
//        //-------------------------//

//        private MultiwayMap<ComboBoxItem, JsonCard.Rarity> rarityComboBoxMap;
//        private MultiwayMap<ComboBoxItem, JsonCard.Type> typeComboBoxMap;
//        private MultiwayMap<string, JsonCard.Type> typeStringMap;
//        private MultiwayMap<ComboBoxItem, JsonCard.Color> colorComboBoxMap;
//    }
//}
