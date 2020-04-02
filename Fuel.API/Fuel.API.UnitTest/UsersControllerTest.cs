using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using Fuel.API.Controllers;
using Fuel.API.Data;
using Fuel.API.Dtos;
using Fuel.API.Helpers;
using Fuel.API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
namespace Fuel.API.UnitTest
{
    public class UsersControllerTest
    {
        //This function test the GetUsers() to get all users from db in UsersController 
        [Test]
        public async Task GetUsersTest_ReturnAllUsers()
        {
            //Arrange: create concrete dependency injection and an instance of UsersController 
            Mock<IUserRepository> mockIUserRepository = new Mock<IUserRepository>();
            Mock<IMapper> mockIMapper = new Mock<IMapper>();
            mockIUserRepository.Setup(user_repo => user_repo.GetUsers()).Returns(Task.FromResult((GetUsers())));
            mockIMapper.Setup(_mapper => _mapper.Map<IEnumerable<UserForListDto>>(It.IsAny<IEnumerable<User>>()))
            .Returns(ReturnedUsers);
            var usersController = new UsersController(mockIUserRepository.Object, mockIMapper.Object);
            // Action call GetUsers() and expect and Ok status code with a JSON of all users 
            var result = await usersController.GetUsers();
            // Check if actual is expected: ok (users)
            Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
        }
        // This function test GetUser(username) in Users Controller => return one user matches the input username
        [TestCase("john")]
        public async Task GetUserTest_ReturnOneUser(string username)
        {
            //Arrange: create concrete dependency injection and an instance of UsersController 
            Mock<IUserRepository> mockIUserRepository = new Mock<IUserRepository>();
            Mock<IMapper> mockIMapper = new Mock<IMapper>();
            // Return a virtual user when query
            mockIUserRepository.Setup(user_repo => user_repo.GetUser(username)).
            ReturnsAsync(AuthControllerTest.GetUser(username, "password"));
            // Mapping virtual object when calling mapper
            var usersController = new UsersController(mockIUserRepository.Object,mockIMapper.Object);
            //Action: Call GetUser to get the user with the matching username and expect an ok status code from Users API Controller
            var response = await usersController.GetUser(username);
            //Assert:  Check if actual response is expected: an ok object result Ok(user)
            Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.OkObjectResult>(response);
        }
        // Create a virtual users data for unit UsersController API method test purpose 
        private IEnumerable<User> GetUsers() {
            List<User> list_users = new List<User>();
            list_users.Add(AuthControllerTest.GetUser("lewis","password"));
            list_users.Add(AuthControllerTest.GetUser("bob","password"));
            IEnumerable<User> users = list_users;
            return users;
        }
        private IEnumerable <UserForListDto> ReturnedUsers()
        {
            List<UserForListDto> list_users = new List<UserForListDto>();
            var user_list = new UserForListDto() {
                Username="peter"
            };
            list_users.Add(user_list);
            return list_users;
        }

    }
}