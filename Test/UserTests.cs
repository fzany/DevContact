using DevContact.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Helpers;
using Test.Model;
using Xunit;

namespace Test
{
    public class UserTests
    {
        /// <summary>
        /// Test for Adding a new contact.
        /// </summary>
        [Fact]
        public void AddUser()
        {
            User user = new User {
                Email = "admin@admin.com",
                 Password ="pass123"
            };
            ResponseModel response = Remote.AddUser(user);
            response.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.)
           
            //insert developer
            DeveloperResponse result = TestStore.Add(developer);
            result.Data = 
            Assert.True(result.Status);
        }
    }
}
