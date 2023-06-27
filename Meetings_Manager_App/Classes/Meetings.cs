using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Meetings_Manager_App.Classes
{
    public class Meetings
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string DateAndTime {
            get
            {
                return $"{Date} {Time}";
            }
        }
        public string Duration { get; set; }
        public string Guests { get; set; }
        public string Description { get; set; }
    }
}
