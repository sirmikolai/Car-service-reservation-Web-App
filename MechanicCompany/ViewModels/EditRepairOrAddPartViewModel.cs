using MechanicCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechanicCompany.ViewModels
{
    public class EditRepairOrAddPartViewModel
    {
        public RepairRecord RepairRecord { get; set; }
        public RepairPart RepairPart { get; set; }
    }
}
