using System;
using CarPark.Model;

namespace CarPark.Calculator
{
    public class RateCalculator : IRateCalculator
    {
        /*
         * These values should ideally be configurable.
         */
        private const decimal RATE_EARLYBIRD = 13.0m;
        private const decimal RATE_NIGHT = 6.50m;
        private const decimal RATE_WEEKEND = 10m;
        private const decimal RATE_HOURLY_CAP = 20m;

        private const string TIME_EARLYBIRD_ENTRY_FROM = "6:00";
        private const string TIME_EARLYBIRD_ENTRY_TO = "9:00";
        private const string TIME_EARLYBIRD_EXIT_FROM = "15:30";
        private const string TIME_EARLYBIRD_EXIT_TO = "23:30";

        private const string TIME_NIGHT_FROM = "18:00";
        private const string TIME_NIGHT_TO = "6:00";

        public RateResult GetRate(DateTime entryTime, DateTime exitTime)
        {
            // EarlyBird Rate
            // Same day, earlybird times
            if(entryTime.Date == exitTime.Date
                && entryTime.TimeOfDay > TimeSpan.Parse(TIME_EARLYBIRD_ENTRY_FROM)
                && entryTime.TimeOfDay < TimeSpan.Parse(TIME_EARLYBIRD_ENTRY_TO)
                && exitTime.TimeOfDay > TimeSpan.Parse(TIME_EARLYBIRD_EXIT_FROM)
                && exitTime.TimeOfDay < TimeSpan.Parse(TIME_EARLYBIRD_EXIT_TO))
            {
                return new RateResult
                {
                    Rate = RATE_EARLYBIRD,
                    RateType = RateType.EarlyBird
                };
            }

            // Night Rate
            // 1 night, night times
            if (exitTime.Day - entryTime.Day == 1
                && entryTime.TimeOfDay > TimeSpan.Parse(TIME_NIGHT_FROM)
                && exitTime.TimeOfDay < TimeSpan.Parse(TIME_NIGHT_TO))
            {
                return new RateResult
                {
                    Rate = RATE_NIGHT,
                    RateType = RateType.NightRate
                };
            }

            // Weekend Rate
            // enter pass Friday midnight
            // exit before Sunday midnight
            if(entryTime.DayOfWeek > DayOfWeek.Friday
                && exitTime.DayOfWeek < DayOfWeek.Monday)
            {
                return new RateResult
                {
                    Rate = RATE_WEEKEND,
                    RateType = RateType.WeekendRate
                };
            }

            /*
             * Hourley Rate is 5 times number of hours but maximum 20.
             **/
            var timeSpan = exitTime - entryTime;
            var psudoResult = (decimal)(Math.Ceiling(timeSpan.TotalHours)) * 5;
            
            return new RateResult
            {
                Rate = Math.Min(psudoResult, RATE_HOURLY_CAP),
                RateType = RateType.StandardRate
            };
        }
    }
}
