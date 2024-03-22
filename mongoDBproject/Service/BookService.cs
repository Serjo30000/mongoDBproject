using MongoDB.Bson;
using mongoDBproject.Models;
using mongoDBproject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongoDBproject.Service
{
    public class BookService
    {
        BookRepository bookRepository = null;
        public BookService(string role)
        {
            bookRepository = new BookRepository(role);
        }

        public List<Book> GetBooks()
        {
            return bookRepository.GetList();
        }
        public List<Book> GetSearchBooks(string searchString, string searchType)
        {
            return bookRepository.SearchBook(searchString, searchType);
        }
        public List<Book> GetSortBooks(string sortBook, List<Book> books)
        {
            return bookRepository.SortBook(sortBook, books);
        }
        public List<BsonDocument> GetListAggregateBooks()
        {
            return bookRepository.ListAggregateBooks();
        }
        public List<Genre> GetGenres(string bookId)
        {
            return bookRepository.GetListGenre(bookId);
        }
        public List<OrderBook> GetOrderBooks(string bookId)
        {
            return bookRepository.GetListOrderBook(bookId);
        }
        public Book GetBook(string bookId)
        {
            return bookRepository.Get(bookId);
        }

        public Book AddBook(Book book)
        {
            if (book.Id==null)
            {
                return bookRepository.Save(book);
            }
            else
            {
                return new Book();
            }
        }
        public Book UpdateBook(Book book)
        {
            if (bookRepository.Get(book.Id) != null)
            {
                return bookRepository.Save(book);
            }
            else
            {
                return new Book();
            }
        }

        public bool RemoveBook(string bookId)
        {
            return bookRepository.Delete(bookId);
        }
    }
}
