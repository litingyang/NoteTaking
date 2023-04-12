using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NotesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/NotesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNote()
        {
          if (_context.Note == null)
          {
              return NotFound();
          }
            // return await _context.Note.ToListAsync();
            string name = User.Identity != null ? User.Identity.Name : "";
            return await _context.Note.Where(j => j.User.Equals(name) | j.Content.Equals("")).ToListAsync();

        }

        // GET: api/NotesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
          if (_context.Note == null)
          {
              return NotFound();
          }
            var note = await _context.Note.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            if(note.User == User.Identity.Name)
            {
                return note;
            }
            return NoContent();

        }

        // PUT: api/NotesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, Note note)
        {
            var update = (from a in _context.Note
                          where a.Id == id
                          select a).SingleOrDefault();
            // var old = await _context.Note.FindAsync(id);
            if (id != note.Id | update.User != User.Identity.Name)
            {
                return BadRequest();
            }

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
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

        // POST: api/NotesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
          if (_context.Note == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Note'  is null.");
          }
            _context.Note.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.Id }, note);
        }

        // DELETE: api/NotesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            if (_context.Note == null)
            {
                return NotFound();
            }
            var note = await _context.Note.FindAsync(id);
                if (note == null | note.User != User.Identity.Name)
            {
                return NotFound();
            }

            _context.Note.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteExists(int id)
        {
            return (_context.Note?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
