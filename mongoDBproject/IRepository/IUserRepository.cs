using mongoDBproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.IRepository
{
    public interface IUserRepository
    {
        User Save(User user);
        User Get(string userId);
        List<User> GetList();
        bool Delete(string userId);
        List<Order> GetListOrder(string userId);
    }
}
