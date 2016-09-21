using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Microsoft.Expression.Interactivity.Core;

namespace Samples.Localization.App
{
    public class XPropertyChangedTrigger : PropertyChangedTrigger
    {
        #region RespectLoadedEvent Dependency Property

        public bool RespectLoadedEvent
        {
            get { return (bool)GetValue(RespectLoadedEventProperty); }
            set { SetValue(RespectLoadedEventProperty, value); }
        }

        public static readonly DependencyProperty RespectLoadedEventProperty =
            DependencyProperty.Register(
                "RespectLoadedEvent", 
                typeof(bool), 
                typeof(XPropertyChangedTrigger), 
                new PropertyMetadata(
                    default(bool), 
                    OnRespectLoadedEventChanged));

        private static void OnRespectLoadedEventChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        protected override void OnAttached()
        {
            if (AssociatedObject is FrameworkElement && RespectLoadedEvent)
            {
                ((FrameworkElement)AssociatedObject).Loaded += XDataTriggerLoaded;
            }

            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (AssociatedObject is FrameworkElement)
            {
                ((FrameworkElement)AssociatedObject).Loaded -= XDataTriggerLoaded;
            }

        }

        private void XDataTriggerLoaded(object sender, RoutedEventArgs e)
        {
            EvaluateBindingChange(e);
        }
    }

    public sealed class EmptyMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return Guid.NewGuid().ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}