using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project
{
    class Month
    {
        public int MonthID { get; set; }
        public int MonthYear { get; set; }
        public String Name { get; set; }
        public List<Week> Weeks { get; set; }
    }
}
