using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WPF_Project
{
    public class todayConverter : IValueConverter
    {
        enum Days { Normal, Today, Other };
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return Days.Other;
            try
            {
                Day day = (Day)value;
                if (day.date == DateTime.Today)
                    return Days.Today;
                else
                    return Days.Normal;
            }
            catch
            {
                return Days.Normal;
            }

        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

    }

    public class stringLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            if (parameter == null)
                return value;
            int _MaxLength;
            if (!int.TryParse(parameter.ToString(), out _MaxLength))
                return value;
            var _String = value.ToString();
            if (_String.Length > _MaxLength)
                _String = _String.Substring(0, _MaxLength) + "...";
            return _String;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
