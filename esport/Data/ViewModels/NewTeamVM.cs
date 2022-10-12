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
    public class NewTeamVM
    {
        public int Id { get; set; }

        [Display(Name = "Team name")]
        [Required(ErrorMessage = "Team is required")]
        public string Name { get; set; }

        [Display(Name = "Team description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Country name")]
        [Required(ErrorMessage = "Country name is required")]
        public string Country { get; set; }


        [Display(Name = "Owner name")]
        [Required(ErrorMessage = "Owner name is required")]
        public string Owner { get; set; }

        [Display(Name = "Maximum Price")]
        [Required(ErrorMessage = "Maximum Price is required")]
        public string MaxPrice { get; set; }

        [Display(Name = "Logo Url")]
        [Required(ErrorMessage = "Logo Url is required")]
        public string LogoUrl { get; set; }

        //Relationships
        //[Display(Name = "Select player(s)")]
        //[Required(ErrorMessage = "Player(s) is required")]
        //public List<int> PlayerIds { get; set; }

        [Display(Name = "Select a trophy")]
        [Required(ErrorMessage = "Movie trophy is required")]
        public int TrophyId { get; set; }

        public string UserId { get; set; }
    }
}
