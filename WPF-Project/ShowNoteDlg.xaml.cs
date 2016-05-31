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
        List<Note> filteredList;
        public ShowNoteDlg()
        {
            InitializeComponent();
            
        }

        public ShowNoteDlg(List<Note> noteList)
        {
            InitializeComponent();
            this.noteList = noteList;
            NoteList.ItemsSource = noteList;
        }

        private void FilterNotes(DateTime from, DateTime? to)
        {
            filteredList = new List<Note>();

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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
