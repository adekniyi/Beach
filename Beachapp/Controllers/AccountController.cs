using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Beachapp.Data;
using Beachapp.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Owin.Security;
using System.Security.Claims;




// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Beachapp.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }



          // GET: /Account/Register
        //[AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

          // POST: /Account/Register
        [HttpPost]
        //[Route("Account/Register")]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { 
                    UserName = model.Email, 
                    Email = model.Email,
                    };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                //AddErrors(result);

            }
            return View(model);
        }


         //GET: /Account/Login
        //[AllowAnonymous]
        //[Route("Account/Login")]
        public async Task<ActionResult> Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var logInViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(logInViewModel);
        }



         // POST: /Account/Login
        [HttpPost]
        //[Route("Account/Login")]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
              if (result.IsLockedOut)
            {
                return View("Lockout");
            }
            if (result.IsNotAllowed)
            {
                return View("Not Allowed");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }


          // POST: /Account/Logout
        [HttpPost]
        //[Route("Account/Logout")]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



        //[AllowAnonymous]
        //[Route("Account/ExternalLogin")]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl =  Url.Action("ExternalLoginCallback", "Account", 
                                            new { ReturnUrl = returnUrl });
            var properties = _signInManager
                .ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }


          // GET: /Account/ExternalLoginCallback
        [HttpGet]
        //[AllowAnonymous]
        //[Route("Account/ExternalLoginCallback")]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");


            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager
                .GetExternalAuthenticationSchemesAsync()).
                ToList()
            };

            if(remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider:{remoteError}");
                return View("Login", loginViewModel);
            }

            var loginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                ModelState.AddModelError(string.Empty, $"Error loading external information");
                return RedirectToAction("Login", loginViewModel);
            }

            var result = await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider,loginInfo.ProviderKey ,isPersistent: false, bypassTwoFactor:true);
            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);     
            }
            else
            {
                var email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email);

                if(email != null)
                {
                    var user = await _userManager.FindByEmailAsync(email);

                    if(user == null)
                    {
                         user = new ApplicationUser
                        {
                            UserName = loginInfo.Principal.FindFirstValue(ClaimTypes.Name),
                            Email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email),
                        };
                       
                        await _userManager.CreateAsync(user);
                    }

                    await _userManager.AddLoginAsync(user, loginInfo);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                ViewBag.ErrorTitle = $"Email claim not recieved from {loginInfo.LoginProvider}";
                ViewBag.ErrorMessage = "Please confirm support on adekniyi@gmail.com";

                return View("Login", loginViewModel);
            }

           // return View("Login", loginViewModel);
        }

    }
}
