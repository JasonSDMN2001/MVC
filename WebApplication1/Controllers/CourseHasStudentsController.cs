using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using X.PagedList;

namespace WebApplication1.Controllers
{
    public class CourseHasStudentsController : Controller
    {
        private readonly MVCDBContext _context;

        public CourseHasStudentsController(MVCDBContext context)
        {
            _context = context;
        }

        // GET: CourseHasStudents
        public IActionResult Index(int? page,string? search)
        {
            // var mVCDBContext = _context.CourseHasStudents.Include(c => c.IdCourseNavigation).Include(c => c.RegistrationNumberNavigation);
            // return View(await mVCDBContext.ToListAsync());
            ViewData["CurrentFilter"] = search;
            var customers = from c in _context.CourseHasStudents
                            select c;
          //  var namecourse = from d in _context.CourseHasStudents select d;
          //  var namestudent = from e in _context.CourseHasStudents select e;


            if (!String.IsNullOrEmpty(search))
            {
                customers = customers.Where(c => c.RegistrationNumber.ToString() == search );
                                       
            }

            customers = customers.OrderBy(c => c.RegistrationNumber).ThenBy(c => c.GradeCourseStudent);
            // Pagination
            if (page != null && page < 1)
            {
                page = 1;
            }

            int PageSize = 10;
            var customersData = customers.ToPagedList(page ?? 1, PageSize);

            return View(customersData);

        }

        // GET: CourseHasStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CourseHasStudents == null)
            {
                return NotFound();
            }

            var courseHasStudent = await _context.CourseHasStudents
                .Include(c => c.IdCourseNavigation)
                .Include(c => c.RegistrationNumberNavigation)
                .FirstOrDefaultAsync(m => m.Pk == id);
            if (courseHasStudent == null)
            {
                return NotFound();
            }

            return View(courseHasStudent);
        }

        // GET: CourseHasStudents/Create
        public IActionResult Create()
        {
            ViewData["IdCourse"] = new SelectList(_context.Courses, "IdCourse", "CourseTitle");
            ViewData["RegistrationNumber"] = new SelectList(_context.Students, "RegistrationNumber", "RegistrationNumber");
            return View();
        }

        // POST: CourseHasStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCourse,RegistrationNumber,GradeCourseStudent,Pk")] CourseHasStudent courseHasStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseHasStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCourse"] = new SelectList(_context.Courses, "IdCourse", "CourseTitle", courseHasStudent.IdCourse);
            ViewData["RegistrationNumber"] = new SelectList(_context.Students, "RegistrationNumber", "RegistrationNumber", courseHasStudent.RegistrationNumber);
            return View(courseHasStudent);
        }

        // GET: CourseHasStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["IdCourse"] = new SelectList(_context.Courses, "IdCourse", "CourseTitle", courseHasStudent.IdCourse);
            ViewData["RegistrationNumber"] = new SelectList(_context.Students, "RegistrationNumber", "RegistrationNumber", courseHasStudent.RegistrationNumber);
            return View(courseHasStudent);
        }

        // POST: CourseHasStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCourse,RegistrationNumber,GradeCourseStudent,Pk")] CourseHasStudent courseHasStudent)
        {
            if (id != courseHasStudent.Pk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
            ViewData["IdCourse"] = new SelectList(_context.Courses, "IdCourse", "CourseTitle", courseHasStudent.IdCourse);
            ViewData["RegistrationNumber"] = new SelectList(_context.Students, "RegistrationNumber", "RegistrationNumber", courseHasStudent.RegistrationNumber);
            return View(courseHasStudent);
        }

        // GET: CourseHasStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CourseHasStudents == null)
            {
                return NotFound();
            }

            var courseHasStudent = await _context.CourseHasStudents
                .Include(c => c.IdCourseNavigation)
                .Include(c => c.RegistrationNumberNavigation)
                .FirstOrDefaultAsync(m => m.Pk == id);
            if (courseHasStudent == null)
            {
                return NotFound();
            }

            return View(courseHasStudent);
        }

        // POST: CourseHasStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CourseHasStudents == null)
            {
                return Problem("Entity set 'MVCDBContext.CourseHasStudents'  is null.");
            }
            var courseHasStudent = await _context.CourseHasStudents.FindAsync(id);
            if (courseHasStudent != null)
            {
                _context.CourseHasStudents.Remove(courseHasStudent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseHasStudentExists(int id)
        {
          return _context.CourseHasStudents.Any(e => e.Pk == id);
        }
    }
}
