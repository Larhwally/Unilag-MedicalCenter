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
        public string GetDrugType()
        {
            EntityConnection con = new EntityConnection("tbl_drugtype");
            string rec = "{'status': true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            return rec;
        }

        [Route("GetDrugDispense")]
        [HttpGet]
        public string GetDrugDispense()
        {
            EntityConnection con = new EntityConnection("tbl_pharmdispense");
            string rec = "{'Status': true, 'Data':" + EntityConnection.ToJson(con.Select()) + "}";
            return rec;
        }

        //End of GET

        //Begin POST
        [Route("PostDrugType")]
        [HttpPost]
        public string PostDrugType([FromBody] Dictionary<string, string> values)
        {
            EntityConnection con = new EntityConnection("tbl_drugtype");
            if (values != null)
            {
                con.Insert(values);
                Response.WriteAsync("Record successfully saved!");
            }
            else
            {
                var resp = Response.WriteAsync("Failed to save test");
                return resp + "";
            }
            return values +"";
        }

        [Route("PostDrugDispense")]
        [HttpPost]
        public string PostDrugDispense([FromBody] Dictionary<string, string> values)
        {
            EntityConnection con = new EntityConnection("tbl_pharmdispense");
            if (values != null)
            {
                con.Insert(values);
                Response.WriteAsync("Record successfully saved!");
            }
            else
            {
                var resp = Response.WriteAsync("Failed to save test");
                return resp + "";
            }
            return values + "";
        }
        //End of POST method

        //Begin Select by ID
        [Route("SearchDrugDispense")]
        [HttpGet("{id}")]
        public string GetRecById(int id)
        {
            EntityConnection con = new EntityConnection("tbl_pharmdispense");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("itbId", id + "");
            string record = "{'status':true,'data':" + EntityConnection.ToJson(con.SelectByColumn(dic)) + "}";
            return record;
        }





    }
}