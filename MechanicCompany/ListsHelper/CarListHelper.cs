using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechanicCompany.ListsHelper
{
    public class CarListHelper
    {
        public static List<SelectListItem> getCarTypeOfBody()
        {
            List<SelectListItem> types = new List<SelectListItem>
            {
                new SelectListItem() { Text="Hatchback"},
                new SelectListItem() { Text="Sedan"},
                new SelectListItem() { Text="Kombi"},
                new SelectListItem() { Text="SUV"},
                new SelectListItem() { Text="Cabriolet"},
                new SelectListItem() { Text="Off-Road"},
                new SelectListItem() { Text="Pickup"}
            };
            return types;
        }

        public static List<SelectListItem> getEngineTypeOfFuel()
        {
            List<SelectListItem> types = new List<SelectListItem>
            {
                new SelectListItem() { Text="Gasoline"},
                new SelectListItem() { Text="Diesel"},
                new SelectListItem() { Text="LPG"}
            };
            return types;
        }
    }
}
