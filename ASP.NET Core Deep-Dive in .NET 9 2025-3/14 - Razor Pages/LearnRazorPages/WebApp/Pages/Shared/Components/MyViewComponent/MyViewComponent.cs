using Microsoft.AspNetCore.Mvc;

namespace WebApp.Pages.Shared.Components.MyViewComponent
{
    [ViewComponent( Name = "MyViewComponent")]
    public class MyViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string message)
        {
            ViewData["message"] = $"My message: {message}";
            return View();
        }
    }
}
