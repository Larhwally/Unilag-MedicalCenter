using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unilag_Medic.Data;

namespace Unilag_Medic.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class StaffSchedule : ControllerBase
    {
        public object obj = new object();

        // GET: api/StaffSchedule
        [HttpGet]
        public IActionResult GetStaffSchedule()
        {
            EntityConnection connection = new EntityConnection("tbl_staff_schedule");

            List<Dictionary<string, object>> result = connection.SelectAllStaffSchedule();
            if (result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                //obj = new {message = "No record available"};
                return Ok(new string[0]);
            }
        }

        // GET: api/StaffSchedule/5
        [HttpGet("{id}")]
        public IActionResult GetSchedulebyId(int id)
        {
            EntityConnection con = new EntityConnection("tbl_staff_schedule");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                { "itbId", id + "" }
            };
            //string record = EntityConnection.ToJson(con.SelectByColumn(pairs));
            if (con.SelectByColumn(pairs).Count > 0)
            {
                return Ok(con.SelectByColumn(pairs));
            }
            else
            {
                return NotFound();
            }

        }

        // POST: api/StaffSchedule
        [HttpPost]
        public IActionResult PostSchedule([FromBody] Dictionary<string, object> param)
        {
            EntityConnection connection = new EntityConnection("tbl_staff_schedule");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                connection.Insert(param);
                return Created("", param);
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                obj = new { message = "Error in adding schedule record" };
                return BadRequest(obj);
            }
        }

        // PUT: api/StaffSchedule/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Dictionary<string, string> content)
        {
            EntityConnection con = new EntityConnection("tbl_staff_schedule");
            if (id != 0)
            {
                con.Update(id, content);
                obj = new { message = "Update successful" };
                return Ok(obj);
            }
            else
            {
                obj = new { message = "Error in updating record!" };
                return BadRequest(obj);
            }
            // return Ok(content);
        }

        // DELETE: api/StaffSchedule/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            EntityConnection con = new EntityConnection("tbl_staff_schedule");
            if (id != 0)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("itbId", id + "");
                con.Delete(param);
                obj = new { message = "Record deleted successfully" };
                return Ok(obj);
            }
            else
            {
                return NotFound();
            }

        }





    }
}