using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unilag_Medic.Data;
using Unilag_Medic.Services;

namespace Unilag_Medic.Controllers
{
    public class ZenosOpController : Controller
    {
        private readonly IZenossOps _zenossOps;
        public ZenosOpController(IZenossOps zenossOps)
        {
            _zenossOps = zenossOps;
        }

        //ENdpoint for generating a session token from dream factory API
        [Route("ZenossLogin")]
        [HttpGet]
        public async Task<IActionResult> GetTokenSession()
        {
            var sessionToken = await _zenossOps.GetSessionTokenAsync();

            return Ok(sessionToken);
        }


        // Endpoint for fetching student detail by matric number from dream factory API
        [Route("ZenossStudent")]
        [HttpGet("{matricNum, session_token}")]
        public async Task<IActionResult> GetStudentById(string matricNum, string session_token)
        {
            var student = await _zenossOps.GetStudentDetailAsync(matricNum, session_token);
            return Ok(student);
        }


        [Route("ZenossStaff")]
        [HttpGet("{staffId, session_token}")]
        public async Task<IActionResult> GetStaffById(string staffId, string session_token)
        {
            var staff = await _zenossOps.GetStaffDetailAsync(staffId, session_token);
            return Ok(staff);
        }

        [Route("ZenStaffs")]
        [HttpGet("{session_token}")]
        [System.Obsolete]
        public async Task<IActionResult> GetAllStaffs(string session_token)
        {
            var staffs = await _zenossOps.GetAllStaffAsync(session_token);
            return Ok(staffs);
        }








    }
}