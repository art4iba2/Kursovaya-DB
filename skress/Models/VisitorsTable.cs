using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace skress.Models
{
    public class VisitorsTable
    {
        [Key] public int Id_visitor { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthdayDate { get; set; }
        public bool Sex { get; set; }

        public string FullName
        {
            get { return $"{Name} {Surname}"; }
        }
    }
}
