using System;

namespace Fuel.API.Models
{
    public class User
    {
        public int UserId {get;set;}
        public string Username{get;set;}
        public byte[] PasswordHash {get;set;}
        public byte[] PasswordSalt {get;set;}
        public DateTime LastLogin { get; set; }
        public DateTime LastActive {get;set;}
        public ClientProfile ClientProfile{get;set;}
    }
}