/******************************************************************************
 * Author      = Ramaswamy Krishnan-Chittur
 *
 * Product     = MvvmCustomBindingDemo
 * 
 * Project     = DemoApp
 *
 * Description = Interaction logic for App.xaml.
 *****************************************************************************/

using System;
using System.Linq;
using System.Windows.Data;

namespace DemoApp
{
    /// <summary>
    /// Image list path converter.
    /// </summary>
    public class ImageListValueConverter : IValueConverter
    {
        /// <summary>
        /// Converts to a list of image paths.
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="targetType">Target type</param>
        /// <param name="parameter">Parameter</param>
        /// <param name="culture">Culture</param>
        /// <returns>List of image paths</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value ?? Enumerable.Empty<string>();
        }

        /// <summary>
        /// Converts from a list of image paths.
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="targetType">Target type</param>
        /// <param name="parameter">Parameter</param>
        /// <param name="culture">Culture</param>
        /// <returns>List of image paths</returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
