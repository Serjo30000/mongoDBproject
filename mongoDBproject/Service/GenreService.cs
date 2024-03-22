using mongoDBproject.Models;
using mongoDBproject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Service
{
    public class GenreService
    {
        GenreRepository genreRepository = null;
        public GenreService(string role)
        {
            genreRepository = new GenreRepository(role);
        }

        public List<Genre> GetGenres()
        {
            return genreRepository.GetList();
        }
        public List<Genre> GetSearchGenres(string searchString, string searchType)
        {
            return genreRepository.SearchGenre(searchString, searchType);
        }
        public List<Genre> GetSortGenres(string sortGenre, List<Genre> genres)
        {
            return genreRepository.SortGenre(sortGenre, genres);
        }
        public Genre GetGenre(string genreId)
        {
            return genreRepository.Get(genreId);
        }
        public Book GetBook(string bookId)
        {
            return genreRepository.GetBook(bookId);
        }

        public Genre AddGenre(Genre genre)
        {
            if (genre.Id == null)
            {
                return genreRepository.Save(genre);
            }
            else
            {
                return new Genre();
            }
        }
        public Genre UpdateGenre(Genre genre)
        {
            if (genreRepository.Get(genre.Id) != null)
            {
                return genreRepository.Save(genre);
            }
            else
            {
                return new Genre();
            }
        }

        public bool RemoveGenre(string genreId)
        {
            return genreRepository.Delete(genreId);
        }
    }
}
