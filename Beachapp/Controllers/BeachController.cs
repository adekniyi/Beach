using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Beachapp.Models;
using Beachapp.Dtos;
using Beachapp.Data;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Beachapp.Controllers
{
    public class BeachController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BeachController(ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var beaches = _context.Beaches.ToList();

            return View(beaches);
        }


         public IActionResult CreatBeach()
        {
            return View();
        }

        [HttpPost]
           public IActionResult CreateBeach(BeachDto model)
        {
               if(!ModelState.IsValid)
            {
                return View(model);
            }

                string uniqueFileName = null;
               if (model.BeachPicture != null)
            {
             string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath,"Image");
             uniqueFileName = Guid.NewGuid().ToString() + "_" + model.BeachPicture.FileName;

            string filePath = Path.Combine(uploadFolder, uniqueFileName);

             model.BeachPicture.CopyTo(new FileStream(filePath, FileMode.Create));
            }

             var beach = new Beach
              {
                 BeachName = model.BeachName,
                 BeachDetails = model.BeachDetails,
                 PhotoPath = uniqueFileName
              };

             _context.Beaches.Add(beach);
             _context.SaveChanges();

            return RedirectToAction("Index","Beach");
        }


          [Route("GetABeach/{id}")]
          public IActionResult GetABeach(int id)
        {
            var beach = _context.Beaches.SingleOrDefault(c => c.BeachId == id);

             if (beach == null)
               {
                return NotFound();
               };

            return View(beach);
        }
    }
}
