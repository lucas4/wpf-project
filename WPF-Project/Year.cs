using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project
{
    public class Year
    {
        public int YearID { get; set; }
        public Dictionary<int, Month> monthsDict;

        public Year()
        {
            monthsDict = new Dictionary<int, Month>();
        }

        public Year(int id)
        {
            YearID = id;
            monthsDict = new Dictionary<int, Month>();
        }
    }

}
