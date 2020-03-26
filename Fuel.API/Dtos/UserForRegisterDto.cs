using System.ComponentModel.DataAnnotations;

namespace Fuel.API.Dtos
{
    public class UserForRegisterDto
    {
        //Required keyword to validate against empty string
        [Required]
        public string Username {get;set;}
        [Required]
        public string Password {get;set;}
        
    }
}