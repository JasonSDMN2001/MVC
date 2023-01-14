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
    public class ProfessorsController : Controller
    {
        private readonly MVCDBContext _context;

        public ProfessorsController(MVCDBContext context)
        {
            _context = context;
        }

        // GET: Professors
        public async Task<IActionResult> Index()
        {
            var mVCDBContext = _context.Professors.Include(p => p.UsernameNavigation);
            return View(await mVCDBContext.ToListAsync());
        }

        // GET: Professors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Professors == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors
                .Include(p => p.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Afm == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // GET: Professors/Create
        public IActionResult Create()
        {
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username");
            return View();
        }

        // POST: Professors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Afm,Name,Surname,Department,Username")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", professor.Username);
            return View(professor);
        }

        // GET: Professors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Professors == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", professor.Username);
            return View(professor);
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Afm,Name,Surname,Department,Username")] Professor professor)
        {
            if (id != professor.Afm)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(professor.Afm))
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
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", professor.Username);
            return View(professor);
        }

        // GET: Professors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Professors == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors
                .Include(p => p.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Afm == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Professors == null)
            {
                return Problem("Entity set 'MVCDBContext.Professors'  is null.");
            }
            var professor = await _context.Professors.FindAsync(id);
            if (professor != null)
            {
                _context.Professors.Remove(professor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(int id)
        {
          return _context.Professors.Any(e => e.Afm == id);
        }
        
        public async Task<IActionResult> Grades(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.AfmNavigation)
                .ThenInclude(c=>c.Courses)
                .ThenInclude(c=>c.CourseHasStudents)
                .FirstOrDefaultAsync(m => m.IdCourse == id)
                ;
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        public async Task<IActionResult> ToGrade(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.AfmNavigation)
                .ThenInclude(c => c.Courses)
                .ThenInclude(c => c.CourseHasStudents)
                .FirstOrDefaultAsync(m => m.IdCourse == id)
                ;
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        public async Task<IActionResult> Professors()
        {
            var mVCDBContext = _context.Professors.Include(x=>x.Courses);
            return View(await mVCDBContext.ToListAsync());
        }
        public async Task<IActionResult> Professors2()
        {
            var mVCDBContext = _context.Professors.Include(x => x.Courses);
            return View(await mVCDBContext.ToListAsync());
        }
        public async Task<IActionResult> Subjects2(int? id)
        {
            if (id == null || _context.Professors == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors
                .Include(x => x.Courses)
                .ThenInclude(x=>x.CourseHasStudents)
                .FirstOrDefaultAsync(m => m.Afm == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }
        public async Task<IActionResult> Subjects(int? id)
        {
            if (id == null || _context.Professors == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors
                .Include(x => x.Courses)
                .ThenInclude(x => x.CourseHasStudents)
                .FirstOrDefaultAsync(m => m.Afm == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }
        public async Task<IActionResult> Grading(int? id)
        {
            if (id == null || _context.CourseHasStudents == null)
            {
                return NotFound();
            }

            var courseHasStudent = await _context.CourseHasStudents.FindAsync(id);
            if (courseHasStudent == null)
            {
                return NotFound();
            }
            ViewData["IdCourse"] = new SelectList(_context.Courses, "IdCourse", "IdCourse", courseHasStudent.IdCourse);
            ViewData["RegistrationNumber"] = new SelectList(_context.Students, "RegistrationNumber", "RegistrationNumber", courseHasStudent.RegistrationNumber);
            return View(courseHasStudent);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Grading(int id, [Bind("IdCourse,RegistrationNumber,GradeCourseStudent,Pk")] CourseHasStudent courseHasStudent)
        {
            if (id != courseHasStudent.Pk)
            {
                return NotFound();
            }

            if (_context.CourseHasStudents != null)
            {
                try
                {
                    _context.Update(courseHasStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseHasStudentExists(courseHasStudent.Pk))
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
            ViewData["IdCourse"] = new SelectList(_context.Courses, "IdCourse", "IdCourse", courseHasStudent.IdCourse);
            ViewData["RegistrationNumber"] = new SelectList(_context.Students, "RegistrationNumber", "RegistrationNumber", courseHasStudent.RegistrationNumber);
            return View(courseHasStudent);
        }
        private bool CourseHasStudentExists(int id)
        {
            return _context.CourseHasStudents.Any(e => e.Pk == id);
        }
    }
}
