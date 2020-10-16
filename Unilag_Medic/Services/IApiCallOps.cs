using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unilag_Medic.Models;

namespace Unilag_Medic.Services
{
    public interface IApiCallOps
    {
        Task<List<string>> GetStateByCountry(string countryName, string token, int nationalityId);
    }
}