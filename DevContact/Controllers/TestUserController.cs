using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DevContact.Helpers;
using DevContact.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevContact.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [ApiController]
    public class TestUserController : ControllerBase
    {
        private static readonly TestDataContext context = new TestDataContext();

        //}
        /// <summary>
        /// Authenticate a user to derive token. 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("test/user/authenticate")]
        public ActionResult<AuthenticateResponse> Authenticate([FromBody]User data)
        {
            try
            {
                data.Email = data.Email.ToLower();

                AuthenticateResponse response = new AuthenticateResponse();
                //check for email and password emptiness
                if (string.IsNullOrWhiteSpace(data.Email))
                {
                    return BadRequest(new { message = Constants.Provide_Email });
                }
                if (string.IsNullOrWhiteSpace(data.Password))
                {
                    return BadRequest(new { message = Constants.Provide_Password });
                }

                //check if user is in the database
                bool IsUserExist = UserService.CheckTestUserExistence(u => u.Email, data.Email);
                if (!IsUserExist)
                {
                    return NotFound(new { message = Constants.Non_Exist });

                }

                //verify Password match
                User user = UserService.FetchOneTestUser(h => h.Email, data.Email);

                if (!data.Password.Equals(user.Password))
                {
                    return BadRequest(new { message = Constants.Invalid_Password });
                }

                //generate jwt token
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(UserService._appSettings.Secret);
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Guid)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

                //fill data
                response.Token = tokenHandler.WriteToken(token);
                response.Status = true;
                user.Password = string.Empty;
                response.Data = user;

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("test/user/add")]
        public ActionResult<AuthenticateResponse> TestAddUser([FromBody]User data)
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

                //Check for email formats
                if (!Checks.IsValidEmail(data.Email))
                {
                    return BadRequest(new { Message = Constants.Invalid_Email });
                }

                //check if password is present
                if (string.IsNullOrWhiteSpace(data.Password))
                {
                    return BadRequest(new { message = Constants.Provide_Password });
                }

                //Check for existence of unique identifiers (email)
                if (UserService.CheckTestUserExistence(e => e.Email, data.Email))
                {
                    return BadRequest(new { message = Constants.Email_Exists });
                }

                //insert the data into the database
                context.User.Insert(data);

                //prepare response data
                response.Status = true;
                response.Message = Constants.Success;

                //return the newly inserted data from the database.
                response.Data = UserService.FetchOneTestUser(d => d.Email, data.Email);
                return Ok(response);


            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return StatusCode(500, ex.ToString());
            }
        }

      
        /// <summary>
        /// Test method for Get All Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("test/user/fetch")]
        public ActionResult<UserResponses> TestGetAll()
        {
            try
            {
                UserResponses responses = new UserResponses();
                MongoCursor<User> users = context.User.FindAll();
                if (users.Count() == 0)
                {
                    return NotFound(new { Message = Constants.Empty_List });
                }
                responses.Data = users.ToList();

                responses.Status = true;

                return Ok(responses);

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return StatusCode(500, ex.ToString());
            }
        }

        /// <summary>
        /// Fetch a User by Email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("test/user/fetch/email/{email}")]
        public ActionResult<UserResponse> GetByEmail(string email)
        {
            try
            {
                UserResponse response = new UserResponse();
                //check if user exists
                if (!UserService.CheckTestUserExistence(e => e.Email, email.ToLower()))
                {
                    return NotFound(new { Message = Constants.Non_Exist });
                }

                response.Data = UserService.FetchOneTestUser(d => d.Email, email.ToLower());
                response.Status = true;
                return Ok(response);

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return StatusCode(500, ex.ToString());
            }
        }

        /// <summary>
        /// Fetch a User by Guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("test/user/fetch/guid/{guid}")]
        public ActionResult<UserResponse> GetByGuid(string guid)
        {
            try
            {
                UserResponse response = new UserResponse();
                //check if user exists
                if (!UserService.CheckTestUserExistence(e => e.Guid, guid.ToLower()))
                {
                    return NotFound(new { Message = Constants.Non_Exist });
                }

                response.Data = UserService.FetchOneTestUser(d => d.Guid, guid.ToLower());
                response.Status = true;
                return Ok(response);

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return StatusCode(500, ex.ToString());
            }
        }

        /// <summary>
        /// Delete a User from the Database.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("test/user/delete/{guid}")]
        public ActionResult<GeneralResponse> Delete(string guid)
        {
            try
            {
                //prepare response
                GeneralResponse response = new GeneralResponse();

                //check if user exists
                if (!UserService.CheckTestUserExistence(e => e.Guid, guid))
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
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
