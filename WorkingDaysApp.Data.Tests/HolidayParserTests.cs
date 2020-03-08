using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkingDaysApp.Data.Tests
{
    [TestClass]
    public class HolidayParserTests
    {
        private IHolidayParser _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new HolidayParser();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseHolidays_WhenHolidayConfigurationIsNull_ThrowsException()
        {
            _sut.ParseHolidays(null, 2020);
        }

        [TestMethod]
        public void ParseHolidays_WhenHolidayConfigurationIsValid_Succeeds()
        {
            var holidayConfigs = new List<HolidayConfiguration>
            {
                new HolidayConfiguration { DayMonth = "8/3", Description = It.IsAny<string>(), IsObserved = true, Occurrence = null },
                new HolidayConfiguration { DayMonth = "14/3", Description = It.IsAny<string>(), IsObserved = true, Occurrence = null },
                new HolidayConfiguration { DayMonth = "19/3", Description = It.IsAny<string>(), IsObserved = true, Occurrence = null },
                new HolidayConfiguration { DayMonth = "21/3", Description = It.IsAny<string>(), IsObserved = false, Occurrence = null },
                new HolidayConfiguration { DayMonth = "26/3", Description = It.IsAny<string>(), IsObserved = false, Occurrence = null },
                new HolidayConfiguration { DayMonth = null, Description = It.IsAny<string>(), IsObserved = false, Occurrence = "4|2|2" }
            };

            var actualDates = _sut.ParseHolidays(holidayConfigs, 2020).ToList();
            var expectedDates = new List<DateTime>() {
                new DateTime(2020,3,9),
                new DateTime(2020,3,16),
                new DateTime(2020,3,19),
                new DateTime(2020,3,21),
                new DateTime(2020,3,26),
                new DateTime(2020,4,13)
            };

            for (int i = 0; i < expectedDates.Count(); i++)
            {
                Assert.AreEqual(expectedDates[i], actualDates[i]);
            }
        }
    }
}
