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
    public class LabTestController : Controller
    {
        //Begin GET method for Lab Tests
       //[Authorize]
       
       //Begin toxicology GET and POST method
	public object obj = new object();
       [Route("Toxicology")]
       [HttpGet]
       public IActionResult GetToxicology()
        {
            EntityConnection con = new EntityConnection("tbl_toxicology");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.Select();
             if (result.Count > 0)
            {
                return Ok(result); 
            }
            else
            {
                obj = new { message = " No record found" };
                return BadRequest(obj);
            }
        }

        [Route("Toxicology")]
        [HttpPost]
        public IActionResult PostToxicology([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_toxicology");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                
                return Created("", param);
            }
            else
            {
                //var resp = Response.WriteAsync("Failed to save test");
		obj = new {message = "Error in creating record"};
                return BadRequest(obj);
            }
        }
        //End toxicology POAT and GET method


        
        //Begin Urinalysis POST and GET method

        [Route("UrinalysisTest")]
        [HttpGet]
        public IActionResult GetUrinalysis()
        {
            EntityConnection con = new EntityConnection("tbl_urinalysis");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.Select();
             if (result.Count > 0)
            {
                return Ok(result); 
            }
            else
            {
                obj = new { message = "No urinalysis record saved" };
                return BadRequest(obj);
            }
        }


        [Route("UrinalysisTest")]
        [HttpPost]
        public IActionResult PostUrinalysis([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_urinalysis");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                
                return Created("", param);
            }
            else
            {
                //var resp = Response.WriteAsync("Failed to save test");
		obj = new { message = " Error in creating record, check details and try again"};
                return BadRequest(obj);
            }
        }
        //End urinalysis POST and GET method


        //Begin Microbiology POST and GET method

        [Route("Microbiology")]
        [HttpGet]
        public IActionResult GetMicrotest()
        {
            EntityConnection con = new EntityConnection("tbl_microbiologytest");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.Select();
             if (result.Count > 0)
            {
                return Ok(result); 
            }
            else
            {
                obj = new { message = " No record found" };
                return BadRequest(obj);
            }
        }

        [Route("Microbiology")]
        [HttpPost]
        public IActionResult PostMicrobiology([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_microbiologytest");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                
                return Created("", param);
            }
            else
            {
                //var resp = Response.WriteAsync("Failed to save test");
		obj = new {message = " Error in creating record"};
                return BadRequest(obj);
            }
        }

        //End microbiology POST and GET method


        //Begin Haematology POST and GET method
        
        [Route("HaematologyTest")]
        [HttpGet]
        public IActionResult GetHaematology()
        {
            EntityConnection con = new EntityConnection("tbl_haematology");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.Select();
             if (result.Count > 0)
            {
                return Ok(result); 
            }
            else
            {
                obj = new { message = " No record found" };
                return BadRequest(obj);
            }
        }

        [Route("HaematologyTest")]
        [HttpPost]
        public IActionResult PostHaematology([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_haematology");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                
                return Created("", param);
            }
            else
            {
                //var resp = Response.WriteAsync("Failed to save test");
		obj = new {message = "Error in creating reord"};
                return BadRequest(obj);
            }
        }

        //End haematology POST and GET method



        //Begin chemistry GET and POST method 
        
        [Route("Chemistry")]
        [HttpGet]
        public IActionResult GetChemTest()
        {
            EntityConnection con = new EntityConnection("tbl_chemistrytest");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.Select();
            if(result.Count > 0)
	    {
		return Ok(result);
            }
	    else
	    {
		obj = new { message = "No chemistry record found"}; 
		return BadRequest(obj);
	    }
        }


        [Route("Chemistry")]
        [HttpPost]
        public IActionResult PostChemistry([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_chemistrytest");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                
                return Created("", param);
            }
            else
            {
                obj = new { message = "Error in creating record"};
                return BadRequest(obj);
            }
        }

        //End Chemistry POST and GET method


        //Begin stoolTest POST and GET method

        [Route("StoolTest")]
        [HttpGet]
        public IActionResult GetStooltest()
        {
            EntityConnection con = new EntityConnection("tbl_stooltest_analysis");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.Select();
            if(result.Count > 0)
	    {
	    	return Ok(result);
	    }
	    else
	    {
		obj = new {message = "No stoot test record available"};
		return BadRequest(obj);	
	    }
        }

        [Route("StoolTest")]
        [HttpPost]
        public IActionResult PostStoolTest([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_stooltest_analysis");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                
                return Created("", param);
            }
            else
            {
                obj = new {message = "Error in creating record"};
                return BadRequest(obj);
            }
        }

        //End stooltest POST and GET method

        [Route("GetSeminal")]
        [HttpGet]
        public IActionResult GetSeminal()
        {
            EntityConnection con = new EntityConnection("tbl_seminal_analysis");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.Select();
            return Ok(result);
        }

        [Route("PostSeminal")]
        [HttpPost]
        public IActionResult PostSeminal([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_seminal_analysis");
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


	 [Route("RadiologicalTest")]
        [HttpGet]
        public IActionResult GetRadiological()
        {
            EntityConnection con = new EntityConnection("tbl_radiological_examination");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.Select();

            if (result.Count > 0)
            {
                return Ok(result); 
            }
            else
            {
                obj = new { message = " No record found" };
                return BadRequest(obj);
            }
        }


	 [Route("RadiologicalTest")]
        [HttpPost]
        public IActionResult PostRadiology([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_radiological_examination");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                
                return Created("", param);
            }
            else
            {
                //var resp = Response.WriteAsync("Failed to save test");
                obj = new {message = "Error in creating record"};
                return BadRequest(obj);
            }
        }




        //End of GET Lab Test









    }
}
