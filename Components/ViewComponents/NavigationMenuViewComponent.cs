using Microsoft.AspNetCore.Mvc;
using assignments.Models;

namespace assignments.Components.ViewComponents
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var consoleList = new List<ConsoleList>
        {
            new ConsoleList { Controller = "Home", Action = "Index", Label = "Home" },
            new ConsoleList { Controller = "Consoles", Action = "Index", Label = "Consoles" },
            new ConsoleList { Controller = "Products", Action = "Index", Label = "Products" },
            new ConsoleList { Controller = "Carts", Action = "Index", Label = "View My Cart" },
            new ConsoleList { Controller = "Home", Action = "Privacy", Label = "Privacy" },
            new ConsoleList { Controller = "Home", Action = "Brief", Label = "Brief" },
        };

            return View(consoleList);
        }
    }
}