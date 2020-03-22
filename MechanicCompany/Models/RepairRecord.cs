using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MechanicCompany.Models
{
    public class RepairRecord
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }
        public string Description { get; set; }
        [Display(Name = "Status Repair")]
        public string StatusRepair { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Expected Visit Date")]
        public DateTime VisitDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date of Repair")]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date of Repair")]
        public DateTime? EndDate { get; set; }
        public IEnumerable<RepairPart> RepairPart { get; set; }
        [Display(Name = "Repair Cost")]
        public double? RepairCost { get; set; }
    }
}
