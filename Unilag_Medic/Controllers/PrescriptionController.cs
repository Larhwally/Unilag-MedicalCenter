using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using Unilag_Medic.Data;

namespace Unilag_Medic.Controllers
{
    [authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        public object obj = new object();
        public object newobj = new object();

        //[Route("Prescription")]
        [HttpGet]
        public IActionResult GetPrescription()
        {
            EntityConnection con = new EntityConnection("tbl_prescription");
            List<Dictionary<string, object>> result = con.Select();

            if (result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                string[] arr = new string[0];
                return Ok(arr);
            }
        }

        //[Route("Prescription")]
        [HttpGet("{id}")]
        public IActionResult GetPrescriptionById(int id)
        {
            EntityConnection connection = new EntityConnection("tbl_prescription");
            Dictionary<string, string> rec = new Dictionary<string, string>();
            rec.Add("itbId", id.ToString());

            Dictionary<string, object> result = connection.SelectByColumn(rec);

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
                    var drugDetails = connection.DrugDetails(orQueries);
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


                obj = new { data = result};
                return Ok(obj);
            }
            else
            {
            	string[] arr = new string[0];
            	                return Ok(arr);
                //string[] arr = new string[0];
                //return Ok(arr);
            }
        }

        //[Route("Prescription")]
        [HttpPost]
        public IActionResult PostPrescrtion([FromBody] Dictionary<string, object> param)
        {
            EntityConnection connection = new EntityConnection("tbl_prescription");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                JArray drugs = (JArray)param["drugs"];
                JArray OtherDrugs = (JArray)param["otherDrugs"];

                param.Remove("drugs");
                param.Remove("otherDrugs");

                param.Add("drugs", String.Join(",", drugs));
                param.Add("otherDrugs", string.Join("|", OtherDrugs));

                connection.Insert(param);
                return Created("", param);
            }
            else
            {
                obj = new { message = "Prescription failed to add, check record and try again!" };
                return BadRequest(obj);
            }
        }


        [Route("OtherDrugs")]
        [HttpGet]
        public IActionResult PostOtherDrugs([FromBody] Dictionary<string, object> param)
        {
            EntityConnection connection = new EntityConnection("tbl_otherdrugpresc");
            return Ok(param);
        }

    }
}
