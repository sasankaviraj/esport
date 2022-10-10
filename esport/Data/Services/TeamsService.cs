﻿using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class TeamsService : EntityBaseRepository<Team>, ITeamsService
    {
        private readonly AppDbContext _context;
        public TeamsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewTeamAsync(NewTeamVM data)
        {
            var newTeam = new Team()
            {
                Name = data.Name,
                Description = data.Description,
                Owner = data.Owner,
                Country = data.Country,
                TrophyId = data.TrophyId,
            };
            await _context.Teams.AddAsync(newTeam);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var playerId in data.PlayerIds)
            {
                var newPlayerTeam = new Player_Team()
                {
                    TeamId = newTeam.Id,
                    PlayerId = playerId
                };
                await _context.Player_Teams.AddAsync(newPlayerTeam);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            var teamDetails = await _context.Teams
                .Include(c => c.Trophy)
                .Include(am => am.Player_Teams).ThenInclude(a => a.Player)
                .FirstOrDefaultAsync(n => n.Id == id);

            return teamDetails;
        }

        public async Task<NewTeamDropdownsVM> GetNewTeamDropdownsValues()
        {
            var response = new NewTeamDropdownsVM()
            {
                Players = await _context.Players.OrderBy(n => n.FullName).ToListAsync(),
                Trophies = await _context.Trophies.OrderBy(n => n.Name).ToListAsync(),
            };

            return response;
        }

        public async Task UpdateTeamAsync(NewTeamVM data)
        {
            var dbTeam = await _context.Teams.FirstOrDefaultAsync(n => n.Id == data.Id);

            if(dbTeam != null)
            {
                dbTeam.Name = data.Name;
                dbTeam.Description = data.Description;
                dbTeam.Owner = data.Owner;
                dbTeam.Country = data.Country;
                dbTeam.TrophyId = data.TrophyId;
                await _context.SaveChangesAsync();
            }

            //Remove existing players
            var existingPlayersDb = _context.Player_Teams.Where(n => n.TeamId == data.Id).ToList();
            _context.Player_Teams.RemoveRange(existingPlayersDb);
            await _context.SaveChangesAsync();

            //Add Team Players
            foreach (var playerId in data.PlayerIds)
            {
                var newPlayerTeam = new Player_Team()
                {
                    TeamId = data.Id,
                    PlayerId = playerId
                };
                await _context.Player_Teams.AddAsync(newPlayerTeam);
            }
            await _context.SaveChangesAsync();
        }
    }
}
