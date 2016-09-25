namespace InterviewPrep.EncapsulatedDL.Entities
{
    public class Order: IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}