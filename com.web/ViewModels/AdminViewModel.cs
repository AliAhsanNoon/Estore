using com.Entities;
using com.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.web.ViewModels
{
    public class AdminViewModel
    {
        public List<Order> Orders { get; set; }
        public string UserId { get;  set; }
        public Pager Pager { get;  set; }
        public string Status { get; set; }
    }

    public class AdminDetailsViewModel
    {
        public  Order Order { get; set; }
        public ApplicationUser OrderBy { get; set; }
        public List<string> OrderStatus { get; set; }
    }
}