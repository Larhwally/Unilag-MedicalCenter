using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Unilag_Medic.Data;
using Unilag_Medic.ViewModel;

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

                // string patientId = "";
                // string patientType = "";
                // string createdBy = "";
                /*check the value of patient type from the patient record created to post student/staff pateint table
                Also get the patientId created from the new patient record created and some other record needed for 
                staff patient table
                */

                // param.TryGetValue("itbId", out patientId);
                // param.TryGetValue("createdBy", out createdBy);
                // param.TryGetValue("patientType", out patientType);
                // if (Convert.ToInt32(patientType) == 1)
                // {
                //     EntityConnection con2 = new EntityConnection("tbl_staff_patient");

                //     StaffPatient model = new StaffPatient();
                //     model.patientId = Convert.ToInt32(patientId);
                //     model.createdBy = createdBy;
                //     model.createDate = DateTime.Now;

                //     con2.InsertStaffPatient(model);
                // }
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
                return Ok(content);
                //Response.WriteAsync("Record updated successfully!");
            }
            else
            {
                return BadRequest();
            }

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
