using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resultms.Areas.Identity.Data;
using Resultms.Models;

namespace Resultms.Controllers
{
    public class StudentController : Controller
    {
        public ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index() 
        {
           var students = _context.Students.ToList();
                return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string name)
        {
            ViewData["GetStudentDetail"] =name;
            Console.WriteLine("Hello");
            var stuquery = from x in _context.Students select x;
            if(!string.IsNullOrEmpty(name))
                    {
                stuquery = stuquery.Where(x => x.Name.Contains(name));
            }
            
            return View(await stuquery.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                var st = new Student()
                {
                    Roll = s.Roll,
                    Name = s.Name,
                    Math = s.Math,
                    Science = s.Science,
                    Total = s.Total
                };
                _context.Students.Add(st);
                _context.SaveChanges();
                TempData["msg"] = "Saved";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Not valid";
                return View();
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var getstdetails = await _context.Students.FindAsync(id);
            return View(getstdetails);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var getstdetails = await _context.Students.FindAsync(id);
            _context.Students.Remove(getstdetails);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if(id==null)
            {
                return RedirectToAction("Index");
            }
            var getstdetails = await _context.Students.FindAsync(id);
            return View(getstdetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student s)
        {
            if(ModelState.IsValid)
            {
                _context.Students.Update(s);
                await _context.SaveChangesAsync();
                TempData["msg"] = "Successfully updated";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Some error occured";
                return View();
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id==null)
            {
                return RedirectToAction("Index");
            }
            var getstdetails = await _context.Students.FindAsync(id);
            return View(getstdetails);
        }
    }
}
