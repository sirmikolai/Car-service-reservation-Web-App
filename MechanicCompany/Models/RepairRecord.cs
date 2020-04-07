using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MechanicCompany.Models
{
    public class RepairRecord
    {
        public int Id { get; set; }
        [Display(Name = "Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }
        [Display(Name = "Mechanic")]
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
        [Display(Name = "Labor Cost")]
        public double LaborCost { get; set; }
        public IList<RepairPart> RepairPart { get; set; }
    }

}
