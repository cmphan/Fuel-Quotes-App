using System;

namespace Fuel.API.Dtos
{
    public class QuoteForDetailedDto
    {
        public int Id {get; set;}
        public double GallonsRequested {get;set;}
        public string DeliveryAddress {get; set;}
        public DateTime DeliveryDate  { get; set; }
        public double SuggestedPrice  { get; set; }
        public double AmountDue {get; set;} 
    }
}