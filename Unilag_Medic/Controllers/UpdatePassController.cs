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
        public object obj = new object();
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
        [HttpPut("{medstaffId}")]
        public IActionResult UpdateUser(int medstaffId, UnilagMedLogin unilag)
        {
            EntityConnection con = new EntityConnection("tbl_userlogin");
            if (medstaffId != 0)
            {
                unilag.createDate = DateTime.Now;
                con.UpdateUser(medstaffId, unilag);
                obj = new {message = "password updated successfully"};
                return Ok(obj);
            }
            else
            {
                obj = new {data = "Password update failed"};
                return BadRequest(obj);
            }

           
        }



        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
