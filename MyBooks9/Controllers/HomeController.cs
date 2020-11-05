using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBooks9.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using static MyBooks9.Helper;

namespace MyBooks9.Controllers
{
    public class HomeController : Controller
    {
        private readonly BooksContext db;
        public HomeController(BooksContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Books.ToListAsync());
        }

        //AddOrEdit

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new MyBooksModel());
            else
            {
                var booksModel = await db.Books.FindAsync(id);
                if (booksModel == null)
                {
                    return NotFound();
                }
                return View(booksModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,NameBook,GenreBook,AuthorBook,Pages,Data")] MyBooksModel booksModel)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    booksModel.Date = DateTime.Now;
                    db.Add(booksModel);
                    await db.SaveChangesAsync();

                }
                // Обнова
                else
                {
                    try
                    {
                        booksModel.Date = DateTime.Now;
                        db.Update(booksModel);
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BooksModelExists(booksModel.Id))
                        {
                            return NotFound();
                        }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = RenderRazorViewToString(this, "Index", db.Books.ToList()) });
            }
            return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddOrEdit", booksModel) });
        }

        private bool BooksModelExists(int id)
        {
            return db.Books.Any(e => e.Id == id);
        }

        //Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booksModel = await db.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booksModel == null)
            {
                return NotFound();
            }

            return View(booksModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booksModel = await db.Books.FindAsync(id);
            db.Books.Remove(booksModel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
            //return Json(new { html = RenderRazorViewToString(this, "Index", db.Books.ToList()) });
        }



    }


}