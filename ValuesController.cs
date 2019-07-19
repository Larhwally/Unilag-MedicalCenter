using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnilagMedCenter.Model;
using UnilagMedCenter.Service.DataServices;
using UnilagMedCenter.Service.Infrastructure;

namespace UnilagMedCenter_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly I_UnilagMedCenter _context;

        public ValuesController(I_UnilagMedCenter context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<tbl_ClinicOpenSchedule>> Get()
        {
            return _context.GetTbl_ClinicOpenSchedule();
        }
       
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTbl_ClinicOpenSchedule([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tbl_ClinicOpenSchedule = await _context.tbl_ClinicOpenSchedule.FindAsync(id);

            if (tbl_ClinicOpenSchedule == null)
            {
                return NotFound();
            }

            return Ok(tbl_ClinicOpenSchedule);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
