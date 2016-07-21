using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MTGSetGenerator
{
    /// <summary>
    /// Interaction logic for AddNewCardInitialPage.xaml
    /// </summary>
    public partial class AddNewCardInitialPage : UserControl, IAddNewCardPage
    {
        public AddNewCardInitialPage(CardPreview preview)
        {
            InitializeComponent();
            componentsInitialized = true;

            Preview = preview;

            ResetToDefault();
            RefreshCardPreview();
        }


        //------//
        // Data //
        //------//

        public bool componentsInitialized = false;

        public string CardName { get { return tb_Name.Text; } }
        
        public CardForms.Label CardForm { get { return GetSelectedCardForm(); } }

        public BitmapImage CardImage { get; private set; }
        public string CardImageName { get { return tbl_ImageFilename.Text; } }

        private string cardImageFullPath;
        public string CardImageFullPath
        {
            get { return cardImageFullPath; }
            private set
            {
                cardImageFullPath = value;
                try
                {
                    tbl_ImageFilename.Text = Path.GetFileName(CardImageFullPath);
                }
                catch (ArgumentException)
                {
                    tbl_ImageFilename.Text = "";
                }
            }
        }

        public CardPreview Preview { get; private set; }



        //---------//
        // Utility //
        //---------//

        /// <summary>
        /// Resets the UI to its default state.
        /// </summary>
        public void ResetToDefault()
        {
            tb_Name.Text = "";
            uc_CardNameError.Visibility = Visibility.Collapsed;

            rb_Standard.IsChecked = true;

            CardImage = null;
            CardImageFullPath = "";
            uc_CardImageError.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Refreshes the card preview.
        /// </summary>
        public void RefreshCardPreview()
        {
            if (!componentsInitialized)
            {
                return;
            }

            Preview.SetCardName(CardName);
            // TODO update SetCardFrame() call once tokens and transform cards are supported
            Preview.SetCardFrame(CardFrames.Label.SILVER, CardTypes.Label.INSTANT);
            Preview.SetCardPicture(CardImage);
        }

        /// <summary>
        /// Checks the page for errors and displays them on the UI.
        /// </summary>
        /// <returns>True if the page has errors; false otherwise.</returns>
        public bool CheckForErrors()
        {
            bool hasErrors = false;

            // Check name
            uc_CardNameError.Visibility = Visibility.Collapsed;
            if (CardName == "")
            {
                hasErrors = true;
                uc_CardNameError.Visibility = Visibility.Visible;
                uc_CardNameError.ErrorMessage = "Please supply a name.";
            }

            // Check image
            uc_CardImageError.Visibility = Visibility.Collapsed;
            if (CardImage == null)
            {
                hasErrors = true;
                uc_CardImageError.Visibility = Visibility.Visible;
                uc_CardImageError.ErrorMessage = "Please supply an image.";
            }

            return hasErrors;
        }

        /// <summary>
        /// Generates and returns the next add new card page.
        /// </summary>
        /// <param name="isFinalPage">True if the next page is the final page; false otherwise.</param>
        /// <returns>The next add new card page.</returns>
        public IAddNewCardPage GenerateNextAddNewCardPage(out bool isFinalPage)
        {
            isFinalPage = false;
            return new AddNewCardCorePage(Preview, this);
        }

        /// <summary>
        /// Opens a dialog for the user to select a card image from.
        /// </summary>
        private void BrowseForCardImage()
        {
            // Create the select file dialog
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG Files (.png)|*.png|GIF Files (*.gif)|*.gif|JPEG Files (*.jpg, *.jpeg)|*.jpg; *.jpeg";

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                // Check the image
                CardImageFullPath = dlg.FileName;
                CardImage = new BitmapImage(new Uri(dlg.FileName));

                if (CardImage == null)
                {
                    throw new InvalidOperationException(string.Format(
                        "Image at path '{0}' could not be loaded.", dlg.FileName));
                }
            }
        }

        /// <summary>
        /// Returns the currently selected card form.
        /// </summary>
        /// <returns>The currently selected card form.</returns>
        private CardForms.Label GetSelectedCardForm()
        {
            if (rb_Standard.IsChecked == true)
            {
                return cardFormRadioButtonsToLabel[rb_Standard];
            }

            if (rb_Token.IsChecked == true)
            {
                return cardFormRadioButtonsToLabel[rb_Token];
            }

            if (rb_Transform.IsChecked == true)
            {
                return cardFormRadioButtonsToLabel[rb_Transform];
            }

            return CardForms.Label.NONE;
        }


        //------//
        // Maps //
        //------//
        /// <summary>
        /// Initialize the control to label maps.
        /// </summary>
        private void InitializeMaps()
        {
            cardFormRadioButtonsToLabel = new MultiwayMap<RadioButton, CardForms.Label>(new Dictionary<RadioButton, CardForms.Label>()
                {
                    { rb_Standard, CardForms.Label.STANDARD },
                    { rb_Token, CardForms.Label.TOKEN },
                    { rb_Transform, CardForms.Label.TRANSFORM },
                });
        }

        private MultiwayMap<RadioButton, CardForms.Label> cardFormRadioButtonsToLabel;


        //----------------//
        // Event Handlers //
        //----------------//

        private void tb_UiElement_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshCardPreview();
        }

        private void rb_UiElement_Checked(object sender, RoutedEventArgs e)
        {
            // TODO implement rb_UiElement_Checked() once tokens and transform cards are supported
            RefreshCardPreview();
        }

        private void b_Browse_Click(object sender, RoutedEventArgs e)
        {
            BrowseForCardImage();
            RefreshCardPreview();
        }
    }
}
