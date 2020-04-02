using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Fuel.API.Data;
using Fuel.API.Dtos;
using Fuel.API.Helpers;
using Fuel.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Fuel.API.Controllers
{
    [Authorize]
    [Route("api/users/{userName}")]
    [ApiController]
    public class ProfileController: ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        // private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        // private Cloudinary _cloudinary;
        private readonly IPhotoUploadService _photoService;

        public ProfileController(IUserRepository repo, IMapper mapper,IPhotoUploadService photoService)
        {
            // _cloudinaryConfig = cloudinaryConfig;
            _mapper = mapper;
            _repo = repo;
            _photoService = photoService;
            // Account acc = new Account (
            //     _cloudinaryConfig.Value.CloudName,
            //     _cloudinaryConfig.Value.ApiKey,
            //     _cloudinaryConfig.Value.ApiSecret
            // );
            // _cloudinary = new Cloudinary(acc);
        }
        /*This function let users create/edit a new profile (corresponding to profile on from front-end)
        and return the new created profile to front-end */
        [HttpPost("profile")]
        public async Task<IActionResult> AddProfileForUser(string userName,[FromForm] ProfileForCreationDto profileForCreationDto)
        {
            var userFromRepo = await _repo.GetUser(userName);
            var profileToInsert = new ClientProfile();
            // If first time user (no profile) => just map
            if (userFromRepo.ClientProfile  == null) 
            {
                profileToInsert = _mapper.Map<ClientProfile>(profileForCreationDto);
            }
            // User has profile pic => map manually to avoid losing photo URL 
            else 
            {
                profileToInsert.Fullname = profileForCreationDto.Fullname;
                profileToInsert.Address1 = profileForCreationDto.Address1;
                profileToInsert.Address2 = profileForCreationDto.Address2;
                profileToInsert.State = profileForCreationDto.State;
                profileToInsert.City = profileForCreationDto.City;
                profileToInsert.Zipcode = profileForCreationDto.Zipcode;
                profileToInsert.PhotoURL = userFromRepo.ClientProfile.PhotoURL;
                profileToInsert.PhotoPublicId = userFromRepo.ClientProfile.PhotoPublicId;
            }
            userFromRepo.ClientProfile = profileToInsert;
            if (await _repo.SaveAll())
            {
                var profileToReturn = _mapper.Map<ProfileForReturnDto>(userFromRepo.ClientProfile);
                // return CreatedAtRoute(nameof(GetProfile), new {id = 1}, profileToReturn);
                return Ok(profileToReturn);
            }
            return BadRequest("Could not add the profile");
        }
        [HttpPost("photo")]
        public async Task<IActionResult>UploadProfilePic(string username, [FromForm] ProfileForCreationDto profileForCreationDto)
        {
            //Fetch user from repo
            var userFromRepo = await _repo.GetUser(username);
            var file = profileForCreationDto.File;
            var uploadResult = new ImageUploadResult();
            // Upload profile pic to cloudinary using API 
            if (file.Length>0)
            {
                 using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };
                    uploadResult = _photoService.GetCloudinaryService().Upload(uploadParams);
                }
            }
            // save return uploaded photo URL & publicId into user repo
            profileForCreationDto.PhotoURL = uploadResult.Uri.ToString();
            profileForCreationDto.PhotoPublicId = uploadResult.PublicId;
            var profile = new ClientProfile();
            // If user has profile already => keep other field values
            if(userFromRepo.ClientProfile != null)
            {
                profile.Fullname = userFromRepo.ClientProfile.Fullname;
                profile.Address1 = userFromRepo.ClientProfile.Address1;
                profile.Address2 = userFromRepo.ClientProfile.Address2;
                profile.City = userFromRepo.ClientProfile.City;
                profile.State = userFromRepo.ClientProfile.State;
                profile.Zipcode = userFromRepo.ClientProfile.Zipcode;
            }
            // User profile is not empty => user only tries to edit their profile photo => keep everything the same because the only change is profile pic
            profile.PhotoPublicId = profileForCreationDto.PhotoPublicId;
            profile.PhotoURL = profileForCreationDto.PhotoURL;
            userFromRepo.ClientProfile = profile;
            if (await _repo.SaveAll())
            {
                return Ok(new {userFromRepo.ClientProfile.PhotoURL});
            }
            return BadRequest("Could not add profile photo");
        }
        /*This function generate a new quote (corresponding to quote form) */
        [HttpPost("quote")]
        public async Task<IActionResult> GenerateNewQuote(string username, [FromForm] QuoteForDetailedDto quoteForGenerationDto)
        {
            // Fetch user from user table with username
            var userFromRepo = await _repo.GetUser(username);
            // Call business login function from user repository to calculate the suggested price
            var suggestedPrice = _repo.CalculatePrice(userFromRepo, quoteForGenerationDto);
            // Assign & Map all values back to database
            quoteForGenerationDto.SuggestedPrice = suggestedPrice;
            quoteForGenerationDto.AmountDue = suggestedPrice * quoteForGenerationDto.GallonsRequested;
            var newQuote = _mapper.Map<Quote>(quoteForGenerationDto);
            userFromRepo.Quote.Add(newQuote);
            // Save and return ok status &  the new quote if success
            if (await _repo.SaveAll())
            {
                // return Ok(new {newQuote.SuggestedPrice, newQuote.AmountDue});
                return Ok(new {newQuote.SuggestedPrice, newQuote.AmountDue});
            }
            return BadRequest("Could not generate quote");
        }

    }
}