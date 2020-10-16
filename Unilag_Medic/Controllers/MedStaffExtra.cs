using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unilag_Medic.Data;

namespace Unilag_Medic.Controllers
{
    public class MedStaffExtra : Controller
    {

        public static IWebHostEnvironment _environment;

        public MedStaffExtra(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public object obj = new object();

        [Route("StaffImage")]
        [HttpGet("{uniquePath}")]
        public IActionResult GetImage(string uniquePath)
        {
            EntityConnection connection = new EntityConnection("tbl_medicalstaff");
            if (connection.CheckImage(uniquePath) == true)
            {
                string path = "/home/unimed/wwwroot/" + uniquePath;
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
                {
                    return PhysicalFile(path, "image/jpg");
                }
            }
            else
            {
                obj = new { message = "Image does not exist" };
                return BadRequest(obj);
            }
        }

    }
}