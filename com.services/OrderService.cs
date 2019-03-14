using com.database;
using com.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace com.services
{
    public class OrderService
    {
        public static OrderService Instance
        {
            get
            {
                if (instance == null) instance = new OrderService();
                return instance;
            }
        }

        private static OrderService instance { get; set; }

        private OrderService() { }

        public List<Order> SearchOrders(string userId, string status, int pageNo, int pageSize)
        {
            using (var _context = new CContext())
            {
                var _Orders = _context.Orders.ToList();

                if (!string.IsNullOrEmpty(userId))
                {
                    _Orders = _Orders.Where(x => x.UserId.ToLower().Contains(userId.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(status))
                {
                    _Orders = _Orders.Where(x => x.Status.ToLower().Contains(status.ToLower())).ToList();
                }

                return _Orders.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int SearchOrdersCount(string userId, string status)
        {
            using (var _context = new CContext())
            {
                var _Orders = _context.Orders.ToList();

                if (!string.IsNullOrEmpty(userId))
                {
                    _Orders = _Orders.Where(x => x.UserId.ToLower().Contains(userId.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(status))
                {
                    _Orders = _Orders.Where(x => x.Status.ToLower().Contains(status.ToLower())).ToList();
                }

                return _Orders.Count;
            }
        }

        public Order GetOrderById(int Id)
        {
            using (var _context = new CContext())
            {
                return _context.Orders.Where(x => x.ID == Id).Include(x => x.OrderItems).Include("OrderItems.Product").FirstOrDefault();
            }
        }

        public bool UpdateOrderStatus(int id, string status)
        {
            using (var context = new CContext())
            {
                var order = context.Orders.Find(id);

                order.Status = status;

                context.Entry(order).State = EntityState.Modified;

                return context.SaveChanges() > 0;
            }
        }
    }
}
