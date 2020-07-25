using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unilag_Medic.Data;

namespace Unilag_Medic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadsController : ControllerBase
    {
        public static IHostingEnvironment _environment;
        
        public UploadsController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        //// GET: api/Uploads
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Uploads/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Uploads
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile file)
        {
            
            string fName = file.FileName;
            string uniqueName = Guid.NewGuid() + "" + "_" + fName;

            if (!file.ContentType.StartsWith("image/"))
            {
                object obj = new { message = "not an image file" };
                return BadRequest(obj);
            }
            if (!file.FileName.EndsWith("jpg") & !file.FileName.EndsWith("jpeg"))
            {
                object obj = new { message = "image is not in jpg format" };
                return BadRequest(obj);
            }
            if (file.Length < 1024 * 1024 * 2)
            {
                string path = Path.Combine(_environment.ContentRootPath, "upload/" + uniqueName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                //save image details to the databse
                EntityConnection con = new EntityConnection("tbl_images");
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("imgPath", path);
                param.Add("imgUniquePath", uniqueName);
                param.Add("uploadBy", "admin");
                param.Add("uplpoadDateTime", DateTime.Now.ToShortDateString());
                con.Insert(param);
                return Ok(uniqueName);
            }
            else
            {
                object obj = new { message = "File too large" };
                return BadRequest(obj);
            }

        }

        // PUT: api/Uploads/5
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
