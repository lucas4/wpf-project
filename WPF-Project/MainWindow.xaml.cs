using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

           DateTime todays = DateTime.Now;

           Week w = new Week();
           int DaysTotal = DateTime.DaysInMonth(todays.Year, todays.Month);
           int DaysCount = 1;
            for (int i = 0; i <= 7; i++)
            {
                if (DaysCount <= DaysTotal)
                {

                    DateTime dt = new DateTime(todays.Year, todays.Month, DaysCount);
                    int dayOfWeek = (dt.DayOfWeek == DayOfWeek.Sunday) ? 6 : (int)dt.DayOfWeek-1;
                    w.day[dayOfWeek] = new Day();
                    w.day[dayOfWeek].date = dt;
                    DaysCount++;
                    if (dt.DayOfWeek == DayOfWeek.Sunday)
                    {
                        dat.Items.Add(w);
                        w = new Week();
                        i = 0;
                    }
                }
            }
        }

        private void dat_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }
    }



    
}
