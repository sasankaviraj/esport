using esport.Data.ViewModels;
using esport.Models;
using eTickets.Data.Base;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace esport.Data.Services
{
    public interface IAuctionService : IEntityBaseRepository<Auction>
    {
        Task<NewAuctionDropDownValues> GetNewAuctionDropdownsValues();

        Task<NewAuctionDropDownValues> GetNewAuctionTeamsDropdownsValuesByUserId(string userId);

        Task<List<Auction>> GetAuctionDetailsAsync();
    }
}
