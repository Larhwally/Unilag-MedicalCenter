using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unilag_Medic.Data;

namespace Unilag_Medic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        // GET: api/UserRole
        [HttpGet]
        public IActionResult GetRoles()
        {
            EntityConnection con = new EntityConnection("tbl_role");
            //string result = "{'Status': true, 'Data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> result = con.Select();
            return Ok(result);
        }

        // GET: api/UserRole/5
        [HttpGet("{id}")]
        public IActionResult GetRole(int id)
        {
            EntityConnection con = new EntityConnection("tbl_role");
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

        // POST: api/UserRole
        [HttpPost]
        public IActionResult PostRole([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_role");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                Response.WriteAsync("Record saves successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                return BadRequest("Error in adding record");
            }
            return Ok(param);
        }

        // PUT: api/UserRole/5
        [HttpPut("{id}")]
        public IActionResult PutRole(int id, [FromBody] Dictionary<string, string> content)
        {
            EntityConnection con = new EntityConnection("tbl_role");
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
            EntityConnection con = new EntityConnection("tbl_role");
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
