/*Controller the implementation of Auth Repository
================================================= */
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Fuel.API.Data;
using Fuel.API.Dtos;
using Fuel.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Fuel.API.Controllers
{
    //Routing 
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }       
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //store all username in lowercase inside database
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if (await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("Username already exists");
            var userToCreate = new User
            {
                Username = userForRegisterDto.Username 
            };
            //Create user 
            var createdUser = await _repo.Register(userToCreate,userForRegisterDto.Password);
            //Status return 201 (created) => successful HTTP POST 
            return StatusCode(201);
        }
        /* Documentation for Kim & Thuy team members: 
        This login function contains the following steps: 
        1. Call login method in repo to check if user name exists => no null then exists in db
        2. Creates a token with 2 claims: using userid and user name 
        3. The server sign the token with a security key (assigned by developer in appsettings.json file)
        4. The server then encrypt the key with a given hashing algorithm 
        5. Create  tokenDescriptor with all infor (claims, expires, singing creds)
        6. Create JWTSecurityTokenHanlder to create token from tokenDescriptor and stored in token variable
        7. Return as token object and write in HTTP response to client 
         */
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            //Check if we have a user 
            var userFromRepo = await _repo.Login(userForLoginDto.UserName.ToLower(),userForLoginDto.PassWord);
            if (userFromRepo == null)
            {
                return Unauthorized();
            }
            //Tokenization created with claims
            var claims = new [] 
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.UserId.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            //Token Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSetting:Token").Value));
            //Create signing credentials 
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            //Create security token Decriptor 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //return token as a object
            return Ok(new{
                token = tokenHandler.WriteToken(token)
            });
        }
        // profile function to post the client profile form (first time user) to the server */
        //   [HttpPost("profile")]
        //  public async Task<IActionResult> Profile(ClientProfileDto clientProfileDto)
        // {
        //     var clientProfileToCreate = new ClientProfile();
        //     //Create new client 
        //     var createdProfile = await _repo.Profile(clientProfileToCreate,clientProfileDto.Fullname, clientProfileDto.Address1, clientProfileDto.Address2,clientProfileDto.City,
        //     clientProfileDto.State, clientProfileDto.Zipcode);
        //     //Status return 201 (created) => successful HTTP POST 
        //     return StatusCode(201);
        // }
    }
    /* Task to do 
    + Provide controller for implementation of auth repository 
    + Build Tokenization 
    */
}