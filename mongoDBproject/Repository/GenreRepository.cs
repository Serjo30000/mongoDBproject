using MongoDB.Driver;
using mongoDBproject.IRepository;
using mongoDBproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private MongoClient _mongoClient = null;
        private IMongoDatabase _database = null;
        private IMongoCollection<Genre> _genreTable = null;
        private IMongoCollection<Book> _bookTable = null;
        public GenreRepository(string role)
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
            _genreTable = _database.GetCollection<Genre>("collectionGenre");
            _bookTable = _database.GetCollection<Book>("collectionBook");
        }
        public bool Delete(string genreId)
        {
            if (_genreTable.Find(x => x.Id == genreId).FirstOrDefault() == null)
            {
                return false;
            }
            else
            {
                _genreTable.DeleteOne(x => x.Id == genreId);
                return true;
            }
        }

        public Genre Get(string genreId)
        {
            return _genreTable.Find(x => x.Id == genreId).FirstOrDefault();
        }

        public List<Genre> GetList()
        {
            return _genreTable.Find(FilterDefinition<Genre>.Empty).ToList();
        }

        public Genre Save(Genre genre)
        {
            var genreObj = _genreTable.Find(x => x.Id == genre.Id).FirstOrDefault();
            if (genreObj == null)
            {
                _genreTable.InsertOne(genre);
            }
            else
            {
                _genreTable.ReplaceOne(x => x.Id == genre.Id, genre);
            }
            return genre;
        }
        public Book GetBook(string bookId)
        {
            return _bookTable.Find(x => x.Id == bookId).FirstOrDefault();
        }

        public List<Genre> SearchGenre(string searchString, string searchType)
        {
            List<Genre> genre = new List<Genre>();
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchType))
            {
                if (searchType.ToLower() == "id")
                {
                    genre = _genreTable.Find(s => s.Id == searchString).ToList();
                }
                if (searchType.ToLower() == "idbook")
                {
                    genre = _genreTable.Find(s => s.IdBook == searchString).ToList();
                }
                if (searchType.ToLower() == "genrename")
                {
                    genre = _genreTable.Find(s => s.GenreName == searchString).ToList();
                }
            }
            return genre;
        }

        public List<Genre> SortGenre(string sortGenre, List<Genre> genres)
        {
            switch (sortGenre)
            {
                case "IdBook":
                    genres = genres.OrderBy(s => s.IdBook).Cast<Genre>().ToList();
                    break;
                case "idBook_desc":
                    genres = genres.OrderByDescending(s => s.IdBook).Cast<Genre>().ToList();
                    break;
                case "GenreName":
                    genres = genres.OrderBy(s => s.GenreName).Cast<Genre>().ToList();
                    break;
                case "genreName_desc":
                    genres = genres.OrderByDescending(s => s.GenreName).Cast<Genre>().ToList();
                    break;
                case "id_desc":
                    genres = genres.OrderByDescending(s => s.Id).Cast<Genre>().ToList();
                    break;
                default:
                    genres = genres.OrderBy(s => s.Id).Cast<Genre>().ToList();
                    break;
            }
            return genres;
        }
    }
}
