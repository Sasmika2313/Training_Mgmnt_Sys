using System;
using System.Collections.Generic;
using System.Text;

using TMS.Models;

namespace TMS.Sessions
{
    public abstract class CourseSession
    {
        public string CourseCode { get; set; }
        public DateTime StartDate { get; set; }
        public SessionDuration Duration { get; set; }

        protected CourseSession(
            string courseCode,
            DateTime startDate,
            SessionDuration duration)
        {
            CourseCode = courseCode;
            StartDate = startDate;
            Duration = duration;
        }

        public abstract decimal CalculateCharge();
    }
}
