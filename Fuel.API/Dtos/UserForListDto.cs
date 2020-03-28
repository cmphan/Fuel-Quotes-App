using System;

namespace Fuel.API.Dtos
{
    public class UserForListDto
    {
        
        public int UserId {get;set;}
        public string Username{get;set;}
        public DateTime DateCreated { get; set; }
        public DateTime LastActive {get;set;}
        public string PhotoURL {get;set;}
    }
}