using mongoDBproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.IRepository
{
    public interface IOrderRepository
    {
        Order Save(Order order);
        Order Get(string orderId);
        List<Order> GetList();
        bool Delete(string orderId);
        User GetUser(string userId);
        List<OrderBook> GetListOrderBook(string orderId);
    }
}
