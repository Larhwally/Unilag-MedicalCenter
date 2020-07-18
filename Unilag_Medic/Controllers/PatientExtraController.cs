using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Unilag_Medic.Data;
using Unilag_Medic.Services;

namespace Unilag_Medic.Controllers
{
    [Authorize]
    public class PatientExtraController : Controller
    {
        [Route("GetStaffPatient")]
        [HttpGet]
        public IActionResult Getstaff()
        {
            EntityConnection con = new EntityConnection("tbl_staff_patient");
            //string result = "{'status': true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.Select();
            return Ok(result);
        }

        [Route("GetStudentPatient")]
        [HttpGet]
        public IActionResult GetStudent()
        {
            EntityConnection con = new EntityConnection("tbl_student_patient");
            //string result = "{'status': true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.Select();
            return Ok(result);
        }

        [Route("GetDependentPatient")]
        [HttpGet]
        public IActionResult GetDependent()
        {
            EntityConnection con = new EntityConnection("tbl_dependent");
            //string result = "{'status': true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.Select();
            return Ok(result);
        }

        //End of GET method

        [Route("GetStudentByMatric")]
        [HttpGet("{matricNumber}")]
        public IActionResult GetStudentByMatric(string matricnum)
        {
            EntityConnection con = new EntityConnection("tbl_student_patient");
            Dictionary<string, object> pairs = new Dictionary<string, object>
            {
                {"matricNumber", matricnum }
            };
            //string result = EntityConnection.ToJson(con.StudentPatient(matricnum));
            List<Dictionary<string, object>> result = con.StudentPatient(matricnum);

            if (con.StudentPatient(matricnum).Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }


        [Route("GetStaffByStaffcode")]
        [HttpGet("{staffCode}")]
        public IActionResult GetStaffByStaffcode(string staffcode)
        {
            EntityConnection con = new EntityConnection("tbl_staff_patient");
            Dictionary<string, object> pairs = new Dictionary<string, object>
            {
                {"staffCode", staffcode }
            };
            //string result = EntityConnection.ToJson(con.StudentPatient(matricnum));
            List<Dictionary<string, object>> result = con.StaffPatient(staffcode);

            if (con.StaffPatient(staffcode).Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }





        //Begin POST method
        [Route("PostStaffPatient")]
        [HttpPost]
        public IActionResult PostStaff([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_staff_patient");
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
                //var resp = Response.WriteAsync("Failed to save test");
                return BadRequest("Failed to save record");
            }
        }

        [Route("PostStudentPatient")]
        [HttpPost]
        public IActionResult PostStudent([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_student_patient");
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
                //var resp = Response.WriteAsync("Failed to save test");
                return BadRequest("Error in creating record");
            }

        }

        [Route("PostDependent")]
        [HttpPost]
        public IActionResult PostDependent([FromBody] Dictionary<string, string> values)
        {
            EntityConnection con = new EntityConnection("tbl_dependent");
            if (values != null)
            {
                values.Add("createDate", DateTime.Now.ToString());
                con.Insert(values);
                Response.WriteAsync("Record successfully saved!");
            }
            else
            {
                //var resp = Response.WriteAsync("Failed to save test");
                return BadRequest("Failed to save record");
            }
            return Ok(values);
        }



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
            List<Dictionary<string, object>> record = con.SelectByColumn(dic);

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


    }
}