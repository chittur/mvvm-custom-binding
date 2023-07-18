/******************************************************************************
 * Author      = Ramaswamy Krishnan-Chittur
 *
 * Product     = MvvmCustomBindingDemo
 * 
 * Project     = DemoApp
 *
 * Description = Interaction logic for MessengerPage.xaml.
 *****************************************************************************/

using System;
using System.Windows;
using System.Windows.Controls;
using DemoViewModel;

namespace DemoApp
{
    /// <summary>
    /// Interaction logic for MessagingPage.xaml
    /// </summary>
    public partial class MessengerPage : Page
    {
        /// <summary>
        /// Creates a new instance of the MessagingPage.
        /// </summary>
        public MessengerPage()
        {
            MessengerViewModel viewModel = new MessengerViewModel();
            DataContext = viewModel;

            InitializeComponent();
        }

        /// <summary>
        /// Handles the click event for the Help button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event args</param>
        private void HelpButtonClick(object sender, RoutedEventArgs e)
        {
            MessengerViewModel viewModel = DataContext as MessengerViewModel;
            string helpMessage = $"Place a file named {viewModel.ReceiveFile}. Schema is as follows.{Environment.NewLine}";
            helpMessage += $"    <Root>{Environment.NewLine}";
            helpMessage += $@"      <Image>C:\Temp\Parrot.jpg</Image>{Environment.NewLine}";
            helpMessage += $@"      <Image>C:\Temp\Tiger.jpg</Image>{Environment.NewLine}";
            helpMessage += $"    </Root>{Environment.NewLine}";
            helpMessage += $"Update the entries and save xml file to dynamically add/remove images from this app's main page.{Environment.NewLine}";
            helpMessage += $"Note: Assumes you have the jpg image files mentioned in the schema entries.{Environment.NewLine}";
            _ = MessageBox.Show(helpMessage, "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
