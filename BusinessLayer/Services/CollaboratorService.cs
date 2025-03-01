using BusinessLayer.Interfaces;
using ModelLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollaboratorService: ICollaboratorService
    {
        private readonly ICollaboratorRepository _collaboratorRepository;
        public CollaboratorService(ICollaboratorRepository collaboratorRepository)
        {
            _collaboratorRepository=collaboratorRepository;
        }

        public Collaborator CreateCollaborator(int UserId, CollaboratorModel model)
        {
            return _collaboratorRepository.CreateCollaborator(UserId, model);
        }

        public bool DeleteCollaborator(int UserId, int CollaboratorId)
        {
            return _collaboratorRepository.DeleteCollaborator(UserId, CollaboratorId);
        }

        public List<Collaborator> GetCollaborators(int UserId)
        {
           return _collaboratorRepository.GetCollaborators(UserId);
        }
    }
}
