using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ILabelsService
    {
        public Labels CreateLabel(int UserId, LabelsModel model);
        public Labels GetLabelById(int LabelId, int UserId);
        public List<Labels> GetAllLabels(int UserId);
        public bool UpdateLabel(string LabelName, int LabelId, int UserId);
        public bool DeleteLabel(int LabelId, int UserId);
    }
}
