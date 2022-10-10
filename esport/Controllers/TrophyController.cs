using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class TrophyController : Controller
    {
        private readonly ITrophyService _service;

        public TrophyController(ITrophyService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allTrophies = await _service.GetAllAsync();
            return View(allTrophies);
        }


        //Get: Trophies/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Description")]Trophy trophy)
        {
            if (!ModelState.IsValid) return View(trophy);
            await _service.AddAsync(trophy);
            return RedirectToAction(nameof(Index));
        }

        //Get: Trophies/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var trophyDetails = await _service.GetByIdAsync(id);
            if (trophyDetails == null) return View("NotFound");
            return View(trophyDetails);
        }

        //Get: Trophies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var trophyDetails = await _service.GetByIdAsync(id);
            if (trophyDetails == null) return View("NotFound");
            return View(trophyDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Trophy trophy)
        {
            if (!ModelState.IsValid) return View(trophy);
            await _service.UpdateAsync(id, trophy);
            return RedirectToAction(nameof(Index));
        }

        //Get: Trophies/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var trophyDetails = await _service.GetByIdAsync(id);
            if (trophyDetails == null) return View("NotFound");
            return View(trophyDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var trophyDetails = await _service.GetByIdAsync(id);
            if (trophyDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
