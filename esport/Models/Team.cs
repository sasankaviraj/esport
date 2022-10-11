using eTickets.Data;
using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Team:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public string Country { get; set; }

        //Relationships
        public List<Player_Team> Player_Teams { get; set; }

        //Trophy
        public int TrophyId { get; set; }
        [ForeignKey("TrophyId")]
        public Trophy Trophy { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

    }
}
