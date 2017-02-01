using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPark
{
    public class RateCalculator : IRateCalculator
    {
        public RateResult GetRate(DateTime entryTime, DateTime exitTime)
        {
            // EarlyBird Rate
            // Same day, earlybird times
            if(entryTime.Date == exitTime.Date
                && entryTime.TimeOfDay > TimeSpan.Parse("6:00")
                && entryTime.TimeOfDay < TimeSpan.Parse("9:00")
                && exitTime.TimeOfDay > TimeSpan.Parse("15:30")
                && exitTime.TimeOfDay < TimeSpan.Parse("23:30"))
            {
                return new RateResult
                {
                    Rate = 13.0m,
                    RateType = RateType.EarlyBird
                };
            }

            // Night Rate
            // 1 night, night times
            if (exitTime.Day - entryTime.Day == 1
                && entryTime.TimeOfDay > TimeSpan.Parse("18:00")
                && exitTime.TimeOfDay < TimeSpan.Parse("6:00"))
            {
                return new RateResult
                {
                    Rate = 6.50m,
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
                    Rate = 10m,
                    RateType = RateType.WeekendRate
                };
            }

            // Hourly Rate
            // number of hours
            var timeSpan = exitTime - entryTime;
            var psudoResult = (decimal)(Math.Ceiling(timeSpan.TotalHours)) * 5;
            
            return new RateResult
            {
                Rate = Math.Min(psudoResult, 20m),
                RateType = RateType.StandardRate
            };
        }
    }
}
