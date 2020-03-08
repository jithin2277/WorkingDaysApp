using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

namespace WorkingDaysApp.Data.Tests
{
    [TestClass]
    public class WorkingDaysTests
    {
        private Mock<IHolidayManager> _mockHolidayManager;
        private IWorkingdays _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHolidayManager = new Mock<IHolidayManager>(MockBehavior.Loose);
            _sut = new WorkingDays(_mockHolidayManager.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockHolidayManager.VerifyAll();
        }

        [TestMethod]
        public void GetWorkingDays_WhenPublicHolidayExists_ExcludesHolidayFromWorkingDays()
        {
            var fromDate = new DateTime(2014, 8, 13);
            var toDate = new DateTime(2014, 8, 21); ;
            var expected = 4;

            _mockHolidayManager.Setup(s => s.IsHoliday(new DateTime(2014, 8, 14))).Returns(true);
            var actual = _sut.GetWorkingDays(fromDate, toDate);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetWorkingDays_WhenNoPublicHoliday_ReturnsWorkingDays()
        {
            var fromDate = new DateTime(2014, 8, 13);
            var toDate = new DateTime(2014, 8, 21); ;
            var expected = 5;

            _mockHolidayManager.Setup(s => s.IsHoliday(It.IsAny<DateTime>())).Returns(false);
            var actual = _sut.GetWorkingDays(fromDate, toDate);

            Assert.AreEqual(expected, actual);
        }
    }
}
