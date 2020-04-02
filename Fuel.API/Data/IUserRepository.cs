using System.Threading.Tasks;
using System.Collections.Generic;
using Fuel.API.Models;
using Fuel.API.Dtos;
using System;

namespace Fuel.API.Data
{
    public interface IUserRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(string username);
         Task<ClientProfile> GetProfile(int id);
         double CalculatePrice(User userFromRepo, QuoteForDetailedDto quoteForGenerationDto);
    }
}