using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.Entities
{
    public class Order
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public DateTime OrderedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
