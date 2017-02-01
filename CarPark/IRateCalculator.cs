using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPark
{
    public interface IRateCalculator
    {
        RateResult GetRate(DateTime entryTime, DateTime exitTime);
    }
}
