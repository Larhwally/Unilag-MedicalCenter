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
    public class LastAppointmentController : ControllerBase
    {
        public object obj = new object();
        // GET: api/LastAppointment
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/LastAppointment/5
        [HttpGet("{patientId}")]
        public IActionResult GetLastVisit(int patientId)
        {
            EntityConnection con = new EntityConnection("tbl_visit");
            Dictionary<string, string> result = new Dictionary<string, string>()
            {
                {"patientId", patientId + ""}
            };
            if (con.LastVisit(patientId).Count > 0)
            {
                return Ok(con.LastVisit(patientId));
            }
            else
            {
                 result = null;
                                 obj = new { data = result };
                                                 return Ok(obj);
            }

        }

        // POST: api/LastAppointment
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/LastAppointment/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
