namespace skress.Models
{
    public class VisitorInfo
    {
        public int Id_visitor { get; set; }
        public string VisitorName { get; set; }
        public string VisitorSurname { get; set; }
        public DateTime VisitorBirthday { get; set; }
        public string PassType { get; set; }
        public int? NumberOfEntries { get; set; }
        public string EquipmentType { get; set; }
        public string TrackName { get; set; }
        public int? TrackDifficulty { get; set; }
        public string InstructorName { get; set; }
        public string InstructorSurname { get; set; }
        public string InstructorSpecialization { get; set; }
    }
}
