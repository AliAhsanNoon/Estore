namespace com.Entities
{
    public class Product : BaseEntity
    {
        public decimal Price { get; set; }

        public Category Category { get; set; }
    }
}