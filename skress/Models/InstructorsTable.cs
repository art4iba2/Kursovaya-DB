using System.ComponentModel.DataAnnotations;

namespace skress.Models
{
    public class InstructorsTable
    {
        [Key] public int Id_instructors {  get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Specialization { get; set; }
        public int Experience { get; set; }
        public double Rating { get; set; }
        public DateTime BirthdayDate { get; set; }

        public string FullName
        {
            get { return $"{Name} {Surname}"; }
        }
    }
}
