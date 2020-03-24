﻿using System;
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

        [Route("GetNationalityById")]
        [HttpGet("{id}")]
        public string GetNationalityById(int id)
        {
            EntityConnection con = new EntityConnection("tbl_nationality");
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("Nationality_id", id + "");
            string record = "{'status':true,'data':" + EntityConnection.ToJson(con.SelectByColumn(param)) + "}";
            return record;
        }


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

        [Route("GetClinic")]
        [HttpGet]
        public string GetClinic()
        {
            EntityConnection con = new EntityConnection("tbl_clinic");
            string rec = "{'Status': true, 'Data':" + EntityConnection.ToJson(con.Select()) + "}";
            return rec;
        }





    }
}