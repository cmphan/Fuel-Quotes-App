using System;

namespace Fuel.API.Models
{
    public class Quote
    {
        public int Id {get;set;}
        public double GallonsRequested {get;set;}
        public string DeliveryAddress {get; set;}
        public DateTime DeliveryDate  { get; set; }
        public double SuggestedPrice  { get; set; }
        public double AmountDue {get; set;}
        public User User {get;set;}
    }
}