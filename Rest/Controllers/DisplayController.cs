using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Rest.Controllers
{
    public class DisplayController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}