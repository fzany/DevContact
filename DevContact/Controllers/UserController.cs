using DevContact.Helpers;
using DevContact.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Linq;

namespace DevContact.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static readonly DataContext context = new DataContext();

        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Authenticate a user to derive token. 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add User to the Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("user/add")]
        public ActionResult<AuthenticateResponse> AddUser([FromBody]User data)
        {
            try
            {
                //initialize response
                AuthenticateResponse response = new AuthenticateResponse();

                //check if guid is present
                if (!string.IsNullOrEmpty(data.Guid))
                {
                    return BadRequest(new { message = Constants.Remove_Guid });
                }

                //check if email is present
                if (string.IsNullOrWhiteSpace(data.Email))
                {
                    return BadRequest(new { message = Constants.Provide_Email });
                }
                //Check for existence of unique identifiers (email and phone)
                if (UserService.CheckUserExistence(e => e.Email, data.Email))
                {
                    return BadRequest(new { message = Constants.Email_Exists });
                }

                //insert the data into the database
                context.User.Insert(data);

                //prepare response data
                response.Status = true;
                response.Message = Constants.Success;

                //return the newly inserted data from the database.
                response.Data = UserService.FetchOneUser(d => d.Email, data.Email);
               return Ok(response);


            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return BadRequest(new { message = ex.ToString() });
            }
        }

        /// <summary>
        /// Fetch all users from the Database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("user/fetch")]
        public ActionResult GetAll()
        {
            try
            {
                UserResponses responses = new UserResponses();
                var users = context.User.FindAll();
                if (users.Count() == 0)
                {
                    return NotFound(new {Message=Constants.Empty_List });
                }
                responses.Data = users.ToList();
               
                responses.Status = true;

                return Ok(responses);
              
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return BadRequest(new { message = ex.ToString() });
            }
        }

        /// <summary>
        /// Fetch a User by Email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("user/fetch/email/{email}")]
        public ActionResult GetByEmail(string email)
        {
            try
            {
                UserResponse response = new UserResponse();
                //check if user exists
                if (!UserService.CheckUserExistence(e => e.Email, email.ToLower()))
                {
                    return NotFound(new { Message = Constants.Non_Exist });
                }

                response.Data = UserService.FetchOneUser(d=>d.Email, email.ToLower());
                response.Status = true;
                return Ok(response);

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return BadRequest(new { message = ex.ToString() });
            }
        }

        /// <summary>
        /// Delete a User from the Database.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("user/delete/{guid}")]
        public ActionResult<GeneralResponse> Delete(string guid)
        {
            try
            {
                //prepare response
                GeneralResponse response = new GeneralResponse();

                //check if user exists
                if (!UserService.CheckUserExistence(e => e.Guid, guid))
                {
                    return NotFound(new { Message = Constants.Non_Exist });
                }

                //proceed to delete the User
                IMongoQuery query = Query<User>.EQ(d => d.Guid, guid);
                context.User.Remove(query);

                //send response
                response.Status = true;
                response.Message = Constants.Success;
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return BadRequest(new { message = ex.ToString() });
            }
        }
    }
}