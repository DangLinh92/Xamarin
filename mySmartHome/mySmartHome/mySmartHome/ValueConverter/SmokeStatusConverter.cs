using mySmartHome.Devices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace mySmartHome.ValueConverter
{
    public class SmokeStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (SmokeDetector.Status)value;

            if(targetType == typeof(Color))
            {
                return GetColor(status);
            }

            if(targetType == typeof(ImageSource))
            {
                return GetImageName(status);
            }

            return null;
        }

        string GetImageName(SmokeDetector.Status status)
        {
            switch (status)
            {
                case SmokeDetector.Status.Fire:
                    return "emergency.png";
                case SmokeDetector.Status.Smoke:
                    return "warning.png";
                default:
                    return "smoke.png";
            }
        }

        Color GetColor(SmokeDetector.Status status)
        {
            switch (status)
            {
                case SmokeDetector.Status.Fire:
                    return Color.Red;
                case SmokeDetector.Status.Smoke:
                    return Color.Orange;
                default:
                    return Color.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
