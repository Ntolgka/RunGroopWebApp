using System.ComponentModel.DataAnnotations;

namespace RunGroopWebApp.Models
{
    public class AppUserModel
    {
        [Key]
        public string Id { get; set; }
        public int? Pace { get; set; }
        public int? Mileage { get; set; }
        public AddressModel? Address { get; set; }
        public ICollection<ClubModel>? Clubs { get; set; }
        public ICollection<RaceModel> Races { get; set; }

    }
}
