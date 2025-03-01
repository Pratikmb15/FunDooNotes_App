using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using MassTransit.Internals.GraphValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ICollaboratorService _collaboratorService; 
        private readonly INotesBusiness _notesBusiness;
        private readonly ILogger<NotesController> _logger;

        public NotesController(INotesBusiness notesBusiness, ICollaboratorService collaboratorService, ILogger<NotesController> logger)
        {
            _notesBusiness = notesBusiness;
            _collaboratorService = collaboratorService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetMyNotes()
        {
            _logger.LogInformation("Fetching all notes...");
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var notes = _notesBusiness.GetUserNotes(userId);
            var Collaborators = _collaboratorService.GetCollaborators(userId);
            if (notes == null)
                return NotFound(new { Success = false, Message = "No notes found" });
            if (Collaborators != null)
            {
                var response = new { Notes = notes, Collaborators = Collaborators };
                return Ok(new { Success = true, Data = response });
            }

            _logger.LogInformation($"Fetched notes.");
            return Ok(new { Success = true, Data = notes });
        }

        [HttpPost]
        public IActionResult CreateNote([FromBody] NoteModel noteModel)
        {
            _logger.LogInformation($"Creating a new note: {noteModel.Title}");
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                return Unauthorized(new { Success = false, Message = "Invalid token. Please log in again." });
            }
            var note = new Notes();

            note.Id = userId;
            note.Title = noteModel.Title;
            note.Description = noteModel.Description;
            note.Color = noteModel.Color;

            var createdNote = _notesBusiness.CreateNote(note);
            _logger.LogInformation($"Created a new note: {note.Id}");
            return Ok(new { Success = true, Message = "Note created successfully", Data = createdNote });
        }

        [HttpPut("/{id}")]
        public IActionResult UpdateNote(int id, [FromBody] NoteModel updatedNote)
        {
            _logger.LogInformation($"Creating a new note:  NoteId_{id}");
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                return Unauthorized(new { Success = false, Message = "Invalid token. Please log in again." });
            }
            var existingNote = _notesBusiness.GetUserNotes(userId).FirstOrDefault(n => n.Notes_id == id);

            if (existingNote == null)
            {
                return NotFound(new { Success = false, Message = "Note not found" });
            }

            existingNote.Title = updatedNote.Title;
            existingNote.Description = updatedNote.Description;
            existingNote.Color = updatedNote.Color;

            var updated = _notesBusiness.UpdateNote(userId, existingNote);
            _logger.LogInformation($"Updated note: NoteId_{id}");
            return Ok(new { Success = true, Data = updated });
        }

        [HttpDelete("/{id}")]
        public IActionResult DeleteNote(int id)
        {
            _logger.LogInformation($"Deleting a note: NoteId_{id}");
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var deleted = _notesBusiness.DeleteNote(id, userId);

            if (!deleted)
                return NotFound(new { Success = false, Message = "Note not found" });

            _logger.LogInformation($"Deleted a note: NoteId_{id}");
            return Ok(new { Success = true, Message = "Note deleted successfully" });
        }

        [HttpPut("/{noteId}/ToggleArchive")]
        public IActionResult ToggleArchive(int noteId)
        {

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Notes note = _notesBusiness.GetNotesById(noteId, userId);
            if (note.IsDeleted == true)
            {
                return BadRequest(new { success = false, Message = "Note cannot be Archieved : Note is in Trash" });
            }
            var archieved = _notesBusiness.ToggleArchieved(noteId, userId);
            note = _notesBusiness.GetNotesById(noteId, userId);
            return Ok(new { success = true, Message = "Note Archieve Toggled Successfully",Data =$"Note Archived Status :{note.isArchive}" });
        }

        [HttpPut("/{noteId}/ToggleTrash")]
        public IActionResult ToggleTrash(int noteId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var Trashed = _notesBusiness.ToggleNoteDeleted(noteId, userId);
            Notes note = _notesBusiness.GetNotesById(noteId, userId);
            return Ok(new { success = true, Message = "Note Trash Toggled Successfully", Data = $"Note Trashed Status :{note.IsDeleted}" });
        }
    }
}
