using System.Diagnostics;
using localization.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace localization.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //we going to make method to change language have 2 parameeter 
        public IActionResult ChangeLanguage(string cultuer, string returnurl)
        {
            //to use cookies
            Response.Cookies.Append(
                //default language
                CookieRequestCultureProvider.DefaultCookieName,
                //declarate language now
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultuer)),
                new CookieOptions //anonemus obj
                {
                    //utcnow to access to user pc
                    Expires = DateTime.UtcNow.AddYears(1)

                }
             );


            return LocalRedirect(returnurl ?? "/");
        }
    }
}
