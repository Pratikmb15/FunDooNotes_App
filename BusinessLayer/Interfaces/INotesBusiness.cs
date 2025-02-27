using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface INotesBusiness
    {
        Notes CreateNote( Notes note);
        List<Notes> GetUserNotes(int userId);
        Notes GetNotesById(int noteId, int userId);
        Notes UpdateNote(int UserId, Notes note);
        bool DeleteNote(int noteId, int userId);
        bool ToggleNoteDeleted(int NoteId, int UserId);
        bool ToggleArchieved(int NoteId, int Userid);

    }
}
