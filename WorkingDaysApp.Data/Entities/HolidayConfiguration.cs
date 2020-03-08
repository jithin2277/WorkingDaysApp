using System;
using System.Collections.Generic;
using System.Text;

namespace WorkingDaysApp.Data
{
    public class HolidayConfiguration
    {
        public string DayMonth { get; set; }
        public string Description { get; set; }
        public bool IsObserved { get; set; }
        public string Occurrence { get; set; }
    }
}
