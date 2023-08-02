namespace RunGroopWebApp.ViewModels
{
    public class EditUserDashboardViewModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? AboutMe { get; set; }
        public string? PhoneNumber { get; set; }
        public string Id { get; set; }  
        public int? Pace { get; set; }
        public int? MileAge { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public IFormFile Image { get; set; }
    }
}
