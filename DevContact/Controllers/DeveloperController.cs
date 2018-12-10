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
        [Route("developer/add")]
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
        [Route("developer/update")]
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
        [Route("developer/fetch")]
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
        [Route("developer/fetch/{guid}")]
        public ActionResult<DeveloperResponse> FetchById(string guid)
        {
            DeveloperResponse developer = new DeveloperResponse();
            try
            {
                developer = Store.FetchById(guid);
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
        [Route("developer/fetch/cat/{category}")]
        public ActionResult<DeveloperResponses> FetchByCategory(int category)
        {
            DeveloperResponses developers = new DeveloperResponses();
            try
            {
                developers = Store.FetchByCategory(category);
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

        [HttpDelete]
        [Route("developer/delete/{guid}")]
        public ActionResult<GeneralResponse> Delete(string guid)
        {
            GeneralResponse developer = new GeneralResponse();
            try
            {
                developer = Store.Delete(guid);
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