/******************************************************************************
 * Author      = Ramaswamy Krishnan-Chittur
 *
 * Product     = MvvmCustomBindingDemo
 * 
 * Project     = DemoModel
 *
 * Description = Interface for the message listener.
 *****************************************************************************/

using System.Collections.Generic;

namespace DemoModel
{
    /// <summary>
    /// Notifies clients that has a message has been received.
    /// </summary>
    public interface IMessageListener
    {
        /// <summary>
        /// Handles the reception of a message.
        /// </summary>
        /// <param name="imageFile">The path to the image file.</param>
        /// <param name="caption">The caption of the image.</param>
        void OnMessageReceived(List<string> imagePaths);
    }
}
