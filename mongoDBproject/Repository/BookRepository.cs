using MongoDB.Bson;
using MongoDB.Driver;
using mongoDBproject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Repository
{
    public class BookRepository : IBookRepository
    {
        private MongoClient _mongoClient = null;
        private IMongoDatabase _database = null;
        private IMongoCollection<Book> _bookTable = null;
        private IMongoCollection<Genre> _genreTable = null;
        private IMongoCollection<OrderBook> _orderBookTable = null;
        public BookRepository(string role)
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
            //_mongoClient = new MongoClient("mongodb://localhost:27017");
            _database = _mongoClient.GetDatabase("mongoDBproject");
            _bookTable = _database.GetCollection<Book>("collectionBook");
            _genreTable = _database.GetCollection<Genre>("collectionGenre");
            _orderBookTable = _database.GetCollection<OrderBook>("collectionOrderBook");
        }
        public bool Delete(string bookId)
        {
            if (_bookTable.Find(x => x.Id == bookId).FirstOrDefault() == null)
            {
                return false;
            }
            else
            {
                _bookTable.DeleteOne(x => x.Id == bookId);
                return true;
            }
        }

        public Book Get(string bookId)
        {
            return _bookTable.Find(x => x.Id == bookId).FirstOrDefault();
        }

        public List<Book> GetList()
        {
            return _bookTable.Find(FilterDefinition<Book>.Empty).ToList();
        }
        public List<Genre> GetListGenre(string bookId)
        {
            List<Genre> lsrG = new List<Genre>();
            foreach (Genre genr in _genreTable.Find(FilterDefinition<Genre>.Empty).ToList())
            {
                if (genr.IdBook == bookId)
                {
                    lsrG.Add(genr);
                }
            }
            return lsrG;
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
        public Book Save(Book book)
        {
            var bookObj = _bookTable.Find(x => x.Id == book.Id).FirstOrDefault();
            if (bookObj == null)
            {
                _bookTable.InsertOne(book);
            }
            else
            {
                _bookTable.ReplaceOne(x => x.Id == book.Id, book);
            }
            return book;
        }

        public List<Book> SearchBook(string searchString, string searchType)
        {
            List<Book> book=new List<Book>();
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchType))
            {
                if (searchType.ToLower() == "id")
                {
                    book = _bookTable.Find(s => s.Id== searchString).ToList();
                }
                if (searchType.ToLower() == "name")
                {
                    book = _bookTable.Find(s => s.Name == searchString).ToList();
                }
                if (searchType.ToLower() == "family")
                {
                    book = _bookTable.Find(s => s.Family == searchString).ToList();
                }
                if (searchType.ToLower() == "secondname")
                {
                    book = _bookTable.Find(s => s.SecondName == searchString).ToList();
                }
                if (searchType.ToLower() == "namebook")
                {
                    book = _bookTable.Find(s => s.NameBook == searchString).ToList();
                }
                if (searchType.ToLower() == "publish")
                {
                    book = _bookTable.Find(s => s.Publish == searchString).ToList();
                }
                if (searchType.ToLower() == "year" && int.TryParse(searchString, out _))
                {
                    book = _bookTable.Find(s => s.Year == int.Parse(searchString)).ToList();
                }
                if (searchType.ToLower() == "price" && int.TryParse(searchString, out _))
                {
                    book = _bookTable.Find(s => s.Price == int.Parse(searchString)).ToList();
                }
                if (searchType.ToLower() == "count" && int.TryParse(searchString, out _))
                {
                    book = _bookTable.Find(s => s.Count == int.Parse(searchString)).ToList();
                }
            }
            return book;
        }

        public List<Book> SortBook(string sortBook,List<Book>books)
        {
            switch (sortBook)
            {
                case "Name":
                    books = books.OrderBy(s => s.Name).Cast<Book>().ToList();
                    break;
                case "name_desc":
                    books = books.OrderByDescending(s => s.Name).Cast<Book>().ToList();
                    break;
                case "Family":
                    books = books.OrderBy(s => s.Family).Cast<Book>().ToList();
                    break;
                case "family_desc":
                    books = books.OrderByDescending(s => s.Family).Cast<Book>().ToList();
                    break;
                case "SecondName":
                    books = books.OrderBy(s => s.SecondName).Cast<Book>().ToList();
                    break;
                case "secondName_desc":
                    books = books.OrderByDescending(s => s.SecondName).Cast<Book>().ToList();
                    break;
                case "NameBook":
                    books = books.OrderBy(s => s.NameBook).Cast<Book>().ToList();
                    break;
                case "nameBook_desc":
                    books = books.OrderByDescending(s => s.NameBook).Cast<Book>().ToList();
                    break;
                case "Publish":
                    books = books.OrderBy(s => s.Publish).Cast<Book>().ToList();
                    break;
                case "publish_desc":
                    books = books.OrderByDescending(s => s.Publish).Cast<Book>().ToList();
                    break;
                case "Year":
                    books = books.OrderBy(s => s.Year).Cast<Book>().ToList();
                    break;
                case "year_desc":
                    books = books.OrderByDescending(s => s.Year).Cast<Book>().ToList();
                    break;
                case "Price":
                    books = books.OrderBy(s => s.Price).Cast<Book>().ToList();
                    break;
                case "price_desc":
                    books = books.OrderByDescending(s => s.Price).Cast<Book>().ToList();
                    break;
                case "Count":
                    books = books.OrderBy(s => s.Count).Cast<Book>().ToList();
                    break;
                case "count_desc":
                    books = books.OrderByDescending(s => s.Count).Cast<Book>().ToList();
                    break;
                case "id_desc":
                    books = books.OrderByDescending(s => s.Id).Cast<Book>().ToList();
                    break;
                default:
                    books = books.OrderBy(s => s.Id).Cast<Book>().ToList();
                    break;
            }
            return books;
        }

        public List<BsonDocument> ListAggregateBooks()
        {
            var books = _bookTable.Aggregate().Group(new BsonDocument {
                { "_id", "$publish" },
                { "avgPrice",new BsonDocument("$avg", "$price")},
                { "firstPublic",new BsonDocument("$first", "$nameBook")},
                { "lastPublic",new BsonDocument("$last", "$nameBook")},
                { "fieldBook", new BsonDocument("$push",  new BsonDocument{{ "nameBook", "$nameBook" },{ "name",new BsonDocument("$concat",new BsonArray{"$name"," ","$family"," ","$secondName"}) },{ "year", "$year" },{ "price", "$price" },{ "count", "$count" } }) }
            }).ToList();
            return books;
        }
    }
}
