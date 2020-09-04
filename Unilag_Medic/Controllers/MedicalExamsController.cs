using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unilag_Medic.Data;

namespace Unilag_Medic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalExamsController : ControllerBase
    {
        public object obj = new object();
        // GET: api/MedicalExams
        [HttpGet]
        public IActionResult Get()
        {
            EntityConnection con = new EntityConnection("tbl_medical_exam");
            List<Dictionary<string, object>> result = con.Select();
            if (result.Count != 0)
            {
                return Ok(result);
            }
            else
            {
                obj = new { message = "No records to in the database!" };
                return BadRequest(obj);
            }

        }

        // GET: api/MedicalExams/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
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

        // POST: api/MedicalExams
        [HttpPost]
        public IActionResult Post([FromBody] Dictionary<string, object> param)
        {
            EntityConnection con = new EntityConnection("tbl_medical_exam");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("", param);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/MedicalExams/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
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
            obj = new { message = "Record deleted successfully!" };
            return Ok(obj);
        }
    }
}
