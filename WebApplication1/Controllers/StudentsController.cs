using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StudentsController : Controller
    {
        private readonly MVCDBContext _context;
        private static int i = 63;

        public StudentsController(MVCDBContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var mVCDBContext = _context.Students.Include(s => s.UsernameNavigation);
            return View(await mVCDBContext.ToListAsync());
        }

        public async Task<IActionResult> Index2()
        {
            var mVCDBContext = _context.Students.Include(s => s.UsernameNavigation).Include(x => x.CourseHasStudents);
            return View(await mVCDBContext.ToListAsync());
        }

        public async Task<IActionResult> Registeredlessons(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Include(x => x.CourseHasStudents)
                .FirstOrDefaultAsync(m => m.RegistrationNumber == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }
        public async Task<IActionResult> Register(int? id)
        {
              if (id == null || _context.Students == null)
              {
                  return NotFound();
              } 
              var courseHasStudent1 = await _context.Students.FindAsync(id);
              if (courseHasStudent1 == null)
              {
                  return NotFound();
              } 
           
            ViewData["IdCourse"] = new SelectList(_context.Courses, "IdCourse", "CourseTitle");
            ViewData["RegistrationNumber"] = new SelectList(_context.Students, "RegistrationNumber", "RegistrationNumber", courseHasStudent1.RegistrationNumber);
            return View();
           
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(int id,[Bind("IdCourse,RegistrationNumber,GradeCourseStudent,Pk")] CourseHasStudent courseHasStudent)
        {
           /*  if (ModelState.IsValid)
             {
                 _context.Add(courseHasStudent);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Registeredlessons));
             } */
            // ViewData["IdCourse"] = new SelectList(_context.Courses, "IdCourse", "CourseTitle", courseHasStudent.IdCourse);
            //  ViewData["RegistrationNumber"] = new SelectList(_context.Students, "RegistrationNumber", "RegistrationNumber", courseHasStudent.RegistrationNumber);
            
            
            if (id != courseHasStudent.RegistrationNumber)
            {
                return NotFound();
            }

            if (_context.CourseHasStudents != null)
            {
                try
                {
                    while (CourseHasStudentExists2(courseHasStudent.Pk))
                    {
                        courseHasStudent.Pk = courseHasStudent.Pk + 1;
                    }
                   // i = i + 1;
                   // courseHasStudent.Pk = i;
                   // courseHasStudent.Pk = courseHasStudent.Pk + 1;
                    _context.Add(courseHasStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseHasStudentExists(courseHasStudent.RegistrationNumber))
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

            ViewData["IdCourse"] = new SelectList(_context.Courses, "IdCourse", "CourseTitle",courseHasStudent.IdCourse);
            ViewData["RegistrationNumber"] = new SelectList(_context.Students, "RegistrationNumber", "RegistrationNumber", courseHasStudent.RegistrationNumber);
            return View(courseHasStudent);
        }
        private bool CourseHasStudentExists(int id)
        {
            return _context.CourseHasStudents.Any(e => e.RegistrationNumber == id);
        }
        private bool CourseHasStudentExists2(int k)
        {
            return _context.CourseHasStudents.Any(e => e.Pk == k);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.RegistrationNumber == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrationNumber,Name,Surname,Department,Username")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", student.Username);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", student.Username);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistrationNumber,Name,Surname,Department,Username")] Student student)
        {
            if (id != student.RegistrationNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.RegistrationNumber))
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
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", student.Username);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.RegistrationNumber == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'MVCDBContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return _context.Students.Any(e => e.RegistrationNumber == id);
        }
    }
}
