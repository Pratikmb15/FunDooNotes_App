using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NotesRepository : INotesRepository
    {
        private readonly AppDbContext _context;

        public NotesRepository(AppDbContext context)
        {
            _context = context;
        }

        public Notes CreateNote( Notes note)
        {

            _context.Notes.Add(note);
            _context.SaveChanges();
            return note;
        }

        public List<Notes> GetUserNotes(int userId)
        {
            return _context.Notes.Where(n => n.Id == userId).ToList();
        }
        public Notes GetNotesById(int noteId, int userId)
        {
            Notes note = _context.Notes.FirstOrDefault(x => x.Notes_id == noteId && x.Id == userId);
            if (note == null)
            { 
            throw new Exception("Note not found");
            }
            return note;
        }

        public Notes UpdateNote(int UserId, Notes note)
        {
            var ENote = _context.Notes.FirstOrDefault(n => n.Notes_id == note.Notes_id && n.Id == UserId);
            if (ENote == null)
            {
                throw new Exception("Note not found"); 
            }
            _context.Notes.Update(note);
            _context.SaveChanges();
            return note;
        }

        public bool DeleteNote(int noteId, int userId)
        {
            var note = _context.Notes.FirstOrDefault(n => n.Notes_id == noteId && n.Id == userId);
            if (note == null) return false;

            _context.Notes.Remove(note);
            _context.SaveChanges();
            return true;
        }
        public bool checkDeletedNote(int NoteId, int UserId) {
            var note = _context.Notes.FirstOrDefault(x => x.Notes_id == NoteId && x.Id==UserId);
            if (note == null) {
                return false;
            }
            note.IsDeleted = !note.IsDeleted;
            _context.SaveChanges();
            return true;
        }
        public bool checkArchieved(int NoteId, int UserId) {
         var note = _context.Notes.FirstOrDefault(x => x.Notes_id == NoteId && x.Id == UserId);
            
                if (note == null)
                {
                    return false;
                }     
                note.isArchive = !note.isArchive;
                _context.SaveChanges();
                return true; 
        }
    }
}
