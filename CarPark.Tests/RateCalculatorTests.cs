using CarPark.Calculator;
using CarPark.Model;
using NUnit.Framework;
using System;

namespace CarPark.Tests
{
    [TestFixture]
    public class RateCalculatorTests
    {
        private readonly RateCalculator _rateCalculator;

        public RateCalculatorTests()
        {
            _rateCalculator = new RateCalculator();
        }

        [Test]
        [TestCase("2017-01-18T06:30:00", "2017-01-18T16:30:00")]
        [TestCase("2017-01-18T08:59:00", "2017-01-18T15:31:00")]
        public void ShouldRateEarlyBird_WhenEarlyBirdTimesMatch(string entryTimeStr, string exitTimeStr)
        {
            // Arrange
            var entryTime = DateTime.Parse(entryTimeStr);
            var exitTime = DateTime.Parse(exitTimeStr);
            
            // Act
            var rateResult = _rateCalculator.GetRate(entryTime, exitTime);

            // Assert
            Assert.AreEqual(13.0m, rateResult.Rate);
            Assert.AreEqual(RateType.EarlyBird, rateResult.RateType);
        }

        [Test]
        [TestCase("2017-01-18T18:30:00", "2017-01-19T05:30:00")]
        public void ShouldRateNightRate_WhenNightTimesMatch(string entryTimeStr, string exitTimeStr)
        {
            // Arrange
            var entryTime = DateTime.Parse(entryTimeStr);
            var exitTime = DateTime.Parse(exitTimeStr);

            // Act
            var rateResult = _rateCalculator.GetRate(entryTime, exitTime);

            // Assert
            Assert.AreEqual(6.50m, rateResult.Rate);
            Assert.AreEqual(RateType.NightRate, rateResult.RateType);
        }

        [Test]
        [TestCase("2017-01-21T00:30:00", "2017-01-22T12:30:00")]
        public void ShouldRateWeekendRate_WhenWeekendTimesMatch(string entryTimeStr, string exitTimeStr)
        {
            // Arrange
            var entryTime = DateTime.Parse(entryTimeStr);
            var exitTime = DateTime.Parse(exitTimeStr);

            // Act
            var rateResult = _rateCalculator.GetRate(entryTime, exitTime);

            // Assert
            Assert.AreEqual(10.0m, rateResult.Rate);
            Assert.AreEqual(RateType.WeekendRate, rateResult.RateType);
        }

        [Test]
        public void ShouldRateNightRate_WhenNightRateAppliesOnASaturdayMorning()
        {
            // Arrange
            // Enter 10 pm on Friday, Exit 5 am on Saturday.
            var entryTime = DateTime.Parse("2017-01-20T22:00:00");
            var exitTime = DateTime.Parse("2017-01-21T05:00:00");

            // Act
            var rateResult = _rateCalculator.GetRate(entryTime, exitTime);

            // Assert
            Assert.AreEqual(6.50m, rateResult.Rate);
            Assert.AreEqual(RateType.NightRate, rateResult.RateType);
        }

        [Test]
        [TestCase("2017-01-18T10:00:00", "2017-01-18T10:30:00", 5)]
        [TestCase("2017-01-18T10:00:00", "2017-01-18T11:30:00", 10)]
        [TestCase("2017-01-18T10:00:00", "2017-01-18T12:30:00", 15)]
        [TestCase("2017-01-18T10:00:00", "2017-01-18T13:30:00", 20)]
        [TestCase("2017-01-18T08:59:00", "2017-01-18T15:29:00", 20)]
        public void ShouldRateStandardRate_WhenStandardTimesMatch(string entryTimeStr, string exitTimeStr, decimal expectedRate)
        {
            // Arrange
            var entryTime = DateTime.Parse(entryTimeStr);
            var exitTime = DateTime.Parse(exitTimeStr);

            // Act
            var rateResult = _rateCalculator.GetRate(entryTime, exitTime);

            // Assert
            Assert.AreEqual(expectedRate, rateResult.Rate);
            Assert.AreEqual(RateType.StandardRate, rateResult.RateType);
        }
    }
}
