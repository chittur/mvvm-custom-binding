/******************************************************************************
 * Author      = Ramaswamy Krishnan-Chittur
 *
 * Product     = MvvmCustomBindingDemo
 * 
 * Project     = DemoViewModel
 *
 * Description = ViewModel for the messenger page.
 *****************************************************************************/

using DemoModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace DemoViewModel
{
    /// <summary>
    /// ViewModel for the Messenger page.
    /// </summary>
    public class MessengerViewModel :
        INotifyPropertyChanged, // Notifies clients that a property value has changed.
        IMessageListener        // Notifies clients that has a message has been received.
    {
        private List<string> _imagePaths; // List of image paths.

        /// <summary>
        /// Creates an instance of the Messenger ViewModel.
        /// </summary>
        public MessengerViewModel()
        {
            _model = new MessengerModel(this);
            _imagePaths = _model.GetImagePaths();
        }

        /// <summary>
        /// Gets the filename being listened to for image list changes.
        /// </summary>
        public string ReceiveFile => _model.ReceiveFile;

        /// <summary>
        /// Gets the image paths.
        /// </summary>
        public List<string> ImagePaths
        {
            get
            {
                lock (this)
                {
                    return _imagePaths;
                }
            }
        }

        /// <summary>
        /// Handles an incoming message.
        /// </summary>
        /// <param name="imagePath">The path to the image.</param>
        /// <param name="caption">The caption for the image.</param>
        public void OnMessageReceived(List<string> imagePaths)
        {
            // OnPropertyChanged event is automatically marshaled to the UI thread. However,
            // the bitmap image has thread affinity and needs to be created on the same
            // thread as it is displayed on (UI thread). Hence let us explicitly dispatch
            // the handling of this message to the UI thread.
            //
            // Also note that we may execute the call asynchronously as the calling
            // thread is not dependent on the callee thread finishing this method call.
            // Hence we may call the dispatcher's BeginInvoke method which kicks off
            // execution async as opposed to Invoke which does it synchronously.

            _ = this.ApplicationMainThreadDispatcher.BeginInvoke(
                        DispatcherPriority.Normal,
                        new Action<List<string>>((images) =>
                        {
                            lock (this)
                            {
                                try
                                {
                                    _imagePaths = new List<string>(images);
                                    this.OnPropertyChanged("ImagePaths");
                                }
                                catch (Exception e)
                                {
                                    Debug.WriteLine(e.Message);
                                }
                            }
                        }),
                        imagePaths);
        }

        /// <summary>
        /// Property changed event raised when a property is changed on a component.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Handles the property changed event raised on a component.
        /// </summary>
        /// <param name="property">The name of the property.</param>
        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Gets the dispatcher to the main thread. In case it is not available
        /// (such as during unit testing) the dispatcher associated with the
        /// current thread is returned.
        /// </summary>
        private Dispatcher ApplicationMainThreadDispatcher =>
            (Application.Current?.Dispatcher != null) ?
                    Application.Current.Dispatcher :
                    Dispatcher.CurrentDispatcher;

        /// <summary>
        /// Underlying data model.
        /// </summary>
        private readonly MessengerModel _model;
    }
}
