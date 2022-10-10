using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.ViewModels
{
    public class NewTeamDropdownsVM
    {
        public NewTeamDropdownsVM()
        {
            Trophies = new List<Trophy>();
            Players = new List<Player>();
        }

      
        public List<Trophy> Trophies { get; set; }
        public List<Player> Players { get; set; }
    }
}
