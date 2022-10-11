using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface ITeamsService:IEntityBaseRepository<Team>
    {
        Task<Team> GetTeamByIdAsync(int id);
        Task<NewTeamDropdownsVM> GetNewTeamDropdownsValues();
        Task AddNewTeamAsync(NewTeamVM data);
        Task UpdateTeamAsync(NewTeamVM data);

        Task<IEnumerable<Team>> GetTeamByUserIdAsync(string id);
    }
}
