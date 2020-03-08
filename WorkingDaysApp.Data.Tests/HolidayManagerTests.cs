using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using System.Collections.Generic;

namespace WorkingDaysApp.Data.Tests
{
    [TestClass]
    public class HolidayManagerTests
    {
        private Mock<IHolidayConfigurationRepository> _mockHolidayConfigurationRepository;
        private Mock<IHolidayParser> _mockHolidayParser;
        private IHolidayManager _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHolidayConfigurationRepository = new Mock<IHolidayConfigurationRepository>(MockBehavior.Strict);
            _mockHolidayParser = new Mock<IHolidayParser>(MockBehavior.Strict);
            _sut = new HolidayManager(_mockHolidayConfigurationRepository.Object, _mockHolidayParser.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockHolidayConfigurationRepository.VerifyAll();
            _mockHolidayParser.VerifyAll();
        }


        [TestMethod]
        public void IsHoliday_WhenValidDate_Succeeds()
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
            var dates = new List<DateTime>() {
                new DateTime(2020,3,9),
                new DateTime(2020,3,16),
                new DateTime(2020,3,19),
                new DateTime(2020,3,21),
                new DateTime(2020,3,26),
                new DateTime(2020,4,13)
            };

            _mockHolidayConfigurationRepository.Setup(s => s.GetHolidayConfigurations()).Returns(holidayConfigs);
            _mockHolidayParser.Setup(s => s.ParseHolidays(holidayConfigs, 2020)).Returns(dates);

            Assert.IsTrue(_sut.IsHoliday(new DateTime(2020, 3, 9)));
            Assert.IsFalse(_sut.IsHoliday(new DateTime(2020, 10, 9)));
        }
    }
}
