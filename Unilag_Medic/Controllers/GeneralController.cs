using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unilag_Medic.Data;
using Unilag_Medic.ViewModel;

namespace Unilag_Medic.Controllers
{
    [Authorize]
    //[Produces("application/json")]
    public class GeneralController : Controller
    {
        
        //Begin  GET requests
        [Route("GetPatByHospnum")]
        [HttpGet("{hospitalNumber}")]
        public IActionResult GetPatByHospnum(string hospNum)
        {
            EntityConnection con = new EntityConnection("tbl_patient");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                { "hospitalNumber", hospNum}
            };

            //string rec = EntityConnection.ToJson(con.SelectByParam(pairs));
            List<Dictionary<string, object>> rec = con.SelectByColumn(pairs);

            if (con.SelectByColumn(pairs).Count > 0)
            {
                return Ok(rec);
            }
            else
            {
                return NotFound();
            }
            
        }

        [Route("GetVitByVisit")]
        [HttpGet("{visitId}")]
        public IActionResult GetVitByVisit(string visitId)
        {
            EntityConnection con = new EntityConnection("tbl_vitalsigns");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                { "visitId", visitId}
            };

            //string rec = EntityConnection.ToJson(con.SelectByParam(pairs));
            List<Dictionary<string, object>> rec = con.SelectByColumn(pairs);

            if (con.SelectByColumn(pairs).Count > 0)
            {
                return Ok(rec);
            }
            else
            {
                return NotFound();
            }
        }


        [Route("GetVitByHospnum")]
        [HttpGet("{hospitalNumber}")]
        public IActionResult GetVitByHospnum(string hospnum)
        {
            EntityConnection con = new EntityConnection("tbl_vitalsigns");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                { "hospitalNumber", hospnum}
            };

            //string rec = EntityConnection.ToJson(con.DisplayVitalValues(hospnum));
            List<Dictionary<string, object>> rec = con.SelectByColumn(pairs);

            if (con.SelectByColumn(pairs).Count > 0)
            {
                return Ok(rec);
            }
            else
            {
                return NotFound();
            }
        }


        [Route("GetVisitByHospnum")]
        [HttpGet("{hospitalNumber}")]
        public IActionResult GetVisitByHospnum(string hospnum)
        {
            EntityConnection con = new EntityConnection("tbl_visit");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                {"hospitalNumber", hospnum }
            };
            //string res = EntityConnection.ToJson(con.DisplayVisitValues(hospnum));
            List<Dictionary<string, object>> rec = con.SelectByColumn(pairs);

            if (con.SelectByColumn(pairs).Count > 0)
            {
                return Ok(rec);
            }
            else
            {
                return NotFound();
            }
        }


        [Route("GetDiagByHospnum")]
        [HttpGet("{hospitalNumber}")]
        public IActionResult GetDiagByHospnum(string hospnum)
        {
            EntityConnection con = new EntityConnection("tbl_diagnosis");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                {"hospitalNumber", hospnum }
            };
            //string res = EntityConnection.ToJson(con.DisplayDiagnosis(hospnum));
            List<Dictionary<string, object>> rec = con.SelectByColumn(pairs);

            if (con.SelectByColumn(pairs).Count > 0)
            {
                return Ok(rec);
            }
            else
            {
                return NotFound();
            }
        }


        [Route("GetClinic")]
        [HttpGet]
        public IActionResult GetClinic()
        {
            EntityConnection con = new EntityConnection("tbl_clinic");
            //string rec = EntityConnection.ToJson(con.Select());
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        [Route("GetClinicSchedule")]
        [HttpGet]
        public IActionResult GetClinicSchedule()
        {
            EntityConnection con = new EntityConnection("tbl_clinicopenschedule");
            //string rec = "{'Status': true, 'Data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        [Route("GetState")]
        [HttpGet]
        public IActionResult GetState()
        {
            EntityConnection con = new EntityConnection("tbl_state");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        [Route("GetNationality")]
        [HttpGet]
        public IActionResult GetNational()
        {
            EntityConnection con = new EntityConnection("tbl_nationality");
            //string result = "{'status': true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        //[Route("GetNationalityById")]
        //[HttpGet("{id}")]
        //public string GetNationalityById(int id)
        //{
        //    EntityConnection con = new EntityConnection("tbl_nationality");
        //    Dictionary<string, string> param = new Dictionary<string, string>();
        //    param.Add("Nationality_id", id + "");
        //    string record = "{'status':true,'data':" + EntityConnection.ToJson(con.SelectByColumn(param)) + "}";
        //    return record;
        //}

        [Route("GetPatienttype")]
        [HttpGet]
        public IActionResult GetPatienttype()
        {
            EntityConnection con = new EntityConnection("tbl_patienttype");
            //string result = "{'status': true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        [Route("GetVisittype")]
        [HttpGet]
        public IActionResult GetVisittype()
        {
            EntityConnection con = new EntityConnection("tbl_visittype");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        [Route("GetHmo")]
        [HttpGet]
        public IActionResult GetHmo()
        {
            EntityConnection con = new EntityConnection("tbl_hmo");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        [Route("GetDepartment")]
        [HttpGet]
        public IActionResult GetDepartment()
        {
            EntityConnection con = new EntityConnection("tbl_department");
            //string result = "{'status':true, 'data':}" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        [Route("GetFaculty")]
        [HttpGet]
        public IActionResult GetFaculty()
        {
            EntityConnection con = new EntityConnection("tbl_faculty");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        [Route("GetDoctor")]
        [HttpGet]
        public IActionResult GetDoctor()
        {
            EntityConnection con = new EntityConnection("tbl_doctor");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        [Route("GetDocSpec")]
        [HttpGet]
        public IActionResult GetDocSpec()
        {
            EntityConnection con = new EntityConnection("tbl_specialization");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }



        //Begin POST requests
        [Route("PostHMO")]
        [HttpPost]
        public IActionResult PostHMO([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_hmo");
           
            
            if (param != null)
            {
                //param.Add("createBy", model.email);
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                Response.WriteAsync("Record saves successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                return BadRequest("Error in creating record");
            }
            return Ok(param);
        }


        [Route("PostPatienttype")]
        [HttpPost]
        public IActionResult PostPatienttype([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_patienttype");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                Response.WriteAsync("Record saves successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                return BadRequest("Error in creating record");
            }
            return Ok(param);
        }


        [Route("PostDepartment")]
        [HttpPost]
        public IActionResult PostDepartment([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_department");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                Response.WriteAsync("Record saves successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                return BadRequest("Error in creating record");
            }
            return Ok(param);
        }


        [Route("PostFaculty")]
        [HttpPost]
        public IActionResult PostFaculty([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_faculty");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                Response.WriteAsync("Record saves successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                return BadRequest("Error in creating record");
            }
            return Ok(param);
        }

         
        [Route("PostClinicSchedule")]
        [HttpPost]
        public IActionResult PostClinicSchedule([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_clinicopenschedule");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                Response.WriteAsync("Record saved successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record, check details and try again!");
                return BadRequest("Error in creating record, check details and try again!");
            }

            return Ok(param);
        }


        [Route("PostDoctor")]
        [HttpPost]
        public IActionResult PostDoctor([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_doctor");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                Response.WriteAsync("Record saves successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                return BadRequest("Error in creating record");
            }
            return Ok(param);
        }


        [Route("PostSpecialization")]
        [HttpPost]
        public IActionResult PostSpecialization([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_specialization");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                Response.WriteAsync("Record saved successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record, check details and try again!");
                return BadRequest("Error in creating record, check details and try again!");
            }

            return Ok(param);
        }

        

        [Route("PostClinic")]
        [HttpPost]
        public IActionResult PostClinic([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_clinic");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                Response.WriteAsync("Record saved successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record, check details and try again!");
                return BadRequest("Error in creating record, check details and try again!");
            }

            return Ok(param);
        }

        [Route("PostVisittype")]
        [HttpPost]
        public IActionResult PostVisittype([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_visittype");
            
            try
            {
                if (param != null)
                {
                    param.Add("createDate", DateTime.Now.ToString());
                    con.Insert(param);
                    Response.WriteAsync("Record saved successfully!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(param);
        }

        [Route("Poststafftype")]
        [HttpPost]
        public IActionResult Poststafftype([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_stafftype");

            try
            {
                if (param != null)
                {
                    param.Add("createDate", DateTime.Now.ToString());
                    con.Insert(param);
                    Response.WriteAsync("Record saved successfully!");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

            return Ok(param);
        }
        //End of POST requests






    }
}