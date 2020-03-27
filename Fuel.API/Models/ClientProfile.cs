namespace Fuel.API.Models
{
    public class ClientProfile
    {
        public int Id {get;set;}
        public string Fullname {get; set;}
        public string PhotoURL {get; set;}
        public string Address1 {get; set;}
        public string Address2 {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public string Zipcode {get; set;}
        public int UserId {get;set;}
        public User user {get;set;}
    }
}