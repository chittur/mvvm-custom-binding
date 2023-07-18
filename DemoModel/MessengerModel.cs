/******************************************************************************
 * Author      = Ramaswamy Krishnan-Chittur
 *
 * Product     = MvvmCustomBindingDemo
 * 
 * Project     = DemoModel
 *
 * Description = Data model for the messenger application.
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace DemoModel
{
    /// <summary>
    /// Data model for the messenger application.
    /// </summary>
    public class MessengerModel
    {
        /// <summary>
        /// Creates an instance of the messenger data model.
        /// We listen/send to files instead of dealing with the network.
        /// </summary>
        /// <param name="listener">The subscriber</param>
        public MessengerModel(IMessageListener listener)
        {
            _client = listener;

            // Setup the receive and send files.
            string receiveFilename = "Received.xml";
            string dir = Environment.GetEnvironmentVariable("temp");
            _receiveFile = Path.Combine(dir, receiveFilename);

            // Notify the client, async.
            _ = Task.Run(this.NotifyClient);

            try
            {
                // Setup a watcher on 'receive' file.
                _watcher = new FileSystemWatcher(dir)
                {
                    NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size,
                    Filter = receiveFilename,
                    EnableRaisingEvents = true
                };

                _watcher.Changed += OnFileChanged;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Gets the filename being listened to for image list changes.
        /// </summary>
        public string ReceiveFile => _receiveFile;

        /// <summary>
        /// Gets the list of image paths defined in the input/receive file.
        /// </summary>
        /// <returns>The list of image paths defined in the input/receive file.</returns>
        public List<string> GetImagePaths()
        {
            try
            {
                // You may add more error-checking as required.
                string content = File.ReadAllText(_receiveFile);
                XmlDocument document = new XmlDocument();
                document.LoadXml(content);
                List<string> imagePaths = new List<string>();
                XmlNodeList nodes = document.GetElementsByTagName("Image");
                foreach (XmlNode node in nodes)
                {
                    imagePaths.Add(node.InnerText);
                }

                return imagePaths;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return null;
        }

        /// <summary>
        /// Handles the file changed event.
        /// </summary>
        /// <param name="sender">The sender of the notification</param>
        /// <param name="e">The file changed event</param>
        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            this.NotifyClient();
        }

        /// <summary>
        /// Notifies the subscriber.
        /// </summary>
        private void NotifyClient()
        {
            // Add some throttling and fault tolerance in case multiple file-changed
            // events are triggered in quick succession and the file is inaccessible.
            string content = string.Empty;
            int attempt = 0;
            bool retry = true;
            do
            {
                ++attempt;
                try
                {
                    content = File.ReadAllText(_receiveFile);
                    retry = false;
                }
                catch
                {
                    Thread.Sleep(attempt * 100);
                }
            } while (retry && (attempt < 3));

            try
            {
                // You may add more error-checking as required.
                XmlDocument document = new XmlDocument();
                document.LoadXml(content);
                List<string> imagePaths = new List<string>();
                XmlNodeList nodes = document.GetElementsByTagName("Image");
                foreach (XmlNode node in nodes)
                {
                    imagePaths.Add(node.InnerText);
                }

                // Notify the subscriber.
                if (_client != null)
                {
                    _client.OnMessageReceived(imagePaths);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        // The file which we listen to for incoming messages.
        private readonly string _receiveFile;

        // Watcher for incoming messages (changes to the 'receive' file).
        private readonly FileSystemWatcher _watcher;

        // The subscriber.
        private IMessageListener _client;
    }
}
