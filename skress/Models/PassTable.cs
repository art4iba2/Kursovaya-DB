using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace skress.Models
{
    public class PassTable
    {
        [Key] public int Id_pass { get; set; }
        [ForeignKey("VisitorsPass")]public int VisitorPassId { get; set; }
        public string Pass_type { get; set; }
        public int? NumEntry { get; set; }
        public DateTime? TimeLeft { get; set; }
        public decimal Price { get; set; }

        public virtual VisitorsTable? VisitorsPass {  get; set; }
    }
}
