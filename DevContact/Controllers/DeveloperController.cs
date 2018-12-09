using DevContact.Helpers;
using DevContact.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DevContact.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        [HttpPost]
        [Route("Developer/Add")]
        public DeveloperResponse Add([FromBody]Developer data)
        {
            DeveloperResponse developer = new DeveloperResponse();
            try
            {
                developer = Store.Add(data);
                return developer;
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                developer.Message = Constants.Error;
                developer.Status = false;
                return developer;
            }
        }

        [HttpPut]
        [Route("Developer/Update")]
        public DeveloperResponse Update([FromBody]Developer data)
        {
            DeveloperResponse developer = new DeveloperResponse();
            try
            {
                developer = Store.Update(data);
                return developer;
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                developer.Message = Constants.Error;
                developer.Status = false;
                return developer;
            }
        }

        [HttpGet]
        [Route("Developer/Fetch")]
        public DeveloperResponses FetchAll()
        {
            DeveloperResponses developers = new DeveloperResponses();
            try
            {
                developers = Store.FetchAll();
                return developers;
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                developers.Message = Constants.Error;
                developers.Status = false;
                return developers;
            }
        }

        [HttpGet]
        [Route("Developer/Fetch/{id}")]
        public ActionResult<DeveloperResponse> FetchById(int id)
        {
            DeveloperResponse developer = new DeveloperResponse();
            try
            {
                developer = Store.FetchById(id);
                return developer;
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                developer.Message = Constants.Error;
                developer.Status = false;
                return developer;
            }
        }

        [HttpDelete]
        [Route("Developer/Delete/{id}")]
        public ActionResult<DeveloperResponse> Delete(int id)
        {
            DeveloperResponse developer = new DeveloperResponse();
            try
            {
                developer = Store.Delete(id);
                return developer;
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                developer.Message = Constants.Error;
                developer.Status = false;
                return developer;
            }
        }
    }
}