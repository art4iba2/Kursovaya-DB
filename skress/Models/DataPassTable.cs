using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace skress.Models
{
    public class DataPassTable
    {
        [Key] public int Id_datapass { get; set; }
        [ForeignKey("Track")]public int Id_Track {  get; set; }
        [ForeignKey("Pass")]public int Id_Pass { get; set; }
        public DateTime DatePass { get; set; }

        public virtual TrackTable? Track { get; set;  }
        public virtual PassTable? Pass { get; set; }

    }
}
