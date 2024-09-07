using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace skress.Models
{
    public class EquipmentTable
    {
        [Key] public int Id_equipment {  get; set; }
        
        public string EquipmentType { get; set; }
        public double Size { get; set; }

        public string Info
        {
            get { return $"{EquipmentType} {Size}см"; }
        }
    }
}
