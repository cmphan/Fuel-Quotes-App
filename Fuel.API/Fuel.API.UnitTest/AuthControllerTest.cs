using System;
using System.Text;
using System.Threading.Tasks;
using Fuel.API.Controllers;
using Fuel.API.Data;
using Fuel.API.Dtos;
using Fuel.API.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
namespace Fuel.API.UnitTest
{
    [TestFixture]
    public class AuthControllerTest
    {
       
        /* Function test for Register method in Auth API Controller 
        Expect to return a status code result 
        */
        [TestCase("usernameForRegister","passwordForRegister")]
        public async Task RegisterTest_ResponseStatusCode(string username, string password)
        {
            // Arrange : set up a new user with the given input username and password
            Mock<IAuthRepository> mockIAuthRepository = new Mock<IAuthRepository>();
            Mock<IConfiguration>mockIConfig = new Mock<IConfiguration>();
            var userForRegisterDto = new UserForRegisterDto() {
                Username = username,
                Password = password
            };
            var userToCreate = new User() {
                Username = username
            };
            mockIAuthRepository.Setup(repo => repo.UserExists(userForRegisterDto.Username))
            .ReturnsAsync(false);
            mockIAuthRepository.Setup(repo => repo.Register(userToCreate, password))
            .ReturnsAsync(GetUser(username,password));
            var authController = new AuthController(mockIAuthRepository.Object, mockIConfig.Object);
            // Action: call auth API controller register method
            var result = await authController.Register(userForRegisterDto);
            // Test reponse the result against the expected status code type 
            Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.StatusCodeResult>(result);
        }
        [TestCase("usernameLogin","passwordLogin")]
        public async Task LoginTest_ResponseStatusCode(string username, string password)
        {
            //Arrage: Create injection 
            Mock<IAuthRepository> mockIAuthRepository = new Mock<IAuthRepository>();
            Mock<IConfiguration>mockIConfig = new Mock<IConfiguration>();
            //Created a user for login 
            var userForLogin = new UserForLoginDto() {
                UserName = username,
                PassWord = password
            };
            mockIAuthRepository.Setup(repo => repo.Login(userForLogin.UserName,userForLogin.PassWord))
            .ReturnsAsync(GetUser(username,password));
            var authController = new AuthController(mockIAuthRepository.Object, mockIConfig.Object);
            //Action : Call the login method and expecte to get a status code 
            var result = await authController.Login(userForLogin);
            Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.StatusCodeResult>(result);
        }
        public static User GetUser(string username, string password)
        {
            var user = new User() {
                Username = username,
                PasswordHash = Encoding.ASCII.GetBytes(password),
                PasswordSalt = Encoding.ASCII.GetBytes(password),
                DateCreated = DateTime.Now,
                LastActive = DateTime.Now
            };
            return user;
        }
    }
}