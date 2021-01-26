using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Beachapp.Models;
using Beachapp.Dtos;
using Beachapp.Data;
using System.Security.Claims;




// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Beachapp.Controllers
{
    public class BeachController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private UserManager<ApplicationUser> _userManager;
        //private readonly IHttpContextAccessor _httpContextAccessor;


        public BeachController(ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            //_httpContextAccessor = httpContextAccessor;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var beaches = _context.Beaches.ToList();

            return View(beaches);
        }

          public IActionResult MyBeach()
        {
            ClaimsPrincipal currentUser = this.User;
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var beaches = _context.Beaches.Where(x=>x.PosterId == userId).ToList();

            return View(beaches);
        }


         public IActionResult CreatBeach()
        {
            return View();
        }

        [HttpPost]
           public IActionResult CreateBeach(BeachDto model)
        {
            
            ClaimsPrincipal currentUser = this.User;
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userName = currentUser.FindFirst(ClaimTypes.Name).Value;


                string uniqueFileName = null;
               if (model.BeachPicture != null)
            {
             string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath,"Image");
             uniqueFileName = Guid.NewGuid().ToString() + "_" + model.BeachPicture.FileName;

            string filePath = Path.Combine(uploadFolder, uniqueFileName);

             model.BeachPicture.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            if(model.BeachId == 0){
                 var beach = new Beach
              {
                 BeachName = model.BeachName,
                 BeachDetails = model.BeachDetails,
                 PhotoPath = uniqueFileName,
                 Location = model.Location,
                 PosterId = userId,
                 Poster = userName,
                 CreatedAt = DateTimeOffset.Now
              };
                _context.Beaches.Add(beach);
            }else{
                 var beachInDb = _context.Beaches.Single(c => c.BeachId == model.BeachId);

                beachInDb.BeachName = model.BeachName;
                beachInDb.BeachDetails = model.BeachDetails;
                beachInDb.PhotoPath = uniqueFileName;
            };

             _context.SaveChanges();

            return RedirectToAction("MyBeach","Beach");
        }


          [Route("GetABeach/{BeachId}")]
          public IActionResult GetABeach(int BeachId)
        {
             ClaimsPrincipal currentUser = this.User;
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            ViewBag.userId = userId;  
            var beach = _context.Beaches.SingleOrDefault(c => c.BeachId == BeachId);

             if (beach == null)
               {
                return NotFound();
               };

            return View(beach);
        }

         [Route("Edit/{BeachId}")]
        public ActionResult EditBeach(int BeachId)
        {
            var beach = _context.Beaches.SingleOrDefault(c => c.BeachId == BeachId);
            if (beach == null)
                return NotFound();
            var beachView = new BeachDto
            {
                BeachId = beach.BeachId,
                BeachName = beach.BeachName,
                BeachDetails = beach.BeachDetails,
            };

            return View("CreatBeach", beachView);
        }

         [HttpPost]
        [Route("Delete/{BeachId}")]
        public ActionResult DeleteBeach(int BeachId)
        {
            var beach = _context.Beaches.SingleOrDefault(c => c.BeachId == BeachId);
            if (beach == null)
                {
                    return NotFound();
                }

           _context.Beaches.Remove(beach);
            _context.SaveChanges();

             return RedirectToAction("MyBeach","Beach");
        }
        [HttpGet]
        public async Task<ActionResult> Test()
        {
            //var UserName = HttpContext.User.Identity.Name;
            //var userId = ((ClaimsIdentity)User.Identity).FindFirst("Id").Value;

ClaimsPrincipal currentUser = this.User;
var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
var user = await _userManager.FindByIdAsync(currentUserName);
            var userName = currentUser.FindFirst(ClaimTypes.Name).Value;
            return Ok(user);
        }
    }
}
