using eTickets.Data.Base;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class TrophyService : EntityBaseRepository<Trophy>, ITrophyService
    {
        public TrophyService(AppDbContext context) : base(context)
        {
        }
    }
}
