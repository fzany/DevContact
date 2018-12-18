using DevContact.Controllers;
using DevContact.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace Test.Tests.Users
{
    public class DeleteTests
    {
        private readonly TestUserController control = new TestUserController();


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

        [Fact]
        public void Known_Guid_Returns_OkResult()
        {
            // Arrange
            string knownGuid = "5c190ded2da88542604d60be";

            // Act
            ActionResult<GeneralResponse> okResponse = control.Delete(knownGuid);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse.Result);
        }
        [Fact]
        public void Remove_Existing_Guid_Ensure_Removal()
        {
            // Arrange
            string knownGuid = "5c1911e0d41c4a400480c864";

            // Act
            //Fetch all User count.
            ActionResult<UserResponses> okResult = control.TestGetAll();
            int count1 = ((UserResponses)((OkObjectResult)okResult.Result).Value).Data.Count();

            //Delete a single user
            ActionResult<GeneralResponse> okResponse = control.Delete(knownGuid);

            //Assert
            Assert.IsType<OkObjectResult>(okResponse.Result);

            //Get the count again
            okResult = control.TestGetAll();
            int count2 = ((UserResponses)((OkObjectResult)okResult.Result).Value).Data.Count();
            // Assert
            Assert.Equal(count1, count2 + 1);
        }
    }
}
