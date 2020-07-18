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
    [Route("api/[controller]")]
    [ApiController]
    public class DrugInvController : ControllerBase
    {
        // GET: api/DrugInv
        [HttpGet]
        public IActionResult GetDrug()
        {
            EntityConnection con = new EntityConnection("tbl_druginventory");
            //string rec = "{'Status': true, 'Data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();
            return Ok(rec);
        }

        // GET: api/DrugInv/5
        [HttpGet("{id}")]
        public IActionResult GetDrugById(int id)
        {
            EntityConnection con = new EntityConnection("tbl_druginventory");
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

        // POST: api/DrugInv
        [HttpPost]
        public IActionResult PostDrug([FromBody] Dictionary<string, string> value)
        {
            EntityConnection con = new EntityConnection("tbl_druginventory");
            if (value != null)
            {
                value.Add("createDate", DateTime.Now.ToString());
                con.Insert(value);
                Response.WriteAsync("Record saves successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                return BadRequest("Error in creating record");
            }
            return Ok(value);
        }

        // PUT: api/DrugInv/5
        [HttpPut("{id}")]
        public IActionResult PutDiagnosis(int id, Dictionary<string, string> value)
        {
            EntityConnection con = new EntityConnection("tbl_druginventory");
            if (id != 0)
            {
                con.Update(id, value);
                Response.WriteAsync("Record updated successfully!");
            }
            else
            {
                return BadRequest("Error in updating record!");
            }
            return Ok(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            EntityConnection con = new EntityConnection("tbl_druginventory");
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
