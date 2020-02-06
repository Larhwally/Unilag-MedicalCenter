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
        public string GetInventory()
        {
            EntityConnection con = new EntityConnection("tbl_Inventory");
            string result = "'statau': true, 'data':" + EntityConnection.ToJson(con.Select());
            return result;
        }

        [Route("GetDispense")]
        [HttpGet]
        public string GetDispense()
        {
            EntityConnection con = new EntityConnection("tbl_StoreDispense");
            string result = "'statau': true, 'data':" + EntityConnection.ToJson(con.Select());
            return result;
        }


        //Begin POST method
        [Route("PostItem")]
        [HttpPost]
        public string PostItem([FromBody] Dictionary<string, string> rec)
        {
            EntityConnection con = new EntityConnection("tbl_Inventory");
            if (rec != null)
            {
                con.Insert(rec);
                Response.WriteAsync("Record successfully saved!");
            }
            else
            {
                var resp = Response.WriteAsync("Failed to save test");
                return resp + "";
            }
            return rec + "";
        }

        [Route("PostDispense")]
        [HttpPost]
        public string PostDispense([FromBody] Dictionary<string, string> rec)
        {
            EntityConnection con = new EntityConnection("tbl_StoreDispense");
            if (rec != null)
            {
                con.Insert(rec);
                Response.WriteAsync("Record successfully saved!");
            }
            else
            {
                var resp = Response.WriteAsync("Failed to save test");
                return resp + "";
            }
            return rec + "";
        }


        //Begin SELECT by ID
        [Route("SearchItem")]
        [HttpGet("{id}")]
        public string GetItemById(int id)
        {
            EntityConnection con = new EntityConnection("tbl_Inventory");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("itbId", id + "");
            string record = "{'status':true,'data':" + EntityConnection.ToJson(con.SelectByColumn(dic)) + "}";
            return record;
        }


        [Route("SearchDispense")]
        [HttpGet("{id}")]
        public string GetRecById(int id)
        {
            EntityConnection con = new EntityConnection("tbl_StoreDispense");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("itbId", id + "");
            string record = "{'status':true,'data':" + EntityConnection.ToJson(con.SelectByColumn(dic)) + "}";
            return record;
        }



    }
}