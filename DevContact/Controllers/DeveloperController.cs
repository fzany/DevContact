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
    /// <summary>
    /// API to Add contact.
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private static readonly DataContext context = new DataContext();

        /// <summary>
        /// Insert the new developer into the database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("developer/add")]
        public ActionResult<DeveloperResponse> Add([FromBody]Developer data)
        {
            DeveloperResponse developer = new DeveloperResponse();
            try
            {

                //initialize response
                DeveloperResponse response = new DeveloperResponse();

                //check if guid is present
                if (!string.IsNullOrEmpty(data.Guid))
                {
                    return BadRequest(new { Message = Constants.Remove_Guid });
                }

                //check if email is present
                if (string.IsNullOrWhiteSpace(data.Email))
                {
                    return BadRequest(new { Message = Constants.Provide_Email });
                }
                data.Email = data.Email.ToLower();

                //Check for email formats
                if (!Checks.IsValidEmail(data.Email))
                {
                    return BadRequest(new { Message = Constants.Invalid_Email });
                }
                //Check for existence of unique identifiers (email and phone)
                if (Store.CheckExistence(e => e.Email, data.Email))
                {
                    return BadRequest(new { Message = Constants.Email_Exists });
                }

                if (Store.CheckExistence(e => e.Phone_Number, data.Phone_Number))
                {
                    return BadRequest(new { Message = Constants.Phone_Exists });
                }

                //insert the data into the database
                context.Developer.Insert(data);

                //prepare response data
                response.Status = true;
                response.Message = Constants.Success;

                //return the newly inserted data from the database.
                response.Data = Store.FetchOne(d => d.Email, data.Email);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return StatusCode(500, ex.ToString());
            }
        }

        /// <summary>
        /// API to update  contact
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("developer/update")]
        public ActionResult<DeveloperResponse> Update([FromBody]Developer data)
        {
            try
            {
                //check if email is present
                if (string.IsNullOrWhiteSpace(data.Email))
                {
                    return BadRequest(new { Message = Constants.Provide_Email });
                }
                //check if email is present
                if (string.IsNullOrWhiteSpace(data.Phone_Number))
                {
                    return BadRequest(new { Message = Constants.Provide_Phone });
                }
                data.Email = data.Email.ToLower();
                //Check for email formats
                if (!Checks.IsValidEmail(data.Email))
                {
                    return BadRequest(new { Message = Constants.Invalid_Email });
                }
                //check if a guid is included in the data
                if (string.IsNullOrWhiteSpace(data.Guid))
                {
                    return BadRequest(new { Message = Constants.Provide_Guid });
                }

                //check if the developer exists on the system via guid.

                if (!Store.CheckExistence(e => e.Guid, data.Guid))
                {
                    return NotFound(new { Message = Constants.Non_Exist });
                }

                //Update the Contact
                MongoDB.Driver.IMongoQuery query = Query<Developer>.EQ(d => d.Guid, data.Guid);
                MongoDB.Driver.IMongoUpdate replacement = Update<Developer>.Replace(data);
                context.Developer.Update(query, replacement);

                //initialize response data.
                DeveloperResponse response = new DeveloperResponse
                {
                    //prepare response data
                    Status = true,
                    Message = Constants.Success,

                    //return the newly inserted data from the database.
                    Data = Store.FetchOne(d => d.Email, data.Email)
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return StatusCode(500, ex.ToString());
            }
        }

        /// <summary>
        /// API to fetch all contacts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("developer/fetch")]
        public ActionResult<DeveloperResponses> FetchAll()
        {
            try
            {
                //prepare responses
                DeveloperResponses responses = new DeveloperResponses();
                MongoCursor<Developer> results = context.Developer.FindAll();

                //test for emptiness
                if (results.Count() == 0)
                {
                    return NotFound(new { Message = Constants.Empty_List });
                }
                responses.Status = true;

                //return data
                responses.Data = results.ToList();
                return Ok(responses);
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return StatusCode(500, ex.ToString());
            }
        }

        /// <summary>
        /// API to fetch contact by guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("developer/fetch/{guid}")]
        public ActionResult<DeveloperResponse> FetchById(string guid)
        {
            try
            {
                //prepare response
                DeveloperResponse response = new DeveloperResponse();
                //check for existence
                if (!Store.CheckExistence(e => e.Guid, guid))
                {
                    return NotFound(new { Message = Constants.Non_Exist });
                }
                response.Data = Store.FetchOne(d => d.Guid, guid);

                //send response
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
        /// API to fetch contacts by stack
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("developer/fetch/stack/{stack}")]
        public ActionResult<DeveloperResponses> FetchByStack(int stack)
        {
            try
            {
                //initialize response
                DeveloperResponses responses = new DeveloperResponses();
                IMongoQuery query = Query<Developer>.EQ(d => (int)d.Stack, stack);
                MongoCursor<Developer> listed = context.Developer.Find(query);
                if (listed.Count() == 0)
                {
                    return NotFound(new { Message = Constants.Non_Exist });
                }

                responses.Data = listed.ToList();

                //prepare response
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
        /// API to fetch contact by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("developer/fetch/email/{email}")]
        public ActionResult<DeveloperResponse> FetchByEmail(string email)
        {
            DeveloperResponse developer = new DeveloperResponse();
            try
            {
                //initialize response
                DeveloperResponse response = new DeveloperResponse();

                //Check for existence of unique identifiers (email and phone)
                if (!Store.CheckExistence(e => e.Email, email.ToLower()))
                {
                    return NotFound(new { Message = Constants.Non_Exist });
                }
                response.Data = Store.FetchOne(d => d.Email, email.ToLower());

                //prepare response
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
        /// API to delete contact by Guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("developer/delete/{guid}")]
        public ActionResult<GeneralResponse> Delete(string guid)
        {
            GeneralResponse developer = new GeneralResponse();
            try
            {
                //prepare response
                GeneralResponse response = new GeneralResponse();

                //check if contact exists
                if (!Store.CheckExistence(e => e.Guid, guid))
                {
                    return NotFound(new { Message = Constants.Non_Exist });
                }

                //proceed to delete the Developer
                IMongoQuery query = Query<Developer>.EQ(d => d.Guid, guid);
                context.Developer.Remove(query);

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