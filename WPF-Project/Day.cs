using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project
{
    public class Day
    {
        public DateTime date { get; set; }
        public bool hasNotes { get; set; }
        public bool hasEvents { get; set; }
        public Day() { hasNotes = false; hasEvents = false; }
        public Day(bool hasNotes, bool hasEvents)
        {
            this.hasNotes = hasNotes;
            this.hasEvents = hasEvents;
        }
    }

    public class DayJson
    {
        public DateTime date;
        public List<Note> noteList;
        public List<EventDay> eventsList;

        public DayJson(DateTime date, List<Note> note, List<EventDay> events)
        {
            this.date = date;
            this.noteList = note;
            this.eventsList = events;
        }
    }
}
