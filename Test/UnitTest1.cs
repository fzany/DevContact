using DevContact.Helpers;
using DevContact.Models;
using System;
using Xunit;

namespace Test
{
    public class UnitTest1
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
                Years_Of_Experience = 2
            };

            //insert developer
            var result = Store.Add(developer);
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
                Years_Of_Experience = 2
            };

            //send for update
            DeveloperResponse result = Store.Update(developer);

            Assert.True(result.Status);
        }
        /// <summary>
        /// Test for deleting a contact.
        /// </summary>
        [Fact]
        public void DeleteContact()
        {
            string contact_id = Guid.NewGuid().ToString();
            bool is_Exists = Store.Is_Developer_Exists(contact_id);
            Assert.True(is_Exists);
            Store.Delete(contact_id);
            Assert.True(is_Exists);
        }

        /// <summary>
        /// Test for fetching a contact
        /// </summary>
        [Fact]
        public void FetchContact()
        {
            string contact_id = Guid.NewGuid().ToString();
            DeveloperResponse contact = Store.FetchById(contact_id);
            Assert.NotNull(contact);
        }

        /// <summary>
        /// Test for fetching all contacts.
        /// </summary>
        [Fact]
        public void FetchAllContacts()
        {
            DeveloperResponses contacts = Store.FetchAll();
            Assert.NotEmpty(contacts.Data);
        }
    }
}
