using eTickets.Data.Base;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class PlayerService : EntityBaseRepository<Player>, IPlayersService
    {
        private readonly AppDbContext _context;
        public PlayerService(AppDbContext context) : base(context) {
            _context = context;
        }

        public async Task<Player> GetPlayerByUserIdAsync(string id)
        {
            var playerDetails = await _context.Players
            .Include(c => c.User)
            .Include(am => am.Player_Teams).ThenInclude(a => a.Player)
            .Where(a => a.UserId == id).FirstOrDefaultAsync();

            return playerDetails;
        }
    }
}
