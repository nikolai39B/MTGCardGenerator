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
    /// Interaction logic for CardPreview.xaml
    /// </summary>
    public partial class CardPreview : UserControl
    {
        public CardPreview()
        {
            InitializeComponent();
            componentsInitialized = true;

            try
            {
                // Set data fields
                cardImageHeight = img_CardPicture.Height;
                cardImageWidth = img_CardPicture.Width;

                // Set default resources
                defaultCardPicture = FindResource("img_GrayFill") as BitmapImage;
                defaultCardFrame = CardFrames.Frames[CardFrames.Label.GOLD].DefaultFrame;
                defaultSetIcon = FindResource("img_SetIconBackground") as BitmapImage;

            
                ResetCardPreview();
            }
            catch (NullReferenceException)
            {
                // TODO handle
                // NOTE this catch is here so that CardPreview behaves in the design preview of other controls
            }
        }

        //-------------------//
        // Preview Resources //
        //-------------------//

        private double cardImageHeight;
        private double cardImageWidth;

        private BitmapImage defaultCardPicture;
        private BitmapImage defaultCardFrame;
        private BitmapImage defaultSetIcon;

        private CroppedBitmap cardImage = null;      
  

        //--------------------//
        // Preview Management //
        //--------------------//

        private bool componentsInitialized = false;

        /// <summary>
        /// Resets all elements that compose the card preview to their default values.
        /// </summary>
        public void ResetCardPreview()
        {
            // Make sure the components have been initialized
            if (!componentsInitialized)
            {
                return;
            }

            SetCardPicture(defaultCardPicture);
            SetCardFrame(CardFrames.Label.GOLD, CardTypes.Label.INSTANT);

            SetCardName("");
            SetCardCost(new List<CostIcons.Label>());

            SetCardType(CardTypes.Label.NONE);
            SetCardSetIcon(defaultSetIcon);

            SetCardText("");

            // TODO reset p/t and loyalty
        }

        /// <summary>
        /// Sets the card picture on the card preview.
        /// </summary>
        /// <param name="cardPicture">The image to set for the card.</param>
        public void SetCardPicture(BitmapImage cardPicture)
        {
            // If the card picture was null, set it to the default image
            if (cardPicture == null)
            {
                cardPicture = defaultCardPicture;
            }
            
            CroppedBitmap croppedPicture = CropImageForCardPreview(cardPicture);
            cardImage = croppedPicture;

            img_CardPicture.Source = croppedPicture;
        }

        /// <summary>
        /// Sets the card frame on the card preview.
        /// </summary>
        /// <param name="color">The color of the card frame.</param>
        /// <param name="type">The type of the card for the card frame.</param>
        public void SetCardFrame(CardFrames.Label color, CardTypes.Label type)
        {
            // Get the class of frames
            CardFrames.CardFrame frame = CardFrames.Frames[color];

            // Get the specific frame
            BitmapImage frameImage = frame.DefaultFrame;
            if (type == CardTypes.Label.CREATURE)
            {
                frameImage = frame.CreatureFrame;
            }
            else if (type == CardTypes.Label.PLANESWALKER)
            {
                frameImage = frame.PlaneswalkerFrame;
            }

            // Make sure the frame isn't null
            if (frameImage == null)
            {
                frameImage = defaultCardFrame;
            }

            img_CardFrame.Source = frameImage;
        }

        /// <summary>
        /// Sets the card name on the card preview.
        /// </summary>
        /// <param name="name">The name to set for the card.</param>
        public void SetCardName(string name)
        {
            tbl_CardName.Text = name;
        }

        /// <summary>
        /// Sets the card cost on the card preview.
        /// </summary>
        /// <param name="costIcons">The cost to set for the card.</param>
        public void SetCardCost(List<CostIcons.Label> costIcons)
        {

        }

        /// <summary>
        /// Resets the card type text on the card preview.
        /// </summary>
        /// <param name="baseType">The base type of the card.</param>
        /// <param name="isLegendary">Whether or not the card is legendary.</param>
        /// <param name="isTribal">Whether or not the card is tribal.</param>
        /// <param name="supertype">The supertype for the card.</param>
        /// <param name="auxType">The auxilary type for the card.</param>
        /// <param name="subtype">The subtype for the card.</param>
        public void SetCardType(CardTypes.Label baseType, bool isLegendary = false, bool isTribal = false, string supertype = "", CardTypes.Label auxType = CardTypes.Label.NONE, string subtype = "")
        {
            List<string> cardTypes = new List<string>();

            // Add the legendary and tribal strings if necessary
            if (isLegendary)
            {
                cardTypes.Add("Legendary");
            }
            if (isTribal)
            {
                cardTypes.Add("Tribal");
            }

            // Add the supertype and aux type
            cardTypes.Add(supertype);
            cardTypes.Add(CardTypes.Types[auxType].TextRepresentation);

            // Create the string for the card type
            string cardType = "";
            foreach (var str in cardTypes)
            {
                if (str != "")
                {
                    cardType += str + " ";
                }
            }

            // Add the base type
            cardType += CardTypes.Types[baseType].TextRepresentation;

            // Add the subtype if necessary
            if (subtype != "")
            {
                cardType += " - " + subtype;
            }

            tbl_CardType.Text = cardType;
        }

        /// <summary>
        /// Sets the card set icon on the card preview.
        /// </summary>
        /// <param name="setIcon">The set icon for the card.</param>
        public void SetCardSetIcon(BitmapImage setIcon)
        {
            img_CardSetIcon.Source = setIcon;
        }

        /// <summary>
        /// Resets the card text on the card preview.
        /// </summary>
        /// <param name="text">The text to set for the card.</param>
        public void SetCardText(string text)
        {
            // Make sure the components have been initialized
            if (!componentsInitialized)
            {
                return;
            }

            tbl_CardText.Text = "";
            tbl_CardText.Inlines.Clear();

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


        //-----------//
        // Utilities //
        //-----------//

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
    }
}
