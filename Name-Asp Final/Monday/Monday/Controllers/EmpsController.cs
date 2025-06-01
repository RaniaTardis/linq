using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Monday.Models;

namespace Monday.Controllers
{
    public class EmpsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmpsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Login(Emp model)
        {
            var data = _context.Emps
                .Where(tbl => tbl.Email == model.Email && tbl.Password == model.Password)
                .ToList().FirstOrDefault();

            if (data != null)
                return RedirectToAction("Index");

            return View();
        }



        [HttpPost]

        public IActionResult Index(int t1)
        {
            var data = _context.Emps.Where(tbl => tbl.GSalary >= t1).ToList();
            return View(data);
        }

        // GET: Emps
        public async Task<IActionResult> Index()
        {
            ViewBag.count = _context.Emps.Count();
            ViewBag.max = _context.Emps.Max(tbl=> tbl.GSalary);
            ViewBag.min = _context.Emps.Min(tbl => tbl.GSalary);
            ViewBag.avg = _context.Emps.Average(tbl => tbl.GSalary);

            return _context.Emps != null ? 
                          View(await _context.Emps.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Emps'  is null.");
        }

        // GET: Emps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Emps == null)
            {
                return NotFound();
            }

            var emp = await _context.Emps
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        // GET: Emps/Create
        public IActionResult Create()
        {
            ViewBag.items = new List<string> { "CS","SE","AI" };
            return View();
        }

        // POST: Emps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (EmpViewModel model)
        {
            if (ModelState.IsValid)
            {
                Emp emp = new Emp();
                emp.Id = model.Id;
                emp.Ename = model.Ename;
                emp.Dept = model.Dept;
                emp.GSalary = model.Salary - model.Deduction;
                emp.Email = model.Email;
                emp.Password = model.Password;
                emp.EImage = UploadedFile(model);


                _context.Add(emp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private string UploadedFile(EmpViewModel model)
        {
            string uniqueFileName = null;

            if (model.EImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = model.EImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.EImage.CopyTo(fileStream);
                }
            }
            return "images/" + uniqueFileName;
        }



        // GET: Emps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Emps == null)
            {
                return NotFound();
            }

            var emp = await _context.Emps.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        // POST: Emps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ename,Dept,GSalary,Email,Password,EImage")] Emp emp)
        {
            if (id != emp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpExists(emp.Id))
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
            return View(emp);
        }

        // GET: Emps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Emps == null)
            {
                return NotFound();
            }

            var emp = await _context.Emps
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        // POST: Emps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Emps == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Emps'  is null.");
            }
            var emp = await _context.Emps.FindAsync(id);
            if (emp != null)
            {
                _context.Emps.Remove(emp);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpExists(int id)
        {
          return (_context.Emps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
