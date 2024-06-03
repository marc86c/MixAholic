using Microsoft.AspNetCore.Mvc;
using MixAholicAPI.Service;
using MixAholicCommon.Model;

namespace MixAholicAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MixController : ControllerBase
    {
        private IMixService MixService { get; set; }
        private IAuthService AuthService { get; set; }
        public MixController(IMixService mixService, IAuthService authService)
        {
            MixService = mixService;
            AuthService = authService;
        }

        [HttpPost("CreateMix", Name = "CreateMix")]
        public ActionResult<Mix> CreateMix([FromBody] Mix mix)
        {
            var userId = AuthService.ValidateSessionKey(GetSessionKey());
            if (userId == 0)
            { 
                return Unauthorized();
            }

            return Ok(MixService.CreateMix(mix, userId));
        }

        [HttpPut("UpdateMix", Name = "UpdateMix")]
        public ActionResult UpdateMix([FromBody] Mix mix)
        {
            var userId = AuthService.ValidateSessionKey(GetSessionKey());
            if (userId == 0)
            {
                return Unauthorized();
            }

            if (mix.CreatorUserID != userId)
            {
                return Forbid();
            }

            MixService.UpdateMix(mix);
            return Ok();
        }

        [HttpPost("RateMix", Name = "RateMix")]
        public ActionResult<int> RateMix([FromBody] Rating rating)
        {
            MixService.RateMix(rating);
            return Ok();
        }

        [HttpDelete("RemoveMix/{mixID}", Name = "RemoveMix")]
        public ActionResult<int> RemoveMix(int mixID)
        {
            var userId = AuthService.ValidateSessionKey(GetSessionKey());
            var mix = MixService.GetMix(mixID);
            if (userId == 0)
            {
                return Unauthorized();
            }

            if (mix.CreatorUserID != userId)
            {
                return Forbid();
            }

            MixService.RemoveMix(mixID);
            return Ok();
        }

        [HttpGet("GetMixes", Name = "GetMixes")]
        public ActionResult<List<Mix>> GetMixes()
        {
            return Ok(MixService.GetMixes());
        }

		[HttpGet("GetMix/{mixId}")]
		public ActionResult<List<Mix>> GetMix(int mixId)
		{
			var userId = AuthService.ValidateSessionKey(GetSessionKey());
			if (userId == 0)
			{
				return Unauthorized();
			}

			return Ok(MixService.GetMix(mixId));
		}

		[HttpGet("IsOwner/{mixId}")]
		public ActionResult<List<Mix>> IsOwner(int mixId)
		{
			var userId = AuthService.ValidateSessionKey(GetSessionKey());

			if (userId == 0)
			{
			    return Ok(false);
			}

			var mix = MixService.GetMix(mixId);

			if (mix.CreatorUserID != userId)
			{
				return Ok(false);
			}

			return Ok(true);

		}

		private string GetSessionKey()
        {
            return HttpContext.Request.Headers["Bearer"].ToString().Split(' ').Last();
        }
    }
}
