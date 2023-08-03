using Microsoft.AspNetCore.Identity;
using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Models;
using System.Diagnostics;
using System.Net;

namespace RunGroopWebApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Clubs.Any())
                {
                    context.Clubs.AddRange(new List<ClubModel>()
                    {
                        new ClubModel()
                        {
                            Title = "Bursa Running Club 1",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first cinema",
                            ClubCategory = ClubCategory.City,
                            CreatedAt = DateTime.UtcNow,
                            Address = new AddressModel()
                            {
                                Street = "Başaran",
                                City = "Bursa",
                                State = "Osmangazi"
                            }
                         },
                        new ClubModel()
                        {
                            Title = "Bursa Running Club 2",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first cinema",
                            ClubCategory = ClubCategory.Endurance,
                            CreatedAt = DateTime.UtcNow,
                            Address = new AddressModel()
                            {
                                Street = "Beşevler",
                                City = "Bursa",
                                State = "Nilüfer"
                            }
                        },
                        new ClubModel()
                        {
                            Title = "Michigan Running Club 1",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first club",
                            ClubCategory = ClubCategory.Trail,
                            CreatedAt = DateTime.UtcNow,
                            Address = new AddressModel()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new ClubModel()
                        {
                            Title = "Michigan Running Club 2",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first club",
                            ClubCategory = ClubCategory.City,
                            CreatedAt = DateTime.UtcNow,
                            Address = new AddressModel()
                            {
                                Street = "123 Main St",
                                City = "Michigan",
                                State = "NC"
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //Races
                if (!context.Races.Any())
                {
                    context.Races.AddRange(new List<RaceModel>()
                    {
                        new RaceModel()
                        {
                            Title = "Bursa Running Race 1",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first race",
                            RaceCategory = RaceCategory.Marathon,
                            CreatedAt = DateTime.UtcNow,
                            Address = new AddressModel()
                            {
                                Street = "Beşevler",
                                City = "Bursa",
                                State = "Nilüfer"
                            }
                        },
                        new RaceModel()
                        {
                            Title = "Charlotte Running Race 1",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first race",
                            RaceCategory = RaceCategory.UltraMarathon,
                            CreatedAt = DateTime.UtcNow,
                            AddressId = 5,
                            Address = new AddressModel()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        }
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
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUserModel>>();
                string adminUserEmail = "tolganalbant@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUserModel()
                    {
                        UserName = "tolganalbant",
                        Email = adminUserEmail,
                        AboutMe = "This is the about me section of the admin",
                        EmailConfirmed = true,
                        Address = new AddressModel()
                        {
                            Street = "Başaran mahallesi",
                            City = "Bursa",
                            State = "Osmangazi"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "test@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUserModel()
                    {
                        UserName = "test",
                        Email = appUserEmail,
                        AboutMe = "This is the about me section of the user",
                        PhoneNumber = "+12 345 678 9012",
                        EmailConfirmed = true,
                        Address = new AddressModel()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}