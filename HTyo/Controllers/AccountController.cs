using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HTyo.Models;
using Microsoft.AspNetCore.Identity;
using HTyo.Data;
using HTyo.Controllers;

namespace HTyo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserContext _context;
        private SignInManager<User> _signManager;
        private UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signManager,UserContext context)
        {
            _context = context;
            _userManager = userManager;
            _signManager = signManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.success = TempData["success"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            
            if (ModelState.IsValid)
            {
                var result = await _signManager.PasswordSignInAsync(model.UserName,
                   model.Password, false, false);

                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "JobOrders");

                }
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        // GET: Users/Create
        public IActionResult Register()
        {
            return View();
        }


        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("ID,UserName,Password,ConfirmPassword,Name,Address,BillingAddress,PhoneNumber,Email,HouseType,FloorArea,LotArea")] User model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(model, model.Password);
                if (result.Succeeded)
                {
                    TempData["success"] = "Succesfully created user!";
                    return RedirectToAction("Login", "Account");
                }
                ViewBag.success = result.Errors.FirstOrDefault().Description;
                
            }
            return View(model);
        }
    }
}
