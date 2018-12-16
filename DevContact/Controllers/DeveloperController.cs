using DevContact.Helpers;
using DevContact.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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

        [HttpPost]
        [Route("developer/add")]
        public ActionResult<DeveloperResponse> Add([FromBody]Developer data)
        {
            DeveloperResponse developer = new DeveloperResponse();
            try
            {
                developer = Store.Add(data);
                if (developer.Status)
                {
                    return Ok(developer);
                }
                else
                {
                    return BadRequest(new { message = developer.Message });
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return BadRequest(new { message = ex.ToString() });
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
            DeveloperResponse developer = new DeveloperResponse();
            try
            {
                developer = Store.Update(data);
                if (developer.Status)
                {
                    return Ok(developer);
                }
                else
                {
                    return BadRequest(new { message = developer.Message });
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return BadRequest(new { message = ex.ToString() });
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
            DeveloperResponses developers = new DeveloperResponses();
            try
            {
                developers = Store.FetchAll();
                if (developers.Status)
                {
                    return Ok(developers);
                }
                else
                {
                    return BadRequest(new { message = developers.Message });
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return BadRequest(new { message = ex.ToString() });
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
            DeveloperResponse developer = new DeveloperResponse();
            try
            {
                developer = Store.FetchById(guid);
                if (developer.Status)
                {
                    return Ok(developer);
                }
                else
                {
                    return BadRequest(new { message = developer.Message });
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return BadRequest(new { message = ex.ToString() });
            }
        }

        /// <summary>
        /// API to fetch contacts by category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("developer/fetch/cat/{category}")]
        public ActionResult<DeveloperResponses> FetchByCategory(int category)
        {
            DeveloperResponses developers = new DeveloperResponses();
            try
            {
                developers = Store.FetchByCategory(category);
                if (developers.Status)
                {
                    return Ok(developers);
                }
                else
                {
                    return BadRequest(new { message = developers.Message });
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return BadRequest(new { message = ex.ToString() });
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
                developer = Store.FetchByEmail_Address(email.ToLower());
                if (developer.Status)
                {
                    return Ok(developer);
                }
                else
                {
                    return BadRequest(new { message = developer.Message });
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return BadRequest(new { message = ex.ToString() });
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
                developer = Store.Delete(guid);
                if (developer.Status)
                {
                    return Ok(developer);
                }
                else
                {
                    return BadRequest(new { message = developer.Message });
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