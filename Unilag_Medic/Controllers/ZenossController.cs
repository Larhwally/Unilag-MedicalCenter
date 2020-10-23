using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Unilag_Medic.Data;

namespace Unilag_Medic.Controllers
{
    public class ZenossController : Controller
    {
        // An endpoint to get all staff record fetched and saved from the zenossAPI
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
    }
    
}