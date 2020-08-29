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
    public class VitalSignsController : ControllerBase
    {
        public object obj = new object();
        // GET: api/VitalSigns
        [HttpGet]
        public IActionResult GetVitalSign()
        {
            EntityConnection con = new EntityConnection("tbl_vitalsigns");
            //string result = "{'Status': true, 'Data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.SelectAllVitalsigns();
            return Ok(result);
        }

        // GET: api/VitalSigns/5
        [HttpGet("{id}")]
        public IActionResult GetVitalSign(int id)
        {
            EntityConnection con = new EntityConnection("tbl_vitalsigns");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("itbId", id + "");
            //string record = "{'status':true,'data':" + EntityConnection.ToJson(con.SelectByColumn(dic)) + "}";
            Dictionary<string, object> record = con.SelectByColumn(dic);

            if (con.SelectByColumn(dic).Count > 0)
            {
                obj = new { vitals = record };
                return Ok(obj);
            }
            else
            {
                return NotFound();
            }

        }

        // POST: api/VitalSigns
        [HttpPost]
        public IActionResult Post([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_vitalsigns");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);

                string visitId = "", assignedTo = "";

                param.TryGetValue("visitId", out visitId);
                param.TryGetValue("assignedTo", out assignedTo);

                con.UpdateVisit(Convert.ToInt32(visitId), Convert.ToInt32(assignedTo));

                return Created("", param);
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                obj = new { message = "Error in adding vital signs record" };
                return BadRequest(obj);
            }

        }

        // PUT: api/VitalSigns/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Dictionary<string, string> content)
        {
            EntityConnection con = new EntityConnection("tbl_vitalsigns");
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

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            EntityConnection con = new EntityConnection("tbl_vitalsigns");
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
