using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroopWebApp.Models
{
    public class AppUserModel : IdentityUser
    {
        public string? Name { get; set; } 
        public string? Surname { get; set; } 
        public string? AboutMe { get; set; } 
        public int? Pace { get; set; } 
        public int? Mileage { get; set; } 
        public string? ProfileImageUrl { get; set; } 
        public string? City { get; set; } 
        public string? State { get; set; } 
        [ForeignKey("Address")] 
        public int? AddressId { get; set; } 
        public AddressModel? Address { get; set; } 
        public ICollection<ClubModel>? Clubs { get; set; } 
        public ICollection<RaceModel> Races { get; set; } 

    }
}
