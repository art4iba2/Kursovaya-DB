
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace skress.Models
{
    public class DataRentTable
    {
        [Key]public int Id_DataRent {  get; set; }
        [ForeignKey("VisitorEq")]public int ID_Visitor { get; set; }
        [ForeignKey("Equip")]public int Id_equip { get; set; }
        public int VisitorsDocument {  get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public decimal RentPrice { get; set; }
       
        public virtual VisitorsTable? VisitorEq {  get; set; }
        public virtual EquipmentTable? Equip {  get; set; }

    }
}
