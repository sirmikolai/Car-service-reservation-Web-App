using System.ComponentModel.DataAnnotations;

namespace MechanicCompany.Models
{
    public class RepairPart
    {
        public int Id { get; set; }
        public int RepairRecordId { get; set; }
        public RepairRecord RepairRecord { get; set; }
        [Display(Name = "Part Name")]
        public string PartName { get; set; }
        [Display(Name = "Part Company")]
        public string PartCompany { get; set; }
        [Display(Name = "Part Cost")]
        public double PartCost { get; set; }
    }
}
