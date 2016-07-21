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
    /// Interaction logic for AddNewCardDetailsPage.xaml
    /// </summary>
    public partial class AddNewCardDetailsPage : UserControl, IAddNewCardPage
    {
        public AddNewCardDetailsPage(CardPreview preview, AddNewCardInitialPage initialPage, AddNewCardCorePage corePage)
        {
            InitializeComponent();
            componentsInitialized = true;

            Preview = preview;
            InitialPage = initialPage;
            CorePage = corePage;

            UpdateDisplayForCardType(corePage.BaseType);
        }

        //--------------------//
        // Data and Notifiers //
        //--------------------//

        public bool componentsInitialized = false;

        public string CardText { get { return tb_Text.Text; } }

        public string Power { get { return tb_Power.Text; } }
        public string Toughness { get { return tb_Toughness.Text; } }

        public string Ability1Cost { get { return (sp_PlaneswalkerAbilities.Children[0] as PlaneswalkerAbilityRow).AbilityCost; } }
        public string Ability1Text { get { return (sp_PlaneswalkerAbilities.Children[0] as PlaneswalkerAbilityRow).AbilityText; } }

        public string Ability2Cost { get { return (sp_PlaneswalkerAbilities.Children[1] as PlaneswalkerAbilityRow).AbilityCost; } }
        public string Ability2Text { get { return (sp_PlaneswalkerAbilities.Children[1] as PlaneswalkerAbilityRow).AbilityText; } }

        public string Ability3Cost { get { return (sp_PlaneswalkerAbilities.Children[2] as PlaneswalkerAbilityRow).AbilityCost; } }
        public string Ability3Text { get { return (sp_PlaneswalkerAbilities.Children[2] as PlaneswalkerAbilityRow).AbilityText; } }

        public string Loyalty { get { return tb_Loyalty.Text; } }

        public CardPreview Preview { get; private set; }
        public AddNewCardInitialPage InitialPage { get; private set; }
        public AddNewCardCorePage CorePage { get; private set; }


        //---------//
        // Utility //
        //---------//

        /// <summary>
        /// Resets the UI to its default state.
        /// </summary>
        public void ResetToDefault()
        {
            tb_Text.Text = "";
            tb_Power.Text = "";
            tb_Toughness.Text = "";

            foreach (var abilityRow in sp_PlaneswalkerAbilities.Children)
            {
                (abilityRow as PlaneswalkerAbilityRow).ResetToDefault();
                // TODO exception handle this
            }

            tb_Loyalty.Text = "";
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

            Preview.SetCardText(CardText);
        }

        /// <summary>
        /// Checks the page for errors and displays them on the UI.
        /// </summary>
        /// <returns>True if the page has errors; false otherwise.</returns>
        public bool CheckForErrors()
        {
            bool hasErrors = false;

            return hasErrors;
        }

        /// <summary>
        /// Generates and returns the next add new card page.
        /// </summary>
        /// <param name="isFinalPage">True if the next page is the final page; false otherwise.</param>
        /// <returns>The next add new card page.</returns>
        public IAddNewCardPage GenerateNextAddNewCardPage(out bool isFinalPage)
        {
            isFinalPage = true;
            return null;
        }

        /// <summary>
        /// Updates the display of the control to provide only the relevant textboxes for the given type.
        /// </summary>
        /// <param name="type">The card type to update the display for.</param>
        private void UpdateDisplayForCardType(CardTypes.Label type)
        {
            g_CardText.Visibility = type != CardTypes.Label.PLANESWALKER ? Visibility.Visible : Visibility.Collapsed;            
            g_CreaturePT.Visibility = type == CardTypes.Label.CREATURE ? Visibility.Visible : Visibility.Collapsed;
            g_PlaneswalkerAbilities.Visibility = type == CardTypes.Label.PLANESWALKER ? Visibility.Visible : Visibility.Collapsed;
            g_PlaneswalkerLoyalty.Visibility = type == CardTypes.Label.PLANESWALKER ? Visibility.Visible : Visibility.Collapsed;
        }


        //----------------//
        // Event Handlers //
        //----------------//

        private void tb_UiElement_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshCardPreview();
            //InvokeCardDetailsChanged();
        }
    }
}
