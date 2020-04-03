using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

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
