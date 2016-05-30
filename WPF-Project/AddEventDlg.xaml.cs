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
    /// Interaction logic for AddEventDlg.xaml
    /// </summary>
    public partial class AddEventDlg : Window
    {
        public string DateText;
        public string TitleText;
        public string DescriptionText;

        public AddEventDlg()
        {
            InitializeComponent();            
        }

        private void TrueButton_Click(object sender, RoutedEventArgs e)
        {
            // add note to list
            //if (TextBoxContent != null)
            //{
            //    NoteContent = TextBoxContent.Text;
            //    DialogResult = true;
            //}
            //else
            //    DialogResult = false;
            if (TitleTextBox.Text != null && DescriptionTextBox.Text != null)
            {
                TitleText = TitleTextBox.Text;
                DescriptionText = DescriptionTextBox.Text;
                DialogResult = true;
            }
            else
                DialogResult = false;
            Close();
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
}
