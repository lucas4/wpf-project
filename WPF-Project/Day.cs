﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project
{
    public class Day
    {
        public DateTime date { get; set; }

        public String notes { get; set; }

        public bool hasNotes { get; set; }
        public bool hasEvents { get; set; }
        public Day() { hasNotes = false; hasEvents = false; }
    }
}
