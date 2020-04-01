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
    public class PatientController : ControllerBase
    {
        
        // GET: api/Patient
        [HttpGet]
        public string GetPatient()
        {
            EntityConnection con = new EntityConnection("tbl_patient");
            string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }

        // GET: api/Patient/5
        [HttpGet("{id}")]
        public string GetPatientbyId(int id)
        {
            EntityConnection con = new EntityConnection("tbl_patient");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                { "itbId", id + "" }
            };
            string record = "{'status':true,'data':" + EntityConnection.ToJson(con.SelectByColumn(pairs)) + "}";
            return record;
        }


        //[HttpGet("{hospitalNumber}")]
        //public string GetPatientbynosp(string hosp)
        //{
        //    EntityConnection con = new EntityConnection("tbl_patient");
        //    Dictionary<string, string> pairs = new Dictionary<string, string>
        //    {
        //        { "hospitalNumber", hosp + "" }
        //    };
        //    string record = "{'status':true,'data':" + EntityConnection.ToJson(con.SelectByColumn(pairs)) + "}";
        //    return record;
        //}



        // POST: api/Patient
        [HttpPost]
        public string Post([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_patient");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return param.Values.FirstOrDefault();
                //Response.WriteAsync("Record saves successfully!");
            }
            else
            {
                var resp = Response.WriteAsync("Error in creating record");
                return resp + "";
            }

          
        }

        // PUT: api/Patient/5
        [HttpPut("{id}")]
        public string Put(int id, Dictionary<string, string> content)
        {
            EntityConnection con = new EntityConnection("tbl_patient");
            if (id != 0)
            {
                con.Update(id, content);
                Response.WriteAsync("Record updated successfully!");
            }
            else
            {
                return BadRequest("Error in updating record!") +"";
            }
            return content + "";
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public string Delete(int id)
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
                return NotFound().ToString();
            }

            return "Record was deleted successfully";
        }


    }
}
