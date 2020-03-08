using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WorkingDaysApp.Data
{
    public static class Utility
    {
        public static bool IsValidHolidayConfig(HolidayConfiguration holidayConfiguration)
        {
            if (holidayConfiguration == null)
            {
                return false;
            }
            else if (!string.IsNullOrEmpty(holidayConfiguration.DayMonth))
            {
                return DateTime.TryParseExact($"{holidayConfiguration.DayMonth}/2020", "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime _);
            }
            else if (string.IsNullOrEmpty(holidayConfiguration.Occurrence))
            {
                return false;
            }
            else if (!string.IsNullOrEmpty(holidayConfiguration.Occurrence))
            {
                return holidayConfiguration.Occurrence.Split('|').Length == 3;
            }

            return true;
        }
    }
}
