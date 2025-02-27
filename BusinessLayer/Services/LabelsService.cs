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
    public class LabelsService : ILabelsService
    {
        private readonly ILabelsRepository repository;

        public LabelsService(ILabelsRepository repository)
        {
            this.repository = repository;
        }

        public Labels CreateLabel(int UserId, LabelsModel model)
        {
            return repository.CreateLabel(UserId, model);
        }
        public Labels GetLabelById(int LabelId, int UserId)
        {
            return repository.GetLabelById(LabelId, UserId);
        }

        public List<Labels> GetAllLabels(int UserId)
        {
            return repository.GetAllLabels(UserId);
        }
        public bool UpdateLabel(string LabelName, int LabelId, int UserId)
        {
            return repository.UpdateLabel(LabelName, LabelId, UserId);
        }

        public bool DeleteLabel(int LabelId, int UserId)
        {
            return repository.DeleteLabel(LabelId, UserId);
        }
    }
}
