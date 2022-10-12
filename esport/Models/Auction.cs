using eTickets.Data.Base;
using eTickets.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esport.Models
{
    public class Auction:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Trophy")]
        public int? TrophyId { get; set; }

        [ForeignKey("TrophyId")]
        public Trophy Trophy { get; set; }

        public int? PlayerId { get; set; }

        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public string Price { get; set; }

        [Display(Name = "Team")]
        public int? TeamId { get; set; }

        [ForeignKey("TeamId")]
        public Team Team { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public bool IsSold { get; set; }
    }
}
