using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Models
{
    public class Course
    {
        public string CId { get; set; }
        public string Title { get; set; }
        public decimal Fee { get; set; }
        public int DurDay { get; set; }
        public int Tot { get; set; }
        public int EnCt { get; set; }

        public Course(string ce, string title, decimal fee,
                      int dur, int total)
        {
            CId = ce;
            Title = title;
            Fee = fee;
            DurDay = dur;
            Tot = total;
            EnCt = 0;
        }
    }
}