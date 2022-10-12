using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin+","+UserRoles.Player)]
    public class PlayerController : Controller
    {
        private readonly IPlayersService _service;

        public PlayerController(IPlayersService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get: Players/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,Sport,Bio,Country,ImageUrl")]Player player)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            player.UserId = userId;
            if (!ModelState.IsValid)
            {
                return View(player);
            }
            await _service.AddAsync(player);
            return RedirectToAction(nameof(Index));
        }

        //Get: Players/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var playerDetails = await _service.GetByIdAsync(id);

            if (playerDetails == null) return View("NotFound");
            return View(playerDetails);
        }

        //Get: Players/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Sport,Bio,Country")] Player player)
        {
            if (!ModelState.IsValid)
            {
                return View(player);
            }
            await _service.UpdateAsync(id, player);
            return RedirectToAction(nameof(Index));
        }

        //Get: Players/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var playerDetails = await _service.GetByIdAsync(id);
            if (playerDetails == null) return View("NotFound");
            return View(playerDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playerDetails = await _service.GetByIdAsync(id);
            if (playerDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
