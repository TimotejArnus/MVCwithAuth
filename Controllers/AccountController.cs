using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NapredniObrazec.Models;
using Newtonsoft.Json;

namespace NapredniObrazec.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public AccountController(UserManager<User> userMng, SignInManager<User> signInmng)
        {
            userManager = userMng;
            signInManager = signInmng;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {

            return View("~/Views/LogIn/Index.cshtml");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogIn()
        {

            return View("~/Views/LogIn/Index.cshtml");
        }

       

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogIn logIn)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(logIn.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, logIn.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index","Home");
                    }
                }
                ModelState.AddModelError(nameof(logIn.Email), "Login Failed");
            }
            return View("~/Views/LogIn/Index.cshtml");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View("~/Views/User/AddUser.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserWithPassword user)
        {
            if (ModelState.IsValid)
            {
                user.UserName = user.Email;

                var userString = JsonConvert.SerializeObject(user);

                IdentityResult result;

                try
                {
                    result = await userManager.CreateAsync(user, user.Password1);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("",error.Description);   
                    }

                    
                }

            }

            return View("~/Views/User/AddUser.cshtml");

        }

        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("GoogleResponse", "Account");
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);   // Naredi okno za vpis
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            string[] userinfo =
                {info.Principal.FindFirst(ClaimTypes.Name).Value, info.Principal.FindFirst(ClaimTypes.Email).Value};

            User u = await userManager.FindByNameAsync(userinfo[1]);

            if (result.Succeeded && u != null)
            {
                return RedirectToAction("Index", "Home");   // lahko dolocimo se ostale podatke ki jih zelimo pridobiti iz google
            }
            else
            {
                User user = new User
                {
                    Name = info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                    LastName = info.Principal.FindFirst(ClaimTypes.Surname).Value,
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
                };

                try
                {
                    IdentityResult identityResult = await userManager.CreateAsync(user);
                    if (identityResult.Succeeded)
                    {
                        identityResult = await userManager.AddLoginAsync(user, info);
                        if (identityResult.Succeeded)
                        {
                            identityResult = await userManager.AddToRoleAsync(user, "User");

                            if (identityResult.Succeeded)
                            {
                                await signInManager.SignInAsync(user, false);
                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }

                    
                }
                catch (Exception e)
                {
                    return AccessDenied();
                    Console.WriteLine(e);
                    throw;
                }

                return RedirectToAction("Index", "Home");


            }

        }

        public IActionResult AccessDenied()
        {
            return View("~/Views/LogIn/AccessDenied.cshtml");
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}