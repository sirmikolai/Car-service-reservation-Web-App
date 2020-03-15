using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechanicCompany.ListsHelper
{
    public class RepairRecordHelper
    {
        public static List<SelectListItem> getRepairStatus()
        {
            List<SelectListItem> types = new List<SelectListItem>
            {
                new SelectListItem() { Text="Pending"},
                new SelectListItem() { Text="Approved"},
                new SelectListItem() { Text="Started"},
                new SelectListItem() { Text="Fixed"},
                new SelectListItem() { Text="Closed"},
            };
            return types;
        }
    }
}
