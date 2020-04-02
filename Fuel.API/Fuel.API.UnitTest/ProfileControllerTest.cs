using System;
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
    public class ProfileControllerTest
    {
        // This test the AddProfileForUser function in Profile Controller 
        // expect an Ok status code with newly created user profile 
        [TestCase("lewis","lewis nguyen","123 Main Street","123 FM 1969", "12345", "cloudinary.com/photo.jpg",
        "Dallas","TX","77099")]
        public async Task AddProfileForUser_Success_ReturnAnOkStatusWithNewProfile(string username, string fullname, string address1, string address2,
        string photoPublicId, string PhotoURL, string City, string State, string Zipcode)
        {
            // Arrange : set up a new user profile 
            Mock<IUserRepository> mockIUserRepository = new Mock<IUserRepository>();
            Mock<IMapper>mockIMapper = new Mock<IMapper>();
            Mock<IPhotoUploadService> mockICloudinary = new Mock<IPhotoUploadService>();
            // return an assigned user when call a mock version of repo 
            var userWithProfile = GetUserWithProfile(username);
            var profileForCreation = GetClientProfile();
            mockIUserRepository.Setup(user_repo => user_repo.GetUser(It.IsAny<string>()))
            .Returns(Task.FromResult(AuthControllerTest.GetUser(username, "password")));
            mockIMapper.Setup(_mapper => _mapper.Map<ClientProfile>(It.IsAny<ProfileForCreationDto>()))
            .Returns(profileForCreation);
            //This true means there a changed in db => new user profile edited successfully 
            mockIUserRepository.Setup(user_repo => user_repo.SaveAll()).Returns(Task.FromResult(true));
            mockIMapper.Setup(_mapper => _mapper.Map<ProfileForReturnDto>(userWithProfile.ClientProfile))
            .Returns(new ProfileForReturnDto() {});
            // Inject Profile Controller dependency 
            var profileController = new ProfileController(mockIUserRepository.Object,mockIMapper.Object,mockICloudinary.Object);
            var response = await profileController.AddProfileForUser(username,new ProfileForCreationDto(){
                Fullname = fullname,
                Address1 = address1,
                Address2 = address2,
                PhotoPublicId = photoPublicId,
                PhotoURL = PhotoURL,
                City = City,
                State = State,
                Zipcode = Zipcode
            });
            // Assert: Test Acutal vs Expect Ok(ProfileUser) object 
             Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.OkObjectResult>(response);
        }
        [TestCase("bob","bob nguyen","1234 Milam Street","1235 6th street", "22142", "cloudinary.com/photo1232.jpg",
        "Austin","TX","77025")]
        public async Task AddProfileForUser_Failure_ReturnABadRequest(string username, string fullname, string address1, string address2,
        string photoPublicId, string PhotoURL, string City, string State, string Zipcode)
        {
            // Arrange : set up a new user profile 
            Mock<IUserRepository> mockIUserRepository = new Mock<IUserRepository>();
            Mock<IMapper>mockIMapper = new Mock<IMapper>();
            Mock<IPhotoUploadService> mockICloudinary = new Mock<IPhotoUploadService>();
            // return an assigned user when call a mock version of repo 
            var userWithProfile = GetUserWithProfile(username);
            //User can only add profile once login so repo must return a user
            mockIUserRepository.Setup(user_repo => user_repo.GetUser(It.IsAny<string>()))
            .Returns(Task.FromResult(AuthControllerTest.GetUser(username, "password")));
            var profileForCreation = GetClientProfile();
            mockIMapper.Setup(_mapper => _mapper.Map<ClientProfile>(It.IsAny<ProfileForCreationDto>()))
            .Returns(profileForCreation);
            //This false => no changes are made to repo 
            mockIUserRepository.Setup(user_repo => user_repo.SaveAll()).Returns(Task.FromResult(false));
            var profileController = new ProfileController(mockIUserRepository.Object,mockIMapper.Object,mockICloudinary.Object);
            //Action: Call AddProfileForUser in API
            var response = await profileController.AddProfileForUser(username,new ProfileForCreationDto(){});
            //Assert: Expect A Bad Request ("Could not add the project") of type Bad Reqest Object Result 
            Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(response);
        }
        private ClientProfile GetClientProfile() {
            return new ClientProfile() {
                Fullname = "fullname",
                Address1 = "address1",
                Address2 = "address2",
                PhotoPublicId = "photoPublicId",
                PhotoURL = "photoURL",
                City = "City",
                State = "State",
                Zipcode = "12345"
            };
        }
        private User GetUserWithProfile(string username) {
            var user = AuthControllerTest.GetUser(username, "password");
            user.ClientProfile = GetClientProfile();
            return user;
        }
    }
}