using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unilag_Medic.Models;

namespace Unilag_Medic.Services
{
    public interface IZenossOps
    {
        Task<ZenosLoginModel> GetSessionTokenAsync();

        Task<StudentModel> GetStudentDetailAsync(string matricNum, string session_token);

        Task<StaffModel> GetStaffDetailAsync(string staffId, string session_token);

        [Obsolete]
        Task<List<StaffModel>> GetAllStaffAsync(string session_token);
    }
}