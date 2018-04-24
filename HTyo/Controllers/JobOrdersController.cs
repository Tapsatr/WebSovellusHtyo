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

namespace HTyo
{
    [Authorize]
    public class JobOrdersController : Controller
    {

        public IActionResult CreateView()
        {

            //ViewBag.AsiakastyyppiID = new SelectList(_context.AsiakasTyypit, "AsiakastyyppiID", "Selite");
            return PartialView("Create");
        }
        private readonly UserContext _context;

        public JobOrdersController(UserContext context)
        {
            _context = context;
        }

        // GET: JobOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobOrders.ToListAsync());
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

        // GET: JobOrders/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: JobOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Orderer,JobDescription,OrderDate,StartDate,ReadyDate,AcceptedOrderDate,RejectedOrderDate,JobComment,ToolsComment,HoursOnJob,PriceEstimate")] JobOrder jobOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobOrder);
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
