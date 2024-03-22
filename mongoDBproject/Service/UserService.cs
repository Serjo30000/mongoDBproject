using MongoDB.Bson;
using mongoDBproject.Models;
using mongoDBproject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Service
{
    public class UserService
    {
        UserRepository userRepository = null;
        public UserService(string role)
        {
            userRepository = new UserRepository(role);
        }

        public List<User> GetUsers()
        {
            return userRepository.GetList();
        }
        public List<User> GetSearchUsers(string searchString, string searchType)
        {
            return userRepository.SearchUser(searchString, searchType);
        }
        public List<User> GetSortUsers(string sortUser, List<User> users)
        {
            return userRepository.SortUser(sortUser, users);
        }
        public List<BsonDocument> GetListAggregateUsers()
        {
            return userRepository.ListAggregateUsers();
        }
        public List<Order> GetOrders(string userId)
        {
            return userRepository.GetListOrder(userId);
        }
        public User GetUser(string userId)
        {
            return userRepository.Get(userId);
        }

        public User AddUser(User user)
        {
            if (user.Id == null)
            {
                return userRepository.Save(user);
            }
            else
            {
                return new User();
            }
        }
        public User UpdateUser(User user)
        {
            if (userRepository.Get(user.Id) != null)
            {
                return userRepository.Save(user);
            }
            else
            {
                return new User();
            }
        }

        public bool RemoveUser(string userId)
        {
            return userRepository.Delete(userId);
        }
    }
}
