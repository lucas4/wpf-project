using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF_Project
{
    public class todayConverter : IValueConverter
    {
        enum Days { Normal, Today, Other };
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return Days.Normal;

            try
            {
                int dValue = Int32.Parse(value.ToString());
                if (dValue < 5)
                    return Days.Other;
                else if (dValue == 16) // if its today date 
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
}
