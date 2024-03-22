using MongoDB.Bson;
using mongoDBproject.Models;
using mongoDBproject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Service
{
    public class OrderService
    {
        OrderRepository orderRepository = null;
        public OrderService(string role)
        {
            orderRepository = new OrderRepository(role);
        }

        public List<Order> GetOrders()
        {
            return orderRepository.GetList();
        }
        public List<Order> GetSearchOrders(string searchString, string searchType)
        {
            return orderRepository.SearchOrder(searchString, searchType);
        }
        public List<Order> GetSortOrders(string sortOrder, List<Order> books)
        {
            return orderRepository.SortOrder(sortOrder, books);
        }
        public List<BsonDocument> GetListAggregateOrders()
        {
            return orderRepository.ListAggregateOrders();
        }
        public List<OrderBook> GetOrderBooks(string orderId)
        {
            return orderRepository.GetListOrderBook(orderId);
        }
        public Order GetOrder(string orderId)
        {
            return orderRepository.Get(orderId);
        }
        public User GetUser(string userId)
        {
            return orderRepository.GetUser(userId);
        }

        public Order AddOrder(Order order)
        {
            if (order.Id == null)
            {
                return orderRepository.Save(order);
            }
            else
            {
                return new Order();
            }
        }
        public Order UpdateOrder(Order order)
        {
            if (orderRepository.Get(order.Id) != null)
            {
                return orderRepository.Save(order);
            }
            else
            {
                return new Order();
            }
        }

        public bool RemoveOrder(string orderId)
        {
            return orderRepository.Delete(orderId);
        }
    }
}
