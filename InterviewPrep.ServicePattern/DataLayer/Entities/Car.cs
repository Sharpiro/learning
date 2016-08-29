namespace InterviewPrep.ServicePattern.DataLayer.Entities
{
    public class Car
    {
        public Car()
        {

        }

        //public Car(string name, int manufacturerId)
        //{
        //    Name = name;
        //    ManufacturerId = manufacturerId;
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
