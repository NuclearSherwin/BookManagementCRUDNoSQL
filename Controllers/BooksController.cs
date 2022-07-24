using BookManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace BookMan.Mvc.Controllers
{
    public class BookController : Controller
    {
        private readonly Service _service;
        public BookController(Service service)
        {
            _service = service;
        }

        //=================INDEX===============================

        public IActionResult Index(int page = 1)
        {
            var model = _service.Paging(page);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            return View(model.books);
        }


        //=================DETAIL===============================
        public IActionResult Details(int id)
        {
            var book = _service.Get(id);
            if (book == null) return NotFound();
            return View(book);
        }


        //=================DELETE===============================
        public IActionResult Delete(int id)
        {
            var b = _service.Get(id);
            if (b == null) return NotFound();
            return View(b);
        }

        [HttpPost]
        public IActionResult Delete(Book book)
        {
            _service.Delete(book.Id);
            _service.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



        //=================CREATE===============================
        public IActionResult Create() => base.View(_service.Create());

        [HttpPost]
        public IActionResult Create(Book book, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                _service.Upload(book, file);
                _service.Add(book);
                _service.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }


        //=================UPDATE===============================
        [HttpGet]
        public IActionResult Update(int id)
        {
            var bookUpdate = _service.Get(id);
            if (bookUpdate == null) return NotFound();
            return View(bookUpdate);
        }

        [HttpPost]
        public IActionResult Update(Book book, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                _service.Upload(book, file);
                _service.Update(book);
                _service.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(book);

        }


        public IActionResult Read(int id)
        {
            var book = _service.Get(id);
            if (book == null) return NotFound();
            if (!System.IO.File.Exists(_service.GetDataPath(book.DataFile))) return NotFound();
            var (stream, type) = _service.Download(book);
            return File(stream, type, book.DataFile);
        }


        //=====================SEARCH==========================
        public IActionResult Search(string stringText)
        {


            return View(nameof(Index), _service.Get(stringText));
        }

    }
}