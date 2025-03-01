using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class NotesResponseModel
    {
        public int Notes_id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public bool IsDeleted { get; set; }

        public bool isArchive { get; set; }

        public int CollaboratorId { get; set; }
        public string Email { get; set; }
    }
}
