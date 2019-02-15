using System.ComponentModel.DataAnnotations;

namespace com.Entities
{
    public class EStoreConfiguration
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
