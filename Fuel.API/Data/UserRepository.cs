using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fuel.API.Dtos;
using Fuel.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Fuel.API.Data
{
    public class UserRepository : IUserRepository
    {
        private DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        /* Suggested Price = Current Price + Margin
Where,

Current price per gallon = $1.50 (this is the price what distributor gets from refinery and it varies based upon crude price. But we are keeping it constant for simplicity)
Margin =  Current Price * (Location Factor - Rate History Factor + Gallons Requested Factor + Company Profit Factor + Rate Fluctuation)

Consider these factors:

Location Factor = 2% for Texas, 4% for out of state.
Rate History Factor = 1% if client requested fuel before, 0% if no history (you can query fuel quote table to check if there are any rows for the client)
Gallons Requested Factor = 2% if more than 1000 Gallons, 3% if less
Company Profit Factor = 10% always
Rate Fluctuation = 4% for summer, 3% otherwise 
*/
        public double CalculatePrice(User userFromRepo, QuoteForDetailedDto quoteForGenerationDto)
        {
            const double currentPricePerGallon = 1.5;
            double locationFactor = 0.04;
            double rateHistory = 0;
            double gallonsRequestedFactor = 0.03;
            double fluctuationRate = 0.03;
            double companyProfit = 0.1;
            // Check Location Factor. If Texas = 0.02, out of state = 0.04 
            if (userFromRepo.ClientProfile.State == "TX" || userFromRepo.ClientProfile.State == "Texas")
            {
                locationFactor = 0.02; 
            }
            // Check if client request quotes before 
            if (userFromRepo.Quote.Any(q => q.User.UserId == userFromRepo.UserId))
            {
                rateHistory = 0.01;
            }
            // Check gallons requested factor 
            if (quoteForGenerationDto.GallonsRequested > 1000)
            {
                gallonsRequestedFactor = 0.02;
            }
            // Check if summer time 
            quoteForGenerationDto.DeliveryDate = Convert.ToDateTime(quoteForGenerationDto.DeliveryDate);
            if(IsSummer(quoteForGenerationDto.DeliveryDate))
            {
                fluctuationRate = 0.04;
            }
            //Calulate based on provided formula
            var margin = currentPricePerGallon * (locationFactor - rateHistory + gallonsRequestedFactor + companyProfit + fluctuationRate);
            var suggestedPrice = currentPricePerGallon + margin;
            return suggestedPrice;
        }
        // Check if the delivery date is in summertime of 2020 (June 20 - Sep 22)
        private bool IsSummer (DateTime DeliveryDate)
        {
            DateTime start_Summer = new DateTime(2020,6, 20, 0, 0, 0);
            DateTime end_Summer = new DateTime(2020, 9, 20, 0, 0, 0);
            if (DateTime.Compare(DeliveryDate,start_Summer)>=0 && DateTime.Compare(DeliveryDate,end_Summer)<=0)
            {
                return true;
            }
            return false;
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<ClientProfile> GetProfile(int id)
        {
            var profile = await _context.ClientProfiles.FirstOrDefaultAsync(p => p.Id == id);
            return profile;
        }

        public async Task<User> GetUser(string username)
        {
            var user = await _context.Users.Include(p => p.ClientProfile).Include(q => q.Quote).FirstOrDefaultAsync(u => u.Username ==username);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Include(p => p.ClientProfile).ToListAsync();
            return users;
        }

        // Return true if more than > 0 changes else return false
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() >0;
        }
    }
}