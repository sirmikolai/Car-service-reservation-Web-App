﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MechanicCompany.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
        [PersonalData]
        public string Address { get; set; }
        [PersonalData]
        public string ZipCode { get; set; }
        [PersonalData]
        public string City { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
