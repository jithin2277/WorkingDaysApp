using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WorkingDaysApp.Data
{
    public interface IHolidayManager : IDisposable
    {
        bool IsHoliday(DateTime date);
    }

    public class HolidayManager : IHolidayManager
    {
        private IHolidayConfigurationRepository _holidayConfigurationRepository;
        private IHolidayParser _holidayParser;

        public HolidayManager(IHolidayConfigurationRepository holidayConfigurationRepository, IHolidayParser holidayParser)
        {
            _holidayConfigurationRepository = holidayConfigurationRepository ?? throw new ArgumentNullException(nameof(holidayConfigurationRepository));
            _holidayParser = holidayParser ?? throw new ArgumentNullException(nameof(holidayParser));
        }

        public bool IsHoliday(DateTime date)
        {
            var holidays = _holidayParser.ParseHolidays(_holidayConfigurationRepository.GetHolidayConfigurations(), date.Year);
            return holidays.Contains(date);
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_holidayConfigurationRepository != null)
                    {
                        _holidayConfigurationRepository.Dispose();
                        _holidayConfigurationRepository = null;
                    }
                    if (_holidayParser != null)
                    {
                        _holidayParser = null;
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
