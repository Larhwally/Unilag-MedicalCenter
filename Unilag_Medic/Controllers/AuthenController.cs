using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Unilag_Medic.Data;
using Unilag_Medic.Models;
using Unilag_Medic.ViewModel;

namespace Unilag_Medic.Controllers
{
    public class AuthenController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("RegisterUser")]
       [HttpPost]
       public string RegUser([FromBody] UnilagMedLogin model)
        {
            //DbAccessLayer db = new DbAccessLayer();
            EntityConnection con = new EntityConnection("tbl_userlogin");
            var user = new UnilagMedLogin
            {
                email = model.email,
                password = model.password,
                createBy = model.createBy,
                createDate  = model.createDate
            };

            string password = model.password;
            byte[] salt = { 2, 3, 1, 2, 3, 6, 7, 4, 2, 3, 1, 7, 8, 9, 6 };
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 256 / 8
                ));

            if (user != null)
            {
                user.password = hashed;
                con.AddUser(user);
                //string result = con.AddUser(user);
            }
            
            return model.email;
        }

        [Route("LoginUser")]
        [HttpPost]
        public string Loginuser([FromBody] UnilagMedLogin model)
        {
            EntityConnection connection = new EntityConnection("tbl_userlogin");
            string email = model.email;
            string pass = model.password;

           
                if (connection.CheckUser(email, pass) == true && model != null)
                {
                var claim = new[]
               {
                        new Claim(JwtRegisteredClaimNames.Sub, model.email)
                    };
                var signingkey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));

                int Expireminutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Site"],
                    audience: _configuration["Jwt:Site"],
                    expires: DateTime.UtcNow.AddMinutes(Expireminutes),
                    signingCredentials: new SigningCredentials(signingkey, SecurityAlgorithms.HmacSha256));

                var result = new JwtSecurityTokenHandler().WriteToken(token);
                    
                return result;

            }

            return Unauthorized().ToString();
        }




    }
}