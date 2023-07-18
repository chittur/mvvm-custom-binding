/******************************************************************************
 * Author      = Ramaswamy Krishnan-Chittur
 *
 * Product     = MvvmCustomBindingDemo
 * 
 * Project     = DemoUnitTest
 *
 * Description = Unit tests for the ViewModel.
 *****************************************************************************/

using DemoViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoUnitTest
{
    /// <summary>
    /// Unit tests for the ViewModel.
    /// </summary>
    [TestClass]
    public class DemoViewModelUnitTest
    {
        /// <summary>
        /// Setup method for the unit tests.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
        }

        /// <summary>
        /// Tests the Receive functionality.
        /// </summary>
        [TestMethod]
        public void TestReceive()
        {
            // Please read the 'Arrange, Act, Assert' pattern described here:
            // https://docs.microsoft.com/en-us/archive/msdn-magazine/2013/november/mvvm-writing-a-testable-presentation-layer-with-mvvm

            MessengerViewModel viewModel = new MessengerViewModel();

            // Trigger the callback by writing to the receive file, and
            // validate that our ViewModel received the event.
        }
    }
}
