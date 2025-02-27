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
    public class LabelsRepository: ILabelsRepository
    {
        private readonly AppDbContext _Context;
        public LabelsRepository(AppDbContext context)
        {
            _Context = context;
        }

        public Labels CreateLabel(int UserId, LabelsModel model)
        {
            Labels label = new Labels();
            label.LabelName = model.LabelName;
            label.NotesId = model.NotesId;
            label.Id = UserId;

            _Context.Labels.Add(label);
            _Context.SaveChanges();

            return label;
        }
        public Labels GetLabelById(int LabelId, int UserId)
        {
            var getLabel = _Context.Labels.FirstOrDefault(x => x.LabelId == LabelId && x.Id == UserId);
            if (getLabel != null)
            {
                return getLabel;
            }
            return null;
        }
        public List<Labels> GetAllLabels(int UserId)
        {
            var list = _Context.Labels.Where(y => y.Id == UserId).OrderBy(y => y.LabelName).ToList();
            return list;
        }
        public bool UpdateLabel(string LabelName, int LabelId, int UserId)
        {
            var labelExist = GetLabelById(LabelId, UserId);
            if (labelExist != null)
            {
                labelExist.LabelName = LabelName;
                _Context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeleteLabel(int LabelId, int UserId)
        {
            var checkIfExist = GetLabelById(LabelId, UserId);
            if (checkIfExist != null)
            {
                _Context.Labels.Remove(checkIfExist);
                _Context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
