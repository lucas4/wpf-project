using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project
{
    class Month
    {
        public Dictionary<int, string> MonthNames = new Dictionary<int, string> 
        { 
            {1, "Styczeń"},
            {2, "Luty"},
            {3, "Marzec"},
            {4, "Kwiecień"},
            {5, "Maj"},
            {6, "Czerwiec"},
            {7, "Lipiec"},
            {8, "Sierpień"},
            {9, "Wrzesień"},
            {10, "Październik"},
            {11, "Listopad"},
            {12, "Grudzień"}
        };

        public int MonthID { get; set; }
        public int MonthYear { get; set; }
        public String Name { get; set; }
        public List<Week> Weeks { get; set; }
        
        public Day getDay(int dayNum)
        {
            foreach (var week in Weeks)
            {
                foreach (var day in week.day)
                {
                    if(day != null)
                    {
                        if (day.date.Day == dayNum)
                            return day;
                    }
                }
            }

            return new Day();
        }
    }

    
}
