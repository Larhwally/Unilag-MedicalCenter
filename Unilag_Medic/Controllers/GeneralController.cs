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
    public class GeneralController : Controller
    {
        //Begin  GET requests
        [Route("GetClinic")]
        [HttpGet]
        public string GetClinic()
        {
            EntityConnection con = new EntityConnection("tbl_clinic");
            string rec = "{'Status': true, 'Data':" + EntityConnection.ToJson(con.Select()) + "}";
            return rec;
        }

        [Route("GetClinicSchedule")]
        [HttpGet]
        public string GetClinicSchedule()
        {
            EntityConnection con = new EntityConnection("tbl_clinicopenschedule");
            string rec = "{'Status': true, 'Data':" + EntityConnection.ToJson(con.Select()) + "}";
            return rec;
        }

        [Route("GetState")]
        [HttpGet]
        public string GetState()
        {
            EntityConnection con = new EntityConnection("tbl_state");
            string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }

        [Route("GetNationality")]
        [HttpGet]
        public string GetNational()
        {
            EntityConnection con = new EntityConnection("tbl_nationality");
            string result = "{'status': true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
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
        public string GetPatienttype()
        {
            EntityConnection con = new EntityConnection("tbl_patienttype");
            string result = "{'status': true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }

        [Route("GetVisittype")]
        [HttpGet]
        public string GetVisittype()
        {
            EntityConnection con = new EntityConnection("tbl_visittype");
            string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }

        [Route("GetHmo")]
        [HttpGet]
        public string GetHmo()
        {
            EntityConnection con = new EntityConnection("tbl_hmo");
            string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }

        [Route("GetDepartment")]
        [HttpGet]
        public string GetDepartment()
        {
            EntityConnection con = new EntityConnection("tbl_department");
            string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }

        [Route("GetFaculty")]
        [HttpGet]
        public string GetFaculty()
        {
            EntityConnection con = new EntityConnection("tbl_faculty");
            string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }

        [Route("GetDoctor")]
        [HttpGet]
        public string GetDoctor()
        {
            EntityConnection con = new EntityConnection("tbl_doctor");
            string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }

        [Route("GetDocSpec")]
        [HttpGet]
        public string GetDocSpec()
        {
            EntityConnection con = new EntityConnection("tbl_specialization");
            string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }



        //Begin POST requests
        [Route("PostHMO")]
        [HttpPost]
        public string PostHMO([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_hmo");
            if (param != null)
            {
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


        [Route("PostPatienttype")]
        [HttpPost]
        public string PostPatienttype([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_patienttype");
            if (param != null)
            {
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


        [Route("PostDepartment")]
        [HttpPost]
        public string PostDepartment([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_department");
            if (param != null)
            {
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


        [Route("PostFaculty")]
        [HttpPost]
        public string PostFaculty([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_faculty");
            if (param != null)
            {
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

         
        [Route("PostClinicSchedule")]
        [HttpPost]
        public string PostPatient([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_clinicopenschedule");
            if (param != null)
            {
                con.Insert(param);
                Response.WriteAsync("Record saved successfully!");
            }
            else
            {
                var resp = Response.WriteAsync("Error in creating record, check details and try again!");
                return resp.ToString();
            }

            return param.ToString();
        }


        [Route("PostDoctor")]
        [HttpPost]
        public string PostDoctor([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_doctor");
            if (param != null)
            {
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


        [Route("PostSpecialization")]
        [HttpPost]
        public string PostSpecialization([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_specialization");
            if (param != null)
            {
                con.Insert(param);
                Response.WriteAsync("Record saved successfully!");
            }
            else
            {
                var resp = Response.WriteAsync("Error in creating record, check details and try again!");
                return resp.ToString();
            }

            return param.ToString();
        }

        

        [Route("PostClinic")]
        [HttpPost]
        public string PostClinic([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_clinic");
            if (param != null)
            {
                con.Insert(param);
                Response.WriteAsync("Record saved successfully!");
            }
            else
            {
                var resp = Response.WriteAsync("Error in creating record, check details and try again!");
                return resp.ToString();
            }

            return param.ToString();
        }

        [Route("PostVisittype")]
        [HttpPost]
        public string PostVisittype([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_visittype");
            
            try
            {
                if (param != null)
                {
                    con.Insert(param);
                    Response.WriteAsync("Record saved successfully!");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return param.ToString();
        }

        [Route("Poststafftype")]
        [HttpPost]
        public string Poststafftype([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_stafftype");

            try
            {
                if (param != null)
                {
                    con.Insert(param);
                    Response.WriteAsync("Record saved successfully!");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return param.ToString();
        }
        //End of POST requests






    }
}