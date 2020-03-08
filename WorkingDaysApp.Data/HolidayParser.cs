using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace WorkingDaysApp.Data
{
    public interface IHolidayParser
    {
        IEnumerable<DateTime> ParseHolidays(IList<HolidayConfiguration> holidayConfigs, int year);
    }

    public class HolidayParser : IHolidayParser
    {
        public IEnumerable<DateTime> ParseHolidays(IList<HolidayConfiguration> holidayConfigs, int year)
        {
            if (holidayConfigs == null)
            {
                throw new ArgumentNullException(nameof(holidayConfigs));
            }
            
            return holidayConfigs.Select(s => GetHoliday(s, year));
        }

        private DateTime GetHoliday(HolidayConfiguration holidayConfiguration, int year)
        {
            if (!Utility.IsValidHolidayConfig(holidayConfiguration))
            {
                throw new InvalidOperationException("Invalid holiday configurations");
            }

            var holidayDate = new DateTime();
            if (!string.IsNullOrEmpty(holidayConfiguration.DayMonth))
            {
                holidayDate = GetFixedHoliday(holidayConfiguration, year);
            }
            else if (!string.IsNullOrEmpty(holidayConfiguration.Occurrence))
            {
                holidayDate = GetVariableHoliday(holidayConfiguration, year);
            }

            return holidayDate;
        }

        private DateTime GetFixedHoliday(HolidayConfiguration holidayConfiguration, int year)
        {
            var holidayDate = DateTime.ParseExact($"{holidayConfiguration.DayMonth}/{year}", "d/M/yyyy", CultureInfo.InvariantCulture);

            if (holidayConfiguration.IsObserved)
            {
                if (holidayDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    holidayDate = holidayDate.AddDays(2);
                }
                else if (holidayDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    holidayDate = holidayDate.AddDays(1);
                }
            }

            return holidayDate;
        }

        private DateTime GetVariableHoliday(HolidayConfiguration holidayConfiguration, int year)
        {
            var occurrenceFormat = holidayConfiguration.Occurrence.Split('|');
            int month = int.Parse(occurrenceFormat[0]);
            DayOfWeek dayOfWeek = DayOfWeek.Monday;
            int occurrence = int.Parse(occurrenceFormat[2]);

            return Enumerable.Range(1, 7)
                    .Select(day => new DateTime(year, month, day))
                    .First(dateTime => (dateTime.DayOfWeek == dayOfWeek))
                    .AddDays(7 * (occurrence - 1));
        }
    }
}
