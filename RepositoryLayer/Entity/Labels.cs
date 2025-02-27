using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class Labels
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId { get; set; }
        public string LabelName { get; set; }
        [ForeignKey("LabelNote")]
        public int NotesId { get; set; }
        [ForeignKey("LabelUser")]
        public int Id { get; set; }

        [JsonIgnore]
        public virtual Notes LabelNote { get; set; }
        [JsonIgnore]
        public virtual User LabelUser { get; set; }
    }
}
