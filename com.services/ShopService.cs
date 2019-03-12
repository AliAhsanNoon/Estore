using com.database;
using com.Entities;

namespace com.services
{
    public class ShopService
    {
        public static ShopService Instance
        {
            get
            {
                if (_instance == null) _instance = new ShopService();
                return _instance;
            }
        }

        private static ShopService _instance { get; set; }

        private ShopService() { }

        public int SaveOrders(Order order)
        {
            using (var _context = new CContext())
            {
                _context.Orders.Add(order);
                return _context.SaveChanges();
            }
        }
    }
}
