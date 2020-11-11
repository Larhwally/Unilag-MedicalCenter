using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        public object objs1 = new object();
        public object objs2 = new object();
        public object objs = new object();

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


            //Check if the parent method returns a record
            if (result.Count > 0)
            {
                var drugs = result["drugs"].ToString();
                var otherDrugs = result["otherDrugs"].ToString();

                result.Remove("drugs");
                result.Remove("otherDrugs");

                var DrugDetails = JsonSerializer.Deserialize<List<object>>(drugs);
                var otherDrugDetail = JsonSerializer.Deserialize<List<object>>(otherDrugs);
                

                result.Add("drugs", DrugDetails);
                result.Add("otherDrugs", otherDrugDetail);

                return Ok(result);
                // result.TryGetValue("otherDrugs", out newobj);
                // result.TryGetValue("drugs", out obj);
                // result.TryGetValue("dosageForm", out objs);
                // result.TryGetValue("dosage", out objs1);
                // result.TryGetValue("measurement", out objs2);


                // string otherDrugs = newobj.ToString();
                // string newdrug = obj.ToString();
                // string dosageForms = objs.ToString();
                // string dosages = objs1.ToString();
                // string measurements = objs2.ToString();

                // result.Remove("drugs");
                // result.Remove("otherDrugs");
                // result.Remove("dosageForm");
                // result.Remove("dosage");
                // result.Remove("measurement");

                //check if the drug list is empty
                // if (newdrug.Trim() != "")
                // {

                //     string[] drugs = newdrug.Split(',');

                //     string[] orQueries = drugs.Select(drugId => "itbId =" + drugId).ToArray(); // [itbId=3, itbId=5]

                //     if (orQueries != null)
                //     {
                //         var drugDetails = connection.DrugDetails(orQueries);
                //         result.Add("drugs", drugDetails);
                //     }
                // }
                // else
                // {
                //     result.Add("drugs", new string[0]);
                // }

                // //check if other drugs is empty 
                // if (otherDrugs.Trim() != "")
                // {
                //     string[] newOtherDrugs = otherDrugs.Split('|');
                //     result.Add("otherDrugs", newOtherDrugs);
                // }
                // else
                // {
                //     result.Add("otherDrugs", new string[0]);
                // }

                //check if dosage form is empty 
                // if (dosageForms.Trim() != "")
                // {
                //     string[] newDosageForms = dosageForms.Split('|');
                //     result.Add("dosageForms", newDosageForms);
                // }
                // else
                // {
                //     result.Add("dosageForms", new string[0]);
                // }

                //  //check if dosage is empty 
                // if (dosages.Trim() != "")
                // {
                //     string[] newDosages = dosages.Split('|');
                //     result.Add("dosages", newDosages);
                // }
                // else
                // {
                //     result.Add("dosages", new string[0]);
                // }

                //  //check if measurements is empty 
                // if (measurements.Trim() != "")
                // {
                //     string[] newMeasurements = measurements.Split('|');
                //     result.Add("measurements", newMeasurements);
                // }
                // else
                // {
                //     result.Add("measurements", new string[0]);
                // }

                // obj = new { data = result };
                // return Ok(obj);
            }
            else
            {
                string[] arr = new string[0];
                return Ok(arr);
            }
        }

        //[Route("Prescription")]
        [HttpPost]
        public IActionResult PostPrescrtion([FromBody] Dictionary<string, object> param)
        {
            EntityConnection connection = new EntityConnection("tbl_prescription");
            if (param != null)
            {
                // param.Add("createDate", DateTime.Now.ToString());
                // JArray drugs = (JArray)param["drugs"];
                // JArray OtherDrugs = (JArray)param["otherDrugs"];
                // JArray dosageForms = (JArray)param["dosageForm"];
                // JArray measurements = (JArray)param["measurement"];
                // JArray dosages = (JArray)param["dosage"];

                // param.Remove("drugs");
                // param.Remove("otherDrugs");
                // param.Remove("dosageForm");
                // param.Remove("measurement");
                // param.Remove("dosage");
                

                // param.Add("drugs", String.Join(",", drugs));
                // param.Add("otherDrugs", string.Join("|", OtherDrugs));
                // param.Add("dosageForm", string.Join("|", dosageForms));
                // param.Add("measurement", string.Join("|", measurements));
                // param.Add("dosage", string.Join("|", dosages));

                param.Add("createDate", DateTime.Now.ToString());

                var drugs = param["drugs"];
                var otherDrugs = param["otherDrugs"];

                Console.WriteLine(drugs);
                Console.WriteLine(otherDrugs);

                param.Remove("drugs");
                param.Remove("otherDrugs");

                var DrugDetails = JsonSerializer.Serialize(drugs);
                var otherDrugDetail = JsonSerializer.Serialize(otherDrugs);

                Console.WriteLine(DrugDetails);
                Console.WriteLine(otherDrugDetail);

                

                param.Add("drugs", DrugDetails);
                param.Add("otherDrugs", otherDrugDetail);

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