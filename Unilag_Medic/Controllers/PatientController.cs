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
    public class PatientController : ControllerBase
    {
        
        // GET: api/Patient
        [HttpGet]
        public IActionResult GetPatient()
        {
            EntityConnection con = new EntityConnection("tbl_patient");
            List<Dictionary<string, object>> result = con.Select();

            return Ok(result);
        }

        // GET: api/Patient/5
        [HttpGet("{id}")]
        public IActionResult GetPatientbyId(int id)
        {
            EntityConnection con = new EntityConnection("tbl_patient");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                { "itbId", id + "" }
            };
            //string record = EntityConnection.ToJson(con.SelectByColumn(pairs));
            if (con.SelectByColumn(pairs).Count > 0)
            {
                return Ok(con.SelectByColumn(pairs));
            }
            else
            {
                return NotFound();
            }
            
        }


        // POST: api/Patient
        [HttpPost]
        public IActionResult Post([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_patient");
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
                return Created("", valkeys);
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                //return resp + "";
                return BadRequest("Error in creating record");
            }

          
        }

        // PUT: api/Patient/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Dictionary<string, string> content)
        {
            EntityConnection con = new EntityConnection("tbl_patient");
            if (id != 0)
            {
                con.Update(id, content);
                Response.WriteAsync("Record updated successfully!");
            }
            else
            {
                return BadRequest("Error in updating record!");
            }
            //return content + "";
            return Ok(content);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            EntityConnection con = new EntityConnection("tbl_patient");
            if (id != 0)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("itbId", id + "");
                con.Delete(param).ToString();
                
            }
            else
            {
                return NotFound();
            }

            return Ok("Record was deleted successfully");
        }


    }
}
