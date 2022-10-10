using eTickets.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player_Team>().HasKey(am => new
            {
                am.PlayerId,
                am.TeamId
            });

            modelBuilder.Entity<Player_Team>().HasOne(m => m.Team).WithMany(am => am.Player_Teams).HasForeignKey(m => m.TeamId);
            modelBuilder.Entity<Player_Team>().HasOne(m => m.Player).WithMany(am => am.Player_Teams).HasForeignKey(m => m.PlayerId);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player_Team> Player_Teams { get; set; }
        public DbSet<Trophy> Trophies { get; set; }
        //public DbSet<Producer> Producers { get; set; }


        ////Orders related tables
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderItem> OrderItems { get; set; }
        //public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
