using DevContact.Models;
using System;
using System.Collections.Generic;

namespace DevContact.Helpers
{
    public class Store
    {
        public static DeveloperResponse Add(Developer data)
        {
            return new DeveloperResponse() { Data = new Developer() { Email = "fzanyajibs@gmail.com" } };
        }

        public static DeveloperResponse Update(Developer data)
        {
            return new DeveloperResponse() { Data = new Developer() { Email = "fzanyajibs@gmail.com" } };
        }

        public static DeveloperResponses FetchAll()
        {
            return new DeveloperResponses() { Data = new List<Developer>() { new Developer() { Email = "fzanyajibs@gmail.com" }, new Developer() { Email = "fzanyajibs@gmail.com" } } };
        }

        public static DeveloperResponse FetchById(int id)
        {
            return new DeveloperResponse() { Data = new Developer() { Email = "fzanyajibs@gmail.com" } };
        }

        public static DeveloperResponse Delete(int id)
        {
            return new DeveloperResponse() { Data = new Developer() { Email = "fzanyajibs@gmail.com" } };
        }

        public static DeveloperResponses FetchByCategory(int cat)
        {
            return new DeveloperResponses() { Data = new List<Developer>() { new Developer() { Email = "fzanyajibs@gmail.com" }, new Developer() { Email = "fzanyajibs@gmail.com" } } };
        }

        public static bool Is_Developer_Exists(int contact_id)
        {
            throw new NotImplementedException();
        }
    }
}
