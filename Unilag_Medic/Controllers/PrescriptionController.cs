namespace Unilag_Medic.Controllers
{
    [authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        public object obj = new object();
        public object newobj = new object();

        // GET: api/Prescription
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
                return ok(new string[0]);
            }
            
        }

        // GET: api/Prescription/5
        [HttpGet("{id}")]
        public IActionResult GetPrescriptionById(int id)
        {
            EntityConnection con = new EntityConnection("tbl_patient");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                { "itbId", id + "" }
            };

            Dictionary<string, object> result = con.SelectByColumn(pairs);

            result.TryGetValue("otherDrugs", out newobj);
            result.TryGetValue("drugs", out obj);
            
            string otherDrugs = newobj.ToString();
            string newdrugs = obj.ToString();

            result.Remove("drugs");
            result.Remove("otherDrugs");

            //check if drug list is empty
            if (newdrug.Trim() != "")
            {
                string[] drugs = newdrugs.Split(',');
                string orQueries = drugs.Select(drugs => "itbId =" + drugId).ToArray();

                if (orQueries != null)
                {
                    var drugDetails = con.DrugDetails(orQueries);
                    result.Add("drugs", drugDetails);
                }
            }
            else
            {
                result.Add(new string[0]);
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


            //check if parent method return a record
            if (result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
            
        }


        // POST: api/Prescription
        [HttpPost]
        public IActionResult PostPrescription([FromBody] Dictionary<string, object> param)
        {
            EntityConnection con = new EntityConnection("tbl_patient");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());

                JArray drugs = (JArray) param["drugs"];
                JArray otherDrugs = (JArray) param["otherDrugs"];

                param.Remove("drugs");
                param.Remove("otherDrugs");

                param.Add("drugs", string.Join(",", drugs));
                param.Add("otherDrugs", string.Join("|", otherrugs));
                
                con.Insert(param);
                
                return Created("", param);
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                //return resp + "";
                return BadRequest();
            }

          
        }

        
    }
}