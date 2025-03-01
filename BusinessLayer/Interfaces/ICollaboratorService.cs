using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
     public interface ICollaboratorService
    {
        public Collaborator CreateCollaborator(int UserId, CollaboratorModel model);
        public List<Collaborator> GetCollaborators(int UserId);
        public bool DeleteCollaborator(int UserId, int CollaboratorId);
    }
}
