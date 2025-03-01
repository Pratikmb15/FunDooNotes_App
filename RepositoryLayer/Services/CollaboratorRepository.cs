using ModelLayer;
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
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly AppDbContext _context;
        public CollaboratorRepository(AppDbContext context)
        {
            _context = context;
        }

        public Collaborator CreateCollaborator(int UserId, CollaboratorModel model)
        {
           var note = _context.Notes.FirstOrDefault(x => x.Id == UserId && x.Notes_id == model.NotesId);
            if (note == null)
            {
                throw new Exception("User Does'nt have access to this note !");
            }
            var collaborator = new Collaborator
            {
                Email = model.Email,
                NotesId = model.NotesId,
                Id = UserId
            };
            _context.Collaborators.Add(collaborator);
            _context.SaveChanges();
            return collaborator;
        }

        public bool DeleteCollaborator(int UserId, int CollaboratorId)
        {
            var Collaborator = _context.Collaborators.FirstOrDefault(x => x.Id == UserId && x.CollaboratorId == CollaboratorId);
            if (Collaborator == null)
            {
                throw new Exception("Collaborator not found !");
            }
            _context.Collaborators.Remove(Collaborator);
            _context.SaveChanges();
            return true;
        }

        public List<Collaborator> GetCollaborators(int UserId)
        {
           var Collaborators = _context.Collaborators.Where(x => x.Id == UserId).ToList();
            if (Collaborators == null)
            {
                throw new Exception("Collaborators not found !");
            }
            return Collaborators;
        }
    }
}
