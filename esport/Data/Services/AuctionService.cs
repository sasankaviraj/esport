using esport.Data.Services;
using esport.Data.ViewModels;
using eTickets.Data;
using eTickets.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace esport.Models
{
    public class AuctionService : EntityBaseRepository<Auction>, IAuctionService
    {
        private readonly AppDbContext _context;
        public AuctionService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Auction>> GetAuctionDetailsAsync()
        {
            Task<List<Auction>> task = _context.Auctions.Include(p => p.Player).Include(t => t.Trophy).Include(t => t.Team).ToListAsync();
            return task;
        }

        public async Task<NewAuctionDropDownValues> GetNewAuctionDropdownsValues()
        {
            var response = new NewAuctionDropDownValues()
            {
                Teams = await _context.Teams.OrderBy(n => n.Name).ToListAsync(),
                Trophies = await _context.Trophies.OrderBy(n => n.Name).ToListAsync(),
            };

            return response;
        }

        public async Task<NewAuctionDropDownValues> GetNewAuctionTeamsDropdownsValuesByUserId(string userId)
        {
            var response = new NewAuctionDropDownValues()
            {
                Teams = await _context.Teams.Where(u=>u.UserId == userId).OrderBy(n => n.Name).ToListAsync(),
            };

            return response;
        }
    }
}
