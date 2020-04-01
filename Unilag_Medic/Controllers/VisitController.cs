﻿using System;
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
    public class VisitController : ControllerBase
    {
        // GET: api/Visit
        [HttpGet]
        public string GetVisit()
        {
            EntityConnection con = new EntityConnection("tbl_visit");
            string result = "{'Status': true, 'Data':" + EntityConnection.ToJson(con.Select()) + "}";
            return result;
        }

        // GET: api/Visit/5
        [HttpGet("{id}")]
        public string GetVisitById(int id)
        {
            EntityConnection con = new EntityConnection("tbl_visit");
            Dictionary<string, string> dicts = new Dictionary<string, string>();
            dicts.Add("itbId", id + "");
            string record = "{'status':true,'data':" + EntityConnection.ToJson(con.SelectByColumn(dicts)) + "}";
            return record;
        }

        // POST: api/Visit
        [HttpPost]
        public string Post([FromBody] Dictionary<string, string> param)
        {
            EntityConnection con = new EntityConnection("tbl_visit");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                Response.WriteAsync("Appointment created successfully");
            }
            else
            {
                var resp = Response.WriteAsync("Error in creating record");
                return resp + "";
            }
            return param + "";
        }

        // PUT: api/Visit/5
        [HttpPut("{id}")]
        public string Put(int id, Dictionary<string, string> content)
        {
            EntityConnection con = new EntityConnection("tbl_visit");
            if (id != 0)
            {
                con.Update(id, content);
                Response.WriteAsync("Record updated successfully!");
            }
            else
            {
                return BadRequest("Error in updating record!") + "";
            }
            return content + "";
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public string Delete(int id)
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
                return NotFound().ToString();
            }
            return "Record deleted successfully";
        }
    }
}
