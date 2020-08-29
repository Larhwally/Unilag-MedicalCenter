using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Unilag_Medic.Data;

namespace Unilag_Medic.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MedStaffController : ControllerBase
    {
        // GET: api/MedStaff
        [HttpGet]
        public IActionResult GetMedStaff()
        {
            EntityConnection con = new EntityConnection("tbl_medicalstaff");
            //string result = "{'Status': true, 'Data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.Select();
            return Ok(result);
        }

        // GET: api/MedStaff/5
        [HttpGet("{id}")]
        public IActionResult GetMedStaffbyId(int id)
        {
            EntityConnection con = new EntityConnection("tbl_medicalstaff");
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("itbId", id + "");
            //string record = "{'status':true,'data':" + EntityConnection.ToJson(con.SelectByColumn(dict)) + "}";
            Dictionary<string, object> result = con.SelectByColumn(dict);

            if (con.SelectByColumn(dict).Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        // POST: api/MedStaff
        [HttpPost]
        public IActionResult Post([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_medicalstaff");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                List<string> keylst = new List<string>();
                List<string> vallst = new List<string>();
                List<string> valkeys = new List<string>();
                foreach (var key in param.Keys)
                {
                    keylst.Add(key);
                }
                string[] vals = param.Values.ToArray();
                for (int i = 0; i < vals.Length; i++)
                {
                    vallst.Add(vals[i]);
                }

                foreach (var key in param.Keys)
                {
                    valkeys.Add(key + ": " + param[key]);
                }
                //var output = JsonConvert.SerializeObject(valkeys);
                return Ok(valkeys);
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                return BadRequest("Error in creating record");
            }
        }

        // PUT: api/MedStaff/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Dictionary<string, string> content)
        {
            EntityConnection con = new EntityConnection("tbl_medicalstaff");
            if (id != 0)
            {
                con.Update(id, content);
                Response.WriteAsync("Record updated successfully!");
            }
            else
            {
                return BadRequest("Error in updating record!");
            }
            return Ok(content);
        }

        // DELETE: api/MedStaff/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            EntityConnection con = new EntityConnection("tbl_medicalstaff");
            if (id != 0)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("itbId", id + "");
                con.Delete(param);
            }
            else
            {
                return NotFound();

            }
            return Ok("Record deleted successfully");
        }


    }
}
