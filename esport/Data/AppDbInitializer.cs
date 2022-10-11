using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Trophies
                if (!context.Trophies.Any())
                {
                    context.Trophies.AddRange(new List<Trophy>()
                    {
                        new Trophy()
                        {
                            Name = "Indian Premiere League",
                            Description = "This is the description of the Indian premiere League",
                        },
                       
                    });
                    context.SaveChanges();
                }
                //players
                if (!context.Players.Any())
                {
                    context.Players.AddRange(new List<Player>()
                    {
                        new Player()
                        {
                            FullName = "Glenn Maxwell",
                            Bio = "This is the Bio of the first player",
                            Country = "Australia",
                            Sport = "Cricket"
                        },
                        new Player()
                        {
                            FullName = "Andre Russell",
                            Bio = "This is the Bio of the second player",
                            Country = "West Indies",
                            Sport = "Cricket"
                        },
                        new Player()
                        {
                            FullName = "David Miller",
                            Bio = "This is the Bio of the second player",
                            Country = "South Africa",
                            Sport = "Cricket"
                        },
                        new Player()
                        {
                            FullName = "Liam Livingstone",
                            Bio = "This is the Bio of the second player",
                            Country = "England",
                            Sport = "Cricket"
                        },
                        new Player()
                        {
                            FullName = "David Warner",
                            Bio = "This is the Bio of the second player",
                            Country = "Australia",
                            Sport = "Cricket"
                        }
                    });
                    context.SaveChanges();
                }
                //Teams
                if (!context.Teams.Any())
                {
                    context.Teams.AddRange(new List<Team>()
                    {
                        new Team()
                        {
                            Name = "Punjab Kings",
                            Description = "This is the Punjab Kings description",
                            Country = "India",
                            Owner = "owner 1",
                            TrophyId = 1,
                        },
                        new Team()
                        {
                            Name = "Chennai Super Kings",
                            Description = "This is the Chennai Super Kingsn description",
                            Country = "India",
                            Owner = "owner 2",
                            TrophyId = 1,

                        },
                        new Team()
                        {
                            Name = "Mumbai Indians",
                            Description = "This is the Mumbai Indians description",
                            Country = "India",
                            Owner = "owner 3",
                            TrophyId = 1,
                        },
                        new Team()
                        {
                            Name = "Royal Challengers Bangalore",
                            Description = "This is the Royal Challengers Bangalore description",
                            Country = "India",
                            Owner = "owner 4",
                            TrophyId = 1,
                        },
                        new Team()
                        {
                            Name = "Kolkata Knight Riders",
                            Description = "This is the Kolkata Knight Riders description",
                            Country = "India",
                            Owner = "owner 5",
                            TrophyId = 1,
                        },
                        new Team()
                        {
                            Name = "Rajasthan Royals",
                            Description = "This is the Rajasthan Royals description",
                            Country = "India",
                            Owner = "owner 6",
                            TrophyId = 1,

                        }
                    });
                    context.SaveChanges();
                }
                //Players & Teams
                if (!context.Player_Teams.Any())
                {
                    context.Player_Teams.AddRange(new List<Player_Team>()
                    {
                        new Player_Team()
                        {
                            PlayerId = 1,
                            TeamId = 1
                        },
                        new Player_Team()
                        {
                            PlayerId = 3,
                            TeamId = 1
                        },

                         new Player_Team()
                        {
                            PlayerId = 1,
                            TeamId = 2
                        },
                         new Player_Team()
                        {
                            PlayerId = 4,
                            TeamId = 2
                        },
                    });
                    context.SaveChanges();
                }
            }

        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.TeamOwner))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.TeamOwner));
                if (!await roleManager.RoleExistsAsync(UserRoles.Player))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Player));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@etickets.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if(adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.TeamOwner);
                }
            }
        }
    }
}
