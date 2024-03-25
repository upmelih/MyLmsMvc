using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLmsMvc.Data;
using MyLmsMvc.Models;

namespace MyLmsMvc.Controllers
{   

    public class BorrowingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BorrowingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Borrowings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Borrowing.Include(b => b.Book).Include(b => b.Reader);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Borrowings/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing
                .Include(b => b.Book)
                .Include(b => b.Reader)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        // GET: Borrowings/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "ISBN");
            ViewData["ReaderId"] = new SelectList(_context.Reader, "Id", "Email");
            return View();
        }

        // POST: Borrowings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,BookId,ReaderId,BorrowDate,DueDate,ReturnDate")] Borrowing borrowing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", borrowing.BookId);
            ViewData["ReaderId"] = new SelectList(_context.Reader, "Id", "FullName", borrowing.ReaderId);
            return View(borrowing);
        }

        // GET: Borrowings/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing.FindAsync(id);
            if (borrowing == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "ISBN", borrowing.BookId);
            ViewData["ReaderId"] = new SelectList(_context.Reader, "Id", "FullName", borrowing.ReaderId);
            return View(borrowing);
        }

        // POST: Borrowings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,ReaderId,BorrowDate,DueDate,ReturnDate")] Borrowing borrowing)
        {
            if (id != borrowing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowingExists(borrowing.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Author", borrowing.BookId);
            ViewData["ReaderId"] = new SelectList(_context.Reader, "Id", "ContactNumber", borrowing.ReaderId);
            return View(borrowing);
        }

        // GET: Borrowings/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing
                .Include(b => b.Book)
                .Include(b => b.Reader)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        // POST: Borrowings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrowing = await _context.Borrowing.FindAsync(id);
            if (borrowing != null)
            {
                _context.Borrowing.Remove(borrowing);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowingExists(int id)
        {
            return _context.Borrowing.Any(e => e.Id == id);
        }
    }
}
