using System;
using Fuel.API.Data;
using Fuel.API.Dtos;
using Fuel.API.Models;
using NUnit.Framework;
namespace Fuel.API.UnitTest
{
    public class MethodUnitTesting
    {
        [TestCase(1.785, 10,"2020-02-02", true, "NY")]
        [TestCase(1.77,150,"2020-07-20", true, "TX")]
        [TestCase(1.8,20000,"2020-09-12", false, "CA")]
        public void CalculatePrice_Test(double actual, double gallonsRequested, string deliveryDate, bool hasQuoteBefore, string state) {
            var clientProfileTest = new ClientProfile() {
                State = state
            };
            IUserRepository _repo = new UserRepository();
            var quoteForGenerationDto = new QuoteForDetailedDto();
            quoteForGenerationDto.GallonsRequested = gallonsRequested;
            quoteForGenerationDto.DeliveryDate = Convert.ToDateTime(deliveryDate);
            // Action 
            var expected = _repo.CalculatePrice(clientProfileTest, quoteForGenerationDto, hasQuoteBefore);
            //Actual 
            Assert.AreEqual(actual,expected);
        }

    }
}