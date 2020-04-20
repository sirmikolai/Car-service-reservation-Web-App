using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string CarBrand { get; set; }
        [Display(Name = "Car Model")]
        [Required]
        public string CarModel { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Production Date")]
        public DateTime ProductionDate { get; set; }
        [Display(Name = "Engine Volume")]
        [Required]
        public int EngineVolume { get; set; }
        [Display(Name = "Engine Name")]
        [Required]
        public string EngineName { get; set; }
        [Display(Name = "Engine Power")]
        [Required]
        public string EnginePower { get; set; }
        [Display(Name = "Engine Type of Fuel")]
        [Required]
        public string EngineTypeOfFuel { get; set; }
        [Display(Name = "Car Type of Body")]
        [Required]
        public string CarTypeOfBody { get; set; }
        [Display(Name = "Registration Number")]
        [Required]
        public string RegistrationNumber { get; set; }
        public string FullNameOfCar { get { return string.Format("{0} {1} {2} {3}", CarBrand, CarModel, " - ", RegistrationNumber); } }
        public ICollection<RepairRecord> RepairRecords { get; set; }
    }
}
