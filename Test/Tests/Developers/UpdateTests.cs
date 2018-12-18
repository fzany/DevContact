using DevContact.Controllers;
using DevContact.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test.Tests.Developers
{
    public class UpdateTests
    {
        private readonly TestDeveloperController control = new TestDeveloperController();

        /// <summary>
        /// Test for Bad response for invalid email input
        /// </summary>
        [Fact]
        public void Invalid_Email_Returns_Bad_Request()
        {
            // Arrange
            Developer developer = new Developer
            {
                Guid = "5c1937cecaa4373dc4d33709",
                Firstname = "Olorunfemi",
                Lastname = "Ajibulu",
                Email = "testd2dd562981e7@admin",
                GitHub_Url = "https://github.com/fzany",
                LinkedIn_Url = "https://www.linkedin.com/in/fzany",
                Phone_Number = "07034337562",
                Stack = Stack.Fullstack,
                Platform = Platform.Mobile,
                Stackoverflow_Url = "https://stackoverflow.com/developers/2768516/olorunfemi-ajibulu",
                Years_Of_Experience = 2,
                Sex = Sex.Male
            };
            // Act
            ActionResult<DeveloperResponse> badResponse = control.Update(developer);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }


        [Fact]
        public void Valid_Developer_Returns_OkResponse()
        {
            Random ran = new Random();
            // Arrange
            Developer developer = new Developer
            {
                Guid = "5c1937cecaa4373dc4d33709",
                Firstname = "Olorunfemi",
                Lastname = "Ajibulu",
                Email = "testd2dd562981e7@admin.com",
                GitHub_Url = "https://github.com/fzany",
                LinkedIn_Url = "https://www.linkedin.com/in/fzany",
                Phone_Number = "07034337562",
                Stack = Stack.Fullstack,
                Platform = Platform.Mobile,
                Stackoverflow_Url = "https://stackoverflow.com/developers/2768516/olorunfemi-ajibulu",
                Years_Of_Experience = 2,
                Sex = Sex.Male
            };

            // Act
            ActionResult<DeveloperResponse> OkResponse = control.Update(developer);

            // Assert response is correct
            Assert.IsType<OkObjectResult>(OkResponse.Result);

            //Assert response contains Developer data
            DeveloperResponse developer_test = (DeveloperResponse)((OkObjectResult)OkResponse.Result).Value;
            Assert.NotNull(developer_test.Data);
        }


        /// <summary>
        /// Test editing a developer but blank Email
        /// </summary>
        [Fact]
        public void Blank_Email_Returns_Bad_Request()
        {
            // Arrange
            Developer developer = new Developer
            {
                Guid = "5c1937cecaa4373dc4d33709",
                Firstname = "Olorunfemi",
                Lastname = "Ajibulu",
                Email = "",
                GitHub_Url = "https://github.com/fzany",
                LinkedIn_Url = "https://www.linkedin.com/in/fzany",
                Phone_Number = "07034337562",
                Stack = Stack.Fullstack,
                Platform = Platform.Mobile,
                Stackoverflow_Url = "https://stackoverflow.com/developers/2768516/olorunfemi-ajibulu",
                Years_Of_Experience = 2,
                Sex = Sex.Male
            };
            // Act
            ActionResult<DeveloperResponse> badResponse = control.Update(developer);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }

        [Fact]
        public void Blank_Guid_Returns_Bad_Request()
        {
            // Arrange
            Developer developer = new Developer
            {
                Guid = "",
                Firstname = "Olorunfemi",
                Lastname = "Ajibulu",
                Email = "testd2dd562981e7@admin.com",
                GitHub_Url = "https://github.com/fzany",
                LinkedIn_Url = "https://www.linkedin.com/in/fzany",
                Phone_Number = "07034337562",
                Stack = Stack.Fullstack,
                Platform = Platform.Mobile,
                Stackoverflow_Url = "https://stackoverflow.com/developers/2768516/olorunfemi-ajibulu",
                Years_Of_Experience = 2,
                Sex = Sex.Male
            };
            // Act
            ActionResult<DeveloperResponse> badResponse = control.Update(developer);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }

        /// <summary>
        /// Test editing a developer but blank Phone
        /// </summary>
        [Fact]
        public void Blank_Phone_Returns_Bad_Request()
        {
            // Arrange
            Developer developer = new Developer
            {
                Guid = "5c1937cecaa4373dc4d33709",
                Firstname = "Olorunfemi",
                Lastname = "Ajibulu",
                Email = "testd2dd562981e7@admin.com",
                GitHub_Url = "https://github.com/fzany",
                LinkedIn_Url = "https://www.linkedin.com/in/fzany",
                Phone_Number = "",
                Stack = Stack.Fullstack,
                Platform = Platform.Mobile,
                Stackoverflow_Url = "https://stackoverflow.com/developers/2768516/olorunfemi-ajibulu",
                Years_Of_Experience = 2,
                Sex = Sex.Male
            };
            // Act
            ActionResult<DeveloperResponse> badResponse = control.Update(developer);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }
    }
}
