using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for ShowEventsDlg.xaml
    /// </summary>
    public partial class ShowEventsDlg : Window
    {
        List<EventDay> eventsList;
        Dictionary<int, Year> yearsDict;

        public ShowEventsDlg()
        {
            InitializeComponent();
        }

        public ShowEventsDlg(List<EventDay> events, Dictionary<int, Year> year)
        {
            InitializeComponent();
            eventsList = events.OrderBy(e => e.date).ToList();
            yearsDict = year;
            EventsList.ItemsSource = this.eventsList;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (DateFrom.SelectedDate != null)
            {
                if (DateTo.SelectedDate != null)
                {
                    EventsList.Items.Filter = delegate(object obj)
                    {
                        EventDay eventT = (EventDay)obj;
                        if (eventT.date > DateFrom.SelectedDate && eventT.date < DateTo.SelectedDate)
                            return true;
                        else
                            return false;
                    };
                }
                else
                {
                    EventsList.Items.Filter = delegate(object obj)
                    {
                        EventDay eventT = (EventDay)obj;
                        if (eventT.date > DateFrom.SelectedDate)
                            return true;
                        else
                            return false;
                    };
                }
            }
        }

        private void DeleteFilterButton_Click(object sender, RoutedEventArgs e)
        {
            EventsList.Items.Filter = null;

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EventDay eventT = (EventDay)EventsList.SelectedItem;
                EventDay toRemove = eventsList.Where(n => n.id == eventT.id).Single();

                int noteCount = (from n in eventsList
                                 where n.date == toRemove.date
                                 select n).Count();
                if (noteCount == 1)
                {
                    DateTime date = toRemove.date;
                    foreach (var week in yearsDict[date.Year].monthsDict[date.Month].Weeks)
                    {
                        foreach (var day in week.day.Where(d => d != null))
                        {
                            if (day.date == date)
                            {
                                day.hasEvents = false;
                                break;
                            }

                        }
                    }

                }

                eventsList.Remove(toRemove);
                EventsList.ItemsSource = eventsList;
                EventsList.Items.Refresh();
            }
            catch
            {

            }
        }
    }
}
