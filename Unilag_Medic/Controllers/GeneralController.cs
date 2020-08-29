using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unilag_Medic.Data;

namespace Unilag_Medic.Controllers
{
    [Authorize]
    //[Produces("application/json")]
    public class GeneralController : Controller
    {
        public object obj = new object();

        [Route("VitalUnits")]
        [HttpPost]
        public IActionResult PostUnit([FromBody] Dictionary<string, string> details)
        {
            EntityConnection con = new EntityConnection("tbl_vitalunits");
            if (details != null)
            {
                con.Insert(details);
                return Ok(details);
            }
            else
            {
                obj = new { message = "error in creating record" };
                return BadRequest(obj);
            }
        }
        
        [Route("VitalUnits")]
        [HttpGet]
        public IActionResult GetUnit()
        {
            EntityConnection con = new EntityConnection("tbl_vitalunits");
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }
        //Begin  GET requests
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
        public IActionResult GetVitByVisit(int visitId)
        {
            EntityConnection con = new EntityConnection("tbl_vitalsigns");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                { "visitId", visitId.ToString()}
            };
            var result = con.SelectByParam(pairs);
            if (result.Count > 0)
            {
                return Ok(result);
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
        //End of get Patients


        //POST and GET method for Visit

        [Route("Clinics")]
        [HttpPost]
        public IActionResult PostClinic([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_clinic");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                long visitID = con.InsertScalar(param);

                param.Add("itbId", visitID.ToString());
                return Created("", param);
                // con.Insert(param);
                // return Created("New record added successfully!", param);
                //Response.WriteAsync("Record saved successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record, check details and try again!");
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }

            //return Ok(param);
        }


        [Route("Clinics")]
        [HttpGet]
        public IActionResult GetClinic()
        {
            EntityConnection con = new EntityConnection("tbl_clinic");
            //string rec = EntityConnection.ToJson(con.Select());
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        //End of clinic POST and GET

        //POST and GET clinic schedule

        [Route("ClinicSchedules")]
        [HttpPost]
        public IActionResult PostClinicSchedule([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_clinicopenschedule");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("Record created successful", param);
                //Response.WriteAsync("Record saved successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record, check details and try again!");
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }

        }


        [Route("ClinicSchedules")]
        [HttpGet]
        public IActionResult GetClinicSchedule()
        {
            EntityConnection con = new EntityConnection("tbl_clinicopenschedule");
            //string rec = "{'Status': true, 'Data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        //End of clinic schedule

        //state GET and POST method

        [Route("States")]
        [HttpGet]
        public IActionResult GetState()
        {
            EntityConnection con = new EntityConnection("tbl_state");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        [Route("States")]
        [HttpPost]
        public IActionResult PostState([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_state");

            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToShortDateString());
                con.Insert(param);
                return Created("Record added successful" ,param);
            }
            else
            {
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }
        }
        
        //End of State POST and GET
    
        //Nationality GET and POST method

        [Route("Nationalities")]
        [HttpGet]
        public IActionResult GetNational()
        {
            EntityConnection con = new EntityConnection("tbl_nationality");
            //string result = "{'status': true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        [Route("Nationalities")]
        [HttpPost]
        public IActionResult PostNationality([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_nationality");

            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToShortDateString());
                con.Insert(param);
                return Created("Record added successful", param);
            }
            else
            {
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }
        }
        
        //End of Nationality

        //Begin Patient type GET and POST

        [Route("PatientTypes")]
        [HttpGet]
        public IActionResult GetPatienttype()
        {
            EntityConnection con = new EntityConnection("tbl_patienttype");
            //string result = "{'status': true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        [Route("PatientTypes")]
        [HttpPost]
        public IActionResult PostPatienttype([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_patienttype");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("Record created successful", param);
                //Response.WriteAsync("Record saves successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }
        }
        
        //End POST and GET method


        //Begin POST and GET Visittype

        [Route("VisitTypes")]
        [HttpGet]
        public IActionResult GetVisittype()
        {
            EntityConnection con = new EntityConnection("tbl_visittype");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        [Route("VisitTypes")]
        [HttpPost]
        public IActionResult PostVisittype([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_visittype");

            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("New record added successfully!", param);
                //Response.WriteAsync("Record saved successfully!");
            }
            else
            {
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }
            //return Ok(param);
        }
        
        //End Visittype GET and POST

        
        //Begin HMO Post and GET method

        [Route("Hmos")]
        [HttpGet]
        public IActionResult GetHmo()
        {
            EntityConnection con = new EntityConnection("tbl_hmo");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        [Route("Hmos")]
        [HttpPost]
        public IActionResult PostHMO([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_hmo");

            if (param != null)
            {
                //param.Add("createBy", model.email);
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("Record created successful", param);
                //Response.WriteAsync("Record saves successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }
        }
        
        //END of HMO GET and POst method

        
        //Begin Department POST and GET method

        [Route("Departments")]
        [HttpGet]
        public IActionResult GetDepartment()
        {
            EntityConnection con = new EntityConnection("tbl_department");
            //string result = "{'status':true, 'data':}" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        [Route("Departments")]
        [HttpPost]
        public IActionResult PostDepartment([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_department");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("Record created successful", param);
                //Response.WriteAsync("Record saves successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }
        }
        
        //End of Department POST and GET method 

        
        //Begin faculty POST and GET method
        [Route("Faculty")]
        [HttpGet]
        public IActionResult GetFaculty()
        {
            EntityConnection con = new EntityConnection("tbl_faculty");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        [Route("Faculty")]
        [HttpPost]
        public IActionResult PostFaculty([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_faculty");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("Record created successful", param);
                //Response.WriteAsync("Record saves successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }
            //return Ok(param);
        }
        //End of faculty POST and GET method


        //Begin Doctor POST and GET method

        [Route("Doctors")]
        [HttpGet]
        public IActionResult GetDoctor()
        {
            EntityConnection con = new EntityConnection("tbl_doctor");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.GetDoctors();
            return Ok(rec);
        }

        [Route("Doctors")]
        [HttpPost]
        public IActionResult PostDoctor([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_doctor");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                //Response.WriteAsync("Record saves successfully!");
                return Created("Record created successful", param);
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }

        }
        //END of Doctor POST and GET method


        //Begin specialization POST and GET method
        [Route("Specializations")]
        [HttpGet]
        public IActionResult GetDocSpec()
        {
            EntityConnection con = new EntityConnection("tbl_specialization");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        [Route("Specializations")]
        [HttpPost]
        public IActionResult PostSpecialization([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_specialization");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("Record created successful", param);
                //Response.WriteAsync("Record saved successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record, check details and try again!");
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }

            //return Ok(param);
        }

        //End of specialization GET and POST method

        [Route("Stafftypes")]
        [HttpPost]
        public IActionResult Poststafftype([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_stafftype");

            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("", param);
                //Response.WriteAsync("Record saved successfully!");
            }
            else
            {
                obj = new { message = "Error in creating record"};
                return BadRequest(obj);
            }
            //return Ok(param);
        }

        [Route("Stafftypes")]
        [HttpGet]
        public IActionResult GetStaffType()
        {
            EntityConnection con = new EntityConnection("tbl_stafftype");
            List<Dictionary<string, object>> rec = con.Select();
            if (rec.Count > 0)
            {
                obj = new { rec };
                return Ok(obj);
            }
            else
            {
                obj = new { message = "No record of staff types" };
                return BadRequest(obj);
            }
        }

        //Get daily appointments by using date(yyyy-mm-dd) as parameter
        [Route("Appointments")]
        [HttpGet("{visitDate}")]
        public IActionResult GetDailyVisit(string visitDate)
        {
            EntityConnection con = new EntityConnection("tbl_visit");
            Dictionary<string, string> result = new Dictionary<string, string>()
            {
                {"visitDateTime", visitDate }
            };

            if (con.DailyVisit(visitDate).Count > 0)
            {
                return Ok(con.DailyVisit(visitDate));
            }
            else
            {
                obj = new { message = "No visit for " + visitDate };
                return NotFound(obj);
            }
        }


        //Get Doctor's appointment list by assignedTo
        [Route("DoctorsAppointment")]
        [HttpGet("{assignedTo}")]
        public IActionResult GetDoctorAppointment(int assignedTo)
        {
            EntityConnection con = new EntityConnection("tbl_visit");
             Dictionary<string, string> result = new Dictionary<string, string>()
            {
                {"assignedTo", assignedTo.ToString() }
            };

            if (con.DoctorsAppoinmentList(assignedTo).Count > 0)
            {
                return Ok(con.DoctorsAppoinmentList(assignedTo));
            }
            else
            {
                obj = new {message = "No patients assigned yet"};
                return NotFound(obj);
            }

        }


        //End of POST requests






    }
}