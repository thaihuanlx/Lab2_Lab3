using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab2_Lab3.Models;

namespace Lab2_Lab3.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public string HelloTeacher(string university)
        {
            return "Hello !" + university;
        }

        public ActionResult ListBook()
        {
            var books = new List<string>();
            books.Add("HTML5 & CSS3 The complete Manual - Author Name Book 1");
            books.Add("HTML & CSS Responsive web Design cookbook - Author Name Book 2");
            books.Add("Professional ASP.NET MVC5 - Author Name Book 3");
            ViewBag.Books = books;
            return View();
        }

        public ActionResult ListBookModel()
        {
            var books = new List<Book>();
            books.Add(new Book(1,"HTML5 & CSS3 The complete " , "Author Name Book 1","Content/Images/Book1.jpg"));
            books.Add(new Book(2,"HTML & CSS Responsive web Design cookbook","Author Name Book 2", "Content/Images/Book2.jpg"));
            books.Add(new Book(3,"Professional ASP.NET MVC5" , "Author Name Book 3", "Content/Images/Book3.jpg"));
            return View(books);
        }

        public ActionResult EditBook(int id)
        {
            var books = new List<Book>();
            books.Add(new Book(1, "HTML5 & CSS3 The complete ", "Author Name Book 1", "Content/Images/Book1.jpg"));
            books.Add(new Book(2, "HTML & CSS Responsive web Design cookbook", "Author Name Book 2", "Content/Images/Book2.jpg"));
            books.Add(new Book(3, "Professional ASP.NET MVC5", "Author Name Book 3", "Content/Images/Book3.jpg"));

            Book book = new Book();
            foreach (Book b in books)
            {
                if (b.Id == id)
                {
                    book = b;
                    break;
                }
            }
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        [HttpPost, ActionName("EditBook")]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(int id, string Title, string Author, string ImageCover )
        {
            var books = new List<Book>();
            books.Add(new Book(1, "HTML5 & CSS3 The complete ", "Author Name Book 1", "Content/Images/Book1.jpg"));
            books.Add(new Book(2, "HTML & CSS Responsive web Design cookbook", "Author Name Book 2", "Content/Images/Book2.jpg"));
            books.Add(new Book(3, "Professional ASP.NET MVC5", "Author Name Book 3", "Content/Images/Book3.jpg"));
            if (id == null)
            {
                return HttpNotFound();
            }
            foreach (Book b in books)
            {
                if (b.Id == id)
                {
                    b.Title = Title;
                    b.Author = Author;
                    b.ImageCover = ImageCover;
                    break;
                }
            }
            return View("ListBookModel", books);
        }

        public ActionResult CreateBook()
        {
            return View();
        }

        [HttpPost, ActionName("CreateBook")]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "id , Title ,Author , ImageCover")] Book book)
        {
            var books = new List<Book>();
            books.Add(new Book(1, "HTLM5 & CSS3 The complete Manual", "Author Name Book 1", "/Content/images/Book1.jpg"));
            books.Add(new Book(2, "HTLM5 & CSS3 Responsive web Design cookbook ", " Author Name Book 2", "/Content/images/Book2.jpg"));
            books.Add(new Book(3, "Professional APS.NET  MVC5 ", " Author Name Book 3", "/Content/images/Book3.jpg"));
            try
            {
                if (ModelState.IsValid)
                {
                    
                    books.Add(book);
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }
            
            return View("ListBookModel", books);
        }
    }
}
