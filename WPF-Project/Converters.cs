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
                if (day.date.Day == DateTime.Today.Day)
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

    public class notesButtonConverter : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return Visibility.Hidden;
            try
            {
                Day day = (Day)value;
                if (day.notes.Count > 0)
                    return Visibility.Visible;
                else
                    return Visibility.Hidden;
            }
            catch
            {
                return Visibility.Hidden;
            }

        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

    }
}
