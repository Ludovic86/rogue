using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace rogue.Controllers
{
    public class JoueurController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}