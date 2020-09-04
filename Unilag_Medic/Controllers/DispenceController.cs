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
    public class DispenceController : Controller
    {
        //Begin Get method
        [Route("GetDrugtype")]
        [HttpGet]
        public IActionResult GetDrugType()
        {
            EntityConnection con = new EntityConnection("tbl_drugtype");
            //string rec = "{'status': true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.Select();
            return Ok(con.Select());
        }

        [Route("GetDrugDispense")]
        [HttpGet]
        public IActionResult GetDrugDispense()
        {
            EntityConnection con = new EntityConnection("tbl_pharmdispense");
            //string rec = "{'Status': true, 'Data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        //End of GET

        //Begin POST
        [Route("PostDrugType")]
        [HttpPost]
        public IActionResult PostDrugType([FromBody] Dictionary<string, object> values)
        {
            EntityConnection con = new EntityConnection("tbl_drugtype");
            if (values != null)
            {
                values.Add("createDate", DateTime.Now.ToString());
                con.Insert(values);
                Response.WriteAsync("Record successfully saved!");
            }
            else
            {
                return BadRequest("Failed to save record");
                //var resp = Response.WriteAsync("Failed to save test");
                //return resp + "";
            }
            return Ok(values);
        }

        [Route("PostDrugDispense")]
        [HttpPost]
        public IActionResult PostDrugDispense([FromBody] Dictionary<string, object> values)
        {
            EntityConnection con = new EntityConnection("tbl_pharmdispense");
            if (values != null)
            {
                values.Add("createDate", DateTime.Now.ToString());
                con.Insert(values);
                Response.WriteAsync("Record successfully saved!");
            }
            else
            {
                return BadRequest("Failed to save record");
                //var resp = Response.WriteAsync("Failed to save test");
                //return resp + "";
            }
            return Ok(values);
        }
        //End of POST method

        //Begin Select by ID
        [Route("SearchDrugDispense")]
        [HttpGet("{id}")]
        public IActionResult GetRecById(int id)
        {
            EntityConnection con = new EntityConnection("tbl_pharmdispense");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("itbId", id + "");
            //string record = "{'status':true,'data':" + EntityConnection.ToJson(con.SelectByColumn(dic)) + "}";

            if (con.SelectByColumn(dic).Count > 0)
            {
                return Ok(con.SelectByColumn(dic));
            }
            else
            {
                return NotFound();
            }

        }





    }
}