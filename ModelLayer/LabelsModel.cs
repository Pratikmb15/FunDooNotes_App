using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class LabelsModel
    {
        [Required]
        public string LabelName { get; set; }
        [Required]
        public int NotesId { get; set; }
    }
}
