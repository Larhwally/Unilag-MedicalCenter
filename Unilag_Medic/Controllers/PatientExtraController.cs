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
    public class PatientExtraController : Controller
    {

        public object obj = new object();

        [Route("Staffs")]
        [HttpGet]
        public IActionResult Getstaff()
        {
            EntityConnection con = new EntityConnection("tbl_staff_patient");
            //string result = "{'status': true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.Select();
            return Ok(result);
        }

        [Route("SearchStaffs")]
        [HttpGet("{staffCode}")]
        public IActionResult GetStaffByStaffcode(string staffcode)
        {
            EntityConnection con = new EntityConnection("tbl_staff_patient");
            Dictionary<string, object> pairs = new Dictionary<string, object>
            {
                {"staffCode", staffcode }
            };
            //string result = EntityConnection.ToJson(con.StudentPatient(matricnum));
            Dictionary<string, object> result = con.StaffPatient(staffcode);

            if (con.StaffPatient(staffcode).Count > 0)
            {
                return Ok(result);
            }
            else
            {
                obj = new { message = staffcode + " does not exist!" };
                return NotFound(obj);
            }

        }



        [Route("Staffs")]
        [HttpPost]
        public IActionResult PostStaff([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_staff_patient");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);

                return Created("", param);
            }
            else
            {
                //var resp = Response.WriteAsync("Failed to save test");
                obj = new { message = "Failed to save record" };
                return BadRequest(obj);
            }
        }

        [Route("Students")]
        [HttpGet]
        public IActionResult GetStudent()
        {
            EntityConnection con = new EntityConnection("tbl_student_patient");
            //string result = "{'status': true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.Select();
            return Ok(result);
        }

        [Route("Students")]
        [HttpGet("{matricNumber}")]
        public IActionResult GetStudentByMatric(string matricnum)
        {
            EntityConnection con = new EntityConnection("tbl_student_patient");
            Dictionary<string, object> pairs = new Dictionary<string, object>
            {
                {"matricNumber", matricnum }
            };
            //string result = EntityConnection.ToJson(con.StudentPatient(matricnum));
            Dictionary<string, object> result = con.StudentPatient(matricnum);

            if (con.StudentPatient(matricnum).Count > 0)
            {
                return Ok(result);
            }
            else
            {
                obj = new { message = matricnum + " does not exist!" };
                return NotFound(obj);
            }

        }


        [Route("Students")]
        [HttpPost]
        public IActionResult PostStudent([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_student_patient");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);

                return Created("", param);
            }
            else
            {
                //var resp = Response.WriteAsync("Failed to save test");
                return BadRequest("Error in creating record");
            }

        }

        [Route("Dependents")]
        [HttpGet]
        public IActionResult GetDependent()
        {
            EntityConnection con = new EntityConnection("tbl_dependent");
            //string result = "{'status': true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
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

        [Route("Dependents")]
        [HttpPost]
        public IActionResult PostDependent([FromBody] Dictionary<string, string> values)
        {
            EntityConnection con = new EntityConnection("tbl_dependent");
            if (values != null)
            {
                values.Add("createDate", DateTime.Now.ToString());
                con.Insert(values);
                return Ok(values);
            }
            else
            {
                //var resp = Response.WriteAsync("Failed to save test");
                return BadRequest("Failed to save record");
            }

        }

        //End of GET method


        //Begin POST method

        //End of POST method

        //Begin Select by ID
        [Route("SearchDependent")]
        [HttpGet("{id}")]
        public IActionResult GetDepById(int id)
        {
            EntityConnection con = new EntityConnection("tbl_dependent");
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

        [Route("UpdateDependent")]
        [HttpPut("{id}")]
        public IActionResult UpdateDependent(int id, Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_dependent");
            if (id != 0)
            {
                con.Update(id, param);
                Response.WriteAsync("Record updated successfully!");
            }
            else
            {
                return BadRequest("Error in updating record!");
            }
            return Ok(param);
        }

        //Get a patient last visit record by using patient ID
        [Route("LastVisits")]
        [HttpGet("{id}")]
        public IActionResult GetLastVisit(int patientId)
        {
            EntityConnection con = new EntityConnection("tbl_visit");
            Dictionary<string, string> result = new Dictionary<string, string>()
            {
                {"patientId", patientId + ""}
            };
            if (con.LastVisit(patientId).Count > 0)
            {
                return Ok(con.LastVisit(patientId));
            }
            else
            {
                obj = new { message = patientId + " does not have a previous appointment record yet" };
                return NotFound(obj);
            }

        }


    }
}