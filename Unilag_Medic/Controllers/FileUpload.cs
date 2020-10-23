using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unilag_Medic.Data;
using Unilag_Medic.Models;

namespace Unilag_Medic.Controllers
{
    [Route("api/[controller]")]
    public class FileUpload : Controller
    {
        public static IWebHostEnvironment _environment;
        public object objs = new object();

        public FileUpload(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        // POST: api/FileUpload
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile file, int patientId, string fileName)
        {
            string fileTitle = file.FileName;
            string uniqueName = Guid.NewGuid() + "" + "_" + fileTitle;

            if (!file.ContentType.StartsWith("application/"))
            {
                objs = new { message = "not in correct file format" };
                return BadRequest(objs);
            }

            if (!file.FileName.EndsWith("pdf"))
            {
                objs = new { message = "file is not in correct format" };
                return BadRequest(objs);
            }

            if (file.Length < 1024 * 1024 * 10)
            {
                string path = Path.Combine(_environment.ContentRootPath, "upload/" + uniqueName);
                using (var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    await file.CopyToAsync(stream);
                }

                 //save image details to the databse
                EntityConnection con = new EntityConnection("tbl_files");
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("fileTitle", fileTitle);
                param.Add("fileName", fileName);
                param.Add("patientId", patientId);
                param.Add("filePath", path);
                param.Add("uniqueName", uniqueName);
                param.Add("uploadedBy", "admin");
                param.Add("createDate", DateTime.Now.ToShortDateString());
                con.Insert(param);

                objs = new {data = param};
                return Ok(objs);
            }
            else
            {
                objs = new { message = "File too large" };
                return BadRequest(objs);
            }


        }


        // GET: api/FileUpload/uniqueName
        [HttpGet("{uniqueName}")]
        public IActionResult Get(string uniqueName)
        {
            EntityConnection con = new EntityConnection("tbl_files");
            if (con.CheckFile(uniqueName) == true)
            {
                string path = Path.Combine(_environment.ContentRootPath, "upload/" + uniqueName);
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
                {
                    return PhysicalFile(path, "application/pdf");
                }

            }
            else
            {
                objs = new { message = "File does not exist" };
                return BadRequest(objs);
            }
        }




    }
}