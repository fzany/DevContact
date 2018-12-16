using DevContact.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test
{
    public class ContactTests
    {
        /// <summary>
        /// Test for Adding a new contact.
        /// </summary>
        [Fact]
        public void AddContact()
        {
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
                Stackoverflow_Url = "https://stackoverflow.com/users/2768516/olorunfemi-ajibulu",
                Years_Of_Experience = 2,
                Sex = Sex.Male
            };

            //insert developer
            DeveloperResponse result = TestStore.Add(developer);
            Assert.True(result.Status);
        }
        /// <summary>
        /// Test for updating a contact.
        /// </summary>
        [Fact]
        public void UpdateContact()
        {
            string contact_id = Guid.NewGuid().ToString();

            //Modify Developer data.
            Developer developer = new Developer
            {
                Guid = contact_id,
                Firstname = "Adeniyi",
                Lastname = "Ajibulu",
                Email = "fzanyajibs@gmail.com",
                GitHub_Url = "https://github.com/fzany",
                LinkedIn_Url = "https://www.linkedin.com/in/fzany",
                Phone_Number = "07034337562",
                Stack = Stack.Fullstack,
                Platform = Platform.Mobile,
                Stackoverflow_Url = "https://stackoverflow.com/users/2768516/olorunfemi-ajibulu",
                Years_Of_Experience = 2,
                Sex = Sex.Male
            };

            //send for update
            DeveloperResponse result = TestStore.Update(developer);

            Assert.True(result.Status);
        }
        /// <summary>
        /// Test for deleting a contact.
        /// </summary>
        [Fact]
        public void DeleteContact()
        {
            string contact_id = Guid.NewGuid().ToString();
            bool is_Exists = TestStore.CheckExistence(d => d.Guid, contact_id);
            Assert.True(is_Exists);
            TestStore.Delete(contact_id);
            Assert.True(is_Exists);
        }

        /// <summary>
        /// Test for fetching a contact
        /// </summary>
        [Fact]
        public void FetchContact()
        {
            string contact_id = Guid.NewGuid().ToString();
            DeveloperResponse contact = TestStore.FetchById(contact_id);
            Assert.NotNull(contact);
        }

        /// <summary>
        /// Test for fetching all contacts.
        /// </summary>
        [Fact]
        public void FetchAllContacts()
        {
            DeveloperResponses contacts = TestStore.FetchAll();
            Assert.NotEmpty(contacts.Data);
        }

        /// <summary>
        /// Test for fetching contacts by category stack
        /// </summary>
        [Fact]
        public void FetchContactsByCategory()
        {
            int category = (int)Stack.Backend;
            DeveloperResponses contacts = TestStore.FetchByCategory(category);
            Assert.NotEmpty(contacts.Data);
        }

        /// <summary>
        /// Test for fetching developers by email
        /// </summary>
        [Fact]
        public void FetchContactByEmail()
        {
            string email = "fzanyajibs@gmail.com";
            Developer developer = TestStore.FetchOne(d => d.Email, email);
            Assert.NotNull(developer);
        }
    }

}
