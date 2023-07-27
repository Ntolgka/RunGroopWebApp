using RunGroopWebApp.Models;

namespace RunGroopWebApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<ClubModel> Clubs { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
