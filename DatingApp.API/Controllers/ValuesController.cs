using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Authorize]
    //POST http://localhost:5000/api/Values/5
    [Route("api/[controller]")]
     public class ValuesController : ControllerBase
    {

        private readonly DataContext _context;

        public ValuesController(DataContext context)
        {
            this._context = context;

        }
      
        // GET api/values
        [HttpGet]
        public async  Task<IActionResult> GetValues()
        {
            var values = await _context.Value.ToListAsync();
            return Ok(values) ;

        }
  [AllowAnonymous]
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task< IActionResult> Get(int id)
        {
           var values =  await _context.Value.Where(c=>c.ID==id).FirstOrDefaultAsync();
               return Ok(values) ;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
