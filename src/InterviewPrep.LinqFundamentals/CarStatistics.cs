using System;
using InterviewPrep.LinqFundamentals.Models;

namespace InterviewPrep.LinqFundamentals
{
    public class CarStatistics
    {
        public int Total { get; set; }
        public int Count { get; set; }
        public double Average { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }

        public CarStatistics()
        {
            Max = int.MinValue;
            Min = int.MaxValue;
        }

        public CarStatistics Next(Car car)
        {
            Count++;
            Total += car.Combined;
            Min = Math.Min(Min, car.Combined);
            Max = Math.Max(Max, car.Combined);
            return this;
        }

        public CarStatistics Compute()
        {
            Average = Total / Count;
            return this;
        }
    }
}
