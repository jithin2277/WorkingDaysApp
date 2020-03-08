using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WorkingDaysApp.Data
{
    public interface IHolidayConfigurationRepository : IDisposable
    {
        IList<HolidayConfiguration> GetHolidayConfigurations();
    }

    public class HolidayConfigurationRepository : IHolidayConfigurationRepository
    {
        private ISerializer _serializer;

        public HolidayConfigurationRepository(ISerializer serializer)
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public IList<HolidayConfiguration> GetHolidayConfigurations()
        {
            using (var reader = new StreamReader("holidayConfig.json"))
            {
                string json = reader.ReadToEnd();

                return _serializer.DeSerialize<List<HolidayConfiguration>>(json);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_serializer != null)
                    {
                        _serializer = null;
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
