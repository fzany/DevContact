using DevContact.Controllers;
using DevContact.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace Test.Tests.Developers
{
    public class AddTests
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
                Firstname = "Olorunfemi",
                Lastname = "Ajibulu",
                Email = "fzanyajibs@gmail",
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
            ActionResult<DeveloperResponse> badResponse = control.Add(developer);

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
                Firstname = "Olorunfemi",
                Lastname = "Ajibulu",
                Email = $"test{Guid.NewGuid().ToString().Replace("-", "").Remove(0, 20)}@admin.com",
                GitHub_Url = "https://github.com/fzany",
                LinkedIn_Url = "https://www.linkedin.com/in/fzany",
                Phone_Number = $"0703433{ran.Next(1000, 9999).ToString()}",
                Stack = Stack.Fullstack,
                Platform = Platform.Mobile,
                Stackoverflow_Url = "https://stackoverflow.com/developers/2768516/olorunfemi-ajibulu",
                Years_Of_Experience = 2,
                Sex = Sex.Male
            };

            // Act
            ActionResult<DeveloperResponse> OkResponse = control.Add(developer);

            // Assert response is correct
            Assert.IsType<OkObjectResult>(OkResponse.Result);

            //Assert response contains Developer data
            DeveloperResponse developer_test = (DeveloperResponse)((OkObjectResult)OkResponse.Result).Value;
            Assert.NotNull(developer_test.Data);
        }

        /// <summary>
        /// Testing adding a developer that already exist via email
        /// </summary>
        [Fact]
        public void Valid_Developer_Exists_Returns_Bad_Request()
        {
            // Arrange
            Developer developer = new Developer
            {
                Firstname = "Olorunfemi",
                Lastname = "Ajibulu",
                Email = "fzanyajibs@gmail.com",
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
            ActionResult<DeveloperResponse> badResponse = control.Add(developer);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }

        /// <summary>
        /// Test adding a developer but blank Email
        /// </summary>
        [Fact]
        public void Blank_Email_Returns_Bad_Request()
        {
            // Arrange
            Developer developer = new Developer
            {
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
            ActionResult<DeveloperResponse> badResponse = control.Add(developer);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }

        /// <summary>
        /// Test adding a developer but blank Phone
        /// </summary>
        [Fact]
        public void Blank_Phone_Returns_Bad_Request()
        {
            // Arrange
            Developer developer = new Developer
            {
                Firstname = "Olorunfemi",
                Lastname = "Ajibulu",
                Email = "fzanyajibs@gmail.com",
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
            ActionResult<DeveloperResponse> badResponse = control.Add(developer);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }

    }
}
