using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Unilag_Medic.Data;
using Unilag_Medic.ViewModel;

namespace Unilag_Medic.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            
        }

        [AllowAnonymous]
        [Route("regAdmin")]
        [HttpPost]
        public async Task<ActionResult> RegisterAdmin([FromBody] RegisterViewModel model)
        {
            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNum,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            
            var result = await _userManager.CreateAsync(user, model.password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            return Ok(new { Username = user.UserName });
        }

        
        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterViewModel model)
        {
            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNum,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            

            var result = await _userManager.CreateAsync(user, model.password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            return Ok(new { Username = user.UserName });
        }
        //Add another register end point with 'admin' role

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            var userrole = await _userManager.GetRolesAsync(user);
            //bool checkrole = await _userRole.IsInRoleAsync();

            //added if role == true

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var claim = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
                };
                var signingKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));

                int expiryInMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Site"],
                    audience: _configuration["Jwt:Site"],
                    expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        
                    });
                
            }
            return Unauthorized();
        }

        
        [Route("GetUser")]
        [HttpGet]
        public string GetUsers()
        {
            EntityConnection con = new EntityConnection("AspNetUsers");
            string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }


        
        [Route("UpdateUser")]
        [HttpPut("{id}")]
        public string Put(int id, Dictionary<string, string> content)
        {
            EntityConnection con = new EntityConnection("AspNetUsers");
            if (id != 0)
            {
                con.Update(id, content);
                Response.WriteAsync("Record updated successfully!");
            }
            else
            {
                return BadRequest("Error in updating record!") + "";
            }
            return content + "";
        }

        [Route("DeleteUser")]
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            EntityConnection con = new EntityConnection("AspNetUsers");
            if (id != 0)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("itbId", id + "");
                con.Delete(param);
            }
            else
            {
                return NotFound().ToString();
            }
            return "Record deleted successfully";
        }



    }
}