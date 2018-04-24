using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HTyo.Models;
using Microsoft.AspNetCore.Identity;
using HTyo.Data;

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
        public async Task<IActionResult> Register([Bind("ID,UserName,Password,Name,Address,BillingAddress,PhoneNumber,Email,HouseType,FloorArea,LotArea")] User model)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(user);
                
                await _userManager.CreateAsync(model, model.Password);//_context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }
    }
}
