using System;
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
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public ProfileController(IUserRepository repo, IMapper mapper,
        IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _mapper = mapper;
            _repo = repo;
            Account acc = new Account (
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(acc);
        }
        // [HttpGet("{id}", Name="GetProfile")]
        // public async Task<IActionResult> GetProfile(int id)
        // {
        //     var profileFromRepo = await _repo.GetProfile(id);
        //     var profile = _mapper.Map<ProfileForReturnDto>(profileFromRepo);
        //     return Ok(profile);
        // }
        /*This function let users create/edit a new profile (corresponding to profile on from front-end)
        and return the new created profile to front-end */
        [HttpPost("profile")]
        public async Task<IActionResult> AddProfileForUser(string userName,[FromForm] ProfileForCreationDto profileForCreationDto)
        {
            var userFromRepo = await _repo.GetUser(userName);
            var file = profileForCreationDto.File;
            var uploadResult = new ImageUploadResult();
            if(file.Length>0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            profileForCreationDto.PhotoURL = uploadResult.Uri.ToString();
            profileForCreationDto.PhotoPublicId = uploadResult.PublicId;
            var profile = _mapper.Map<ClientProfile>(profileForCreationDto);
            userFromRepo.ClientProfile = profile;
            
            if (await _repo.SaveAll())
            {
                var profileToReturn = _mapper.Map<ProfileForReturnDto>(profile);
                // return CreatedAtRoute(nameof(GetProfile), new {id = 1}, profileToReturn);
                return Ok(profileToReturn);
            }
            return BadRequest("Could not add the profile");
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