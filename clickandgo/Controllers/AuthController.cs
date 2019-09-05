using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
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
        private readonly IDriver _driverRepository;
        private readonly TokenHelper _tokenHelper = new TokenHelper();

        public AuthController(IUsers userRepository, IConfiguration config, IDriver driver)
        {
            _config = config;
            _userRepository = userRepository;
            _driverRepository = driver;
        }

        [AllowAnonymous]
        [Route("api/users/verify/{code}/{email}")]
        public async Task<IActionResult> VerifyAccount(string code, string email)
        {

            var newEmail = Base64Decode(email);
            Users user = await _userRepository.CheckUser(newEmail);

            if (user != null)
            {
                if (user.VerificationCode == code)
                {
                    var result = await _userRepository.UpdateVerificationCode(code, newEmail);

                    return Ok(new { status = "success", result, email= newEmail });
                    
                }
                else
                {

                    return Ok(new { status = "fail " + code + " " + newEmail });
                }
            }
            else {
                return Ok(new { status = "noUser"});
            }

            
        }

        [AllowAnonymous]
        [Route("api/users/validateMobile/{code}/{trn}")]
        [HttpGet]
        public async Task<IActionResult> validateMobile(string code, string trn)
        {
            Driver user = await _driverRepository.GetDriverByTrn(trn);

            if (user == null)
                return NotFound();

            if (user.Code == code)
            {
                return Ok("success");
            }
            else
            {
                return Ok("fail");
            }

        }

        [AllowAnonymous]
        [Route("api/users/loginRiderMobile/{user}/{password}")]
        [HttpGet]
        public async Task<IActionResult> loginRider(string user, string password)
        {
            Users userData = new Users();

            userData = await _userRepository.LoginUser(user, password);

            if (userData == null)
                return NotFound();

            if (userData.Verified == null)
            {
                return Ok(new { status = "codeError" });
            }
            else
            {
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userData._id.ToString()),
                new Claim(ClaimTypes.Name, userData.FirstName)

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
                    type = userData.Type, 
                    user = userData
                });
            }


            

           //Users user = await _userRepository.LoginUser(user, password);
   
        }

        [AllowAnonymous]
        [Route("api/users/loginMobile/{trn}/{password}")]
        [HttpGet]
        public async Task<IActionResult> loginMobile(string trn, string password)
        {
            Driver user = await _driverRepository.LoginDriver(trn, password);


            if (user != null)
            {



                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user._id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName)

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
                    type = user.status
                });
            }
            return Ok();
        }

        [AllowAnonymous]
        [Route("api/users/registerRiderMobile")]
        [HttpPost]
        public async Task<IActionResult> RegisterRiderMobile([FromBody] Users user)
        {

            string password = user.Password;

            Random random = new Random();
            int number = random.Next(1000, 9999);

            // send email
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.clickandgoja.com");

                mail.From = new MailAddress("admin@clickandgoja.com");
                mail.To.Add(user.Email);
                mail.Subject = "New Account";
                mail.IsBodyHtml = true;
                mail.Body = "Use this code to verify your account<b>" + number + "</b>";

                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("admin@clickandgoja.com", "clickandgoja");
                SmtpServer.EnableSsl = false;

                SmtpServer.Send(mail);


                mail.From = new MailAddress("admin@clickandgoja.com");
                mail.To.Add("ecampbell@mysticlightsstudios.com");
                mail.Subject = "New Account";
                mail.IsBodyHtml = true;
                mail.Body = "Please click on this link to activate account" + number;

                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("admin@clickandgoja.com", "clickandgoja");
                SmtpServer.EnableSsl = false;

                SmtpServer.Send(mail);

                user.Password = null;
                user.VerificationCode = number.ToString();

                await _userRepository.CreateUser(user, password, "rider");
                return Ok(new { user = "created" });


            }
            catch (Exception ex)
            {
                return Ok(new { status = ex.ToString() });
            }

            
        }
               
        [AllowAnonymous]
        [Route("api/users/registerMobile/{trn}/{email}/{password}")]
        [HttpGet]
        public async Task<IActionResult> registerMobile(string trn, string email, string password)
        {
            // check if user exists 
            Driver user = await _driverRepository.GetDriverByTrn(trn);

            if (user == null)
                return NotFound();

            Random random = new Random();
            int number = random.Next(1000, 9999);

            // send email
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.clickandgoja.com");

                mail.From = new MailAddress("admin@clickandgoja.com");
                mail.To.Add(email);
                mail.Subject = "New Account";
                mail.IsBodyHtml = true;
                mail.Body = "Use this code to verify your account<b>"+ number + "</b>";

                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("admin@clickandgoja.com", "clickandgoja");
                SmtpServer.EnableSsl = false;

                SmtpServer.Send(mail);


                mail.From = new MailAddress("admin@clickandgoja.com");
                mail.To.Add("ecampbell@mysticlightsstudios.com");
                mail.Subject = "New Account";
                mail.IsBodyHtml = true;
                mail.Body = "Please click on this link to activate account" + number;

                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("admin@clickandgoja.com", "clickandgoja");
                SmtpServer.EnableSsl = false;

                SmtpServer.Send(mail);

                await _driverRepository.updateDriverCode(number, trn);
                await _driverRepository.UpdatePassword(trn, password);

                return Ok(new { status = "success"});
                
            }
            catch (Exception ex)
            {
                return Ok(new { status = ex.ToString() });
            }

        }

        [AllowAnonymous]
        [Route("api/users/GetCode/{code}/{email}")]
        [HttpGet]
        public async Task<IActionResult> GetCode(string code, string email)
        {
            Users user = await _userRepository.CheckUser(email); 

            if (user == null)
                return NotFound();


            if (user.VerificationCode != code)
                return BadRequest();

            user.Verified = "true";

            await _userRepository.UpdateUserMainAsync("rider", user);


            return Ok(new { status = "updated"});
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

                await _userRepository.CreateUser(dest, register.Password, "owner");

                // login user
                var token = await LoginUser(register.Email, register.Password);

                string baseString = register.Email + ":" + register.Password;
                var newBaseString = Base64Encode(baseString);
                var code = GetStringSha256Hash(newBaseString);


                // store crytic code in database 

                var email = Base64Encode(register.Email.Trim());

                await _userRepository.SetVerificationCode(code, register.Email);


                // send email
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("mail.clickandgoja.com");

                    mail.From = new MailAddress("admin@clickandgoja.com");
                    mail.To.Add(register.Email);
                    mail.Subject = "New Account";
                    mail.IsBodyHtml = true;
                    mail.Body = "Please click on this link to activate account <a href='http://clickandgoja.com/verification/" + code + "/"+ email + "'>verify</a>";

                    SmtpServer.Port = 25;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("admin@clickandgoja.com", "clickandgoja");
                    SmtpServer.EnableSsl = false;

                    SmtpServer.Send(mail);


                    mail.From = new MailAddress("admin@clickandgoja.com");
                    mail.To.Add("ecampbell@mysticlightsstudios.com");
                    mail.Subject = "New Account";
                    mail.IsBodyHtml = true;
                    mail.Body = "Please click on this link to activate account <a href='http://clickandgoja.com/verification/" + code + "/" + email + "'>verify</a>";

                    SmtpServer.Port = 25;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("admin@clickandgoja.com", "clickandgoja");
                    SmtpServer.EnableSsl = false;

                    SmtpServer.Send(mail);

                    return Ok(new { status = "success", token });
                }
                catch (Exception ex)
                {
                    return Ok(new { status = ex.ToString() });
                }


            }
            else
            {
                return Ok(new { status = "fail" });
            }

            //add user to database and return user information
            //return NotFound();
        }

        [AllowAnonymous]
        [Route("api/users/resetpassword")]
        [HttpPost]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdateUserPassword updateUserPassword)
        {
            var user = await _userRepository.CheckUser(updateUserPassword.Email);
            if(user != null)
            {
               if(await _userRepository.UpdatePassword(updateUserPassword.Email, updateUserPassword.Password))
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest(false);
                }
            }

            return Ok(false);
        }

        [AllowAnonymous]
        [Route("api/users/forgotpassword/{email}")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userRepository.CheckUser(email);
            
            if (user != null)
            {
                string baseString = email;
                var newBaseString = Base64Encode(baseString);
                var code = GetStringSha256Hash(newBaseString);
                await _userRepository.SetVerificationCodeReset(code, email); // this should be a sha hash
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("mail.clickandgoja.com");

                    mail.From = new MailAddress("admin@clickandgoja.com");
                    mail.To.Add(email); // when testing, it is advised to hard code your email here so you can get the emails
                    mail.Subject = "Reset Password link";
                    mail.IsBodyHtml = true;
                    mail.Body = "Please click on this link to change password <a href='http://clickandgoja.com/forgotpassword/" + code + "/" + Base64Encode(email) + "'>verify</a>"; // email must be base64 before transit

                    SmtpServer.Port = 25;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("admin@clickandgoja.com", "clickandgoja");
                    SmtpServer.EnableSsl = false;

                    SmtpServer.Send(mail);
                    return Ok(true);
                }catch(Exception e)
                {
                    return BadRequest(e);
                }
            }
            return BadRequest();
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

        [Route("api/users/getUser")]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var token = Request.Headers["Authorization"];
            string id = _tokenHelper.getUserFromToken(token);

            Users user = await _userRepository.CheckUserById(id);
            return Ok(new { status = user.Verified});
        }

        // Generate a random number between two numbers
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
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

                Models.Address address = new Models.Address();


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
                await _userRepository.CreateUser(userData, finalString, "owner");

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
            user.Stage = "second";

            Models.Address address = new Models.Address();

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



        //encode
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            base64EncodedData = base64EncodedData.Replace('-', '+').Replace('_', '/').PadRight(4 * ((base64EncodedData.Length + 3) / 4), '=');
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static byte[] DecodeUrlBase64(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/').PadRight(4 * ((s.Length + 3) / 4), '=');
            return Convert.FromBase64String(s);
        }

        public string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
    }
  

}