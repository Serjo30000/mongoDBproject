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
    public class UserRepository : IUserRepository
    {
        private MongoClient _mongoClient = null;
        private IMongoDatabase _database = null;
        private IMongoCollection<User> _userTable = null;
        private IMongoCollection<Order> _orderTable = null;
        public UserRepository(string role)
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
            _userTable = _database.GetCollection<User>("collectionUsers");
            _orderTable = _database.GetCollection<Order>("collectionOrder");
        }
        public bool Delete(string userId)
        {
            if (_userTable.Find(x => x.Id == userId).FirstOrDefault() == null)
            {
                return false;
            }
            else
            {
                _userTable.DeleteOne(x => x.Id == userId);
                return true;
            }
        }

        public User Get(string userId)
        {
            return _userTable.Find(x => x.Id == userId).FirstOrDefault();
        }

        public List<User> GetList()
        {
            return _userTable.Find(FilterDefinition<User>.Empty).ToList();
        }

        public List<Order> GetListOrder(string userId)
        {
            List<Order> lsrOr = new List<Order>();
            foreach (Order or in _orderTable.Find(FilterDefinition<Order>.Empty).ToList())
            {
                if (or.IdUser == userId)
                {
                    lsrOr.Add(or);
                }
            }
            return lsrOr;
        }

        public User Save(User user)
        {
            var userObj = _userTable.Find(x => x.Id == user.Id).FirstOrDefault();
            if (userObj == null)
            {
                _userTable.InsertOne(user);
            }
            else
            {
                _userTable.ReplaceOne(x => x.Id == user.Id, user);
            }
            return user;
        }

        public List<User> SearchUser(string searchString, string searchType)
        {
            List<User> user = new List<User>();
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchType))
            {
                if (searchType.ToLower() == "id")
                {
                    user = _userTable.Find(s => s.Id == searchString).ToList();
                }
                if (searchType.ToLower() == "login")
                {
                    user = _userTable.Find(s => s.Login == searchString).ToList();
                }
                if (searchType.ToLower() == "role")
                {
                    user = _userTable.Find(s => s.Role == searchString).ToList();
                }
                if (searchType.ToLower() == "password")
                {
                    user = _userTable.Find(s => s.Password == searchString).ToList();
                }
                if (searchType.ToLower() == "numberphone")
                {
                    user = _userTable.Find(s => s.NumberPhone == searchString).ToList();
                }
                if (searchType.ToLower() == "email")
                {
                    user = _userTable.Find(s => s.Email == searchString).ToList();
                }
            }
            return user;
        }

        public List<User> SortUser(string sortUser, List<User> users)
        {
            switch (sortUser)
            {
                case "Login":
                    users = users.OrderBy(s => s.Login).Cast<User>().ToList();
                    break;
                case "login_desc":
                    users = users.OrderByDescending(s => s.Login).Cast<User>().ToList();
                    break;
                case "Role":
                    users = users.OrderBy(s => s.Role).Cast<User>().ToList();
                    break;
                case "role_desc":
                    users = users.OrderByDescending(s => s.Role).Cast<User>().ToList();
                    break;
                case "Password":
                    users = users.OrderBy(s => s.Password).Cast<User>().ToList();
                    break;
                case "password_desc":
                    users = users.OrderByDescending(s => s.Password).Cast<User>().ToList();
                    break;
                case "NumberPhone":
                    users = users.OrderBy(s => s.NumberPhone).Cast<User>().ToList();
                    break;
                case "numberPhone_desc":
                    users = users.OrderByDescending(s => s.NumberPhone).Cast<User>().ToList();
                    break;
                case "Email":
                    users = users.OrderBy(s => s.Email).Cast<User>().ToList();
                    break;
                case "email_desc":
                    users = users.OrderByDescending(s => s.Email).Cast<User>().ToList();
                    break;
                case "id_desc":
                    users = users.OrderByDescending(s => s.Id).Cast<User>().ToList();
                    break;
                default:
                    users = users.OrderBy(s => s.Id).Cast<User>().ToList();
                    break;
            }
            return users;
        }

        public List<BsonDocument> ListAggregateUsers()
        {
            var users = _userTable.Aggregate().Group(new BsonDocument {
                { "_id", new BsonDocument( "$first", "$Roles" )},
                { "newLastUser",new BsonDocument("$last", "$UserName")},
                { "fieldUser", new BsonDocument("$push",  new BsonDocument{{ "UserName", "$UserName" },{ "PasswordHash", "$PasswordHash" },{ "PhoneNumber", "$PhoneNumber" },{ "Email", "$Email" } }) }
            }).ToList();
            return users;
        }
    }
}
