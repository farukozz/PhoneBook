using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PhoneBook.API.Models;

namespace PhoneBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IMapper _mapper;
        private IConfiguration _configuration;

        public AuthController(IAuthService authService, IMapper mapper,IConfiguration configuration)
        {
            _authService = authService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost(template: "login")]
        public async Task<ActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = await _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest
                    (userToLogin.Message);
            }

            var token = CreateToken(userToLogin.Data);
            User authUser = userToLogin.Data;
            authUser.PasswordHash = null;
            authUser.PasswordSalt = null;
            return Ok(new { User = authUser, Token = token });
        }

        [HttpPost(template: "register")]
        public async Task<ActionResult> Register(RegisterModel registerModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new { Message = ModelState });
            }
            var userExists = await _authService.UserUxists(registerModel.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }
            var userForRegisterDto = _mapper.Map<RegisterModel, UserForRegisterDto>(registerModel);



            var registerResult = await _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var token = CreateToken(registerResult.Data);
            User createdUser = registerResult.Data;
            createdUser.PasswordHash = null;
            createdUser.PasswordSalt = null;
            return Ok(new { User=createdUser,Token=token});
        }

        public string CreateToken(User user)
        {
            // Authentication(Yetkilendirme) başarılı ise JWT token üretilir.
            var tokenHandler = new JwtSecurityTokenHandler();
            string securityKey = _configuration.GetValue<string>("JWT:SecurityKey");
            var key = Encoding.ASCII.GetBytes(securityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string jwt = tokenHandler.WriteToken(token);
            return jwt;
        }
    }
}