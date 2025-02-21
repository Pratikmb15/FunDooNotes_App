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

        public Notes CreateNote(Notes note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();
            return note;
        }

        public List<Notes> GetUserNotes(int userId)
        {
            return _context.Notes.Where(n => n.Id == userId).ToList();
        }

        public Notes UpdateNote(Notes note)
        {
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
    }
}
