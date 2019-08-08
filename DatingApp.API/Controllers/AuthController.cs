using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]


    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            this._config = config;
            this._repo = repo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDto userforLoginDto)
        {

            var userFromRepo = await _repo.Login(userforLoginDto.UserName.ToLower(), userforLoginDto.Password);

            if (userFromRepo == null)
            {
                return Unauthorized();
            }
            var claims = new[]
            {


new Claim(ClaimTypes.NameIdentifier.ToString(),userFromRepo.Id.ToString()),
new Claim(ClaimTypes.Name.ToString(),userFromRepo.UserName.ToString())

};
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512);
        var TokenDescriptor = new SecurityTokenDescriptor
        {

            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds

        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(TokenDescriptor);
        return Ok (new{
token =  tokenHandler.WriteToken(token)
        });



}




        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
        {


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if (await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("UserNameAlreadyExist");


            var userToCreat = new User
            {
                UserName = userForRegisterDto.Username,
            };

            var createdUser = await _repo.Register(userToCreat, userForRegisterDto.Password);
            return StatusCode(201);
        }

    }
}