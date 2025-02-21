using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface INotesRepository
    {
        Notes CreateNote(Notes note);
        List<Notes> GetUserNotes(int userId);
        Notes UpdateNote(Notes note);
        bool DeleteNote(int noteId, int userId);
    }
}
