using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project
{
    class Day
    {
        public int DayId { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }

        public DateTime date { get; set; }

        public string Name { get; set; }

        public DayOfWeek DayName { get; set; }
        public List<String> notes { get; set; }

        public bool hasNotes { get; set; }
        public Day() { hasNotes = false; }
    }
}
