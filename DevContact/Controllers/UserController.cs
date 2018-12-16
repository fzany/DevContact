using DevContact.Helpers;
using DevContact.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DevContact.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("user/authenticate")]
        public ActionResult<AuthenticateResponse> Authenticate([FromBody]User data)
        {
            try
            {
                AuthenticateResponse response = _userService.Authenticate(data.Email, data.Password);
                if (response.Status)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new { message = response.Message });
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return BadRequest(new { message = ex.ToString() });
            }
        }

        [HttpGet]
        [Route("user/fetch")]
        public ActionResult GetAll()
        {
            try
            {
                UserResponses users = _userService.GetAll();
                if (users.Status)
                {
                    return Ok(users);
                }
                else
                {
                    return BadRequest(new { message = users.Message });
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return BadRequest(new { message = ex.ToString() });
            }
        }
    }
}