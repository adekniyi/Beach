using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Beachapp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Beachapp.Controllers
{
    public class BeachController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var beaches = new List<Beach>
            {
                new Beach{BeachName="Oniru beach",
                    BeachDetails="lorem hda dahjdeood haskjeu akjdjufr adhfncfjkad hafhemadfdfna",
                    BeachId=1,BeachPicture="url"},
                new Beach{BeachName="Elegushi beach",
                    BeachDetails="lorem hda dahjdeood haskjeu akjdjufr adhfncfjkad hafhemadfdfna",
                    BeachId=2,BeachPicture="url"},
                new Beach{BeachName="New Fresh beach",
                    BeachDetails="lorem hda dahjdeood haskjeu akjdjufr adhfncfjkad hafhemadfdfna",
                    BeachId=2,BeachPicture="url"}
            };
            return View(beaches);
        }


         public IActionResult CreatBeach()
        {
            return View();
        }
    }
}
