using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unilag_Medic.Services;

namespace Unilag_Medic.Controllers
{
    public class ZenosOpController : Controller
    {
        private readonly IZenossOps _zenossOps;
        public ZenosOpController(IZenossOps zenossOps)
        {
            _zenossOps = zenossOps;
        }


        [Route("ZenossLogin")]
        [HttpGet]
        public async Task<IActionResult> GetTokenSession()
        {
            var sessionToken = await _zenossOps.GetSessionTokenAsync();
            return Ok(sessionToken);
        }







    }
}