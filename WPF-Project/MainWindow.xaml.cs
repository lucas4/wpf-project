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
            for (int j = 0; j < 4; j++)
            {
                Week w = new Week();
                for (int i = 0; i < 7; i++)
                {
                    w.day[i] = (i+1) + (j*7) + "";
                }
                dat.Items.Add(w);
            }
            
            


        }

        private void dat_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }
    }

    public class Week
    {
        public string[] day { get; set; }
        public Week()
        {
            day = new string[7];
        }

    }


    
}
