using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CoursesController : Controller
    {
        private readonly MVCDBContext _context;

        public CoursesController(MVCDBContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var mVCDBContext = _context.Courses.Include(c => c.AfmNavigation).OrderBy(c => c.CourseSemaster);
            return View(await mVCDBContext.ToListAsync());
        }

        public async Task<IActionResult> Index2()
        {
            var mVCDBContext = _context.Courses.Include(c => c.AfmNavigation).OrderBy(c => c.CourseSemaster);
            return View(await mVCDBContext.ToListAsync());
        }

        public async Task<IActionResult> Index3()
        {
            var mVCDBContext = _context.Courses.Include(c => c.AfmNavigation).OrderBy(c => c.CourseSemaster);
            return View(await mVCDBContext.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.AfmNavigation)
                .FirstOrDefaultAsync(m => m.IdCourse == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["Afm"] = new SelectList(_context.Professors, "Afm", "Afm");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCourse,CourseTitle,CourseSemaster,Afm")] Course course)
        {
            if (_context.Courses != null)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Afm"] = new SelectList(_context.Professors, "Afm", "Afm", course.Afm);
            return View(course);
        }

        public async Task<IActionResult> Anathesi(int? id)
        {
            // var mVCDBContext = _context.Courses.Include(c => c.AfmNavigation);
            // return View(await mVCDBContext.ToListAsync());
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["Afm"] = new SelectList(_context.Professors, "Afm", "Afm", course.AfmNavigation);
            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Anathesi(int id, [Bind("IdCourse,CourseTitle,CourseSemaster,Afm")] Course course)
        {
            if (id != course.IdCourse)
            {
                return NotFound();
            }

            if (_context.Courses != null)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.IdCourse))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index2));
            }

            ViewData["Afm"] = new SelectList(_context.Professors, "Afm", "Afm", course.AfmNavigation);
            return View(course);

        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["Afm"] = new SelectList(_context.Professors, "Afm", "Afm", course.Afm);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCourse,CourseTitle,CourseSemaster,Afm")] Course course)
        {
            if (id != course.IdCourse)
            {
                return NotFound();
            }

            if (_context.Courses != null)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.IdCourse))
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
            ViewData["Afm"] = new SelectList(_context.Professors, "Afm", "Afm", course.Afm);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.AfmNavigation)
                .FirstOrDefaultAsync(m => m.IdCourse == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'MVCDBContext.Courses'  is null.");
            }
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
          return _context.Courses.Any(e => e.IdCourse == id);
        }
    }
}
