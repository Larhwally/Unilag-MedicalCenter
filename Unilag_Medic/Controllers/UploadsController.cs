using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unilag_Medic.Data;

namespace Unilag_Medic.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadsController : ControllerBase
    {
        public static IHostingEnvironment _environment;
        public object objs = new object();

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
        [HttpGet("{uniquePath}")]
        public IActionResult Get(string uniquePath)
        {
            EntityConnection con = new EntityConnection("tbl_upload");
            if (con.CheckImage(uniquePath) == true)
            {
                string path = "/home/unimed/wwwroot/" + uniquePath;
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    return PhysicalFile(path, "image/jpg");
                }

            }
            else
            {
                objs = new { message = "Image does not exist" };
                return BadRequest(objs);
            }
        }

        // POST: api/Uploads
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile file)
        {
            string fName = file.FileName;
            string uniqueName = Guid.NewGuid() + "" + "_" + fName;

            if (!file.ContentType.StartsWith("image/"))
            {
                objs = new { message = "not an image file" };
                return BadRequest(objs);
            }
            if (!file.FileName.EndsWith("jpg") & !file.FileName.EndsWith("jpeg"))
            {
                objs = new { message = "image is not in jpg format" };
                return BadRequest(objs);
            }
            if (file.Length < 1024 * 1024 * 2)
            {
                string path = Path.Combine("/home/unimed/wwwroot", uniqueName);

                using (var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    await file.CopyToAsync(stream);
                }

                //save image details to the databse
                EntityConnection con = new EntityConnection("tbl_upload");
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("fullPath", path);
                param.Add("uniquePath", uniqueName);
                param.Add("createBy", "admin");
                param.Add("createDate", DateTime.Now.ToShortDateString());
                con.Insert(param);
                objs = new { uniqueName };
                return Ok(objs);
            }
            else
            {
                objs = new { message = "File too large" };
                return BadRequest(objs);
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
