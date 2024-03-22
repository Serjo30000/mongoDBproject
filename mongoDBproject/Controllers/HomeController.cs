using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using mongoDBproject.Models;
using mongoDBproject.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace mongoDBproject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BookService _bookService;
        private GenreService _genreService;
        private UserService _userService;
        private OrderService _orderService;
        private OrderBookService _orderBookService;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _bookService = new BookService(AccountController.roleUser);
            _genreService = new GenreService(AccountController.roleUser);
            _userService = new UserService(AccountController.roleUser);
            _orderService = new OrderService(AccountController.roleUser);
            _orderBookService = new OrderBookService(AccountController.roleUser);
        }
        [Authorize]
        public IActionResult Index()
        {
            if (this.User != null && AccountController.roleUser == "client")
            {
                AccountController.roleUser = this.User.FindFirst(ClaimTypes.Role).Value;
            }
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetListBook(string sortBook, string searchString, string searchType)
        {
            var books = _bookService.GetBooks();
            ViewData["IdSortParmBook"] = String.IsNullOrEmpty(sortBook) ? "id_desc" : "";
            ViewData["NameSortParmBook"] = sortBook == "Name" ? "name_desc" : "Name";
            ViewData["FamilySortParmBook"] = sortBook == "Family" ? "family_desc" : "Family";
            ViewData["SecondNameSortParmBook"] = sortBook == "SecondName" ? "secondName_desc" : "SecondName";
            ViewData["NameBookSortParmBook"] = sortBook == "NameBook" ? "nameBook_desc" : "NameBook";
            ViewData["PublishSortParmBook"] = sortBook == "Publish" ? "publish_desc" : "Publish";
            ViewData["YearSortParmBook"] = sortBook == "Year" ? "year_desc" : "Year";
            ViewData["PriceSortParmBook"] = sortBook == "Price" ? "price_desc" : "Price";
            ViewData["CountSortParmBook"] = sortBook == "Count" ? "count_desc" : "Count";
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchType))
            {
                books = _bookService.GetSearchBooks(searchString, searchType);
            }
            books = _bookService.GetSortBooks(sortBook, books);
            return View(books);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetBook(string id)
        {
            var book = _bookService.GetBook(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult CreateBook()
        {
            Book book = new Book();
            return View(book);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<Book> CreateBook(Book book)
        {
            if (book.Name=="" || book.Family=="" || book.SecondName=="" || book.NameBook=="" || book.Publish=="" || book.Name== null || book.Family== null || book.SecondName== null || book.NameBook== null || book.Publish== null || book.Year<=0 || book.Price <= 0)
            {
                
            }
            else
            {
                _bookService.AddBook(book);
            }
            return View("GetListBook",_bookService.GetBooks());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteBook(string id)
        {
            var book = _bookService.GetBook(id);
            return View(book);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<Book> DeleteBook(Book book)
        {

            if (book == null)
            {
                return NotFound();
            }

            _bookService.RemoveBook(book.Id);

            return View("GetListBook", _bookService.GetBooks());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateBook(string id)
        {
            var book = _bookService.GetBook(id);
            return View(book);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<Book> UpdateBook(Book book)
        {

            if (book == null)
            {
                return NotFound();
            }
            if (book.Name == "" || book.Family == "" || book.SecondName == "" || book.NameBook == "" || book.Publish == "" || book.Name == null || book.Family == null || book.SecondName == null || book.NameBook == null || book.Publish == null || book.Year <= 0 || book.Price <= 0)
            {

            }
            else
            {
                _bookService.UpdateBook(book);
            }
            return View("GetListBook", _bookService.GetBooks());
        }






        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetListGenre(string sortGenre, string searchString, string searchType)
        {
            var genres = _genreService.GetGenres();
            ViewData["IdSortParmGenre"] = String.IsNullOrEmpty(sortGenre) ? "id_desc" : "";
            ViewData["IdBookSortParmGenre"] = sortGenre == "IdBook" ? "idBook_desc" : "IdBook";
            ViewData["GenreNameSortParmGenre"] = sortGenre == "GenreName" ? "genreName_desc" : "GenreName";
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchType))
            {
                genres = _genreService.GetSearchGenres(searchString, searchType);
            }
            genres = _genreService.GetSortGenres(sortGenre, genres);
            return View(genres);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetGenre(string id)
        {
            var genre = _genreService.GetGenre(id);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult CreateGenre()
        {
            Genre genre = new Genre();
            return View(genre);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<Genre> CreateGenre(Genre genre)
        {
            if (genre.GenreName == "" || genre.GenreName== null || genre.IdBook == null || genre.IdBook == "")
            {

            }
            else
            {
                _genreService.AddGenre(genre);
            }
            return View("GetListGenre", _genreService.GetGenres());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteGenre(string id)
        {
            var genre = _genreService.GetGenre(id);
            return View(genre);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<Genre> DeleteGenre(Genre genre)
        {

            if (genre == null)
            {
                return NotFound();
            }

            _genreService.RemoveGenre(genre.Id);

            return View("GetListGenre", _genreService.GetGenres());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateGenre(string id)
        {
            var genre = _genreService.GetGenre(id);
            return View(genre);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<Genre> UpdateGenre(Genre genre)
        {
            if (genre == null)
            {
                return NotFound();
            }
            if (genre.GenreName == "" || genre.GenreName == null || genre.IdBook == null || genre.IdBook == "")
            {

            }
            else
            {
                _genreService.UpdateGenre(genre);
            }
            return View("GetListGenre", _genreService.GetGenres());
        }




        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetAggregateUsers()
        {
            var users = _userService.GetListAggregateUsers();
            List<UserGroup> lst = new List<UserGroup>();
            for (int i = 0; i < users.Count; i++)
            {
                UserGroup us = BsonSerializer.Deserialize<UserGroup>(users[i]);
                lst.Add(us);
            }
            return View(lst);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetAggregateUser(string id)
        {
            var users = _userService.GetListAggregateUsers();
            UserGroup us = new UserGroup();
            List<User> lst = new List<User>();
            for (int i = 0; i < users.Count; i++)
            {
                us = BsonSerializer.Deserialize<UserGroup>(users[i]);
                if (us.Id == id)
                {
                    for (int j = 0; j < ((MongoDB.Bson.BsonArray)users[i]["fieldUser"]).Count; j++)
                    {
                        lst.Add(BsonSerializer.Deserialize<User>((MongoDB.Bson.BsonDocument)((MongoDB.Bson.BsonArray)users[i]["fieldUser"])[j]));
                    }
                    break;
                }
            }
            return View(lst);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetAggregateOrders()
        {
            var orders = _orderService.GetListAggregateOrders();
            List<OrderGroup> lst = new List<OrderGroup>();
            for (int i = 0; i < orders.Count; i++)
            {
                OrderGroup or = BsonSerializer.Deserialize<OrderGroup>(orders[i]);
                lst.Add(or);
            }
            return View(lst);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetAggregateOrder(string id)
        {
            var orders = _orderService.GetListAggregateOrders();
            OrderGroup or = new OrderGroup();
            List<Order> lst = new List<Order>();
            for (int i = 0; i < orders.Count; i++)
            {
                or = BsonSerializer.Deserialize<OrderGroup>(orders[i]);
                if (or.Id == id)
                {
                    for (int j = 0; j < ((MongoDB.Bson.BsonArray)orders[i]["fieldOrder"]).Count; j++)
                    {
                        lst.Add(BsonSerializer.Deserialize<Order>((MongoDB.Bson.BsonDocument)((MongoDB.Bson.BsonArray)orders[i]["fieldOrder"])[j]));
                    }
                    break;
                }
            }
            return View(lst);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetAggregateBooks()
        {
            var books = _bookService.GetListAggregateBooks();
            List<BookGroup> lst = new List<BookGroup>();
            for (int i = 0; i < books.Count; i++)
            {
                BookGroup bk = BsonSerializer.Deserialize<BookGroup>(books[i]);
                lst.Add(bk);
            }
            return View(lst);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetAggregateBook(string id)
        {
            var books = _bookService.GetListAggregateBooks();
            BookGroup bk = new BookGroup();
            List<Book> lst = new List<Book>();
            for (int i = 0; i < books.Count; i++)
            {
                bk = BsonSerializer.Deserialize<BookGroup>(books[i]);
                if (bk.Id == id)
                {
                    for (int j = 0; j < ((MongoDB.Bson.BsonArray)books[i]["fieldBook"]).Count; j++)
                    {
                        lst.Add(BsonSerializer.Deserialize<Book>((MongoDB.Bson.BsonDocument)((MongoDB.Bson.BsonArray)books[i]["fieldBook"])[j]));
                    }
                    break;
                }
            }
            return View(lst);
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetListUser(string sortUser, string searchString, string searchType)
        {
            var users = _userService.GetUsers();
            ViewData["IdSortParmUser"] = String.IsNullOrEmpty(sortUser) ? "id_desc" : "";
            ViewData["LoginSortParmUser"] = sortUser == "Login" ? "login_desc" : "Login";
            ViewData["RoleSortParmUser"] = sortUser == "Role" ? "role_desc" : "Role";
            ViewData["PasswordSortParmUser"] = sortUser == "Password" ? "password_desc" : "Password";
            ViewData["NumberPhoneSortParmUser"] = sortUser == "NumberPhone" ? "numberPhone_desc" : "NumberPhone";
            ViewData["EmailSortParmUser"] = sortUser == "Email" ? "email_desc" : "Email";
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchType))
            {
                users = _userService.GetSearchUsers(searchString, searchType);
            }
            users = _userService.GetSortUsers(sortUser, users);
            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetUser(string id)
        {
            var user = _userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult CreateUser()
        {
            User user = new User();
            return View(user);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<User> CreateUser(User user)
        {
            
            if (user.Login=="" || user.NumberPhone=="" || user.Password=="" || user.Role == "" || user.Login == null || user.NumberPhone == null || user.Password == null || user.Role == null)
            {

            }
            else
            {
                _userService.AddUser(user);
            }
            return View("GetListUser", _userService.GetUsers());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteUser(string id)
        {
            var user = _userService.GetUser(id);
            return View(user);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<User> DeleteUser(User user)
        {

            if (user == null)
            {
                return NotFound();
            }

            _userService.RemoveUser(user.Id);

            return View("GetListUser", _userService.GetUsers());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateUser(string id)
        {
            var user = _userService.GetUser(id);
            return View(user);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<User> UpdateUser(User user)
        {
            if (user == null)
            {
                return NotFound();
            }
            if (user.Login == "" || user.NumberPhone == "" || user.Password == "" || user.Role == "" || user.Login == null || user.NumberPhone == null || user.Password == null || user.Role == null)
            {

            }
            else
            {
                _userService.UpdateUser(user);
            }
            return View("GetListUser", _userService.GetUsers());
        }






        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetListOrder(string sortOrder, string searchString, string searchType)
        {
            var orders = _orderService.GetOrders();
            ViewData["IdSortParmOrder"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["IdUserSortParmOrder"] = sortOrder == "IdUser" ? "idUser_desc" : "IdUser";
            ViewData["DateOrderSortParmOrder"] = sortOrder == "DateOrder" ? "dateOrder_desc" : "DateOrder";
            ViewData["CountOrderSortParmOrder"] = sortOrder == "CountOrder" ? "countOrder_desc" : "CountOrder";
            ViewData["IdOrderBookSortParmOrder"] = sortOrder == "IdOrderBook" ? "idOrderBook_desc" : "IdOrderBook";
            ViewData["IdBookSortParmOrder"] = sortOrder == "IdBook" ? "idBook_desc" : "IdBook";
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchType))
            {
                orders = _orderService.GetSearchOrders(searchString, searchType);
            }
            orders = _orderService.GetSortOrders(sortOrder, orders);
            return View(orders);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetOrder(string id)
        {
            var order = _orderService.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult CreateOrder()
        {
            Order order = new Order();
            return View(order);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<Order> CreateOrder(Order order)
        {
            if (order.IdUser=="" || order.IdUser==null)
            {

            }
            else
            {
                _orderService.AddOrder(order);
            }
            return View("GetListOrder", _orderService.GetOrders());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteOrder(string id)
        {
            var order = _orderService.GetOrder(id);
            return View(order);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<Order> DeleteOrder(Order order)
        {

            if (order == null)
            {
                return NotFound();
            }

            _orderService.RemoveOrder(order.Id);

            return View("GetListOrder", _orderService.GetOrders());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateOrder(string id)
        {
            var order = _orderService.GetOrder(id);
            return View(order);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<Order> UpdateOrder(Order order)
        {
            if (order == null)
            {
                return NotFound();
            }
            if (order.IdUser == "" || order.IdUser == null || order.DateOrder == null)
            {

            }
            else
            {
                _orderService.UpdateOrder(order);
            }    
            return View("GetListOrder", _orderService.GetOrders());
        }






        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetListOrderBook(string sortOrderBook, string searchString, string searchType)
        {
            var orderBooks = _orderBookService.GetOrderBooks();
            ViewData["IdSortParmOrderBook"] = String.IsNullOrEmpty(sortOrderBook) ? "id_desc" : "";
            ViewData["IdBookSortParmOrderBook"] = sortOrderBook == "IdBook" ? "idBook_desc" : "IdBook";
            ViewData["MarkSortParmOrderBook"] = sortOrderBook == "Mark" ? "mark_desc" : "Mark";
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchType))
            {
                orderBooks = _orderBookService.GetSearchOrderBooks(searchString, searchType);
            }
            orderBooks = _orderBookService.GetSortOrderBooks(sortOrderBook, orderBooks);
            return View(orderBooks);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetOrderBook(string id)
        {
            var orderBook = _orderBookService.GetOrderBook(id);

            if (orderBook == null)
            {
                return NotFound();
            }

            return View(orderBook);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult CreateOrderBook()
        {
            OrderBook orderBook = new OrderBook();
            return View(orderBook);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<OrderBook> CreateOrderBook(OrderBook orderBook)
        {
            if (orderBook.IdBook=="" || orderBook.IdBook==null || orderBook.IdOrder=="" || orderBook.IdOrder == null)
            {

            }
            else
            {
                _orderBookService.AddOrderBook(orderBook);
            }
            return View("GetListOrderBook", _orderBookService.GetOrderBooks());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteOrderBook(string id)
        {
            var orderBook = _orderBookService.GetOrderBook(id);
            return View(orderBook);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<OrderBook> DeleteOrderBook(OrderBook orderBook)
        {

            if (orderBook == null)
            {
                return NotFound();
            }

            _orderBookService.RemoveOrderBook(orderBook.Id);

            return View("GetListOrderBook", _orderBookService.GetOrderBooks());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateOrderBook(string id)
        {
            var orderBook = _orderBookService.GetOrderBook(id);
            return View(orderBook);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<OrderBook> UpdateOrderBook(OrderBook orderBook)
        {
            if (orderBook == null)
            {
                return NotFound();
            }
            if (orderBook.IdBook == "" || orderBook.IdBook == null || orderBook.IdOrder == "" || orderBook.IdOrder == null)
            {

            }
            else
            {
                _orderBookService.UpdateOrderBook(orderBook);
            }
            return View("GetListOrderBook", _orderBookService.GetOrderBooks());
        }




        [HttpGet]
        [Authorize(Roles = "librarian")]
        public ActionResult GetListLibrarianBook(string sortBook,string searchString, string searchType)
        {
            var books = _bookService.GetBooks();
            ViewData["IdSortParmBook"] = String.IsNullOrEmpty(sortBook) ? "id_desc" : "";
            ViewData["NameSortParmBook"] = sortBook == "Name" ? "name_desc" : "Name";
            ViewData["FamilySortParmBook"] = sortBook == "Family" ? "family_desc" : "Family";
            ViewData["SecondNameSortParmBook"] = sortBook == "SecondName" ? "secondName_desc" : "SecondName";
            ViewData["NameBookSortParmBook"] = sortBook == "NameBook" ? "nameBook_desc" : "NameBook";
            ViewData["PublishSortParmBook"] = sortBook == "Publish" ? "publish_desc" : "Publish";
            ViewData["YearSortParmBook"] = sortBook == "Year" ? "year_desc" : "Year";
            ViewData["PriceSortParmBook"] = sortBook == "Price" ? "price_desc" : "Price";
            ViewData["CountSortParmBook"] = sortBook == "Count" ? "count_desc" : "Count";
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchType))
            {
                books = _bookService.GetSearchBooks(searchString, searchType);
            }
            books = _bookService.GetSortBooks(sortBook, books);
            return View(books);
        }

        [HttpGet]
        [Authorize(Roles = "librarian")]
        public ActionResult GetLibrarianBook(string id)
        {
            var book = _bookService.GetBook(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        [HttpGet]
        [Authorize(Roles = "librarian")]
        public ActionResult CreateLibrarianBook()
        {
            Book book = new Book();
            return View(book);
        }
        [HttpPost]
        [Authorize(Roles = "librarian")]
        public ActionResult<Book> CreateLibrarianBook(Book book)
        {
            if (book.Name == "" || book.Family == "" || book.SecondName == "" || book.NameBook == "" || book.Publish == "" || book.Name == null || book.Family == null || book.SecondName == null || book.NameBook == null || book.Publish == null || book.Year <= 0 || book.Price <= 0)
            {

            }
            else
            {
                _bookService.AddBook(book);
            }
            return View("GetListLibrarianBook", _bookService.GetBooks());
        }
        [HttpGet]
        [Authorize(Roles = "librarian")]
        public ActionResult DeleteLibrarianBook(string id)
        {
            var book = _bookService.GetBook(id);
            return View(book);
        }
        [HttpPost]
        [Authorize(Roles = "librarian")]
        public ActionResult<Book> DeleteLibrarianBook(Book book)
        {

            if (book == null)
            {
                return NotFound();
            }

            _bookService.RemoveBook(book.Id);

            return View("GetListLibrarianBook", _bookService.GetBooks());
        }
        [HttpGet]
        [Authorize(Roles = "librarian")]
        public ActionResult UpdateLibrarianBook(string id)
        {
            var book = _bookService.GetBook(id);
            return View(book);
        }
        [HttpPost]
        [Authorize(Roles = "librarian")]
        public ActionResult<Book> UpdateLibrarianBook(Book book)
        {

            if (book == null)
            {
                return NotFound();
            }
            if (book.Name == "" || book.Family == "" || book.SecondName == "" || book.NameBook == "" || book.Publish == "" || book.Name == null || book.Family == null || book.SecondName == null || book.NameBook == null || book.Publish == null || book.Year <= 0 || book.Price <= 0)
            {

            }
            else
            {
                _bookService.UpdateBook(book);
            }
            return View("GetListLibrarianBook", _bookService.GetBooks());
        }




        [HttpGet]
        [Authorize(Roles = "librarian")]
        public ActionResult GetListLibrarianGenre(string sortGenre, string searchString, string searchType)
        {
            var genres = _genreService.GetGenres();
            ViewData["IdSortParmGenre"] = String.IsNullOrEmpty(sortGenre) ? "id_desc" : "";
            ViewData["IdBookSortParmGenre"] = sortGenre == "IdBook" ? "idBook_desc" : "IdBook";
            ViewData["GenreNameSortParmGenre"] = sortGenre == "GenreName" ? "genreName_desc" : "GenreName";
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchType))
            {
                genres = _genreService.GetSearchGenres(searchString, searchType);
            }
            genres = _genreService.GetSortGenres(sortGenre, genres);
            return View(genres);
        }

        [HttpGet]
        [Authorize(Roles = "librarian")]
        public ActionResult GetLibrarianGenre(string id)
        {
            var genre = _genreService.GetGenre(id);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }
        [HttpGet]
        [Authorize(Roles = "librarian")]
        public ActionResult CreateLibrarianGenre()
        {
            Genre genre = new Genre();
            return View(genre);
        }
        [HttpPost]
        [Authorize(Roles = "librarian")]
        public ActionResult<Genre> CreateLibrarianGenre(Genre genre)
        {
            if (genre.GenreName == "" || genre.GenreName == null || genre.IdBook == null || genre.IdBook == "")
            {

            }
            else
            {
                _genreService.AddGenre(genre);
            }
            return View("GetListLibrarianGenre", _genreService.GetGenres());
        }
        [HttpGet]
        [Authorize(Roles = "librarian")]
        public ActionResult DeleteLibrarianGenre(string id)
        {
            var genre = _genreService.GetGenre(id);
            return View(genre);
        }
        [HttpPost]
        [Authorize(Roles = "librarian")]
        public ActionResult<Genre> DeleteLibrarianGenre(Genre genre)
        {

            if (genre == null)
            {
                return NotFound();
            }

            _genreService.RemoveGenre(genre.Id);

            return View("GetListLibrarianGenre", _genreService.GetGenres());
        }
        [HttpGet]
        [Authorize(Roles = "librarian")]
        public ActionResult UpdateLibrarianGenre(string id)
        {
            var genre = _genreService.GetGenre(id);
            return View(genre);
        }
        [HttpPost]
        [Authorize(Roles = "librarian")]
        public ActionResult<Genre> UpdateLibrarianGenre(Genre genre)
        {
            if (genre == null)
            {
                return NotFound();
            }
            if (genre.GenreName == "" || genre.GenreName == null || genre.IdBook == null || genre.IdBook == "")
            {

            }
            else
            {
                _genreService.UpdateGenre(genre);
            }
            return View("GetListLibrarianGenre", _genreService.GetGenres());
        }




        [HttpGet]
        [Authorize(Roles = "librarian")]
        public ActionResult GetListLibrarianOrderBook(string sortOrderBook, string searchString, string searchType)
        {
            var orderBooks = _orderBookService.GetOrderBooks();
            ViewData["IdSortParmOrderBook"] = String.IsNullOrEmpty(sortOrderBook) ? "id_desc" : "";
            ViewData["IdBookSortParmOrderBook"] = sortOrderBook == "IdBook" ? "idBook_desc" : "IdBook";
            ViewData["MarkSortParmOrderBook"] = sortOrderBook == "Mark" ? "mark_desc" : "Mark";
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchType))
            {
                orderBooks = _orderBookService.GetSearchOrderBooks(searchString, searchType);
            }
            orderBooks = _orderBookService.GetSortOrderBooks(sortOrderBook, orderBooks);
            return View(orderBooks);
        }

        [HttpGet]
        [Authorize(Roles = "librarian")]
        public ActionResult GetLibrarianOrderBook(string id)
        {
            var orderBook = _orderBookService.GetOrderBook(id);

            if (orderBook == null)
            {
                return NotFound();
            }

            return View(orderBook);
        }



        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult GetListClientBook(string sortBook, string searchString, string searchType)
        {
            var books = _bookService.GetBooks();
            ViewData["IdSortParmBook"] = String.IsNullOrEmpty(sortBook) ? "id_desc" : "";
            ViewData["NameSortParmBook"] = sortBook == "Name" ? "name_desc" : "Name";
            ViewData["FamilySortParmBook"] = sortBook == "Family" ? "family_desc" : "Family";
            ViewData["SecondNameSortParmBook"] = sortBook == "SecondName" ? "secondName_desc" : "SecondName";
            ViewData["NameBookSortParmBook"] = sortBook == "NameBook" ? "nameBook_desc" : "NameBook";
            ViewData["PublishSortParmBook"] = sortBook == "Publish" ? "publish_desc" : "Publish";
            ViewData["YearSortParmBook"] = sortBook == "Year" ? "year_desc" : "Year";
            ViewData["PriceSortParmBook"] = sortBook == "Price" ? "price_desc" : "Price";
            ViewData["CountSortParmBook"] = sortBook == "Count" ? "count_desc" : "Count";
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchType))
            {
                books = _bookService.GetSearchBooks(searchString, searchType);
            }
            books = _bookService.GetSortBooks(sortBook,books);
            return View(books);
        }

        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult GetClientBook(string id)
        {
            var book = _bookService.GetBook(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult CreateClientOrder(string idBook)
        {
            Debug.WriteLine(idBook);
            Order order = new Order();
            order.IdUser = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            order.DateOrder = DateTime.Now;
            order.IdBook = idBook;
            return View(order);
        }
        [HttpPost]
        [Authorize(Roles = "client")]
        public ActionResult<Order> CreateClientOrder(Order order)
        {
            if (order.IdUser == "" || order.IdUser == null || order.CountOrder>_bookService.GetBook(order.IdBook).Count)
            {

            }
            else
            {
                var book = _bookService.GetBook(order.IdBook);
                book.Count = book.Count - order.CountOrder;
                OrderBook orderBook = new OrderBook();
                _bookService.UpdateBook(book);
                orderBook.Mark = false;
                orderBook.IdBook = order.IdBook;
                _orderBookService.AddOrderBook(orderBook);
                order.IdOrderBook = orderBook.Id;
                _orderService.AddOrder(order);
            }
            return View("GetListClientOrder", _orderService.GetOrders());
        }
        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult GetListClientOrder(string sortOrder, string searchString, string searchType)
        {
            var orders = _orderService.GetOrders();
            ViewData["IdSortParmOrder"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["IdUserSortParmOrder"] = sortOrder == "IdUser" ? "idUser_desc" : "IdUser";
            ViewData["DateOrderSortParmOrder"] = sortOrder == "DateOrder" ? "dateOrder_desc" : "DateOrder";
            ViewData["CountOrderSortParmOrder"] = sortOrder == "CountOrder" ? "countOrder_desc" : "CountOrder";
            ViewData["IdOrderBookSortParmOrder"] = sortOrder == "IdOrderBook" ? "idOrderBook_desc" : "IdOrderBook";
            ViewData["IdBookSortParmOrder"] = sortOrder == "IdBook" ? "idBook_desc" : "IdBook";
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchType))
            {
                orders = _orderService.GetSearchOrders(searchString, searchType);
            }
            orders = _orderService.GetSortOrders(sortOrder, orders);
            return View(orders);
        }

        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult GetClientOrder(string id)
        {
            var order = _orderService.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult DeleteClientOrder(string id)
        {
            var order = _orderService.GetOrder(id);
            return View(order);
        }
        [HttpPost]
        [Authorize(Roles = "client")]
        public ActionResult<Order> DeleteClientOrder(Order order)
        {

            if (order == null)
            {
                return NotFound();
            }
            var orderBook = _orderBookService.GetOrderBook(order.IdOrderBook);
            orderBook.Mark = true;
            var book = _bookService.GetBook(order.IdBook);
            book.Count = book.Count + order.CountOrder;
            _bookService.UpdateBook(book);
            _orderBookService.UpdateOrderBook(orderBook);
            _orderService.RemoveOrder(order.Id);
            return View("GetListClientOrder", _orderService.GetOrders());
        }
    }
}
