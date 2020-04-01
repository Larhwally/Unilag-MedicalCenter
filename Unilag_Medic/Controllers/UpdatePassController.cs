using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unilag_Medic.Data;
using Unilag_Medic.ViewModel;

namespace Unilag_Medic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdatePassController : ControllerBase
    {
        // GET: api/UpdatePass
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/UpdatePass/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/UpdatePass
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT: api/UpdatePass/5
        [HttpPut("{id}")]
        public string UpdateUser(int id, UnilagMedLogin unilag)
        {
            EntityConnection con = new EntityConnection("tbl_userlogin");
            if (id != 0)
            {
                unilag.createDate = DateTime.Now;
                con.UpdateUser(id, unilag);
                Response.WriteAsync("Password successfully updated!");
            }
            else
            {
                return BadRequest("Error in updating password!") + "";
            }

            return unilag + "";
        }



        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
