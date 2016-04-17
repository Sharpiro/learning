namespace InterviewPrep.OdeToFoodCore.Entities
{
    public interface IBaseEntity
    {
        int Id { get; set; }
    }

    public class Restaurant : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; }
    }

    public enum CuisineType
    {
        None,
        Italian,
        French,
        American
    }
}
