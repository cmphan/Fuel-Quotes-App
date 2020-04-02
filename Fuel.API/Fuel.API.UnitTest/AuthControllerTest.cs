using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fuel.API.Controllers;
using Fuel.API.Data;
using Fuel.API.Dtos;
using Fuel.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
namespace Fuel.API.UnitTest
{
    [TestFixture]
    public class AuthControllerTest
    {
       
        /* Function test for Register method in Auth API Controller 
        Expect to return a status code 201 - new user is created succesfully
        */
        [TestCase("usernameForRegister","passwordForRegister")]
        public async Task Register_Successful_ResponseStatusCode201(string username, string password)
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
            //False means Username does not exist in database before => able to create a new user
            mockIAuthRepository.Setup(repo => repo.UserExists(It.IsAny<String>()))
            .Returns(Task.FromResult(false));
            mockIAuthRepository.Setup(repo => repo.Register(It.IsAny<User>(), It.IsAny<String>()))
            .ReturnsAsync(GetUser(username,password));
            var authController = new AuthController(mockIAuthRepository.Object, mockIConfig.Object);
            // Action: call auth API controller register method
            var actionResult = await authController.Register(userForRegisterDto);
            // Test reponse the result. Expect StatusCode(201) for successful registration  
            Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.StatusCodeResult>(actionResult);
        }
        /*Test Register method in Auth Controller 
        Expect to return a bad resquest because username already exists */
         [TestCase("usernameForRegister","passwordForRegister")]
        public async Task Register_Failure_ResponseBadRequestUsernameAlreadyExists(string username, string password)
        {
            //Arrange: inject dependency into Auth Controller
            Mock<IAuthRepository> mockIAuthRepository = new Mock<IAuthRepository>();
            Mock<IConfiguration>mockIConfig = new Mock<IConfiguration>();
            var userForRegisterDto = new UserForRegisterDto() {
                Username = username,
                Password = password
            };
            var userToCreate = new User() {
                Username = username
            };
            // Set up so that username is already exits in the system -> return a bad request 
            mockIAuthRepository.Setup(repo => repo.UserExists(It.IsAny<String>()))
            .ReturnsAsync(true);
            var authController = new AuthController(mockIAuthRepository.Object, mockIConfig.Object);
            // Action: call register user method
             var actionResult = await authController.Register(userForRegisterDto);
            // Assert: expected a bad request object 
             Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(actionResult);
        }
        /* Test for Login function in Auth Controller 
        In case of success login => Controller return a Ok(token) (OkObjectResult) with the generated token */
        [TestCase("usernameLogin","passwordLogin")]
        public async Task Login_Success_ResponseStatusOkObjectToken(string username, string password)
        {
            //Arrage: Create injection 
            Mock<IAuthRepository> mockIAuthRepository = new Mock<IAuthRepository>();
            Mock<IConfiguration>mockIConfig = new Mock<IConfiguration>();
            //Created a user for login 
            var userForLogin = new UserForLoginDto() {
                UserName = username,
                PassWord = password
            };
            mockIAuthRepository.Setup(repo => repo.Login(It.IsAny<string>(),It.IsAny<string>()))
            .Returns(Task.FromResult(GetUser(username,password)));
            var mockIConfig_Value = new Mock<IConfigurationSection>();
            // instantiate an icongif interface
            mockIConfig_Value.Setup(c => c.Value).Returns("secret secret key");
            mockIConfig.Setup(_config => _config.GetSection(It.IsAny<string>())).Returns(mockIConfig_Value.Object);
            var authController = new AuthController(mockIAuthRepository.Object, mockIConfig.Object);
            //Action : Call the login method and expecte to get a status code 
            var result = await authController.Login(userForLogin);
            //Assert: expect a Ok(token), which is an OkObjectResult 
            Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
        }
        /* Test for Login function in Auth Controller 
        In case of success login => Controller return an UnAuthozied() response */
        [TestCase("usernameLogin","passwordLogin")]
        public async Task Login_Failure_ReponseUnAuthorized(string username, string password)
        {
            //Arrage: Create injection 
            Mock<IAuthRepository> mockIAuthRepository = new Mock<IAuthRepository>();
            Mock<IConfiguration>mockIConfig = new Mock<IConfiguration>();
            //Created a user for login 
            var userForLogin = new UserForLoginDto() {
                UserName = username,
                PassWord = password
            };
            // Login fail => return null as no users in the system matches the input username
            mockIAuthRepository.Setup(repo => repo.Login(It.IsAny<string>(),It.IsAny<string>()))
            .Returns(Task.FromResult((User)null));
            var authController = new AuthController(mockIAuthRepository.Object, mockIConfig.Object);
            //Action : Call the login method and expecte to get a UnAuthorized Request 
            var response = await authController.Login(userForLogin);
            //Assert: expect an Unauthorized response
            Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.UnauthorizedResult>(response);
        }
        public static User GetUser(string username, string password)
        {
            var user = new User() {
                UserId = 1,
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