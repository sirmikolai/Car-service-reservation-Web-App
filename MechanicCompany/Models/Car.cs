using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MechanicCompany.Models
{
    public class Car
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "User Id")]
        public string ApplicationUserId { get; set; }
        [Display(Name = "User Name")]
        public ApplicationUser ApplicationUser { get; set; }
        [Display(Name = "Car Brand")]
        public string CarBrand { get; set; }
        [Display(Name = "Car Model")]
        public string CarModel { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Production Year")]
        public DateTime ProductionYear { get; set; }
        [Display(Name = "Engine Volume")]
        public int EngineVolume { get; set; }
        [Display(Name = "Engine Name")]
        public string EngineName { get; set; }
        [Display(Name = "Engine Power")]
        public string EnginePower { get; set; }
        [Display(Name = "Engine Type of Fuel")]
        public string EngineTypeOfFuel { get; set; }
        [Display(Name = "Car Type of Body")]
        public string CarTypeOfBody { get; set; }
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }
        public string FullNameOfCar { get { return string.Format("{0} {1} {2} {3}", CarBrand, CarModel, " - ", RegistrationNumber); } }
        public ICollection<RepairRecord> RepairRecords { get; set; }
    }
}
