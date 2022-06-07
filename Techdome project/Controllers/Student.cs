using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Techdome_project.Models;

namespace Techdome_project.Controllers
{
    public class Student : Controller
    {
        private ApplicationDbContext _db;

        public Student(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]

        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var employee = _db.students.FirstOrDefault(m => m.Id == Id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(Student stu)
        {
            if (ModelState.IsValid)
            {
                _db.Add(stu);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(stu);
        }

        public IActionResult List()
        {
            var data = _db.students.ToList();

            return View(data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.students.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Student stu)
        {

            if (ModelState.IsValid)
            {
                _db.Update(stu);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("List");
            }
            return View(stu);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empdb = _db.students.Find(id);
            if (empdb == null)
            {
                return NotFound();
            }
            return View(empdb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.students.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.students.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
