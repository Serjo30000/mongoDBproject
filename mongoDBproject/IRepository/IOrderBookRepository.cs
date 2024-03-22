using mongoDBproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.IRepository
{
    interface IOrderBookRepository
    {
        OrderBook Save(OrderBook orderBook);
        OrderBook Get(string orderBookId);
        List<OrderBook> GetList();
        bool Delete(string orderBookId);
        Order GetOrder(string orderId);
        Book GetBook(string bookId);
    }
}
