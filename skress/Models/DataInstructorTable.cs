using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace skress.Models
{
    public class DataInstructorTable
    {
        [Key] public int Id_Event { get; set; }

        [ForeignKey("Visitors")] public int Id_visitor { get; set; }

        [ForeignKey("Instructor")]public int Id_Instructor { get; set; }
        public decimal Price { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }

        public virtual InstructorsTable? Instructor { get; set; }
        public virtual VisitorsTable? Visitors { get; set; }
    }
}
