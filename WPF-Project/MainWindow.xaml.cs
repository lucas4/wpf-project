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
           DateTime todays = DateTime.Now;
           DateTime date = new DateTime(todays.Year, todays.Month, 8);
           Week w = new Week();
           Boolean nextWeek = false;
           Boolean hasDays = true;
           int dayI = 0;
            while(hasDays)
            {
                while(!nextWeek)
                {
                    w.day[dayI] = new Day();
                    w.day[dayI].date = new DateTime(todays.Year, todays.Month, (dayI + 1));
                }
            }
            
            //for (int j = 0; j < 4; j++)
            //{
            //    Week w = new Week();
            //    //for (int i = 1; i < 7; i++)
            //    //{
            //    //    w.day[i] = new Day();
            //    //    //w.day[i] = (i+1) + (j*7) + "";
            //    //    w.day[i].DayId = (i + 1) + (j * 7);
            //    //    w.day[i].Name = "test";
            //    //}

            //    for (int i = 0; i < DateTime.DaysInMonth(todays.Year, todays.Month); i++)
            //    {
            //        w.day[i] = new Day();
            //        w.day[i].date = new DateTime(todays.Year, todays.Month, i + 1);
            //    }
            //    //Boolean hasDays = true;
            //    //int i = 0;
            //    //while(hasDays)
            //    //{
            //    //    w.day[i] = new Day();
            //    //    w.day[i].date = new DateTime(todays.Year, todays.Month, i +1)
            //    //}
            //        dat.Items.Add(w);
            //}
            
            


        }

        private void dat_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }
    }

    //public class Week
    //{
    //    public string[] day { get; set; }
    //    public Week()
    //    {
    //        day = new string[7];
    //    }

    //}


    
}
