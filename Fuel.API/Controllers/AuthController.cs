/*Controller the implementation of Auth Repository
================================================= */
using Fuel.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace Fuel.API.Controllers
{
    //Routing 
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }
    }
    /* Task to do 
    + Provide controller for implementation of auth repository 
    + Build Tokenization 
    */
}