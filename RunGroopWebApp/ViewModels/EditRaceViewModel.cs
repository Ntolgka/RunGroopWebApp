using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.ViewModels
{
    public class EditRaceViewModel
    {

        public int Id { get; set; }
        public string AppUserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public int AdressId { get; set; }
        public AddressModel Address { get; set; }
        public string? URL { get; set; }
        public RaceCategory RaceCategory { get; set; }
    }
}
