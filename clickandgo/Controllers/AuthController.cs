using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using clickandgo.Data;
using clickandgo.dto;
using clickandgo.Helper;
using clickandgo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace clickandgo.Controllers
{
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUsers _userRepository;
        private readonly TokenHelper _tokenHelper = new TokenHelper();

        public AuthController(IUsers userRepository, IConfiguration config)
        {
            _config = config;
            _userRepository = userRepository; 
        }

        [AllowAnonymous]
        [Route("api/users/register")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] RegisterDto register)
        {
            // check if user is already in database using email 

            Users user = await _userRepository.CheckUser(register.Email);



            if (user == null)
            {
                Users userData = new Users();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<RegisterDto, Users>();
                });
                IMapper mapper = new Mapper(config);
                Users dest = mapper.Map<RegisterDto, Users>(register);

                await _userRepository.CreateUser(dest, register.Password);

                return Ok(new { status = "success" });
            }
            else
            {
                return Ok(new { status = "fail" });
            }

            //add user to database and return user information
            //return NotFound();
        }

        [AllowAnonymous]
        [Route("api/users/login/{email}/{password}")]
        [HttpGet]
        public async Task<IActionResult> LoginUser(string email, string password)
        {
            Users user = await _userRepository.LoginUser(email, password);

            if (user != null)
            {

                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user._id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)

            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    type = user.Type
                });
            }
            else
            {
                return Ok(new
                {
                    error = "invalid"
                });
            }

            
        }

        [Route("api/users/adminCreateOwner")]
        [HttpPost]
        public async Task<IActionResult> createOwnerAdmin([FromBody]OwnerDto owner)
        {
           
            // check if user is already in the system
            Users user = await _userRepository.CheckUser(owner.Email);

            if (user == null)
            {

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<OwnerDto, Users>();
                });
                IMapper mapper = new Mapper(config);
                Users userData = mapper.Map<OwnerDto, Users>(owner);

                Address address = new Address();


                address.Street = owner.Address1;
                address.City = owner.City; 
                address.District = owner.District; 
                address.Parish = owner.Parish; 
                address.Country = owner.Country;

                userData.Address = address;

                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[8];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);


                // add user to the system 
                await _userRepository.CreateUser(userData, finalString);

                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("mail.clickandgoja.com");

                    mail.From = new MailAddress("admin@clickandgoja.com");
                    mail.To.Add(userData.Email);
                    mail.Subject = "New Account";
                    mail.Body = "Your email is "+userData.Email+" and your password is "+finalString;

                    SmtpServer.Port = 25;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("admin@clickandgoja.com", "clickandgoja");
                    SmtpServer.EnableSsl = false;

                    SmtpServer.Send(mail);
                    return Ok(new { status = "email sent" });
                }
                catch (Exception ex)
                {
                    return Ok(new { status = ex.ToString() }); 
                }
            }

            return Ok(new { status = "no data" });
        }

        [AllowAnonymous]
        [Route("api/users/createOwner")]
        [HttpPost]
        public async Task<IActionResult> createOwner([FromBody]OwnerDto owner)
        {
            // check if user is already in the system
            Users user = await _userRepository.CheckUser(owner.Email);

            CultureInfo culture = new CultureInfo("en-US");
            DateTime tempDate = Convert.ToDateTime(owner.DOB, culture);

            user.DOB = tempDate;
            user.MiddleName = owner.MiddleName;
            user.Trn = owner.Trn;
            user.Stage = owner.Stage;
            user.Gender = owner.Gender;

            Address address = new Address();

            address.Street = owner.Address1;
            address.City = owner.City;
            address.District = owner.District;
            address.Parish = owner.Parish;
            address.Country = owner.Country;

            user.Address = address;

            await _userRepository.UpdateUserMainAsync(owner.Email, user);

            return Ok(new { status = "updated"});
        }

        [Route("api/users/checkStage")]
        [HttpGet]
        public async Task<IActionResult> checkStage()
        {
            var token = Request.Headers["Authorization"];
            string id = _tokenHelper.getUserFromToken(token);

            Users user = await _userRepository.CheckUserById(id);

            return Ok(new { stage = user.Stage });
        }
    }
  

}