using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Unilag_MedicalCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // POST api/auth/logout
        [HttpPost("logout")]
        public ActionResult<string> Post()
        {
            return "this  could be another entry point to the application";
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "this  could be another entry point to the application";
        }

        // POST api/auth
        [HttpPost]
        public ActionResult<string> Post([FromBody] string value)
        {
            return "this will perform the necessary operation for login";
        }

    }
}
