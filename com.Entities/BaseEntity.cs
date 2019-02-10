namespace com.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isFeatured { get; set; }
        public string ImageURL { get; set; }
    }
}
