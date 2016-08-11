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
        //2016, BMW , M4 GTS, 3.0, 6, 16, 23, 19
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public float Liters { get; set; }
        public int Cylinders { get; set; }
        public int City { get; set; }
        public int Highway { get; set; }
        public int Combined { get; set; }
    }
}
