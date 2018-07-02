using System.Collections.Generic;

namespace InterviewPrep.ServicePattern.DataLayer.Entities
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Car> Cars { get; set; }

        public Manufacturer()
        {
            Cars = new List<Car>();
        }

        public void AddCar(Car newCar)
        {
            Cars.Add(newCar);
        }
    }
}