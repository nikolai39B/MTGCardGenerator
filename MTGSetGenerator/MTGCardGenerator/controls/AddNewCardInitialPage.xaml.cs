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
        public AddNewCardInitialPage()
        {
            InitializeComponent();
            componentsInitialized = true;

            InitializeMaps();

            ResetToDefault();
            InvokeCardDetailsChanged();

            // TODO update textblocks on the UI for cmc and image name
        }

        //--------------------//
        // Data and Notifiers //
        //--------------------//

        private bool componentsInitialized;
        
        public event EventHandler CardDetailsChanged;
        /// <summary>
        /// Safely invoke the CardDetailsChanged event, and only invoke if all components are initialized.
        /// </summary>
        private void InvokeCardDetailsChanged()
        {
            if (componentsInitialized)
            {
                CardDetailsChanged.SafeInvoke(this, EventArgs.Empty);
            }
        }

        public string CardName { get { return tb_Name.Text; } }

        public List<CostIcons.Label> Cost { get { return ParseCastingCost(); } }
        public int Cmc { get { return int.Parse(tbl_Cmc.Text); } }

        public bool ColorIsInferred { get { return cb_InferColor.IsChecked == true; } }
        public List<CardColors.Label> Color { get { return CalculateColors(); } }
        public CardFrames.Label FrameColor { get { return CalculateFrameColor(); } }

        public bool IsLegendary { get { return cb_Legendary.IsChecked == true; } }
        public bool IsTribal { get { return cb_Tribal.IsChecked == true; } }
        public string Supertype { get { return tb_Supertype.Text; } }

        public CardTypes.Label AuxType { get { return auxTypeComboBoxItemToLabel[cmb_AuxType.SelectedItem as ComboBoxItem]; } }
        public CardTypes.Label BaseType { get { return baseTypeComboBoxItemToLabel[cmb_BaseType.SelectedItem as ComboBoxItem]; } }
        public string Subtype { get { return tb_Subtype.Text; } }

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

        //---------//
        // Utility //
        //---------//

        /// <summary>
        /// Resets the UI to its default state.
        /// </summary>
        public void ResetToDefault()
        {
            tb_Name.Text = "";
            tb_Cost.Text = "";
            tbl_Cmc.Text = "0";

            cb_InferColor.IsChecked = true;
            foreach (var cb in cardColorCheckBoxesToLabel.KeyOnes)
            {
                cb.IsChecked = false;
                cb.IsEnabled = false;
            }

            cb_Legendary.IsChecked = false;
            cb_Tribal.IsChecked = false;
            tb_Supertype.Text = "";

            cmb_AuxType.SelectedIndex = 0;
            cmb_BaseType.SelectedIndex = 0;
            tb_Subtype.Text = "";

            CardImage = null;
            CardImageFullPath = "";
        }

        /// <summary>
        /// Parses the card's casting cost from the user input.
        /// </summary>
        /// <returns>The casting cost of the card.</returns>
        private List<CostIcons.Label> ParseCastingCost()
        {
            List<CostIcons.Label> costIcons = new List<CostIcons.Label>();

            string costString = tb_Cost.Text;

            while (costString.Length > 0)
            {
                // If the next character isn't a open brace, the cost is invalid
                if (costString[0] != '{')
                {
                    return new List<CostIcons.Label>();
                }

                int indexOfEndBrace = costString.IndexOf('}');

                // If there is no ending brace, the cost is invalid
                if (indexOfEndBrace == -1)
                {
                    return new List<CostIcons.Label>();
                }

                // Identify the cost icon
                string costIconString = costString.Substring(1, indexOfEndBrace - 1);
                CostIcons.Label costIcon = (from pair in CostIcons.Icons
                                            where pair.Value.TextRepresentation == costIconString
                                            select pair.Key).FirstOrDefault();
                
                // If the cost icon string is invalid, the cost is invalid
                if (costIcon == CostIcons.Label.NONE)
                {
                    return new List<CostIcons.Label>();
                }

                // Otherwise, add the icon and continue
                costIcons.Add(costIcon);
                costString = costString.Substring(indexOfEndBrace + 1);
            }

            return costIcons;
        }

        /// <summary>
        /// Calculates the card's cmc from its cost.
        /// </summary>
        /// <returns>The cmc of the card.</returns>
        private int CalculateCmc()
        {
            int cmc = 0;
            foreach (var icon in Cost)
            {
                cmc += CostIcons.Icons[icon].CmcContribution;
            }

            return cmc;
        }

        /// <summary>
        /// Calculates the card's colors either from the card's casting cost if infer color is selected, or from the color checkboxes otherwise.
        /// </summary>
        /// <returns>The color of the card.</returns>
        private List<CardColors.Label> CalculateColors()
        {
            // Infer the color from the cost
            if (cb_InferColor.IsChecked == true)
            {
                List<CardColors.Label> colors = new List<CardColors.Label>();

                // Loop through each icon in the card's cost
                foreach (var iconLabel in Cost)
                {
                    CostIcons.CostIcon icon = CostIcons.Icons[iconLabel];

                    // Add the colors for this icon
                    if (icon.ColorContribution != CardColors.Label.NONE && !colors.Contains(icon.ColorContribution))
                    {
                        colors.Add(icon.ColorContribution);
                    }
                    if (icon.SecondaryColorContribution != CardColors.Label.NONE && !colors.Contains(icon.SecondaryColorContribution))
                    {
                        colors.Add(icon.SecondaryColorContribution);
                    }
                }

                return colors;
            }
            
            // Read the color manually
            else
            {
                return (from pair in cardColorCheckBoxesToLabel.KeyPairs
                        where pair.Item1.IsChecked == true
                        select pair.Item2
                        ).ToList();                
            }
        }

        /// <summary>
        /// Calculates the card's frame's color from the card's color.
        /// </summary>
        /// <returns>The color of the frame of the card.</returns>
        private CardFrames.Label CalculateFrameColor()
        {
            // If there are no colors, return SILVER
            if (Color.Count == 0)
            {
                return CardFrames.Label.SILVER;
            }

            // If there is just one color, return that color
            if (Color.Count == 1)
            {
                return CardColors.Colors[Color[0]].CardFrame;
            }

            // Otherwise, return GOLD
            return CardFrames.Label.GOLD;
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


        //------//
        // Maps //
        //------//
        /// <summary>
        /// Initialize the control to label maps.
        /// </summary>
        private void InitializeMaps()
        {
            auxTypeComboBoxItemToLabel = new MultiwayMap<ComboBoxItem, CardTypes.Label>(new Dictionary<ComboBoxItem, CardTypes.Label>()
                {
                    { cbi_AuxTypeBlank, CardTypes.Label.NONE },
                    { cbi_AuxTypeArtifact, CardTypes.Label.ARTIFACT },
                    { cbi_AuxTypeEnchantment, CardTypes.Label.ENCHANTMENT },
                    { cbi_AuxTypeLand, CardTypes.Label.LAND },
                });

            baseTypeComboBoxItemToLabel = new MultiwayMap<ComboBoxItem, CardTypes.Label>(new Dictionary<ComboBoxItem, CardTypes.Label>()
                {
                    { cbi_BaseTypeCreature, CardTypes.Label.CREATURE },
                    { cbi_BaseTypeArtifact, CardTypes.Label.ARTIFACT },
                    { cbi_BaseTypeEnchantment, CardTypes.Label.ENCHANTMENT },
                    { cbi_BaseTypeInstant, CardTypes.Label.INSTANT },
                    { cbi_BaseTypeLand, CardTypes.Label.LAND },
                    { cbi_BaseTypePlaneswalker, CardTypes.Label.PLANESWALKER },
                    { cbi_BaseTypeSorcery, CardTypes.Label.SORCERY },
                });

            cardColorCheckBoxesToLabel = new MultiwayMap<CheckBox, CardColors.Label>(new Dictionary<CheckBox, CardColors.Label>()
                {
                    { cb_White, CardColors.Label.WHITE },
                    { cb_Blue, CardColors.Label.BLUE },
                    { cb_Black, CardColors.Label.BLACK },
                    { cb_Red, CardColors.Label.RED },
                    { cb_Green, CardColors.Label.GREEN },
                });
        }

        private MultiwayMap<ComboBoxItem, CardTypes.Label> auxTypeComboBoxItemToLabel;
        private MultiwayMap<ComboBoxItem, CardTypes.Label> baseTypeComboBoxItemToLabel;
        private MultiwayMap<CheckBox, CardColors.Label> cardColorCheckBoxesToLabel;


        //----------------//
        // Event Handlers //
        //----------------//

        private void tb_UiElement_TextChanged(object sender, TextChangedEventArgs e)
        {
            InvokeCardDetailsChanged();
        }

        private void cb_UiElement_Click(object sender, RoutedEventArgs e)
        {
            InvokeCardDetailsChanged();
        }

        private void cmb_UiElement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InvokeCardDetailsChanged();
        }

        private void tb_Cost_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbl_Cmc.Text = CalculateCmc().ToString();
            InvokeCardDetailsChanged();
        }

        private void cb_InferColor_Click(object sender, RoutedEventArgs e)
        {
            bool enableColorOverride = cb_InferColor.IsChecked == false;

            // Toggle the enable status of the color override checkboxes
            foreach (var cb in cardColorCheckBoxesToLabel.KeyOnes)
            {
                cb.IsChecked = false;
                cb.IsEnabled = enableColorOverride;
            }
        }

        private void b_Browse_Click(object sender, RoutedEventArgs e)
        {
            BrowseForCardImage();
            InvokeCardDetailsChanged();
        }
    }
}
