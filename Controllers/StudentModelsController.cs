using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using reviseAuthentication.Data;
using reviseAuthentication.Models;
using reviseAuthentication.Views.Shared.Components.SearchBar;

namespace reviseAuthentication.Controllers
{
    public class StudentModelsController : Controller
    {
        private readonly MyDbContext _context;

        public StudentModelsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: StudentModels
        public IActionResult Index(string SearchText = "", int pg=1)
        {
            List<StudentModel> StudentModel = _context.StudentModel.ToList();

            //Pagination
           
            if (SearchText != "" && SearchText != null)
            {
                     StudentModel =  _context.StudentModel
                    .Where(s => s.FullName.Contains(SearchText))
                    .ToList();
            }
            else
            

                StudentModel = _context.StudentModel.ToList();
                SPager SearchPager = new SPager() { Action = "Index", Controller = "StudentModels", SearchText = SearchText };
                ViewBag.SearchPager = SearchPager;

            const int pageSize = 5;
            if (pg < 1)
                pg = 1;

            int rescCount = StudentModel.Count();
            var pager = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg-1) * pageSize;
            var data = StudentModel.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;


            return View(data);
            
        }

        // GET: StudentModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentModel == null)
            {
                return NotFound();
            }

            var studentModel = await _context.StudentModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentModel == null)
            {
                return NotFound();
            }

            return View(studentModel);
        }

        // GET: StudentModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Address")] StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentModel);
        }

        // GET: StudentModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentModel == null)
            {
                return NotFound();
            }

            var studentModel = await _context.StudentModel.FindAsync(id);
            if (studentModel == null)
            {
                return NotFound();
            }
            return View(studentModel);
        }

        // POST: StudentModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Address")] StudentModel studentModel)
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

        // GET: StudentModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentModel == null)
            {
                return NotFound();
            }

            var studentModel = await _context.StudentModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentModel == null)
            {
                return NotFound();
            }

            return View(studentModel);
        }

        // POST: StudentModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentModel == null)
            {
                return Problem("Entity set 'MyDbContext.StudentModel'  is null.");
            }
            var studentModel = await _context.StudentModel.FindAsync(id);
            if (studentModel != null)
            {
                _context.StudentModel.Remove(studentModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentModelExists(int id)
        {
          return (_context.StudentModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
