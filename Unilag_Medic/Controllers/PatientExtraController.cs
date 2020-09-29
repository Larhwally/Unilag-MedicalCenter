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
        public Object obj = new Object();
        public Object newobj = new Object();

        [Route("Staffs")]
        [HttpGet]
        public IActionResult Getstaff()
        {
            EntityConnection con = new EntityConnection("tbl_staff_patient");
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
        public IActionResult PostStaff([FromBody] Dictionary<string, object> param)
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

        [Route("SearchStudents")]
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
                obj = new { message = matricnum + " record not found" };
                return NotFound(obj);
            }

        }


        [Route("Students")]
        [HttpPost]
        public IActionResult PostStudent([FromBody] Dictionary<string, object> param)
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
                obj = new { message = "Failed to create record check details and try again!" };
                return BadRequest(obj);
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
        public IActionResult PostDependent([FromBody] Dictionary<string, object> values)
        {
            EntityConnection con = new EntityConnection("tbl_dependent");
            if (values != null)
            {
                values.Add("createDate", DateTime.Now.ToString());
                con.Insert(values);
                return Created("", values);
                //Response.WriteAsync("Record successfully saved!");
            }
            else
            {
                //var resp = Response.WriteAsync("Failed to save test");
                obj = new { message = "Failed to save record, check details and try again!" };
                return BadRequest(obj);
            }
            //return Created("", values);
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

        // [Route("LastVisits", Name = "LastAppoint")]
        // [HttpGet("{id:int}")]
        // public IActionResult GetLastVisit(int patientId)
        // {
        //     EntityConnection con = new EntityConnection("tbl_visit");
        //     Dictionary<string, string> result = new Dictionary<string, string>()
        //     {
        //         {"patientId", patientId + ""}
        //     };
        //     if (con.LastVisit(patientId).Count > 0)
        //     {
        //         return Ok(con.LastVisit(patientId));
        //     }
        //     else
        //     {
        //         obj = new { message = patientId + " does not have a previous appointment record yet" };
        //         return NotFound(obj);
        //     }

        // }

        //Get patient diagnosis record by visitID
        [Route("VisitDiagnosis")]
        [HttpGet("{visitId}")]
        public IActionResult GetDiagnosisByVisit(int visitId)
        {
            EntityConnection connection = new EntityConnection("tbl_diagnosis");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                {"visitId", visitId.ToString()}
            };
            if (connection.SelectDiagnosisByVisit(visitId).Count > 0)
            {
                obj = new { data = connection.SelectDiagnosisByVisit(visitId) };
                return Ok(obj);
            }
            else
            {
                pairs = null;
                obj = new { data = pairs };
                return Ok(obj);
            }
        }


        [Route("Patients")]
        [HttpGet("{hospitalNumber}")]
        public IActionResult GetPatByHospnum(string hospitalNumber)
        {
            EntityConnection con = new EntityConnection("tbl_patient");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                { "hospitalNumber", hospitalNumber}
            };

            //string rec = EntityConnection.ToJson(con.SelectByParam(pairs));
            if (con.SelectByParam(pairs).Count > 0)
            {
                return Ok(con.SelectByParam(pairs));
            }
            else
            {
                obj = new { message = hospitalNumber + "does not exist" };
                return NotFound(obj);
            }

        }

        [Route("Visits")]
        [HttpGet("{hospitalNumber}")]
        public IActionResult GetVisitByHospnum(string hospitalNumber)
        {
            EntityConnection con = new EntityConnection("tbl_visit");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                {"hospitalNumber", hospitalNumber }
            };

            if (con.DisplayVisitValues(hospitalNumber).Count > 0)
            {
                return Ok(con.DisplayVisitValues(hospitalNumber));
            }
            else
            {
                obj = new { message = hospitalNumber + "does not have a visit record" };
                return NotFound(obj);
            }
            //string res = EntityConnection.ToJson(con.DisplayVisitValues(hospnum));

        }


        [Route("VisitVitals")]
        [HttpGet("{visitId}")]
        public IActionResult GetVitByVisit(string visitId)
        {
            EntityConnection con = new EntityConnection("tbl_vitalsigns");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                { "visitId", visitId}
            };

            if (con.SelectByParam(pairs).Count > 0)
            {
                return Ok(con.SelectByParam(pairs));
            }
            else
            {
                obj = new { message = visitId + "does not exist" };
                return NotFound(obj);
            }
            //string rec = EntityConnection.ToJson(con.SelectByParam(pairs));

        }


        [Route("VitalSigns")]
        [HttpGet("{hospitalNumber}")]
        public IActionResult GetVitByHospnum(string hospitalNumber)
        {
            EntityConnection con = new EntityConnection("tbl_vitalsigns");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                { "hospitalNumber", hospitalNumber}
            };

            if (con.DisplayVitalValues(hospitalNumber).Count > 0)
            {
                return Ok(con.DisplayVitalValues(hospitalNumber));
            }
            else
            {
                obj = new { message = hospitalNumber + "does not have a vital sign record yet" };
                return NotFound(obj);
            }
            //string rec = EntityConnection.ToJson(con.DisplayVitalValues(hospnum));

        }


        [Route("Diagnosis")]
        [HttpGet("{hospitalNumber}")]
        public IActionResult GetDiagByHospnum(string hospitalNumber)
        {
            EntityConnection con = new EntityConnection("tbl_diagnosis");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                {"hospitalNumber", hospitalNumber }
            };

            if (con.DisplayDiagnosis(hospitalNumber).Count > 0)
            {
                return Ok(con.DisplayDiagnosis(hospitalNumber));
            }
            else
            {
                //obj = new { message = hospitalNumber + "does not have a diagnosis record yet" };
                string[] arr = new string[0];
                return Ok(arr);
            }
            //string res = EntityConnection.ToJson(con.DisplayDiagnosis(hospnum));

        }



        // Get the prescription record by visitId
        [Route("VisitPrescription")]
        [HttpGet("visitId")]
        public IActionResult GetPrescriptionbyVisit(int visitId)
        {
            EntityConnection con = new EntityConnection("tbl_diagnosis");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                {"visitId", visitId + ""}
            };

            Dictionary<string, object> result = con.GetPrescriptionVisit(visitId);


            //Check if the parent method returns a record
            if (result.Count > 0)
            {
                result.TryGetValue("otherDrugs", out newobj);
                result.TryGetValue("drugs", out obj);

                string otherDrugs = newobj.ToString();
                string newdrug = obj.ToString();

                result.Remove("drugs");
                result.Remove("otherDrugs");

                //check if the drug list is empty
                if (newdrug.Trim() != "")
                {

                    string[] drugs = newdrug.Split(',');

                    string[] orQueries = drugs.Select(drugId => "itbId =" + drugId).ToArray(); // [itbId=3, itbId=5]

                    if (orQueries != null)
                    {
                        var drugDetails = con.DrugDetails(orQueries);
                        result.Add("drugs", drugDetails);
                    }
                }
                else
                {
                    result.Add("drugs", new string[0]);
                }

                //check if other drugs is empty 
                if (otherDrugs.Trim() != "")
                {
                    string[] newOtherDrugs = otherDrugs.Split('|');
                    result.Add("otherDrugs", newOtherDrugs);
                }
                else
                {
                    result.Add("otherDrugs", new string[0]);
                }

                obj = new { data = result };
                return Ok(obj);
            }
            else
            {
                string[] arr = new string[0];
                return Ok(arr);
            }





        }

        //End of get Patients






    }
}
