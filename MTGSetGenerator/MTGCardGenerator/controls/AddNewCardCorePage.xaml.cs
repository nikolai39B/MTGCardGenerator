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
    /// Interaction logic for AddNewCardCorePage.xaml
    /// </summary>
    public partial class AddNewCardCorePage : UserControl, IAddNewCardPage
    {
        public AddNewCardCorePage(CardPreview preview, AddNewCardInitialPage initialPage)
        {
            InitializeComponent();
            componentsInitialized = true;

            Preview = preview;
            InitialPage = initialPage;

            InitializeMaps();

            ResetToDefault();
            RefreshCardPreview();
            //InvokeCardDetailsChanged();
        }

        //--------------------//
        // Data and Notifiers //
        //--------------------//

        private bool componentsInitialized;

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

        public CardPreview Preview { get; private set; }
        public AddNewCardInitialPage InitialPage { get; private set; }

        //---------//
        // Utility //
        //---------//

        /// <summary>
        /// Resets the UI to its default state.
        /// </summary>
        public void ResetToDefault()
        {
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

            Preview.SetCardFrame(FrameColor, BaseType);

            Preview.SetCardCost(Cost);

            Preview.SetCardType(BaseType, IsLegendary, IsTribal, Supertype, AuxType, Subtype);
        }

        /// <summary>
        /// Generates and returns the next add new card page.
        /// </summary>
        /// <param name="isFinalPage">True if the next page is the final page; false otherwise.</param>
        /// <returns>The next add new card page.</returns>
        public IAddNewCardPage GenerateNextAddNewCardPage(out bool isFinalPage)
        {
            isFinalPage = true;
            return new AddNewCardDetailsPage(Preview, InitialPage, this);
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
                CostIcons.Label costIcon;

                // If the next character isn't a open brace, try to parse the next single letter
                if (costString[0] != '{')
                {
                    costIcon = (from pair in CostIcons.Icons
                                where pair.Value.TextRepresentation == costString[0].ToString()
                                select pair.Key).FirstOrDefault();

                    // Trim the character off
                    costString = costString.Substring(1);
                }

                // Otherwise, parse the group
                else
                {
                    int indexOfEndBrace = costString.IndexOf('}');

                    // If there is no ending brace, the cost is invalid
                    if (indexOfEndBrace == -1)
                    {
                        return new List<CostIcons.Label>();
                    }

                    // Identify the cost icon
                    string costIconString = costString.Substring(1, indexOfEndBrace - 1);
                    costIcon = (from pair in CostIcons.Icons
                                where pair.Value.TextRepresentation == costIconString
                                select pair.Key).FirstOrDefault();

                    // Trim the string off
                    costString = costString.Substring(indexOfEndBrace + 1);
                }

                // If the cost icon string is invalid, the cost is invalid
                if (costIcon == CostIcons.Label.NONE)
                {
                    return new List<CostIcons.Label>();
                }

                // Otherwise, add the icon and continue
                costIcons.Add(costIcon);
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
            List<CardColors.Label> colors = new List<CardColors.Label>(Color);

            // Remove all colorless colors
            colors.RemoveAll(x => x == CardColors.Label.COLORLESS);
            
            // If there are no colors, return SILVER
            if (colors.Count == 0)
            {
                return CardFrames.Label.SILVER;
            }

            // If there is just one color, return that color
            if (colors.Count == 1)
            {
                return CardColors.Colors[colors[0]].CardFrame;
            }

            // Otherwise, return GOLD
            return CardFrames.Label.GOLD;
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
            RefreshCardPreview();
            //InvokeCardDetailsChanged();
        }

        private void cb_UiElement_Click(object sender, RoutedEventArgs e)
        {
            RefreshCardPreview();
            //InvokeCardDetailsChanged();
        }

        private void cmb_UiElement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshCardPreview();
            //InvokeCardDetailsChanged();
        }

        private void tb_Cost_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbl_Cmc.Text = CalculateCmc().ToString();
            RefreshCardPreview();
            //InvokeCardDetailsChanged();
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

            RefreshCardPreview();
            //InvokeCardDetailsChanged();
        }

        //private void b_Browse_Click(object sender, RoutedEventArgs e)
        //{
        //    BrowseForCardImage();
        //    RefreshCardPreview();
        //    //InvokeCardDetailsChanged();
        //}
    }
}
