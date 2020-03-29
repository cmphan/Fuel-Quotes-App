using Fuel.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Fuel.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Value>Values{get;set;}
        public DbSet<User>Users{get;set;}
        public DbSet<ClientProfile>ClientProfiles{get;set;}
        public DbSet<Quote> Quote {get; set;}
    }
}