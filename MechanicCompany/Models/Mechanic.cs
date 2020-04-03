using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MechanicCompany.Models
{
    public class Mechanic
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Role in Company")]
        public string RoleInCompany { get; set; }
        public string FullName { get { return string.Format("{0} {1} {2} {3}", FirstName, LastName, " - ", RoleInCompany); } }
        public ICollection<RepairRecord> RepairRecords { get; set; }
    }
}
