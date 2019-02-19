using System.ComponentModel.DataAnnotations;

namespace com.Entities
{
    public class Product : BaseEntity
    {
        [Range(1,10000)]
        public decimal Price { get; set; }

        public Category Category { get; set; }
    }
}