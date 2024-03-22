using mongoDBproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.IRepository
{
    public interface IGenreRepository
    {
        Genre Save(Genre genre);
        Genre Get(string genreId);
        List<Genre> GetList();
        bool Delete(string genreId);
        Book GetBook(string bookId);
    }
}
