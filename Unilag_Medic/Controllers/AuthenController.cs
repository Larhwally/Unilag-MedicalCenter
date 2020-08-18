using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Unilag_Medic.Data;
using Unilag_Medic.Models;
using Unilag_Medic.ViewModel;

namespace Unilag_Medic.Controllers
{
    [Authorize]
    public class AuthenController : Controller
    {
        public object obj = new object();
        private readonly IConfiguration _configuration;

        public AuthenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       [Route("RegisterUser")]
       [HttpPost]
       public IActionResult RegUser([FromBody] UnilagMedLogin model)
        {
            //DbAccessLayer db = new DbAccessLayer();
            EntityConnection con = new EntityConnection("tbl_userlogin");
            var user = new UnilagMedLogin
            {
                email = model.email,
                password = model.password,
                roleId = model.roleId,
                medstaffId = model.medstaffId,
                createBy = model.createBy,
                createDate  = DateTime.Now
            };

            string password = model.password;
            byte[] salt = { 2, 3, 1, 2, 3, 6, 7, 4, 2, 3, 1, 7, 8, 9, 6 };
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 1000,
                numBytesRequested: 50
                ));

            if (user != null)
            {
                user.password = hashed;
                con.AddUser(user);
                //string result = con.AddUser(user);
            }
            else
            {
                return BadRequest("check user details and try again");
            }
            var res = new { user.email, user.createBy, user.createDate };
            //var result = res;
            return Created("", res);
        }


        [AllowAnonymous]
        [Route("LoginUser")]
        [HttpPost]
        public IActionResult Loginuser([FromBody] UnilagMedLogin model)
        {
            EntityConnection connection = new EntityConnection("tbl_userlogin");
            string email = model.email;
            string pass = model.password;
            DateTime logindate = DateTime.Now;



                if (connection.CheckUser(email, pass) == true && model != null)
                {
                var claim = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, model.roleId.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, model.email),//gotta add role as a sub for claim
                       
                    };
                var signingkey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));

                int Expireminutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Site"],
                    audience: _configuration["Jwt:Site"],
                    expires: DateTime.Today.AddDays(2),
                    signingCredentials: new SigningCredentials(signingkey, SecurityAlgorithms.HmacSha256));

                var tokenval = new JwtSecurityTokenHandler().WriteToken(token);

                //var output = JsonConvert.SerializeObject(res);
                //var result = JsonConvert.SerializeObject(tokenval);


                var role = connection.DisplayRoles(email);
                var tempresult = new { logindate, tokenval };
               
                return Ok(new {role, tempresult });

            }

            obj = new {message = "Please check login details and try again!"};
            return Unauthorized(obj);
        }


        

    }
}
