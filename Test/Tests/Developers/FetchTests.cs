using DevContact.Controllers;
using DevContact.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;
namespace Test.Tests.Developers
{
    public class FetchTests
    {
        private readonly TestDeveloperController control = new TestDeveloperController();

        /// <summary>
        /// Test get all developers (provided the list isn't Empty)
        /// </summary>
        [Fact]
        public void All_Developers_Returns_Ok_Result()
        {
            // Act
            ActionResult<DeveloperResponses> okResult = control.FetchAll();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);

            DeveloperResponses developer_test = (DeveloperResponses)((OkObjectResult)okResult.Result).Value;

            //Assert list is type <List<Developer>
            Assert.IsType<List<Developer>>(developer_test.Data);

            //Assert list is not empty
            Assert.NotEmpty(developer_test.Data);
        }

        /// <summary>
        /// Test get all developers (provided the list is Empty), probably after the table is cleared.
        /// </summary>
        //[Fact]
        //public void All_Developers_Returns_Not_Found_Result()
        //{
        //    // Act
        //    ActionResult<DeveloperResponses> not_Found_Result = control.TestGetAll();

        //    // Assert
        //    Assert.IsType<NotFoundObjectResult>(not_Found_Result.Result);

        //}

        [Fact]
        public void By_Unknown_Email_Returns_Not_Found_Result()
        {
            // Act
            ActionResult<DeveloperResponse> notFoundResult = control.FetchByEmail($"admin{Guid.NewGuid().ToString().Replace("-", "").Remove(0, 20)}@admin.com");

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);
        }

        [Fact]
        public void By_known_Email_Returns_Ok_Result()
        {

            // Act
            ActionResult<DeveloperResponse> okResult = control.FetchByEmail("fzanyajibs@gmail.com");

            // Assert the response code
            Assert.IsType<OkObjectResult>(okResult.Result);

            DeveloperResponse developer_test = (DeveloperResponse)((OkObjectResult)okResult.Result).Value;

            //Assert list is type <List<Developer>
            Assert.IsType<Developer>(developer_test.Data);

            //Assert response contains Developer data
            Assert.NotNull(developer_test.Data);
        }

        [Fact]
        public void By_known_Email_Returns_Ok_Result_Ensure_Right_Data()
        {
            //Arrange
            var email = "fzanyajibs@gmail.com";

            // Act
            ActionResult<DeveloperResponse> okResult = control.FetchByEmail(email);

            // Assert the response code
            Assert.IsType<OkObjectResult>(okResult.Result);

            DeveloperResponse developer_test = (DeveloperResponse)((OkObjectResult)okResult.Result).Value;

            //Assert list is type Developer
            Assert.IsType<Developer>(developer_test.Data);

            //Assert response contains Developer data
            Assert.NotNull(developer_test.Data);

            //Assert the response has the same email as the request.
            Assert.Equal(email, developer_test.Data.Email);
        }

        [Fact]
        public void By_Unknown_Guid_Returns_Not_Found_Result()
        {
            // Act
            ActionResult<DeveloperResponse> notFoundResult = control.FetchById("5c18e213c48e24276808b1fc");

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);
        }

        [Fact]
        public void By_known_Guid_Returns_Ok_Result()
        {
            //Arrange
            var guid = "5c1934130715c550800f22f3";

            // Act
            ActionResult<DeveloperResponse> okResult = control.FetchById(guid);

            // Assert the response code
            Assert.IsType<OkObjectResult>(okResult.Result);

            DeveloperResponse developer_test = (DeveloperResponse)((OkObjectResult)okResult.Result).Value;

            //Assert list is type Developer
            Assert.IsType<Developer>(developer_test.Data);

            //Assert response contains Developer data
            Assert.NotNull(developer_test.Data);
        }

        [Fact]
        public void By_known_Guid_Returns_Ok_Result_Ensure_Right_Data()
        {
            //Arrange
            var guid = "5c1934130715c550800f22f3";

            // Act
            ActionResult<DeveloperResponse> okResult = control.FetchById(guid);

            // Assert the response code
            Assert.IsType<OkObjectResult>(okResult.Result);

            DeveloperResponse developer_test = (DeveloperResponse)((OkObjectResult)okResult.Result).Value;

            //Assert list is type <List<Developer>
            Assert.IsType<Developer>(developer_test.Data);

            //Assert response contains Developer data
            Assert.NotNull(developer_test.Data);

            //Assert the response has the same guid as the request.
            Assert.Equal(guid, developer_test.Data.Guid);
        }
    }
}
