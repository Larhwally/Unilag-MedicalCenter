using Unilag_Medic.ViewModel;
using System;


namespace Unilag_Medic.Models
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse(string Token, UnilagMedLogin medLogin)
        {
            email = medLogin.email;
            token = Token;
        }
        public string email { get; set; }
        public string token { get; set; }


    }
}