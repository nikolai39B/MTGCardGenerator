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

            // Creating the initial page
            CreateInitialPage();
        }

        //------//
        // Data //
        //------//

        //private bool componentsInitialized = false;

        private List<IAddNewCardPage> pages = new List<IAddNewCardPage>();
        private IAddNewCardPage CurrentPage { get { return pages.LastOrDefault(); } }

        private CardPreview Preview { get { return uc_CardPreview; } }

        //-----------------//
        // Pages Interface //
        //-----------------//

        /// <summary>
        /// Creates the inital page for the add new card form.
        /// </summary>
        private void CreateInitialPage()
        {
            // Create the initial page
            AddNewCardInitialPage initialPage = new AddNewCardInitialPage(Preview);
            pages.Add(initialPage);
            uc_CardInfoPage.Content = initialPage;
            
            // Update the previous, next, and save buttons
            b_Previous.Visibility = Visibility.Hidden;
            b_Next.Visibility = Visibility.Visible;
            b_Save.IsEnabled = false;

            // Refresh the card preview
            RefreshPreview(true);
        }

        /// <summary>
        /// Moves the add new card dialog to the previous page.
        /// </summary>
        private void MoveToPreviousPage()
        {
            // Make sure there is a previous page to return to
            int pageIndexToRemove = pages.Count - 1;
            if (pageIndexToRemove <= 0)
            {
                throw new InvalidOperationException("No previous page to return to.");
            }

            // Remove the current page
            pages.RemoveAt(pageIndexToRemove);
            int indexOfPreviousPage = pageIndexToRemove - 1;
            uc_CardInfoPage.Content = pages[indexOfPreviousPage];            

            // Update the previous, next, and save buttons
            b_Previous.Visibility = indexOfPreviousPage == 0 ? Visibility.Hidden : Visibility.Visible;
            b_Next.Visibility = Visibility.Visible;
            b_Save.IsEnabled = false;

            // Refresh the card preview
            RefreshPreview(true);
        }

        /// <summary>
        /// Moves the add new card dialog to the next page.
        /// </summary>
        private void MoveToNextPage()
        {
            // Make sure a page exists already
            if (CurrentPage == null)
            {
                throw new InvalidOperationException("No current page; cannot move to next page.");
            }

            // Check the current page for errors
            bool errorsOnPage = CurrentPage.CheckForErrors();
            if (errorsOnPage)
            {
                return;
            }

            bool nextPageIsLastPage;
            IAddNewCardPage nextPage = CurrentPage.GenerateNextAddNewCardPage(out nextPageIsLastPage);

            // Update the previous, next, and save buttons
            b_Previous.Visibility =  Visibility.Visible;
            b_Next.Visibility = nextPageIsLastPage ? Visibility.Hidden : Visibility.Visible;
            b_Save.IsEnabled = nextPageIsLastPage;

            // Add the references to the new page
            pages.Add(nextPage);
            uc_CardInfoPage.Content = nextPage;

            // Refresh the card preview
            RefreshPreview(false);
        }


        //-------------------//
        // Preview Interface //
        //-------------------//

        /// <summary>
        /// Calls the appropriate preview refresh function based on the current page.
        /// </summary>
        /// <param name="fullRefresh">Whether to refresh all values on the preview, or just the values from the current page.</param>
        public void RefreshPreview(bool fullRefresh = false)
        {
            // If we're doing a full refresh, then reset the card preview and refresh for each page
            if (fullRefresh)
            {
                Preview.ResetCardPreview();
                foreach (var page in pages)
                {
                    page.RefreshCardPreview();
                }
            }

            // Otherwise, just refresh for the current page
            else
            {
                CurrentPage.RefreshCardPreview();
            }
        }


        //----------------//
        // Event Handlers //
        //----------------//

        private void OnCardDetailsChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void b_Previous_Click(object sender, RoutedEventArgs e)
        {
            MoveToPreviousPage();
        }

        private void b_Next_Click(object sender, RoutedEventArgs e)
        {
            MoveToNextPage();
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

        private void b_Cancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
