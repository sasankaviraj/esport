using eTickets.Models;
using System.Collections.Generic;

namespace esport.Data.ViewModels
{
    public class NewAuctionDropDownValues
    {


        public NewAuctionDropDownValues()
        {
            Trophies = new List<Trophy>();
            Teams = new List<Team>();
        }

        public List<Trophy> Trophies { get; set; }
        public List<Team> Teams { get; set; }
    }
}
