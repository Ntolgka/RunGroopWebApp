namespace RunGroopWebApp.Models
{
    public class AppUserModel
    {
        public int? Pace { get; set; }
        public int? Mileage { get; set; }
        public AddressModel? Address { get; set; }
        public ICollection<ClubModel>? Clubs { get; set; }
        public ICollection<RacesModel> Races { get; set; }

    }
}
