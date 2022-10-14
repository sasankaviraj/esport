using esport.Data.Services;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace esport.Controllers
{
    public class ReportController : Controller
    {
        private readonly IAuctionService _service;
        private readonly IPlayersService _playerService;

        public ReportController(IAuctionService service,IPlayersService playersService)
        {
            _service = service;
            _playerService = playersService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AuctionData()
        {
            var reportData = await _service.GetAuctionDetailsAsync();
            return View(reportData);
        }

        public async Task<IActionResult> PlayerData()
        {
            var reportData = await _playerService.GetAllAsync();
            return View(reportData);
        }
    }
}
