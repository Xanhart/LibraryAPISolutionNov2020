using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryAPI.Controllers
{
    public class CasheController : ControllerBase
    {
        private readonly ILookUpOnCallDevs _onCallLookup;

        public CasheController(ILookUpOnCallDevs onCallLookup)
        {
            _onCallLookup = onCallLookup;
        }

        [HttpGet("/oncall")]
        public async Task<ActionResult> GetOnCallDev()
        {
            string devEmail = await _onCallLookup.GetOnCallDevAsync();

            return Ok(new
            {
                email = devEmail
            });
        }

        [HttpGet("/cache/time")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 15)]
        public ActionResult GetTheTime()
        {
            return Ok(new
            {
                CreatedAt = DateTime.Now.ToLongTimeString()
            }) ;
        }
    }
}
