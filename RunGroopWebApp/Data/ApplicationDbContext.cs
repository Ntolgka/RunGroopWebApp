﻿using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<AppUserModel> AppUsers { get; set; }
        public DbSet<ClubModel> Clubs { get; set; }
        public DbSet<RaceModel> Races { get; set; }



    }
}
