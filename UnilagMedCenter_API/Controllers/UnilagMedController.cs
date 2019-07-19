using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnilagMedCenter.Model;
using UnilagMedCenter_Data;

namespace UnilagMedCenter_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnilagMedController : ControllerBase
    {
        private readonly UnilagMedCenDBContext _context;

        public UnilagMedController(UnilagMedCenDBContext context)
        {
            _context = context;
        }

        // GET: api/UnilagMed
        [HttpGet]
        public IEnumerable<tbl_MedicalStaff> Gettbl_MedicalStaff()
        {
            return _context.tbl_MedicalStaff;
        }

        // GET: api/UnilagMed/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Gettbl_MedicalStaff([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tbl_MedicalStaff = await _context.tbl_MedicalStaff.FindAsync(id);

            if (tbl_MedicalStaff == null)
            {
                return NotFound();
            }

            return Ok(tbl_MedicalStaff);
        }

        // PUT: api/UnilagMed/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Puttbl_MedicalStaff([FromRoute] int id, [FromBody] tbl_MedicalStaff tbl_MedicalStaff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_MedicalStaff.ItbID)
            {
                return BadRequest();
            }

            _context.Entry(tbl_MedicalStaff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_MedicalStaffExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UnilagMed
        [HttpPost]
        public async Task<IActionResult> Posttbl_MedicalStaff([FromBody] tbl_MedicalStaff tbl_MedicalStaff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.tbl_MedicalStaff.Add(tbl_MedicalStaff);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Gettbl_MedicalStaff", new { id = tbl_MedicalStaff.ItbID }, tbl_MedicalStaff);
        }

        // DELETE: api/UnilagMed/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletetbl_MedicalStaff([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tbl_MedicalStaff = await _context.tbl_MedicalStaff.FindAsync(id);
            if (tbl_MedicalStaff == null)
            {
                return NotFound();
            }

            _context.tbl_MedicalStaff.Remove(tbl_MedicalStaff);
            await _context.SaveChangesAsync();

            return Ok(tbl_MedicalStaff);
        }

        private bool tbl_MedicalStaffExists(int id)
        {
            return _context.tbl_MedicalStaff.Any(e => e.ItbID == id);
        }
    }
}