using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ShowNoteDlg.xaml
    /// </summary>
    public partial class ShowNoteDlg : Window
    {
        List<Note> noteList;
        Dictionary<int, Year> yearsDict;

        public ShowNoteDlg()
        {
            InitializeComponent();
            
        }

        public ShowNoteDlg(List<Note> noteList, Dictionary<int, Year> yearsDict)
        {
            InitializeComponent();
            this.noteList = noteList;
            this.yearsDict = yearsDict;
            NoteList.ItemsSource = noteList;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (DateFrom.SelectedDate != null)
            {
                if (DateTo.SelectedDate != null)
                {
                    NoteList.Items.Filter = delegate(object obj)
                    {
                        Note note = (Note)obj;
                        if (note.date > DateFrom.SelectedDate && note.date < DateTo.SelectedDate)
                            return true;
                        else
                            return false;
                    };
                }
                else
                {
                    NoteList.Items.Filter = delegate(object obj)
                    {
                        Note note = (Note)obj;
                        if (note.date > DateFrom.SelectedDate)
                            return true;
                        else
                            return false;
                    };
                }
            }
        }

        private void DeleteFilterButton_Click(object sender, RoutedEventArgs e)
        {
            NoteList.Items.Filter = null;    

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Note note = (Note)NoteList.SelectedItem;
            Note toRemove = noteList.Where(n => n.id == note.id).Single();

            int noteCount = (from n in noteList
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
                            day.hasNotes = false;
                            break;
                        }

                    }
                }
                            
            }

            noteList.Remove(toRemove);
            NoteList.Items.Refresh();

        }

    }
}
