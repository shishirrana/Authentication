using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reviseAuthentication.Data;
using reviseAuthentication.Models;

namespace reviseAuthentication.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentModelsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public StudentModelsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/StudentModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentModel>>> GetStudentModel()
        {
          if (_context.StudentModel == null)
          {
              return NotFound();
          }
            return await _context.StudentModel.ToListAsync();
        }

        // GET: api/StudentModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentModel>> GetStudentModel(int id)
        {
          if (_context.StudentModel == null)
          {
              return NotFound();
          }
            var studentModel = await _context.StudentModel.FindAsync(id);

            if (studentModel == null)
            {
                return NotFound();
            }

            return studentModel;
        }

        // PUT: api/StudentModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentModel(int id, StudentModel studentModel)
        {
            if (id != studentModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(studentModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StudentModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentModel>> PostStudentModel(StudentModel studentModel)
        {
          if (_context.StudentModel == null)
          {
              return Problem("Entity set 'MyDbContext.StudentModel'  is null.");
          }
            _context.StudentModel.Add(studentModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentModel", new { id = studentModel.Id }, studentModel);
        }

        // DELETE: api/StudentModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentModel(int id)
        {
            if (_context.StudentModel == null)
            {
                return NotFound();
            }
            var studentModel = await _context.StudentModel.FindAsync(id);
            if (studentModel == null)
            {
                return NotFound();
            }

            _context.StudentModel.Remove(studentModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentModelExists(int id)
        {
            return (_context.StudentModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
