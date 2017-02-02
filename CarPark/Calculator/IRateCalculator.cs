using CarPark.Model;
using System;

namespace CarPark.Calculator
{
    public interface IRateCalculator
    {
        RateResult GetRate(DateTime entryTime, DateTime exitTime);
    }
}
