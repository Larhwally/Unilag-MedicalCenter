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
    public class StoreController : Controller
    {
        //Begin GET method
        [Route("GetInventory")]
        [HttpGet]
        public IActionResult GetInventory()
        {
            EntityConnection con = new EntityConnection("tbl_inventory");
            //string result = "'statau': true, 'data':" + EntityConnection.ToJson(con.Select());
            List<Dictionary<string, object>> result = con.Select();
            return Ok(result);
        }

        [Route("GetDispense")]
        [HttpGet]
        public IActionResult GetDispense()
        {
            EntityConnection con = new EntityConnection("tbl_storedispense");
            //string result = "'statau': true, 'data':" + EntityConnection.ToJson(con.Select());
            List<Dictionary<string, object>> result = con.Select();
            return Ok(result);
        }


        //Begin POST method
        [Route("PostItem")]
        [HttpPost]
        public IActionResult PostItem([FromBody] Dictionary<string, string> rec)
        {
            EntityConnection con = new EntityConnection("tbl_inventory");
            if (rec != null)
            {
                rec.Add("createDate", DateTime.Now.ToString());
                con.Insert(rec);
                Response.WriteAsync("Record successfully saved!");
            }
            else
            {
                //var resp = Response.WriteAsync("Failed to save test");
                return BadRequest("Failed to save item");
            }
            return Ok(rec);
        }

        [Route("PostDispense")]
        [HttpPost]
        public IActionResult PostDispense([FromBody] Dictionary<string, string> rec)
        {
            EntityConnection con = new EntityConnection("tbl_storedispense");
            if (rec != null)
            {
                rec.Add("createDate", DateTime.Now.ToString());
                con.Insert(rec);
                Response.WriteAsync("Record successfully saved!");
            }
            else
            {
                //var resp = Response.WriteAsync("Failed to save test");
                return BadRequest("Failed to save record");
            }
            return Ok(rec);
        }


        //Begin SELECT by ID
        [Route("SearchItem")]
        [HttpGet("{id}")]
        public IActionResult GetItemById(int id)
        {
            EntityConnection con = new EntityConnection("tbl_inventory");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("itbId", id + "");
            //string record = "{'status':true,'data':" + EntityConnection.ToJson(con.SelectByColumn(dic)) + "}";
            List<Dictionary<string, object>> record = con.SelectByColumn(dic);

            if (con.SelectByColumn(dic).Count > 0)
            {
                return Ok(record);
            }
            else
            {
                return NotFound();
            }
            
        }


        [Route("SearchDispense")]
        [HttpGet("{id}")]
        public IActionResult GetRecById(int id)
        {
            EntityConnection con = new EntityConnection("tbl_storedispense");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("itbId", id + "");
            //string record = "{'status':true,'data':" + EntityConnection.ToJson(con.SelectByColumn(dic)) + "}";
            List<Dictionary<string, object>> record = con.SelectByColumn(dic);

            if (con.SelectByColumn(dic).Count > 0)
            {
                return Ok(record);
            }
            else
            {
                return NotFound();
            }
            
        }



    }
}