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

        public IActionResult CreateView()
        {

            //ViewBag.AsiakastyyppiID = new SelectList(_context.AsiakasTyypit, "AsiakastyyppiID", "Selite");
            return PartialView("Create");
        }

        // GET: JobOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobOrders.ToListAsync());
        }
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public async Task<IActionResult> EditUser()
        {


            var userI = await GetCurrentUserAsync();
            var userId = userI?.Id;

            var user = _context.Users.First(u => u.Id == userId);
            return View(user);
        }

        // GET: JobOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOrder = await _context.JobOrders
                .SingleOrDefaultAsync(m => m.ID == id);
            if (jobOrder == null)
            {
                return NotFound();
            }

            return View(jobOrder);
        }
     
        public async Task<IActionResult> GetOrders(string name, string address)
        {
            var userI = await GetCurrentUserAsync();
            var userId = userI?.Id;

            var user = await _context.Users.Include(s => s.JobOrders).AsNoTracking().SingleOrDefaultAsync(m => m.Id == userId);

            return View("Index", user);
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
                jobOrder.OrderDate = DateTime.Now;
                jobOrder.Orderer = User.Identity.Name;
                _context.Add(jobOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index",jobOrder);
        }

        // GET: JobOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            return View(jobOrder);
        }

        // POST: JobOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Orderer,JobDescription,OrderDate,StartDate,ReadyDate,AcceptedOrderDate,RejectedOrderDate,JobComment,ToolsComment,HoursOnJob,PriceEstimate")] JobOrder jobOrder)
        {
            if (id != jobOrder.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobOrderExists(jobOrder.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(jobOrder);
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
            if (jobOrder == null)
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
