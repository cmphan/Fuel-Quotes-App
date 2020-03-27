using System;
using System.Collections.Generic;
using System.Linq;
using Fuel.API.Models;
using Newtonsoft.Json;

namespace Fuel.API.Data
{
    public class Seed
    {
        public static void SeedUsers(DataContext context)
        {
            // Check if database is empty. Only run seeding users when database is empty
            if(!context.Users.Any())
            {
                // Read from the Seed Json file
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                // Get a list of users 
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    byte [] passwordhash, passwordSalt;
                    EncryptPassword("password", out passwordhash, out passwordSalt);
                    user.PasswordHash = passwordhash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();
                    Console.WriteLine(user);
                    context.Users.Add(user);
                }
                context.SaveChanges();
            }
        }
        private static void EncryptPassword(string password, out byte[] hashedPassword, out byte[] saltedPassword)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                saltedPassword = hmac.Key;
                hashedPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}