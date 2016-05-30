using System;
using System.Collections.Generic;
using System.Data;
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
        //static List<Month> monthsList;
        static Dictionary<int, Year> yearsDict;

        static Year actualYearSelected;
        static Month actualMonthSelected;
        static Day actualDaySelected;

        public MainWindow()
        {
            InitializeComponent();
            yearsDict = new Dictionary<int, Year>();
            dat.SelectionMode = DataGridSelectionMode.Single;

            DateTime todays = DateTime.Now;
            Month month = generateMonth(todays.Year, todays.Month);

            actualDaySelected = month.getDay(todays.Day);
            actualMonthSelected = month;
            actualYearSelected = new Year();
            actualYearSelected.YearID = todays.Year;

            yearsDict.Add(todays.Year, actualYearSelected);
            yearsDict[actualYearSelected.YearID].monthsDict.Add(todays.Month, month);

            MonthBlock.Text = month.MonthNames[month.MonthID] + " " + month.MonthYear;
            dat.ItemsSource = month.Weeks;
            
            
        }

        private void dat_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                actualDaySelected.notes = DayNotes.Text;

                Week week = (Week)dat.SelectedCells[0].Item;
                int index = dat.CurrentCell.Column.DisplayIndex;
                Day selectedDay = week.day[index];
                actualDaySelected = selectedDay;
                DayNotes.Text = selectedDay.notes;
            } catch { }
        }

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
        }

        private void PreviousMonthButton_Click(object sender, RoutedEventArgs e)
        {
            Year searchedYear;
            Month searchMonth;
            if ((actualMonthSelected.MonthID - 1) < 1)
            {
                if (!yearsDict.ContainsKey(actualYearSelected.YearID - 1))
                {
                    searchedYear = new Year();
                    searchedYear.YearID = actualYearSelected.YearID - 1;
                    yearsDict.Add(searchedYear.YearID, searchedYear);
                }
                else
                    searchedYear = yearsDict[actualYearSelected.YearID - 1];
            }
            else
            {
                if (!yearsDict.ContainsKey(actualYearSelected.YearID))
                {
                    searchedYear = new Year();
                    searchedYear.YearID = actualYearSelected.YearID;
                    yearsDict.Add(searchedYear.YearID, searchedYear);
                }
                else
                    searchedYear = yearsDict[actualYearSelected.YearID];
            }


            if ((actualMonthSelected.MonthID - 1) < 1)
            {
                if (!yearsDict[searchedYear.YearID].monthsDict.ContainsKey(12))
                {
                    searchMonth = generateMonth(searchedYear.YearID, 12);
                    yearsDict[searchedYear.YearID].monthsDict.Add(searchMonth.MonthID, searchMonth);
                }
                else
                {
                    searchMonth = searchedYear.monthsDict[12];
                }
            }
            else
            {
                if (!yearsDict[searchedYear.YearID].monthsDict.ContainsKey(actualMonthSelected.MonthID - 1))
                {
                    searchMonth = generateMonth(searchedYear.YearID, actualMonthSelected.MonthID - 1);
                    yearsDict[searchedYear.YearID].monthsDict.Add(searchMonth.MonthID, searchMonth);
                }
                else
                {
                    searchMonth = searchedYear.monthsDict[actualMonthSelected.MonthID - 1];
                }
            }

            dat.ItemsSource = searchMonth.Weeks;
            actualMonthSelected = searchMonth;
            actualYearSelected = searchedYear;
            MonthBlock.Text = actualMonthSelected.MonthNames[actualMonthSelected.MonthID] + " " + actualYearSelected.YearID;
        }

        private void NextMonthButton_Click(object sender, RoutedEventArgs e)
        {
            Year searchedYear;
            Month searchMonth;
            if ((actualMonthSelected.MonthID + 1) > 12)
            {
                if (!yearsDict.ContainsKey(actualYearSelected.YearID + 1))
                {
                    searchedYear = new Year();
                    searchedYear.YearID = actualYearSelected.YearID + 1;
                    yearsDict.Add(searchedYear.YearID, searchedYear);
                }
                else
                    searchedYear = yearsDict[actualYearSelected.YearID + 1];
            }
            else
            {
                if (!yearsDict.ContainsKey(actualYearSelected.YearID))
                {
                    searchedYear = new Year();
                    searchedYear.YearID = actualYearSelected.YearID;
                    yearsDict.Add(searchedYear.YearID, searchedYear);
                }
                else
                    searchedYear = yearsDict[actualYearSelected.YearID];
            }


            if ((actualMonthSelected.MonthID + 1) > 12)
            {
                if (!yearsDict[searchedYear.YearID].monthsDict.ContainsKey(1))
                {
                    searchMonth = generateMonth(searchedYear.YearID, 1);
                    yearsDict[searchedYear.YearID].monthsDict.Add(searchMonth.MonthID, searchMonth);
                }
                else
                {
                    searchMonth = searchedYear.monthsDict[1];
                }
            }
            else
            {
                if (!yearsDict[searchedYear.YearID].monthsDict.ContainsKey(actualMonthSelected.MonthID + 1))
                {
                    searchMonth = generateMonth(searchedYear.YearID, actualMonthSelected.MonthID + 1);
                    yearsDict[searchedYear.YearID].monthsDict.Add(searchMonth.MonthID, searchMonth);
                }
                else
                {
                    searchMonth = searchedYear.monthsDict[actualMonthSelected.MonthID + 1];
                }
            }

            dat.ItemsSource = searchMonth.Weeks;
            actualMonthSelected = searchMonth;
            actualYearSelected = searchedYear;
            MonthBlock.Text = actualMonthSelected.MonthNames[actualMonthSelected.MonthID] + " " + actualYearSelected.YearID;
            
        }

        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var contextMenu = (ContextMenu)menuItem.Parent;
            var item = (DataGrid)contextMenu.PlacementTarget;
            var index = item.CurrentCell.Column.DisplayIndex;

            var week = (Week)item.SelectedCells[0].Item;
            var day = week.day[index];
            if (day != null)
            {
                // create modal window and add events to list
                day.hasEvents = true;
                dat.Items.Refresh();
            }
        }
        private void AddNote_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var contextMenu = (ContextMenu)menuItem.Parent;
            var item = (DataGrid)contextMenu.PlacementTarget;
            var index = item.CurrentCell.Column.DisplayIndex;

            var week = (Week)item.SelectedCells[0].Item;
            var day = week.day[index];
            if (day != null)
            {
                // create modal window and add events to list
                day.hasNotes = true;
                dat.Items.Refresh();
            }
        }
    }


    
}
