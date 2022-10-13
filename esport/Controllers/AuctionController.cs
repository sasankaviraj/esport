using esport.Data.Services;
using esport.Models;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Threading.Tasks;

namespace esport.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IAuctionService _service;
        private readonly IPlayersService _playerService;
        private readonly ITeamsService _teamService;

        public AuctionController(IAuctionService service, IPlayersService playerService, ITeamsService teamService)
        {
            _service = service;
            _playerService = playerService;
            _teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            var auctions = await _service.GetAuctionDetailsAsync();
            return View(auctions);
        }

        //GET: Teams/Create
        public async Task<IActionResult> Create()
        {
            var auctionDropdownsData = await _service.GetNewAuctionDropdownsValues();

            ViewBag.Trophies = new SelectList(auctionDropdownsData.Trophies, "Id", "Name");
            ViewBag.Teams = new SelectList(auctionDropdownsData.Teams, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Auction auction)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            eTickets.Models.Player player = await _playerService.GetPlayerByUserIdAsync(userId);
            auction.PlayerId = player.Id;
            auction.UserId = userId;
            var auctionDropdownsData = await _service.GetNewAuctionDropdownsValues();
            if (!ModelState.IsValid)
            {

                ViewBag.Trophies = new SelectList(auctionDropdownsData.Trophies, "Id", "Name");

                return View();
            }
            await _service.AddAsync(auction);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Bid(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var auctionDetails = await _service.GetByIdAsync(id);
            var auctionDropdownsData = await _service.GetNewAuctionTeamsDropdownsValuesByUserId(userId);
            ViewBag.Teams = new SelectList(auctionDropdownsData.Teams, "Id", "Name");
            if (auctionDetails == null) return View("NotFound");
            return View(auctionDetails);
        }


        [HttpPost]
        public async Task<IActionResult> Bid(int id, [Bind("Price,TeamId")] Auction auction)
        {
            var auctionDetails = await _service.GetByIdAsync(id);
            auctionDetails.Price = auction.Price;
            auctionDetails.TeamId = auction.TeamId;
            auctionDetails.IsSold = true;
            
            if (!ModelState.IsValid)
            {
                return View(auction);
            }
            await _service.UpdateAsync(id, auctionDetails);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SetBasePrice(int id)
        {
            var auctionDetails = await _service.GetByIdAsync(id);
            if (auctionDetails == null) return View("NotFound");
            return View(auctionDetails);
        }

        [HttpPost]
        public async Task<IActionResult> SetBasePrice(int id, [Bind("Price")] Auction auction)
        {
            var auctionDetails = await _service.GetByIdAsync(id);
            auctionDetails.Price = auction.Price;

            if (!ModelState.IsValid)
            {
                return View(auction);
            }
            await _service.UpdateAsync(id, auctionDetails);
            return RedirectToAction(nameof(Index));
        }

    }
}