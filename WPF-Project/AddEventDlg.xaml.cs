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
    /// Interaction logic for AddEventDlg.xaml
    /// </summary>
    public partial class AddEventDlg : Window
    {
        public string DateText;
        public string TitleText;
        public string DescriptionText;
        public TimeSpan Time;

        private AddEventViewModel _eventViewModel;
        public AddEventDlg()
        {
            InitializeComponent();
            _eventViewModel = new AddEventViewModel();
            DataContext = _eventViewModel;
        }

        private void TrueButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_eventViewModel.HasErrors)
            {
                if (TitleTextBox.Text != null && DescriptionTextBox.Text != null)
                {

                    TitleText = _eventViewModel.Title;
                    DescriptionText = _eventViewModel.Description;
                    Time = new TimeSpan(Int32.Parse(_eventViewModel.Hour), Int32.Parse(_eventViewModel.Minute), 0);
                    DialogResult = true;
                }
                else
                    DialogResult = false;
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DateTextBlock.Text = DateText;
        }
    }

    public class AddEventViewModel : IDataErrorInfo
    {
        private string _hour;
        private string _minute;
        private bool _hourErr;
        private string _description;
        private string _title;

        public AddEventViewModel()
        {
            DateTime today = DateTime.Now;
            this.Minute = today.Minute.ToString();
            this.Hour = today.Hour.ToString();
        }

        public bool HasErrors
        {
            get { return _hourErr; }         
        }

        public string Hour
        {
            get { return _hour; }
            set {
                _hourErr = false;
                if (String.IsNullOrEmpty(value))
                {
                    _hourErr = true;
                }
                else
                    try
                    {
                        if (Int32.Parse(value) > 24 || Int32.Parse(value) < 0)
                        {
                            _hourErr = true;
                        }
                    }
                    catch { _hourErr = true; }
                _hour = value;
            }
        }

        public string Minute
        {
            get { return _minute; }
            set
            {
                _hourErr = false;
                if (String.IsNullOrEmpty(value))
                {
                    _hourErr = true;
                }
                else
                    try
                    {
                        if (Int32.Parse(value) > 60 || Int32.Parse(value) < 0)
                        {
                            _hourErr = true;
                        }
                    }
                    catch { _hourErr = true; }

                _minute = value;
            }
        }

        public string Description
        {
            get { return _description; }
            set { 
                _description = value;
            }
        }

        public string Title
        {
            get { return _title; }
            set { 
                _title = value;
                }
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case "Hour":
                        if (_hourErr)
                            return "Wprowadź prawidłową godzinę!";
                        break;

                    case "Minute":
                        if (_hourErr)
                            return "Wprowadź prawidłową godzinę!";
                        break;
                }
                return string.Empty;
            }
        }

    }
}
