using DevContact.Controllers;
using DevContact.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace Test.Tests.Users
{
    public class AddTests
    {
        private readonly TestUserController control = new TestUserController();

        /// <summary>
        /// Test for Bad response for invalid email input
        /// </summary>
        [Fact]
        public void Invalid_Email_Returns_Bad_Request()
        {
            // Arrange
            User user = new User
            {
                Email = "admin@admin",
                Password = "pass123"
            };
            // Act
            ActionResult<AuthenticateResponse> badResponse = control.TestAddUser(user);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }


        [Fact]
        public void Valid_User_Returns_OkResponse()
        {
            // Arrange
            User user = new User
            {
                Email = $"admin{Guid.NewGuid().ToString().Replace("-", "").Remove(0, 20)}@admin.com",
                //Email = "admin@admin.com",
                Password = "pass123"
            };
            // Act
            ActionResult<AuthenticateResponse> OkResponse = control.TestAddUser(user);

            // Assert response is correct
            Assert.IsType<OkObjectResult>(OkResponse.Result);

            //Assert response contains User data
            AuthenticateResponse user_test = (AuthenticateResponse)((OkObjectResult)OkResponse.Result).Value;
            Assert.NotNull(user_test.Data);
        }

        /// <summary>
        /// Testing adding a user that already exist
        /// </summary>
        [Fact]
        public void Valid_User_Exists_Returns_Bad_Request()
        {
            // Arrange
            User user = new User
            {
                Email = "admin@admin.com",
                Password = "pass123"
            };
            // Act
            ActionResult<AuthenticateResponse> badResponse = control.TestAddUser(user);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }

        /// <summary>
        /// Test adding a user but blank Email
        /// </summary>
        [Fact]
        public void Blank_Email_Returns_Bad_Request()
        {
            // Arrange
            User user = new User
            {
                Email = "",
                Password = "pass123"
            };
            // Act
            ActionResult<AuthenticateResponse> badResponse = control.TestAddUser(user);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }

        /// <summary>
        /// Test adding a user but blank password
        /// </summary>
        [Fact]
        public void Blank_Password_Returns_Bad_Request()
        {
            // Arrange
            User user = new User
            {
                Email = "admin@admin.com",
                Password = ""
            };
            // Act
            ActionResult<AuthenticateResponse> badResponse = control.TestAddUser(user);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }
    }
}
