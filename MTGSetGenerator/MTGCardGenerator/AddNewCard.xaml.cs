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
    /// Interaction logic for AddNewCard.xaml
    /// </summary>
    public partial class AddNewCard : UserControl
    {
        public AddNewCard()
        {
            InitializeComponent();
            //componentsInitialized = true;

            // Manage creating the initial page
            AddNewCardInitialPage page = new AddNewCardInitialPage();
            uc_CardDetailsPage.Content = page;
            page.CardDetailsChanged += addNewCardInitialPage_CardDetailsChanged;
            RefreshPreviewBasedOnInitialPage(page);
        }

        //------//
        // Data //
        //------//

        //private bool componentsInitialized = false;

        private IAddNewCardPage CardDetailsPage { get { return uc_CardDetailsPage.Content as IAddNewCardPage; } }
        private CardPreview Preview { get { return uc_CardPreview; } }


        //-------------------//
        // Preview Interface //
        //-------------------//

        /// <summary>
        /// Refreshes the card preview based on the given add new card initial page.
        /// </summary>
        /// <param name="page">The add new card page to use to refresh the preview.</param>
        private void RefreshPreviewBasedOnInitialPage(AddNewCardInitialPage page)
        {
            Preview.SetCardPicture(page.CardImage);
            Preview.SetCardFrame(page.FrameColor, page.BaseType);

            Preview.SetCardName(page.CardName);
            Preview.SetCardCost(page.Cost);

            Preview.SetCardType(page.BaseType, page.IsLegendary, page.IsTribal, page.Supertype, page.AuxType, page.Subtype);
        }


        //----------------//
        // Event Handlers //
        //----------------//

        private void addNewCardInitialPage_CardDetailsChanged(object sender, EventArgs e)
        {
            AddNewCardInitialPage page = CardDetailsPage as AddNewCardInitialPage;
            RefreshPreviewBasedOnInitialPage(page);
        }

        private void b_Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_Next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_Previous_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_Clear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_Help_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
