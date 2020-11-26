using Microsoft.Extensions.Options;
using Unilag_Medic.Helpers;
using Unilag_Medic.ViewModel;
using Unilag_Medic.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Unilag_Medic.Models;
using System.Collections.Generic;

namespace Unilag_Medic.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(UnilagMedLogin model);
        Dictionary<string, object> SelectStaffByEmail(string email);

    }
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(UnilagMedLogin model)
        {
            var token = "";
            EntityConnection connection = new EntityConnection("tbl_medicalstaff");
            if (connection.CheckUser(model.email, model.password) == true && model != null)
            {
                token = GenerateJwtToken(model);
            }
            return new AuthenticateResponse(token, model);
        }

        public Dictionary<string, object> SelectStaffByEmail(string email)
        {
            EntityConnection connection = new EntityConnection("tbl_medicalstaff");

            Dictionary<string, object> result = new Dictionary<string, object>();
            result = connection.SelectStaffByEmail(email);

            return result;
        }

        private string GenerateJwtToken(UnilagMedLogin model)
        {
            var tokenHander = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("email", model.email) }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHander.CreateToken(tokenDescriptor);
            var tokenval = tokenHander.WriteToken(token);

            return tokenval;
        }
    }
}