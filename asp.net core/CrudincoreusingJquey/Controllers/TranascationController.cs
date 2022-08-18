using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudincoreusingJquey.Data;
using CrudincoreusingJquey.Models;
using CrudincoreusingJquey.Helper;

namespace CrudincoreusingJquey.Controllers
{
    public class TranascationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TranascationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tranascation
        public async Task<IActionResult> Index()
        {
            return View(await _context.tranascations.ToListAsync());
        }

        // GET: Tranascation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tranascationModel = await _context.tranascations
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (tranascationModel == null)
            {
                return NotFound();
            }

            return View(tranascationModel);
        }

        // GET: Tranascation/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Tranascation/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] TranascationModel tranascationModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(tranascationModel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(tranascationModel);
        //}

        //// GET: Tranascation/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var tranascationModel = await _context.tranascations.FindAsync(id);
        //    if (tranascationModel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(tranascationModel);
        //}

        //// POST: Tranascation/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] TranascationModel tranascationModel)
        //{
        //    if (id != tranascationModel.TransactionId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(tranascationModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TranascationModelExists(tranascationModel.TransactionId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(tranascationModel);
        //}
        [HttpGet]
        public async Task<IActionResult> AddorEdit(int? id)
        {
            if (id == 0)
            {
                return View(new TranascationModel());

            }
            else
            {
                var tranasactionmodel = await _context.tranascations.FindAsync(id);
                if (tranasactionmodel == null)
                {
                    return NotFound();
                }
                return View(tranasactionmodel);
            }
         
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddorEdit(int? id, [Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] TranascationModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                transactionModel.Date = DateTime.Now;
                _context.Add(transactionModel);
                await _context.SaveChangesAsync();
            }
            else
            {
                try
                {
                    _context.Update(transactionModel);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    if (!TranascationModelExists(transactionModel.TransactionId))
                    { return NotFound(); }
                    else
                    { throw; }

                }
                return Json(new { isValid = true, html = Helper.Helper.RenderRazorViewToString(this, "_ViewAll", _context.tranascations.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.Helper.RenderRazorViewToString(this, "AddOrEdit", transactionModel) });
        }

        // GET: Tranascation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tranascationModel = await _context.tranascations
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (tranascationModel == null)
            {
                return NotFound();
            }

            return View(tranascationModel);
        }

        // POST: Tranascation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tranascationModel = await _context.tranascations.FindAsync(id);
            _context.tranascations.Remove(tranascationModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TranascationModelExists(int id)
        {
            return _context.tranascations.Any(e => e.TransactionId == id);
        }
    }
}
