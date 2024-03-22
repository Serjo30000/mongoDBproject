using MongoDB.Driver;
using mongoDBproject.IRepository;
using mongoDBproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Repository
{
    public class OrderBookRepository : IOrderBookRepository
    {
        private MongoClient _mongoClient = null;
        private IMongoDatabase _database = null;
        private IMongoCollection<OrderBook> _orderBookTable = null;
        private IMongoCollection<Order> _orderTable = null;
        private IMongoCollection<Book> _bookTable = null;
        public OrderBookRepository(string role)
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
            _orderBookTable = _database.GetCollection<OrderBook>("collectionOrderBook");
            _orderTable = _database.GetCollection<Order>("collectionOrder");
            _bookTable = _database.GetCollection<Book>("collectionBook");
        }
        public bool Delete(string orderBookId)
        {
            if (_orderBookTable.Find(x => x.Id == orderBookId).FirstOrDefault() == null)
            {
                return false;
            }
            else
            {
                _orderBookTable.DeleteOne(x => x.Id == orderBookId);
                return true;
            }
        }

        public OrderBook Get(string orderBookId)
        {
            return _orderBookTable.Find(x => x.Id == orderBookId).FirstOrDefault();
        }

        public List<OrderBook> GetList()
        {
            return _orderBookTable.Find(FilterDefinition<OrderBook>.Empty).ToList();
        }

        public OrderBook Save(OrderBook orderBook)
        {
            var userObj = _orderBookTable.Find(x => x.Id == orderBook.Id).FirstOrDefault();
            if (userObj == null)
            {
                _orderBookTable.InsertOne(orderBook);
            }
            else
            {
                _orderBookTable.ReplaceOne(x => x.Id == orderBook.Id, orderBook);
            }
            return orderBook;
        }
        public Order GetOrder(string orderId)
        {
            return _orderTable.Find(x => x.Id == orderId).FirstOrDefault();
        }
        public Book GetBook(string bookId)
        {
            return _bookTable.Find(x => x.Id == bookId).FirstOrDefault();
        }
        public List<OrderBook> SearchOrderBook(string searchString, string searchType)
        {
            List<OrderBook> orderBook = new List<OrderBook>();
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchType))
            {
                if (searchType.ToLower() == "id")
                {
                    orderBook = _orderBookTable.Find(s => s.Id == searchString).ToList();
                }
                if (searchType.ToLower() == "idbook")
                {
                    orderBook = _orderBookTable.Find(s => s.IdBook == searchString).ToList();
                }
                if (searchType.ToLower() == "mark" && bool.TryParse(searchString, out _))
                {
                    orderBook = _orderBookTable.Find(s => s.Mark == bool.Parse(searchString)).ToList();
                }
            }
            return orderBook;
        }

        public List<OrderBook> SortOrderBook(string sortOrderBook, List<OrderBook> orderBooks)
        {
            switch (sortOrderBook)
            {
                case "IdBook":
                    orderBooks = orderBooks.OrderBy(s => s.IdBook).Cast<OrderBook>().ToList();
                    break;
                case "idBook_desc":
                    orderBooks = orderBooks.OrderByDescending(s => s.IdBook).Cast<OrderBook>().ToList();
                    break;
                case "Mark":
                    orderBooks = orderBooks.OrderBy(s => s.Mark).Cast<OrderBook>().ToList();
                    break;
                case "mark_desc":
                    orderBooks = orderBooks.OrderByDescending(s => s.Mark).Cast<OrderBook>().ToList();
                    break;
                case "id_desc":
                    orderBooks = orderBooks.OrderByDescending(s => s.Id).Cast<OrderBook>().ToList();
                    break;
                default:
                    orderBooks = orderBooks.OrderBy(s => s.Id).Cast<OrderBook>().ToList();
                    break;
            }
            return orderBooks;
        }
    }
}
