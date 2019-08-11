using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SurveyDemoApp.Controllers
{
    public class LoginController : Controller
    {
        // 
        // GET: /login/
        public IActionResult Index()
        {
            return View();
        }

        // 
        // GET: /login/welcome/
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}