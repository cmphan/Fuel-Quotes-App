using System.ComponentModel.DataAnnotations;

namespace Fuel.API.Dtos
{
    public class ClientProfileDto
    {
        [Required]
        public string Username {get; set;}
        [Required]
        public string Fullname {get; set;}
        [Required]
        public string Address1 {get; set;}
        public string Address2 {get; set;}
        [Required]
        public string City {get; set;}
        [Required]
        public string State {get; set;}
        [Required]
        public string Zipcode {get; set;}
    }
}