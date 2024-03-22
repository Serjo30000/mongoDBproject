using mongoDBproject.Models;
using mongoDBproject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Service
{
    public class OrderBookService
    {
        OrderBookRepository orderBookRepository = null;
        public OrderBookService(string role)
        {
            orderBookRepository = new OrderBookRepository(role);
        }

        public List<OrderBook> GetOrderBooks()
        {
            return orderBookRepository.GetList();
        }
        public List<OrderBook> GetSearchOrderBooks(string searchString, string searchType)
        {
            return orderBookRepository.SearchOrderBook(searchString, searchType);
        }
        public List<OrderBook> GetSortOrderBooks(string sortOrderBook, List<OrderBook> orderBooks)
        {
            return orderBookRepository.SortOrderBook(sortOrderBook, orderBooks);
        }
        public OrderBook GetOrderBook(string orderBookId)
        {
            return orderBookRepository.Get(orderBookId);
        }
        public Book GetOrder(string orderId)
        {
            return orderBookRepository.GetBook(orderId);
        }
        public Book GetBook(string bookId)
        {
            return orderBookRepository.GetBook(bookId);
        }

        public OrderBook AddOrderBook(OrderBook orderBook)
        {
            if (orderBook.Id == null)
            {
                return orderBookRepository.Save(orderBook);
            }
            else
            {
                return new OrderBook();
            }
        }
        public OrderBook UpdateOrderBook(OrderBook orderBook)
        {
            if (orderBookRepository.Get(orderBook.Id) != null)
            {
                return orderBookRepository.Save(orderBook);
            }
            else
            {
                return new OrderBook();
            }
        }

        public bool RemoveOrderBook(string orderBookId)
        {
            return orderBookRepository.Delete(orderBookId);
        }
    }
}
