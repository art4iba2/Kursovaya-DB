using System.ComponentModel.DataAnnotations;

namespace skress.Models
{
    public class TrackTable
    {
        [Key] public int Id_track { get; set; }
        public string NameTrack { get; set; }
        public Int16 Difficulty { get; set; }
        public int TrackLength { get; set; }
        public int TrackDelta { get; set; }
        public int TrackWidth { get; set; }
    }
}
