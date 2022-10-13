using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin+","+UserRoles.TeamOwner)]
    
    public class TeamsController : Controller
    {
        private readonly ITeamsService _service;

        public TeamsController(ITeamsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allTeams = await _service.GetTeamByUserIdAsync(userId);
            return View(allTeams);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allTeams = await _service.GetAllAsync(n => n.Trophy);

            if (!string.IsNullOrEmpty(searchString))
            {
                //var filteredResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                var filteredResultNew = allTeams.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allTeams);
        }

        //GET: Teams/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var teamDetail = await _service.GetPlayersByTeamAsync(id);
            return View(teamDetail);
        }

        //GET: Teams/Create
        public async Task<IActionResult> Create()
        {
            var teamDropdownsData = await _service.GetNewTeamDropdownsValues();

            ViewBag.Trophies = new SelectList(teamDropdownsData.Trophies, "Id", "Name");
            ViewBag.Players = new SelectList(teamDropdownsData.Players, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewTeamVM team)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            team.UserId = userId;
            if (!ModelState.IsValid)
            {
                var teamDropdownsData = await _service.GetNewTeamDropdownsValues();

                ViewBag.Trophies = new SelectList(teamDropdownsData.Trophies, "Id", "Name");
                ViewBag.Players = new SelectList(teamDropdownsData.Players, "Id", "FullName");

                return View(team);
            }

            await _service.AddNewTeamAsync(team);
            return RedirectToAction(nameof(Index));
        }


        //GET: Teams/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var teamDetail = await _service.GetTeamByIdAsync(id);
            if (teamDetail == null) return View("NotFound");

            var response = new NewTeamVM()
            {
                Id = teamDetail.Id,
                Name = teamDetail.Name,
                Description = teamDetail.Description,
                TrophyId = teamDetail.TrophyId,
                Country = teamDetail.Country,
                Owner = teamDetail.Owner,
                //PlayerIds = teamDetail.Player_Teams.Select(n => n.PlayerId).ToList(),
            };

            var teamDropdownsData = await _service.GetNewTeamDropdownsValues();

            ViewBag.Trophies = new SelectList(teamDropdownsData.Trophies, "Id", "Name");
            ViewBag.Players = new SelectList(teamDropdownsData.Players, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewTeamVM team)
        {
            if (id != team.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var teamDropdownsData = await _service.GetNewTeamDropdownsValues();

                ViewBag.Trophies = new SelectList(teamDropdownsData.Trophies, "Id", "Name");
                ViewBag.Players = new SelectList(teamDropdownsData.Players, "Id", "FullName");

                return View(team);
            }

            await _service.UpdateTeamAsync(team);
            return RedirectToAction(nameof(Index));
        }
    }
}