using RunGroopWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroopWebApp.Models
{
    public class RacesModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public AddressModel Address { get; set; }
        public RaceCategory RaceCategory { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUserModel? AppUser { get; set; }
    }
}
