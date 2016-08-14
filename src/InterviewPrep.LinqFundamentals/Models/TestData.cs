using System.Diagnostics;

namespace InterviewPrep.LinqFundamentals.Models
{
    public class TestData
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
    }

    public class SwappableObject
    {
        public int Value1 { get; set; }
        public int Value2 { get; set; }
    }

    public class Movie
    {
        public string Title { get; set; }
        public float Rating { get; set; }
        public int Year { get; set; }
    }

    public class EcoEntry
    {
        private string _make;
        public string Make
        {
            get { Debug.WriteLine($"{_make} was accessed"); return _make; }
            set { _make = value; }
        }
        public int Year { get; set; }
        public string Model { get; set; }
        public float Liters { get; set; }
        public int Cylinders { get; set; }
        public int City { get; set; }
        public int Highway { get; set; }
        public int Combined { get; set; }
    }
}
