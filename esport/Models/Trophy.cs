using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Trophy:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Trophy Name")]
        [Required(ErrorMessage = "Trophy name is required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Trophy description is required")]
        public string Description { get; set; }

        //Relationships
        public List<Team> Teams { get; set; }
    }
}
