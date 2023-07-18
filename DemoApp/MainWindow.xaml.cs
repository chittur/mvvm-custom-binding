/******************************************************************************
 * Author      = Ramaswamy Krishnan-Chittur
 *
 * Product     = MvvmCustomBindingDemo
 * 
 * Project     = DemoApp
 *
 * Description = Interaction logic for MainWindow.xaml.
 *****************************************************************************/

using System.Windows;
using System.Windows.Controls;

namespace DemoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Creates a new instance of the MainWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Page messengerPage = new MessengerPage();
            MainFrame.Navigate(messengerPage);
        }
    }
}
