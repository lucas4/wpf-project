using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project
{
    public class Note
    {
        public string id { get; set; }
        public DateTime date { get; set; }
        public string content { get; set; }
        public Note() { }
       
        public Note(DateTime date, string content)
        {
            this.id = Guid.NewGuid().ToString();
            this.date = date;
            this.content = content;

        }
    }
}
