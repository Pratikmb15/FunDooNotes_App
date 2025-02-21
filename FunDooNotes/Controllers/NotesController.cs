using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System.Security.Claims;

namespace FunDooNotes.Controllers
{
    [Authorize]
    [Route("api/notes")]
    [ApiController]
    public class NotesController : ControllerBase
    {
       
        private readonly INotesBusiness _notesBusiness;

        public NotesController(INotesBusiness notesBusiness)
        {
            _notesBusiness = notesBusiness;
        }

        [HttpGet]
        public IActionResult GetMyNotes()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var notes = _notesBusiness.GetUserNotes(userId);
            return Ok(new { Success = true, Data = notes });
        }

        [HttpPost("create")]
        public IActionResult CreateNote([FromBody] NoteModel noteModel)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId==null)
            {
                return Unauthorized(new { Success = false, Message = "Invalid token. Please log in again." });
            }
            var note = new Notes();
            
               note.Id = userId;
               note.Title = noteModel.Title;
               note.Description = noteModel.Description;
               note.Color = noteModel.Color;
            
            var createdNote = _notesBusiness.CreateNote(note);

            return Ok(new { Success = true, Message = "Note created successfully", Data = createdNote });    
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateNote(int id, [FromBody] NoteModel updatedNote)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                return Unauthorized(new { Success = false, Message = "Invalid token. Please log in again." });
            }
            var existingNote = _notesBusiness.GetUserNotes(userId).FirstOrDefault(n => n.Notes_id == id);

            if (existingNote == null)
                return NotFound(new { Success = false, Message = "Note not found" });

            existingNote.Title = updatedNote.Title;
            existingNote.Description = updatedNote.Description;
            existingNote.Color = updatedNote.Color;

            var updated = _notesBusiness.UpdateNote(existingNote);
            return Ok(new { Success = true, Data = updated });
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteNote(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var deleted = _notesBusiness.DeleteNote(id, userId);

            if (!deleted)
                return NotFound(new { Success = false, Message = "Note not found" });

            return Ok(new { Success = true, Message = "Note deleted successfully" });
        }
    }
}
