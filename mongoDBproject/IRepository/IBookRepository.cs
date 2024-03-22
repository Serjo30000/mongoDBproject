using mongoDBproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Repository
{
    public interface IBookRepository
    {
        Book Save(Book book);
        Book Get(string bookId);
        List<Book> GetList();
        bool Delete(string bookId);
        List<Genre> GetListGenre(string bookId);
        List<OrderBook> GetListOrderBook(string bookId);
    }
}
