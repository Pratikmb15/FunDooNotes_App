using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class Notes
    {
        [Key]
        public int Notes_id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public bool IsDeleted { get; set; }

        public bool isArchive { get; set; }

        [ForeignKey("User")]
        public int Id { get; set; }
    }
}
