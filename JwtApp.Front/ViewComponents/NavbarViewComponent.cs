using Microsoft.AspNetCore.Mvc;

namespace JwtApp.Front.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
