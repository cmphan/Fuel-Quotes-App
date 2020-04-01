// using System;
// using System.Text;
// using System.Threading.Tasks;
// using AutoMapper;
// using CloudinaryDotNet;
// using Fuel.API.Controllers;
// using Fuel.API.Data;
// using Fuel.API.Dtos;
// using Fuel.API.Helpers;
// using Fuel.API.Models;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Options;
// using Moq;
// using NUnit.Framework;
// namespace Fuel.API.UnitTest
// {
//     public class ProfileControllerTest
//     {
//         [TestCase("lewis","lewis nguyen","123 Main Street","123 FM 1969", "12345", "cloudinary.com/photo.jpg",
//         "Dallas","TX","77099")]
//         public async Task AddProfileForUserTest_ResponseStatusCode(string username, string fullname, string address1, string address2,
//         string photoPublicId, string PhotoURL, string City, string State, string Zipcode)
//         {
//             // Arrange : set up a new user profile with the given input username and password
//             Mock<IUserRepository> mockIUserRepository = new Mock<IUserRepository>();
//             Mock<IMapper>mockIMapper = new Mock<IMapper>();
//             Mock<IOptions<CloudinarySettings>> mockICloudinary = new  Mock<IOptions<CloudinarySettings>>();
//             // return an assigned user when call a mock version of repo 
//             var userWithProfile = GetUserWithProfile(username);
//             mockIUserRepository.Setup(user_repo => user_repo.GetUser(username))
//             .ReturnsAsync(AuthControllerTest.GetUser(username, "password"));
//             var profileForCreation = GetClientProfile();
//             mockIMapper.Setup(_mapper => _mapper.Map<ClientProfile>(profileForCreation))
//             .Returns(profileForCreation);
//             mockIUserRepository.Setup(user_repo => user_repo.SaveAll()).ReturnsAsync(false);
//             mockIMapper.Setup(_mapper => _mapper.Map<ProfileForReturnDto>(userWithProfile.ClientProfile))
//             .Returns(new ProfileForReturnDto() {});
//             var profileController = new ProfileController(mockIUserRepository.Object,mockIMapper.Object,mockICloudinary.Object);
//             var result = await profileController.AddProfileForUser(username,new ProfileForCreationDto(){
//                 Fullname = fullname,
//                 Address1 = address1,
//                 Address2 = address2,
//                 PhotoPublicId = photoPublicId,
//                 PhotoURL = PhotoURL,
//                 City = City,
//                 State = State,
//                 Zipcode = Zipcode
//             });
//              Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.StatusCodeResult>(result);
//         }
//         private ClientProfile GetClientProfile() {
//             return new ClientProfile() {
//                 Fullname = "fullname",
//                 Address1 = "address1",
//                 Address2 = "address2",
//                 PhotoPublicId = "photoPublicId",
//                 PhotoURL = "photoURL",
//                 City = "City",
//                 State = "State",
//                 Zipcode = "12345"
//             };
//         }
//         private User GetUserWithProfile(string username) {
//             var user = AuthControllerTest.GetUser(username, "password");
//             user.ClientProfile = GetClientProfile();
//             return user;
//         }
//     }
// }