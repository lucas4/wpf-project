﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project
{
    public class EventDay
    {
        public string id { get; set; }
        public DateTime date { get; set; }
        public string TimeOfDay { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool alertMade { get; set; }

        public EventDay() { }
        
        public EventDay(DateTime date, string name, string description)
        {
            this.id = Guid.NewGuid().ToString();
            this.date = date;
            this.name = name;
            this.description = description;
            this.alertMade = false;
        }

        public EventDay(DateTime date, TimeSpan time, string name, string description)
        {
            this.id = Guid.NewGuid().ToString();
            this.date = date;
            this.date = this.date.Add(time);
            this.name = name;
            this.description = description;
            this.TimeOfDay = this.date.ToString("HH:mm");
            this.alertMade = false;
        }

    }
}
