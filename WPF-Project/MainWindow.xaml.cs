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
        List<Month> monthsList;
        Month actualMonthSelected;
        public MainWindow()
        {
            InitializeComponent();
            monthsList = new List<Month>();

           DateTime todays = DateTime.Now;
           Month month = generateMonth(todays.Year, todays.Month);
           
           monthsList.Add(month);
           MonthBlock.Text = month.Name;
           dat.ItemsSource = month.Weeks;
           actualMonthSelected = month;

        }

        private void dat_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        //private List<Week> generateMonth(int year, int month)
        private Month generateMonth(int yearId, int monthId)
        {
            Month month = new Month();
            List<Week> weeks = new List<Week>();
            Week w = new Week();
            int DaysTotal = DateTime.DaysInMonth(yearId, monthId);
            int DaysCount = 1;
            for (int i = 0; i <= 7; i++)
            {
                if (DaysCount <= DaysTotal)
                {

                    DateTime dt = new DateTime(yearId, monthId, DaysCount);
                    int dayOfWeek = (dt.DayOfWeek == DayOfWeek.Sunday) ? 6 : (int)dt.DayOfWeek - 1;
                    w.day[dayOfWeek] = new Day();
                    w.day[dayOfWeek].date = dt;

                    if (w.day[dayOfWeek].date.Day == 9 || w.day[dayOfWeek].date.Day == 19)
                    {
                        w.day[dayOfWeek].notes = new List<string>();
                        w.day[dayOfWeek].notes.Add("fwafwa");
                        w.day[dayOfWeek].notes.Add("fwafwa");
                    }

                    if (dt.DayOfWeek == DayOfWeek.Sunday || DaysCount == DaysTotal)
                    {
                        weeks.Add(w);
                        w = new Week();
                        i = 0;
                    }
                    DaysCount++;
                }
            }

            month.MonthID = monthId;
            month.MonthYear = yearId;
            DateTime date = new DateTime(yearId, monthId,1);
            month.Name = date.ToString("MMM", CultureInfo.InvariantCulture);
            month.Weeks = weeks;
            return month;
            //return weeks;
        }

        private void PreviousMonthButton_Click(object sender, RoutedEventArgs e)
        {

            foreach (var month in monthsList)
            {
                if (month.MonthID == actualMonthSelected.MonthID-1)
                {
                    dat.ItemsSource = month.Weeks;
                    MonthBlock.Text = month.Name;
                    actualMonthSelected = month;
                    return;
                }
            }
            Month newMonth = generateMonth(actualMonthSelected.MonthYear, actualMonthSelected.MonthID - 1);
            monthsList.Add(newMonth);
            dat.ItemsSource = newMonth.Weeks;
            actualMonthSelected = newMonth;
            MonthBlock.Text = newMonth.Name;



        }

        private void NextMonthButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var month in monthsList)
            {
                if (month.MonthID == actualMonthSelected.MonthID +1)
                {
                    dat.ItemsSource = month.Weeks;
                    MonthBlock.Text = month.Name;
                    actualMonthSelected = month;
                    return;
                }
            }
            Month newMonth = generateMonth(actualMonthSelected.MonthYear, actualMonthSelected.MonthID +1);
            monthsList.Add(newMonth);
            dat.ItemsSource = newMonth.Weeks;
            actualMonthSelected = newMonth;
            MonthBlock.Text = newMonth.Name;

        }
    }



    
}
