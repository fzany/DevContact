using System;
using System.Collections.Generic;
using DevContact.Controllers;
using DevContact.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Test.Tests.Users
{
    public class FetchTests
    {
        private readonly TestUserController control = new TestUserController();

        /// <summary>
        /// Test get all users (provided the list isn't Empty)
        /// </summary>
        [Fact]
        public void All_Users_Returns_Ok_Result()
        {
            // Act
            ActionResult<UserResponses> okResult = control.TestGetAll();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);

            UserResponses user_test = (UserResponses)((OkObjectResult)okResult.Result).Value;

            //Assert list is type <List<User>
            Assert.IsType<List<User>>(user_test.Data);

            //Assert list is not empty
            Assert.NotEmpty(user_test.Data);
        }

        /// <summary>
        /// Test get all users (provided the list is Empty), probably after the table is cleared.
        /// </summary>
        //[Fact]
        //public void All_Users_Returns_Not_Found_Result()
        //{
        //    // Act
        //    ActionResult<UserResponses> not_Found_Result = control.TestGetAll();

        //    // Assert
        //    Assert.IsType<NotFoundObjectResult>(not_Found_Result.Result);

        //}

        [Fact]
        public void By_Unknown_Email_Returns_Not_Found_Result()
        {
            // Act
            ActionResult<UserResponse> notFoundResult = control.GetByEmail($"admin{Guid.NewGuid().ToString().Replace("-", "").Remove(0, 20)}@admin.com");

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);
        }

        [Fact]
        public void By_known_Email_Returns_Ok_Result()
        {

            // Act
            ActionResult<UserResponse> okResult = control.GetByEmail("admin@admin.com");

            // Assert the response code
            Assert.IsType<OkObjectResult>(okResult.Result);

            UserResponse user_test = (UserResponse)((OkObjectResult)okResult.Result).Value;

            //Assert list is type <List<User>
            Assert.IsType<User>(user_test.Data);

            //Assert response contains User data
            Assert.NotNull(user_test.Data);
        }

        [Fact]
        public void By_known_Email_Returns_Ok_Result_Ensure_Right_Data()
        {
            //Arrange
            var email = "admin@admin.com";

            // Act
            ActionResult<UserResponse> okResult = control.GetByEmail(email);

            // Assert the response code
            Assert.IsType<OkObjectResult>(okResult.Result);

            UserResponse user_test = (UserResponse)((OkObjectResult)okResult.Result).Value;

            //Assert list is type User
            Assert.IsType<User>(user_test.Data);

            //Assert response contains User data
            Assert.NotNull(user_test.Data);

            //Assert the response has the same email as the request.
            Assert.Equal(email, user_test.Data.Email);
        }

        [Fact]
        public void By_Unknown_Guid_Returns_Not_Found_Result()
        {
            // Act
            ActionResult<UserResponse> notFoundResult = control.GetByGuid("5c18e213c48e24276808b1fc");

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);
        }

        [Fact]
        public void By_known_Guid_Returns_Ok_Result()
        {
            //Arrange
            var guid = "5c1907e90a539c4634ea4e48";

            // Act
            ActionResult<UserResponse> okResult = control.GetByGuid(guid);

            // Assert the response code
            Assert.IsType<OkObjectResult>(okResult.Result);

            UserResponse user_test = (UserResponse)((OkObjectResult)okResult.Result).Value;

            //Assert list is type User
            Assert.IsType<User>(user_test.Data);

            //Assert response contains User data
            Assert.NotNull(user_test.Data);
        }

        [Fact]
        public void By_known_Guid_Returns_Ok_Result_Ensure_Right_Data()
        {
            //Arrange
            var guid = "5c1907e90a539c4634ea4e48";

            // Act
            ActionResult<UserResponse> okResult = control.GetByGuid(guid);

            // Assert the response code
            Assert.IsType<OkObjectResult>(okResult.Result);

            UserResponse user_test = (UserResponse)((OkObjectResult)okResult.Result).Value;

            //Assert list is type <List<User>
            Assert.IsType<User>(user_test.Data);

            //Assert response contains User data
            Assert.NotNull(user_test.Data);

            //Assert the response has the same guid as the request.
            Assert.Equal(guid, user_test.Data.Guid);
        }
    }

}
