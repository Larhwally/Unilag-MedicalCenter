using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public string GetDiagnosis()
        {
            EntityConnection con = new EntityConnection("tbl_diagnosis");
            string result = "{'Status': true, 'Data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }

        // GET: api/Diagnosis/5
        [HttpGet("{id}")]
        public string GetById(int id)
        {
            EntityConnection con = new EntityConnection("tbl_diagnosis");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("itbId", id + "");
            string record = "{'status':true,'data':" + EntityConnection.ToJson(con.SelectByColumn(dic)) + "}";
            return record;
        }

        // POST: api/Diagnosis
        [HttpPost]
        public string PostDiagnosis([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_diagnosis");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                Response.WriteAsync("Record saves successfully!");
            }
            else
            {
                var resp = Response.WriteAsync("Error in creating record");
                return resp + "";
            }
            return param + "";
        }

        // PUT: api/Diagnosis/5
        [HttpPut("{id}")]
        public string PutDiagnosis(int id, Dictionary<string, string> content)
        {
            EntityConnection con = new EntityConnection("tbl_diagnosis");
            if (id != 0)
            {
                con.Update(id, content);
                Response.WriteAsync("Record updated successfully!");
            }
            else
            {
                return BadRequest("Error in updating record!") + "";
            }
            return content + "";
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public string Delete(int id)
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
                return NotFound().ToString();
            }
            return "Record deleted successfully";
        }


    }
}
