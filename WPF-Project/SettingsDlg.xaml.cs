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
    /// Interaction logic for SettingsDlg.xaml
    /// </summary>
    public partial class SettingsDlg : Window
    {
        public int settingAlert;
        public bool alertToRun;

        public SettingsDlg()
        {
            InitializeComponent();
        }
        
        public SettingsDlg(int time, bool alert)
        {
            InitializeComponent();
            settingAlert = time;
            alertToRun = alert;
            if (alertToRun)
            {
                IsAlertRun.IsChecked = true;
            }
        }

        private void TrueButton_Click(object sender, RoutedEventArgs e)
        {
            if (AlertBox.Text != null)
            {
                settingAlert = Int32.Parse(AlertBox.Text);
                alertToRun = (bool)IsAlertRun.IsChecked;
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
            AlertBox.Text = settingAlert.ToString();
        }
    }
}
