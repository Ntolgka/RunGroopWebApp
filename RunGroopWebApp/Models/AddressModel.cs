using System.ComponentModel.DataAnnotations;

namespace RunGroopWebApp.Models
{
    public class AddressModel
    {
        [Key]
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
