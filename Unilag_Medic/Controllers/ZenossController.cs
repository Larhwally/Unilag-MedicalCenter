using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Unilag_Medic.Data;

namespace Unilag_Medic.Controllers
{
    public class ZenossController : Controller
    {
        [Route("ZenossStaffs")]
        [HttpGet("staffId")]
        public IActionResult GetZenossStaffById(string staffId)
        {
            ZenossConnection connection = new ZenossConnection("tbl_zenoss_staffs");
            Dictionary<string, string> rec = new Dictionary<string, string>();
            rec.Add("staffId", staffId);

            Dictionary<string, object> record = connection.SelectByColumn(rec);
            if (record.Count > 0)
            {
                return Ok(record);
            }
            else
            {
                return Ok(new string[0]);
            }
        }


        [Route("CountryStates")]
        [HttpGet("{countryName, token, nationalityId}")]
        public async Task<IActionResult> GetStatesByCountry(string countryName, string token, int nationalityId)
        {
            var states = await _zenossOps.GetStateByCountry(countryName, token, nationalityId);
            return Ok(states);
        }


    }
}