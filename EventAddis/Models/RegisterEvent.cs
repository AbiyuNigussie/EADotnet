using System.ComponentModel.DataAnnotations.Schema;

namespace EventAddis.Models
{
    public class RegisterEvent
    {
        public string Title { get; set; }
        public DateTime Schedule { get; set; }
        public string Description { get; set; }
        public string Organizer { get; set; }
        public string? Image { get; set; }
        public string Contact { get; set; }
        public string CategoryName {get; set;}
        public string Region { get; set; }
        public string City { get; set; }
        public string? StreetName { get; set; }
        public string? PostalCode { get; set; }
        public Guid UserId { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }



    }
}
