using Microsoft.Win32;
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
            componentsInitialized = true;

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
            rarityComboBoxMap = new MultiwayMap<ComboBoxItem, JsonCard.Rarity>(
                new Dictionary<ComboBoxItem, JsonCard.Rarity>()
                {
                    { cbi_Common, JsonCard.Rarity.COMMON },
                    { cbi_Uncommon, JsonCard.Rarity.UNCOMMON },
                    { cbi_Rare, JsonCard.Rarity.RARE },
                    { cbi_Mythic, JsonCard.Rarity.MYTHIC }
                }
            );

            typeComboBoxMap = new MultiwayMap<ComboBoxItem, JsonCard.Type>(
                new Dictionary<ComboBoxItem, JsonCard.Type>()
                {
                    { cbi_Creature, JsonCard.Type.CREATURE },
                    { cbi_Instant, JsonCard.Type.INSTANT },
                    { cbi_Sorcery, JsonCard.Type.SORCERY },
                    { cbi_Planeswalker, JsonCard.Type.PLANESWALKER },
                    { cbi_Enchantment, JsonCard.Type.ENCHANTMENT },
                    { cbi_Artifact, JsonCard.Type.ARTIFACT },
                    { cbi_Land, JsonCard.Type.LAND }
                }
            );

            typeStringMap = new MultiwayMap<string, JsonCard.Type>(
                new Dictionary<string, JsonCard.Type>()
                {
                    { "Creature", JsonCard.Type.CREATURE },
                    { "Instant", JsonCard.Type.INSTANT },
                    { "Sorcery", JsonCard.Type.SORCERY },
                    { "Planeswalker", JsonCard.Type.PLANESWALKER },
                    { "Enchantment", JsonCard.Type.ENCHANTMENT },
                    { "Artifact", JsonCard.Type.ARTIFACT },
                    { "Land", JsonCard.Type.LAND }
                }
            );

            colorComboBoxMap = new MultiwayMap<ComboBoxItem, JsonCard.Color>(
                new Dictionary<ComboBoxItem, JsonCard.Color>
                {
                    { cbi_Colorless, JsonCard.Color.COLORLESS },
                    { cbi_White, JsonCard.Color.WHITE },
                    { cbi_Blue, JsonCard.Color.BLUE },
                    { cbi_Black, JsonCard.Color.BLACK },
                    { cbi_Red, JsonCard.Color.RED },
                    { cbi_Green, JsonCard.Color.GREEN },
                    { cbi_Gold, JsonCard.Color.GOLD }
                }
            );
        }


        //----------------//
        // Parent Objects //
        //----------------//

        private ContentControl controlToReturnTo;
        private JsonSet currentSet;
        private bool componentsInitialized = false;

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

            RefreshPowerToughnessAndLoyaltyTextBoxes();
            InferCmc();
            InferColor();

            RefreshCardPreview();
        }


        /// <summary>
        /// Refreshes and enables the power, toughness, and loyalty text boxes as necessary.
        /// </summary>
        private void RefreshPowerToughnessAndLoyaltyTextBoxes()
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
        /// Infers the card's converted mana cost from its casting cost.
        /// </summary>
        /// <returns>The card's inferred cmc.</returns>
        public int InferCmc()
        {
            // TODO implement InferCmc()
            return 0;
        }

        /// <summary>
        /// Infers the card's color from its casting cost.
        /// </summary>
        /// <returns>The card's inferred color.</returns>
        public JsonCard.Color InferColor()
        {
            // TODO implement InferColor()
            return JsonCard.Color.COLORLESS;
        }

        /// <summary>
        /// Opens a dialog for the user to select a card image from.
        /// </summary>
        public void BrowseForCardImage()
        {
            // Create the select file dialog
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG Files (.png)|*.png|GIF Files (*.gif)|*.gif|JPEG Files (*.jpg, *.jpeg)|*.jpg; *.jpeg";

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                // Set the image
                BitmapImage image = new BitmapImage(new Uri(dlg.FileName));
                cardImage = CropImageForCardPreview(image);
                img_CardPicture.Source = cardImage;

                // Set the filename on the UI
                tbl_ImageFilename.Text = Path.GetFileName(dlg.FileName);
            }
        }


        //------------------//
        // Image Management //
        //------------------//

        private const int cardImageHeight = 125;
        private const int cardImageWidth = 170;
        private CroppedBitmap cardImage = null;

        /// <summary>
        /// Refreshes all elements that compose the card preview.
        /// </summary>
        private void RefreshCardPreview()
        {
            // TODO finish implementing RefreshCardImage()
            RefreshCardFrame();
            // Note: tbl_CardName.Text is dynamically bound, so no need to update
            RefreshCardCastingCostIcons();
            RefreshCardPreviewImage();
            RefreshCardPreviewType();
            RefreshCardPreviewSetIcon();
            RefreshCardText();
        }

        /// <summary>
        /// Refreshes the card frame on the card preview.
        /// </summary>
        private void RefreshCardFrame()
        {
            // TODO implement RefreshCardFrame()
        }

        /// <summary>
        /// Refreshes the casting cost icons on the card preview.
        /// </summary>
        private void RefreshCardCastingCostIcons()
        {
            // TODO implement RefreshCardCastingCostIcons()
        }

        /// <summary>
        /// Refreshes the card image on the card preview.
        /// </summary>
        private void RefreshCardPreviewImage()
        {            
            img_CardPicture.Source = cardImage;
        }

        /// <summary>
        /// Refreshes the card type text on the card preview.
        /// </summary>
        private void RefreshCardPreviewType()
        {
            // Make sure the components have been initialized
            if (!componentsInitialized)
            {
                return;
            }

            string cardType = "";

            // Add the pretype if necessary
            if (tb_Pretype.Text != "")
            {
                cardType += tb_Pretype.Text + " ";
            }

            cardType += typeStringMap[typeComboBoxMap[cmb_Type.SelectedItem as ComboBoxItem]];

            // Add the subtype if necessary
            if (tb_Subtype.Text != "")
            {
                cardType += " - " + tb_Subtype.Text;
            }

            tbl_CardType.Text = cardType;
        }

        /// <summary>
        /// Refreshes the set icon on the card preview.
        /// </summary>
        private void RefreshCardPreviewSetIcon()
        {
            // TODO add support for different rarities
            img_CardSetIcon.Source = currentSet.Icon;
        }

        /// <summary>
        /// Refreshes the card text on the card preview.
        /// </summary>
        private void RefreshCardText()
        {
            // Make sure the components have been initialized
            if (!componentsInitialized)
            {
                return;
            }

            tbl_CardText.Text = "";
            tbl_CardText.Inlines.Clear();

            string text = tb_Text.Text;
            string openFlavorTag = "<f>";
            string closeFlavorTag = "</f>";

            // Find the first flavor tag
            int currentIndex = 0;
            int indexOfFlavorTag = text.IndexOf(openFlavorTag);

            while (indexOfFlavorTag != -1)
            {
                // Add the text until the next flavor tag
                tbl_CardText.Inlines.Add(new Run(text.Substring(currentIndex, indexOfFlavorTag - currentIndex)));

                // If we don't find a close tag, the markup is invalid
                int indexOfCloseTag = text.IndexOf(closeFlavorTag, indexOfFlavorTag);
                if (indexOfCloseTag == -1)
                {
                    tbl_CardText.Inlines.Clear();
                    tbl_CardText.Inlines.Add(new Run("Invalid text markup."));
                    return;
                }

                // Otherwise, add this text as italics
                int substringStartIndex = indexOfFlavorTag + openFlavorTag.Length;
                string substring = text.Substring(substringStartIndex, indexOfCloseTag - substringStartIndex);
                tbl_CardText.Inlines.Add(new Run(substring) { FontStyle = FontStyles.Italic });

                // Look for a new flavor tag
                currentIndex = indexOfCloseTag + closeFlavorTag.Length;
                indexOfFlavorTag = text.IndexOf(openFlavorTag, currentIndex);
            }

            // Add the last bit of text
            tbl_CardText.Inlines.Add(new Run(text.Substring(currentIndex)));
        }

        /// <summary>
        /// Crops the given card image to fit neatly in the allocated space in the card preview.
        /// </summary>
        /// <param name="image">The image to crop.</param>
        /// <returns>The cropped image.</returns>
        private CroppedBitmap CropImageForCardPreview(BitmapImage image)
        {
            // Ensure we have an image to work with
            if (image == null)
            {
                return null;
            }
            
            // Compute the image ratios
            double requiredHeightOverWidth = ((double)cardImageHeight) / cardImageWidth;
            double currentHeightOverWidth = ((double)image.PixelHeight) / image.PixelWidth;

            int centerX = image.PixelWidth / 2;
            int centerY = image.PixelHeight / 2;

            // Calculate the final widths and heights for the image
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

            // Crop the image
            CroppedBitmap croppedImage = new CroppedBitmap(image, new Int32Rect(
                centerX - finalWidth / 2, 
                centerY - finalHeight / 2, 
                finalWidth, finalHeight));

            return croppedImage;
        }

        //----------------//
        // Event Handlers //
        //----------------//

        private void tb_Pretype_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshCardPreviewType();
        }

        private void cmb_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Make sure the components have been initialized
            if (!componentsInitialized)
            {
                return;
            }

            RefreshPowerToughnessAndLoyaltyTextBoxes();
            RefreshCardPreviewType();
        }

        private void tb_Subtype_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshCardPreviewType();
        }

        private void tb_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshCardText();
        }

        private void b_Browse_Click(object sender, RoutedEventArgs e)
        {
            BrowseForCardImage();
        }

        private void b_Help_Click(object sender, RoutedEventArgs e)
        {
            // Render the image
            double imageScale = 200.0;
            BitmapSource img = ImageUtilities.CaptureScreen(cnvs_CardPreview, imageScale, imageScale);

            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(img));
            using (Stream fileStream = File.Create(@"C:\Users\ad3vfw\Documents\Visual Studio 2013\Projects\MTGSetGenerator\MTGSetGenerator\MTGCardGenerator\images\test\test.png"))
            {
                pngImage.Save(fileStream);
            }
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
            // TODO move this logic into a separate function
            // TODO check all fields before creating a new json card

            // Make new card
            JsonCard newCard = new JsonCard();
            List<string> invalidLog = new List<string>();

            newCard.name = tb_Name.Text;

            newCard.power = tb_Power.Text;
            newCard.toughness = tb_Toughness.Text;

            newCard.rarity = rarityComboBoxMap[cmb_Rarity.SelectedItem as ComboBoxItem];

            newCard.subtype = tb_Subtype.Text;

            if (invalidLog.Count() > 0)
            {
                MessageBox.Show(string.Format(
                    "The following errors have occured in the card submission: \n-{0}", String.Join("\n-", invalidLog.ToArray())),
                    "Card Save Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // Add card to collection manager
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

        private MultiwayMap<ComboBoxItem, JsonCard.Rarity> rarityComboBoxMap;
        private MultiwayMap<ComboBoxItem, JsonCard.Type> typeComboBoxMap;
        private MultiwayMap<string, JsonCard.Type> typeStringMap;
        private MultiwayMap<ComboBoxItem, JsonCard.Color> colorComboBoxMap;
    }
}
