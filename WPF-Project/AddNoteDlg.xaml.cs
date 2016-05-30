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
    public partial class AddNoteDlg : Window
    {
        public string NoteContent;
        public AddNoteDlg()
        {
            InitializeComponent();
        }

        private void TrueButton_Click(object sender, RoutedEventArgs e)
        {
            // add note to list
            if(TextBoxContent != null)
            {
                NoteContent = TextBoxContent.Text;
                DialogResult = true;
            } else
                DialogResult = false;

            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
