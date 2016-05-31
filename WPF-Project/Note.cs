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
        public string shortContent { get; set; }
        public Note() { }
       
        public Note(DateTime date, string content)
        {
            this.id = Guid.NewGuid().ToString();
            this.date = date;
            this.content = content;
            if (content.Length < 20)
            {
                this.shortContent = content.Substring(0, (content.Length / 2)) + "...";
            }
            else
            {
                this.shortContent = content.Substring(0, 20) + "...";
            }

        }

        public void changeContent(string content)
        {
            this.content = content;
            if (content.Length < 20)
            {
                this.shortContent = content.Substring(0, (content.Length / 2)) + "...";
            }
            else
            {
                this.shortContent = content.Substring(0, 20) + "...";
            }
        }
    }
}
