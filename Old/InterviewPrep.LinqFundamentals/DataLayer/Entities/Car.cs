namespace InterviewPrep.LinqFundamentals.DataLayer.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public float Liters { get; set; }
        public int Cylinders { get; set; }
        public int City { get; set; }
        public int Highway { get; set; }
        public int Combined { get; set; }
    }
}