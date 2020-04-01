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
    public class LabTestController : Controller
    {
        //Begin GET method for Lab Tests
       //[Authorize]
       [Route("GetToxicology")]
       [HttpGet]
       public string GetToxicology()
        {
            EntityConnection con = new EntityConnection("tbl_toxicology");
            string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }

        [Route("GetUrinalysis")]
        [HttpGet]
        public string GetUrinalysis()
        {
            EntityConnection con = new EntityConnection("tbl_urinalysis");
            string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }

        [Route("GetMicrobiology")]
        [HttpGet]
        public string GetMicrotest()
        {
            EntityConnection con = new EntityConnection("tbl_microbiologytest");
            string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }

        [Route("GetHaematology")]
        [HttpGet]
        public string GetHaematology()
        {
            EntityConnection con = new EntityConnection("tbl_haematology");
            string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }

        [Route("GetChemistry")]
        [HttpGet]
        public string GetChemTest()
        {
            EntityConnection con = new EntityConnection("tbl_chemistrytest");
            string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }

        [Route("GetStooltest")]
        [HttpGet]
        public string GetStooltest()
        {
            EntityConnection con = new EntityConnection("tbl_stooltest");
            string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }

        [Route("GetSeminal")]
        [HttpGet]
        public string GetSeminal()
        {
            EntityConnection con = new EntityConnection("tbl_seminal_analysis");
            string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }

        //End of GET Lab Test

        //Begin POST Lab test
        [Route("PostToxicology")]
        [HttpPost]
        public string PostToxicology([FromBody] Dictionary<string, string> values)
        {
            EntityConnection con = new EntityConnection("tbl_toxicology");
            if (values != null)
            {
                values.Add("createDate", DateTime.Now.ToString());
                con.Insert(values);
                Response.WriteAsync("Record saved successfully");
            }
            else
            {
                var resp = Response.WriteAsync("Failed to save test");
                return resp + "";
            }
            return values +"";
        }

        [Route("PostUrinalysis")]
        [HttpPost]
        public string PostUrinalysis([FromBody] Dictionary<string, string> values)
        {
            EntityConnection con = new EntityConnection("tbl_urinalysis");
            if (values != null)
            {
                values.Add("createDate", DateTime.Now.ToString());
                con.Insert(values);
                Response.WriteAsync("Record saved successfully");
            }
            else
            {
                var resp = Response.WriteAsync("Failed to save test");
                return resp + "";
            }
            return values + "";
        }

        [Route("PostMicrobiology")]
        [HttpPost]
        public string PostMicrobiology([FromBody] Dictionary<string, string> values)
        {
            EntityConnection con = new EntityConnection("tbl_microbiologytest");
            if (values != null)
            {
                values.Add("createDate", DateTime.Now.ToString());
                con.Insert(values);
                Response.WriteAsync("Record saved successfully");
            }
            else
            {
                var resp = Response.WriteAsync("Failed to save test");
                return resp + "";
            }
            return values + "";
        }

        [Route("PostHaematology")]
        [HttpPost]
        public string PostHaematology([FromBody] Dictionary<string, string> values)
        {
            EntityConnection con = new EntityConnection("tbl_haematology");
            if (values != null)
            {
                values.Add("createDate", DateTime.Now.ToString());
                con.Insert(values);
                Response.WriteAsync("Record saved successfully");
            }
            else
            {
                var resp = Response.WriteAsync("Failed to save test");
                return resp + "";
            }
            return values + "";
        }

        [Route("PostChemistry")]
        [HttpPost]
        public string PostChemistry([FromBody] Dictionary<string, string> values)
        {
            EntityConnection con = new EntityConnection("tbl_chemistrytest");
            if (values != null)
            {
                values.Add("createDate", DateTime.Now.ToString());
                con.Insert(values);
                Response.WriteAsync("Record saved successfully");
            }
            else
            {
                var resp = Response.WriteAsync("Failed to save test");
                return resp + "";
            }
            return values + "";
        }

        [Route("PostSeminal")]
        [HttpPost]
        public string PostSeminal([FromBody] Dictionary<string, string> values)
        {
            EntityConnection con = new EntityConnection("tbl_seminal_analysis");
            if (values != null)
            {
                values.Add("createDate", DateTime.Now.ToString());
                con.Insert(values);
                Response.WriteAsync("Record saved successfully");
            }
            else
            {
                var resp = Response.WriteAsync("Failed to save test");
                return resp + "";
            }
            return values + "";
        }




    }
}