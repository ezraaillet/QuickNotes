using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace NotesApp.NotesController;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly Context _context;
    
    public HomeController(Context context) => _context = context;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var notesQuery = await _context.Notes.ToListAsync();

        return Ok(notesQuery);
    }


    [HttpPost]
    public async Task<IActionResult> InsertNote(NoteDto note)
    {
        _context.Notes.Add(new Note { Content = note.Note });
        await _context.SaveChangesAsync();
        return Ok();
    }

    public class NoteDto
    {
        public string Note { get; set; }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(int id)
    {
        var note = await _context.Notes.FindAsync(id);

        if (note == null)
        {
            return NotFound();
        }

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    
    public async Task<IActionResult> UpdateNote(int id, Note note)
    {
        var noteToUpdate = await _context.Notes.FindAsync(id);
        if (noteToUpdate == null) return BadRequest();

        noteToUpdate.Content = note.Content;

        await _context.SaveChangesAsync();
        return NoContent();
    }

}
