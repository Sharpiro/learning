using System.Collections.Generic;

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

    public class CarModel
    {
        private string _make;
        private int _combined;
        public string Make
        {
            get { /*Debug.WriteLine($"{_make} was accessed");*/ return _make; }
            set { _make = value; }
        }
        public int Year { get; set; }
        public string Model { get; set; }
        public float Liters { get; set; }
        public int Cylinders { get; set; }
        public int City { get; set; }
        public int Highway { get; set; }
        public int Combined
        {
            get { /*Debug.WriteLine($"{_combined} was accessed");*/ return _combined; }
            set { _combined = value; }
        }

        public override string ToString()
        {
            var data = $"{Year},{Make},{Model},{Liters},{Cylinders},{City},{Highway},{Combined}";
            return data;
        }
    }

    public class Manufacturer
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int Year { get; set; }
    }

    public class CarHolder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Passenger> Passengers { get; set; }
    }

    public class Passenger
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
