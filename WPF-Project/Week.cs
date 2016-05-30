using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project
{
    class Week
    {
        //public string[] day { get; set; }
        public Day[] day { get; set; }
        public Week()
        {
            day = new Day[7];
        }

    }
}
