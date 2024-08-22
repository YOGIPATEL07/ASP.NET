using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using assignments.Models;
using Microsoft.AspNetCore.Authorization;

namespace assignments.Controllers
{
    public class ConsolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Consoles
        public async Task<IActionResult> Index()
        {
              return _context.Consoles != null ? 
                          View(await _context.Consoles.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Consoles'  is null.");
        }

        // GET: Consoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Consoles == null)
            {
                return NotFound();
            }

            var console = await _context.Consoles
                .FirstOrDefaultAsync(m => m.ConsoleId == id);
            if (console == null)
            {
                return NotFound();
            }

            return View(console);
        }

        // GET: Consoles/Create
         [Authorize(Roles="Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsoleId,Title,Description,Price")] Models.Console console)
        {
            if (ModelState.IsValid)
            {
                _context.Add(console);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(console);
        }

        // GET: Consoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Consoles == null)
            {
                return NotFound();
            }

            var console = await _context.Consoles.FindAsync(id);
            if (console == null)
            {
                return NotFound();
            }
            return View(console);
        }

        // POST: Consoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsoleId,Title,Description,Price")] Models.Console console)
        {
            if (id != console.ConsoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(console);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsoleExists(console.ConsoleId))
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
            return View(console);
        }

        // GET: Consoles/Delete/5
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Consoles == null)
            {
                return NotFound();
            }

            var console = await _context.Consoles
                .FirstOrDefaultAsync(m => m.ConsoleId == id);
            if (console == null)
            {
                return NotFound();
            }

            return View(console);
        }

        // POST: Consoles/Delete/5
        [Authorize(Roles="Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Consoles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Consoles'  is null.");
            }
            var console = await _context.Consoles.FindAsync(id);
            if (console != null)
            {
                _context.Consoles.Remove(console);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsoleExists(int id)
        {
          return (_context.Consoles?.Any(e => e.ConsoleId == id)).GetValueOrDefault();
        }
    }
}
