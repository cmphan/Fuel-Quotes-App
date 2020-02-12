using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fuel.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fuel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController: ControllerBase
    {
        private readonly DataContext _context;
        //Constructor of Values Controller
        public ValuesController(DataContext context)
        {
            _context = context;
        }
        //Get api/values
        [HttpGet]
        public async Task<IActionResult>GetValues()
        {
            var values = await _context.Values.ToListAsync();
            return Ok(values);
        }
        //Get api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult>GetValues(int id)
        {
            //Fet value out of db based on ID
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }
    }
}