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
	public object obj = new object();
        // GET: api/Patient
        [HttpGet]
        public IActionResult GetPatient()
        {
            EntityConnection con = new EntityConnection("tbl_patient");
            List<Dictionary<string, object>> result = con.Select();
            if (result.Count > 0)
            {
                return Ok(result);    
            }
            else
            {
                return ok(new string[0]);
            }
            
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
                
                return Created("", param);
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                //return resp + "";
                return BadRequest();
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
	    obj = new {message = "Record was deleted successfully"};
            return Ok(obj);
        }


    }
}
