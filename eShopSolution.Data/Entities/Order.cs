using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Entities
{
    public class Order
    {
        public int Id { set; get; }
        public DateTime OrderDate { set; get; }
        //public string cartid { set; get; }
        public Guid UserId { set; get; }
        public string ShipAddress { set; get; }
        public string ShipEmail { set; get; }
        public string ShipPhoneNumber { set; get; }
        public Status Status { set; get; }

        // Tao quan he 1-n (OrderDetail la so nhieu)
        public List<OrderDetail> OrderDetails { get; set; }
        public AppUser AppUser { get; set; }


    }
}
