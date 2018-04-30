using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HTyo.Data;
using HTyo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HTyo
{
    [Authorize]
    public class JobOrdersController : Controller
    {
        private readonly UserContext _context;
        private SignInManager<User> _signManager;
        private UserManager<User> _userManager;
        public JobOrdersController(UserManager<User> userManager, SignInManager<User> signManager, UserContext context)
        {
            _context = context;
            _userManager = userManager;
            _signManager = signManager;
        }

        public IActionResult Create()
        {
            return PartialView("Create");
        }
        public async Task<IActionResult> AcceptedOrRejected(string button, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOrder = await _context.JobOrders.SingleOrDefaultAsync(m => m.ID == id);
            if (jobOrder == null)
            {
                return NotFound();
            }
            if (button == "accept")
            {
                jobOrder.Status = Status.ACCEPTED;
                jobOrder.AcceptedOrderDate = DateTime.Now;
            }
            else if(button =="reject")
            {
                jobOrder.Status = Status.REJECTED;
                jobOrder.RejectedOrderDate = DateTime.Now;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: JobOrders
        public async Task<IActionResult> Index()
        {
            ViewBag.edit = TempData["edit"];
            return View(await _context.JobOrders.Where(s => s.Orderer == User.Identity.Name).ToListAsync());
        }
        // POST: JobOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Orderer,JobDescription")] JobOrder jobOrder)
        {
            if (ModelState.IsValid)
            {
                jobOrder.OrderDate = DateTime.Today;
                jobOrder.Orderer = User.Identity.Name;
                jobOrder.Status = Status.ORDERED;
                _context.Add(jobOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", jobOrder);
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public async Task<IActionResult> EditUser()
        {
            var userI = await GetCurrentUserAsync();
            var userId = userI?.Id;

            var user = _context.Users.First(u => u.Id == userId);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser([Bind("Id,Password,ConfirmPassword,Name,Address,BillingAddress,PhoneNumber,Email," +
            "HouseType,FloorArea,LotArea")] User model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                user.Name = model.Name;
                user.PhoneNumber = model.PhoneNumber;
                user.BillingAddress = model.BillingAddress;
                user.HouseType = model.HouseType;
                user.FloorArea = model.FloorArea;
                user.Email = model.Email;
                user.Address = model.Address;
                user.LotArea = model.LotArea;

                user.Password = model.Password;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);                    
                await _userManager.UpdateSecurityStampAsync(user);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["edit"] = "Succesfully edited user!";
                    return RedirectToAction("Index", "JobOrders");
                }
                ViewBag.success = result.Errors.FirstOrDefault().Description;

            }
            return View(model);
        }



        // GET: JobOrders/Edit/5
        public async Task<IActionResult> EditOrder(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var jobOrder = await _context.JobOrders.SingleOrDefaultAsync(m => m.ID == id);
            if (jobOrder == null)
            {
                return NotFound();
            }
            return PartialView("EditOrder",jobOrder);
        }

        // POST: JobOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOrderPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.JobOrders.SingleOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<JobOrder>(
                studentToUpdate,
                "",
                s => s.JobDescription))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }

        // GET: JobOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOrder = await _context.JobOrders
                .SingleOrDefaultAsync(m => m.ID == id);
            if (jobOrder == null || jobOrder.Status != Status.ORDERED)
            {
                return NotFound();
            }

            return View(jobOrder);
        }

        // POST: JobOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobOrder = await _context.JobOrders.SingleOrDefaultAsync(m => m.ID == id);
            _context.JobOrders.Remove(jobOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobOrderExists(int id)
        {
            return _context.JobOrders.Any(e => e.ID == id);
        }
    }
}
