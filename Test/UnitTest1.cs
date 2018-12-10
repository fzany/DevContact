using System;
using Xunit;
using DevContact.Models;
using DevContact.Helpers;

namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //  Assert.True(File.Exists("./.travis.yml"));
            //            Assert.Contains(DateTime.Now.Year.ToString(), line); has fact

        }

        [Fact]
        public void AddContact()
        {
            Developer developer = new Developer {
                Firstname = "Olorunfemi Ajibulu",
                Lastname = "Ajibulu",
                Email = "fzanyajibs@gmail.com",
                GitHub_Url = "https://github.com/fzany",
                LinkedIn_Url = "https://www.linkedin.com/in/fzany",
                Phone_Number = "07034337562",
                Stack = Stack.Fullstack,
                Stackoverflow_Url = "https://stackoverflow.com/users/2768516/olorunfemi-ajibulu",
                Years_Of_Experience = 2
            };

        }
        [Fact]
        public void UpdateContact()
        {
            int contact_id = 3;
            var response = Store.FetchById(contact_id);
            Developer developer = response.Data;

        }

        [Fact]
        public void DeleteContact()
        {
            int contact_id = 3;
            bool is_Exists = Store.Is_Developer_Exists(contact_id);
            Assert.True(is_Exists);
            Store.Delete(contact_id);
            Assert.True(is_Exists);
        }

        [Fact]
        public void FetchContact()
        {
            int contact_id = 3;
            var contact = Store.FetchById(contact_id);
            Assert.NotNull(contact);
        }

        [Fact]
        public void FetchAllContacts()
        {
            var contacts = Store.FetchAll();
            Assert.NotEmpty(contacts.Data);
        }
    }
}
