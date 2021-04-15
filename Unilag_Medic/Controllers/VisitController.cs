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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VisitController : ControllerBase
    {
        public object obj = new object();
        // GET: api/Visit
        [HttpGet]
        public IActionResult GetVisit()
        {
            EntityConnection con = new EntityConnection("tbl_visit");
            //string result = "{'Status': true, 'Data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.SelectAllVisit();
            return Ok(result);
        }

        // GET: api/Visit/5
        [HttpGet("{id}")]
        public IActionResult GetVisitById(int id)
        {
            EntityConnection con = new EntityConnection("tbl_visit");
            //Dictionary<string, string> dicts = new Dictionary<string, string>();
            //dicts.Add("itbId", id + "");
            //string record = "{'status':true,'data':" + EntityConnection.ToJson(con.SelectByColumn(dicts)) + "}";
            //List<Dictionary<string, object>> record = con.SelectVisitById(id);
            var record = con.SelectVisitById(id);
            var appointedTo = con.SelectAppointedDetails(id);

            obj = new { record, appointedTo };

            if (con.SelectVisitById(id).Count > 0)
            {
                return Ok(obj);
            }
            else
            {
                return NotFound();
            }

        }


        // POST: api/Visit
        [HttpPost]
        public IActionResult Post([FromBody] Dictionary<string, object> param)
        {
            EntityConnection con = new EntityConnection("tbl_visit");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("", param);

            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                obj = new { message = "Error in adding visit record" };
                return BadRequest(obj);
            }
        }

        // PUT: api/Visit/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Dictionary<string, string> content)
        {
            EntityConnection con = new EntityConnection("tbl_visit");
            if (id != 0)
            {
                con.Update(id, content);
                Response.WriteAsync("Record updated successfully!");
            }
            else
            {
                return BadRequest("Error in updating record!");
            }
            return Ok(content);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            EntityConnection con = new EntityConnection("tbl_visit");
            if (id != 0)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("itbId", id + "");
                con.Delete(param);
            }
            else
            {
                return NotFound();
            }
            return Ok("Record deleted successfully");
        }
    }
}
