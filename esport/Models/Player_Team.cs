using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Player_Team
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
