using BusinessLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NotesBusiness : INotesBusiness
    {
        private readonly INotesRepository _notesRepository;

        public NotesBusiness(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public Notes CreateNote(Notes note)
        {
            return _notesRepository.CreateNote(note);
        }

        public List<Notes> GetUserNotes(int userId)
        {
            return _notesRepository.GetUserNotes(userId);
        }

        public Notes UpdateNote(Notes note)
        {
            return _notesRepository.UpdateNote(note);
        }

        public bool DeleteNote(int noteId, int userId)
        {
            return _notesRepository.DeleteNote(noteId, userId);
        }
    }
}
