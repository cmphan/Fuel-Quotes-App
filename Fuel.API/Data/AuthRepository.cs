using System;
using System.Threading.Tasks;
using Fuel.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Fuel.API.Data
{
    // Implementation of IAuthRepository Interface.
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        // Asyn login function return a user object from database 
        public async Task<User> Login(string username, string password)
        {
            //Fetch user object out of database based on username; 
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            //If not user exists return null 
            if (user == null) return null;
            //Verify password by comparing the input password with the hashed + salted parts of password from db
            if(!HashedPasswordValidation(password,user.PasswordHash,user.PasswordSalt))
            //If password validation fails => return null
            {
                return null;
            }
            //If queries executed successfully => return user object from database. 
            return user;
        }

        //Function to validate password for log-in operation 
        private bool HashedPasswordValidation(string password, byte[] passwordHash, byte[] passwordSalt)
        {
             /* Hash + Salt the input password from user and compare character by character with the stored password
             inside database */
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i=0; i<computedPasswordHash.Length; i++)
                {
                    if(computedPasswordHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }
        //Function allows users to register with username & password 
        // return newly created user object from db. 
        public async Task<User> Register(User user, string password)
        {
            byte[] hashedPassword, saltedPassword;
            EncryptPassword(password, out hashedPassword, out saltedPassword);
            user.PasswordHash = hashedPassword;
            user.PasswordSalt = saltedPassword;
            //Update user object in database 
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Password Encryption using built-in Hashing function 
        private void EncryptPassword(string password, out byte[] hashedPassword, out byte[] saltedPassword)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                saltedPassword = hmac.Key;
                hashedPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
            //Function to check if user is already existed inside the database by username => keep each username unique
        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;
            return false;
        }
        // Profile method 
        public async Task<ClientProfile>Profile(ClientProfile profile, string fullName, string address1, string address2, string city, string state, string zipcode)
        {
            profile.Fullname = fullName;
            profile.PhotoURL = "123.com/j.jpg";
            profile.Address1 = address1;
            profile.Address2 = address2;
            profile.City = city;
            profile.State = state;
            profile.Zipcode = zipcode;
            await _context.ClientProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

    }
}