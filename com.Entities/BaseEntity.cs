using System.ComponentModel.DataAnnotations;

namespace com.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50), MinLength(3)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public bool isFeatured { get; set; }
        public string ImageURL { get; set; }
    }
}
