using MongoDB.Bson;
using MongoDB.Driver;
using mongoDBproject.IRepository;
using mongoDBproject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private MongoClient _mongoClient = null;
        private IMongoDatabase _database = null;
        private IMongoCollection<Order> _orderTable = null;
        private IMongoCollection<User> _userTable = null;
        private IMongoCollection<OrderBook> _orderBookTable = null;
        public OrderRepository(string role)
        {
            if (role == "admin")
            {
                _mongoClient = new MongoClient("mongodb://userAdmin1:123456@localhost:27017/?authMechanism=SCRAM-SHA-256");
            }
            else if (role == "librarian")
            {
                _mongoClient = new MongoClient("mongodb://userLibrarian1:123456@localhost:27017/?authMechanism=SCRAM-SHA-256");
            }
            else if (role == "client")
            {
                _mongoClient = new MongoClient("mongodb://userClient1:123456@localhost:27017/?authMechanism=SCRAM-SHA-256");
            }
            else
            {
                _mongoClient = new MongoClient("mongodb://localhost:27017");
            }
            _database = _mongoClient.GetDatabase("mongoDBproject");
            _orderTable = _database.GetCollection<Order>("collectionOrder");
            _userTable = _database.GetCollection<User>("collectionUser");
            _orderBookTable = _database.GetCollection<OrderBook>("collectionOrderBook");
        }
        public bool Delete(string orderId)
        {
            if (_orderTable.Find(x => x.Id == orderId).FirstOrDefault() == null)
            {
                return false;
            }
            else
            {
                _orderTable.DeleteOne(x => x.Id == orderId);
                return true;
            }
        }

        public Order Get(string orderId)
        {
            return _orderTable.Find(x => x.Id == orderId).FirstOrDefault();
        }

        public List<Order> GetList()
        {
            return _orderTable.Find(FilterDefinition<Order>.Empty).ToList();
        }

        public Order Save(Order order)
        {
            var userObj = _orderTable.Find(x => x.Id == order.Id).FirstOrDefault();
            if (userObj == null)
            {
                _orderTable.InsertOne(order);
            }
            else
            {
                _orderTable.ReplaceOne(x => x.Id == order.Id, order);
            }
            return order;
        }
        public User GetUser(string userId)
        {
            return _userTable.Find(x => x.Id == userId).FirstOrDefault();
        }
        public List<OrderBook> GetListOrderBook(string bookId)
        {
            List<OrderBook> lsrOr = new List<OrderBook>();
            foreach (OrderBook or in _orderBookTable.Find(FilterDefinition<OrderBook>.Empty).ToList())
            {
                if (or.IdBook == bookId)
                {
                    lsrOr.Add(or);
                }
            }
            return lsrOr;
        }

        public List<Order> SearchOrder(string searchString, string searchType)
        {
            List<Order> order = new List<Order>();
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchType))
            {
                if (searchType.ToLower() == "id")
                {
                    order = _orderTable.Find(s => s.Id == searchString).ToList();
                }
                if (searchType.ToLower() == "iduser")
                {
                    order = _orderTable.Find(s => s.IdUser == searchString).ToList();
                }
                if (searchType.ToLower() == "dateorder" && DateTime.TryParse(searchString, out _))
                {
                    order = _orderTable.Find(s => s.DateOrder == DateTime.Parse(searchString)).ToList();
                }
                if (searchType.ToLower() == "countorder" && int.TryParse(searchString, out _))
                {
                    order = _orderTable.Find(s => s.CountOrder == int.Parse(searchString)).ToList();
                }
                if (searchType.ToLower() == "idorderbook")
                {
                    order = _orderTable.Find(s => s.IdOrderBook == searchString).ToList();
                }
                if (searchType.ToLower() == "idbook")
                {
                    order = _orderTable.Find(s => s.IdBook == searchString).ToList();
                }
            }
            return order;
        }

        public List<Order> SortOrder(string sortOrder, List<Order> orders)
        {
            switch (sortOrder)
            {
                case "IdUser":
                    orders = orders.OrderBy(s => s.IdUser).Cast<Order>().ToList();
                    break;
                case "idUser_desc":
                    orders = orders.OrderByDescending(s => s.IdUser).Cast<Order>().ToList();
                    break;
                case "DateOrder":
                    orders = orders.OrderBy(s => s.DateOrder).Cast<Order>().ToList();
                    break;
                case "dateOrder_desc":
                    orders = orders.OrderByDescending(s => s.DateOrder).Cast<Order>().ToList();
                    break;
                case "CountOrder":
                    orders = orders.OrderBy(s => s.CountOrder).Cast<Order>().ToList();
                    break;
                case "countOrder_desc":
                    orders = orders.OrderByDescending(s => s.CountOrder).Cast<Order>().ToList();
                    break;
                case "IdOrderBook":
                    orders = orders.OrderBy(s => s.IdOrderBook).Cast<Order>().ToList();
                    break;
                case "idOrderBook_desc":
                    orders = orders.OrderByDescending(s => s.IdOrderBook).Cast<Order>().ToList();
                    break;
                case "IdBook":
                    orders = orders.OrderBy(s => s.IdBook).Cast<Order>().ToList();
                    break;
                case "idBook_desc":
                    orders = orders.OrderByDescending(s => s.IdBook).Cast<Order>().ToList();
                    break;
                case "id_desc":
                    orders = orders.OrderByDescending(s => s.Id).Cast<Order>().ToList();
                    break;
                default:
                    orders = orders.OrderBy(s => s.Id).Cast<Order>().ToList();
                    break;
            }
            return orders;
        }

        public List<BsonDocument> ListAggregateOrders()
        {
            var orders = _orderTable.Aggregate().Group(new BsonDocument {
                { "_id", "$_idBook" },
                { "avgCountOrder",new BsonDocument("$avg", "$countOrder")},
                { "lastDateOrder",new BsonDocument("$max", "$dateOrder")},
                { "fieldOrder", new BsonDocument("$push",  new BsonDocument{{ "countOrder", "$countOrder" },{ "dateOrder", "$dateOrder" } }) }
            }).ToList();
            return orders;
        }
    }
}
