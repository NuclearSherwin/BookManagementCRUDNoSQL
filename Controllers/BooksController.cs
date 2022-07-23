using BookManagement.Models;
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
        public IActionResult Index()
        {
            return View(_service.Get());
        }

        public IActionResult Details(int id)
        {
            var book = _service.Get(id);
            if (book == null) return NotFound();
            return View(book);
        }

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

        [HttpGet]
        public IActionResult Update(int id)
        {
            var bookUpdate = _service.Get(id);
            if (bookUpdate == null) return NotFound();
            return View(bookUpdate);
        }

        [HttpPost]
        public IActionResult Update(Book book)
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

        public IActionResult Create() => base.View(_service.Create());

        [HttpPost]
        public IActionResult Create(Book book)
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
    }
}