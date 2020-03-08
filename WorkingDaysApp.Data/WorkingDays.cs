using System;
using System.Linq;

namespace WorkingDaysApp.Data
{
    public interface IWorkingdays : IDisposable
    {
        int GetWorkingDays(DateTime fromDate, DateTime toDate);
    }

    public class WorkingDays : IWorkingdays
    {
        private IHolidayManager _holidayManager;

        public WorkingDays(IHolidayManager holidayManager)
        {
            _holidayManager = holidayManager ?? throw new ArgumentNullException(nameof(holidayManager));
        }

        public int GetWorkingDays(DateTime fromDate, DateTime toDate)
        {
            // Get Difference between dates excluding from and to dates
            var dayDifference = (int)toDate.Subtract(fromDate).TotalDays - 1;

            if (dayDifference > 0)
            {
                return Enumerable
                    .Range(1, dayDifference) // Gets a list of integral numbers between the dates
                    .Select(x => fromDate.AddDays(x)) // Converts the integral numbers to dates from from date
                    .Count(x => x.DayOfWeek != DayOfWeek.Saturday && x.DayOfWeek != DayOfWeek.Sunday && !_holidayManager.IsHoliday(x)); // Exclude dates which are weekends and holidays
            }

            throw new InvalidOperationException("toDate should be greater than fromDate");
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_holidayManager != null)
                    {
                        _holidayManager.Dispose();
                        _holidayManager = null;
                    }
                }

                disposedValue = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
