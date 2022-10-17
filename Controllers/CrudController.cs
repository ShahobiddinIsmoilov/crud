using crud.Data;
using crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CrudController : ControllerBase
    {
        private readonly CrudDbContext _context;
        public CrudController(CrudDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Crud>> Get()
            => await _context.Crudd.ToListAsync();

        [HttpGet("id")]
        [ProducesResponseType(typeof(Crud), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var crud = await _context.Crudd.FindAsync(id);
            
            return crud == null ? NotFound() : Ok(crud);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Crud crud)
        {
            await _context.Crudd.AddAsync(crud);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new {id = crud.Id}, crud);
        }

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Crud crud)
        {
            if (id != crud.Id) return BadRequest();

            _context.Entry(crud).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var crudToDelete = await _context.Crudd.FindAsync(id);
            if (crudToDelete == null) return NotFound();

            _context.Crudd.Remove(crudToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [Route("upload")]
        public IActionResult Upload(IFormFile file)
        {
            
            return Ok("File upload API is running...");
        }
    }
}