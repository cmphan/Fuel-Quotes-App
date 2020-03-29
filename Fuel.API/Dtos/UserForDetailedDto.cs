using System;
using System.Collections.Generic;
using Fuel.API.Models;

namespace Fuel.API.Dtos
{
    public class UserForDetailedDto
    {
        
        public int UserId {get;set;}
        public string Username{get;set;}
        public DateTime DateCreated { get; set; }
        public DateTime LastActive {get;set;}
        public string PhotoURL {get; set;}
        public ClientProfileForDetailedDto ClientProfile{get;set;}
        public IEnumerable<QuoteForDetailedDto> Quote {get;set;}
    }
}