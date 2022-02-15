using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMvcProject.DAL;
using DemoMvcProject.Models;
using DemoMvcProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace DemoMvcProject.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly SchoolContext _context;

        public StudentController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Student

        public async Task<IActionResult> Index(IndexView indexView)
        {
            indexView = await IndexView.GetViewInstance(_context);
            ViewBag.Total = indexView.students.Count();
            return View(indexView);
        }

        [HttpPost]
        public async Task<IActionResult> Index(StudentSearch search)
        {
            if (ModelState.IsValid)
            {
                var indexViewResult = await IndexView.GetInstance(_context, search);
                ViewBag.Total = indexViewResult.students.Count();
                return View(indexViewResult);
            }
            else
            {
                var indexViewResult = await IndexView.GetViewInstance(_context);
                ViewBag.Total = indexViewResult.students.Count();
                return View(indexViewResult);
            }
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentModel = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentModel == null)
            {
                return NotFound();
            }

            return View(studentModel);
        }


        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,DOB,Email,Phone")] StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                studentModel.Id = Guid.NewGuid();
                _context.Add(studentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentModel);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentModel = await _context.Students.FindAsync(id);
            if (studentModel == null)
            {
                return NotFound();
            }
            return View(studentModel);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FirstName,LastName,DOB,Email,Phone")] StudentModel studentModel)
        {
            if (id != studentModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentModelExists(studentModel.Id))
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
            return View(studentModel);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentModel = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentModel == null)
            {
                return NotFound();
            }

            return View(studentModel);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var studentModel = await _context.Students.FindAsync(id);
            _context.Students.Remove(studentModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckEmailPresent(String Email, Guid Id)
        {
            bool status = false;
            //string emailId = std.Email;
            try
            {
                StudentModel student = new StudentModel();
                student = _context.Students.Where(x => x.Email == Email && x.Id != Id).FirstOrDefault();


                if (student == null)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }

                return Json(status);
            }
            catch
            {
                return Json(status);
            }
        }

        private bool StudentModelExists(Guid id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        public IActionResult Populate()
        {
            return View("Population");
        }

        [HttpPost]
        public async Task<IActionResult> Populate(int Population)
        {
            if (Population >= 0)
            {
                ViewBag.IsSucess = false;
                ViewBag.Message = Population;

                return View("Population");
            }

            for (int i = 0; i < Population; i++)
            {
                string FName = Faker.Name.First();
                string LName = Faker.Name.Last();
                DateTime D_birth = Faker.Identification.DateOfBirth();
                string mail = FName + LName.Substring(0, 2) + "_" + (D_birth.Year).ToString() + "@example.com";
                string ph = Faker.Phone.Number();

                StudentModel student = new StudentModel
                {
                    FirstName = FName,
                    LastName = LName,
                    DOB = D_birth,
                    Email = mail,
                    Phone = ph
                };

                _context.Students.Add(student);
            }

            await _context.SaveChangesAsync();
            ViewBag.IsSucess = true;
            ViewBag.Message = Population;
            return View("Population");
        }



        //Partial View For ajax requests
        public IActionResult CreatePartial()
        {
            return PartialView("_CreatePartial");
        }
        public async Task<IActionResult> DetailsPartial(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentModel = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentModel == null)
            {
                return NotFound();
            }

            return PartialView("_DetailsPartial", studentModel);
        }

        public async Task<IActionResult> EditPartial(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentModel = await _context.Students.FindAsync(id);
            if (studentModel == null)
            {
                return NotFound();
            }
            return PartialView("_EditPartial", studentModel);
        }

        public async Task<IActionResult> DeletePartial(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentModel = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentModel == null)
            {
                return NotFound();
            }

            return PartialView("_DeletePartial", studentModel);
        }


    }
}
