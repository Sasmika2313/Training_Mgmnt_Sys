using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Models
{
    public record CourseSummary(
        string CourseCode,
        string Title,
        decimal Fee,
        int AvailableSeats);
}