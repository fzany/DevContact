using DevContact.Controllers;
using DevContact.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace Test.Tests.Developers
{
    public class DeleteTests
    {
        private readonly TestDeveloperController control = new TestDeveloperController();
        
        /// <summary>
        /// Test to remove a developer by providing an unknown guid
        /// </summary>
        [Fact]
        public void Unknown_Guid_Returns_NotFound()
        {
            // Arrange
            string unknowngGuid = "5c18e213c48e24276808b1fc";

            // Act
            ActionResult<GeneralResponse> badResponse = control.Delete(unknowngGuid);

            // Assert
            Assert.IsType<NotFoundObjectResult>(badResponse.Result);
        }

        /// <summary>
        /// Test to remove a developer
        /// </summary>
        [Fact]
        public void Known_Guid_Returns_OkResult()
        {
            // Arrange
            string knownGuid = "5c1938352e8ade0b1c4c5a96";

            // Act
            ActionResult<GeneralResponse> okResponse = control.Delete(knownGuid);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse.Result);
        }

        /// <summary>
        /// Test to remove a developer and ensure only one is removed
        /// </summary>
        [Fact]
        public void Remove_Existing_Guid_Ensure_Removal()
        {
            // Arrange
            string knownGuid = "5c1937cecaa4373dc4d33709";

            // Act
            //Fetch all Developer count.
            ActionResult<DeveloperResponses> okResult = control.FetchAll();
            int count1 = ((DeveloperResponses)((OkObjectResult)okResult.Result).Value).Data.Count();

            //Delete a single developer
            ActionResult<GeneralResponse> okResponse = control.Delete(knownGuid);

            //Assert
            Assert.IsType<OkObjectResult>(okResponse.Result);

            //Get the count again
            okResult = control.FetchAll();
            int count2 = ((DeveloperResponses)((OkObjectResult)okResult.Result).Value).Data.Count();
            // Assert
            Assert.Equal(count1, count2 + 1);
        }
    }
}
