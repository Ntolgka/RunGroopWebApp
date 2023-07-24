using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroopWebApp.Models
{
    public class AppUserModel : IdentityUser
    {
        public int? Pace { get; set; }
        public int? Mileage { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public AddressModel? Address { get; set; }
        public ICollection<ClubModel>? Clubs { get; set; }
        public ICollection<RaceModel> Races { get; set; }

    }
}
