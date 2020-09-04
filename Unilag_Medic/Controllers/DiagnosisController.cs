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
    public class DiagnosisController : ControllerBase
    {
        // GET: api/Diagnosis
        [HttpGet]
        public IActionResult GetDiagnosis()
        {
            EntityConnection con = new EntityConnection("tbl_diagnosis");
            //string result = "{'Status': true, 'Data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.Select();
            return Ok(result);
        }

        // GET: api/Diagnosis/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            EntityConnection con = new EntityConnection("tbl_diagnosis");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("itbId", id + "");
            //string record = "{'status':true,'data':" + EntityConnection.ToJson(con.SelectByColumn(dic)) + "}";
            Dictionary<string, object> record = con.SelectByColumn(dic);

            if (con.SelectByColumn(dic).Count > 0)
            {
                return Ok(record);
            }
            else
            {
                return NotFound();
            }

        }

        // POST: api/Diagnosis
        [HttpPost]
        public IActionResult PostDiagnosis([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_diagnosis");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.InsertRecord(param);
                string visitId = "";
                param.TryGetValue("visitId", out visitId);
                con.UpdateDiagnosis(Convert.ToInt32(visitId));
                return Created("", param);
            }
            else
            {
                return BadRequest("Error in creating record");
                //var resp = Response.WriteAsync("Error in creating record");
                //return resp + "";
            }

        }

        // PUT: api/Diagnosis/5
        [HttpPut("{id}")]
        public IActionResult PutDiagnosis(int id, Dictionary<string, string> content)
        {
            EntityConnection con = new EntityConnection("tbl_diagnosis");
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
            EntityConnection con = new EntityConnection("tbl_diagnosis");
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
