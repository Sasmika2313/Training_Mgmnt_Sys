using System;
using System.Collections.Generic;
using System.Text;

using System;
using TMS.Models;

namespace TMS.Sessions
{
    public class ClassroomSession : CourseSession
    {
        public ClassroomSession(
            string courseCode,
            DateTime startDate,
            SessionDuration duration)
            : base(courseCode, startDate, duration)
        {
        }

        public override decimal CalculateCharge()
        {
            return 800m;
        }
    }
}
