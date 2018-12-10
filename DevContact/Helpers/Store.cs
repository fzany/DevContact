using DevContact.Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DevContact.Helpers
{
    public class Store
    {
        private static readonly DataContext context = new DataContext();
        /// <summary>
        /// Insert the new developer into the database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DeveloperResponse Add(Developer data)
        {
            //initialize response
            DeveloperResponse response = new DeveloperResponse();

            //check if email is present
            if (string.IsNullOrWhiteSpace(data.Email))
            {
                response.Status = false;
                response.Message = Constants.Provide_Email;
                return response;
            }
            //Check for existence of unique identifiers (email and phone)
            if (CheckExistence(e => e.Email, data.Email))
            {
                response.Status = false;
                response.Message = Constants.Email_Exists;
                return response;
            }
            if (CheckExistence(e => e.Phone_Number, data.Phone_Number))
            {
                response.Status = false;
                response.Message = Constants.Phone_Exists;
                return response;
            }

            //insert the data into the database
            context.Developer.Insert(data);

            //prepare response data
            response.Status = true;
            response.Message = Constants.Success;

            //return the newly inserted data from the database.
            response.Data = FetchOne(d=>d.Email, data.Email);
            return response;
        }

        /// <summary>
        /// Fetch a Developer by an Expression
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Developer FetchOne(Expression<Func<Developer, string>> expression, string value)
        {
            IMongoQuery query = Query<Developer>.EQ(expression, value);
            return context.Developer.FindOne(query);
        }

        /// <summary>
        /// Check existence of an expression from the database
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CheckExistence(Expression<Func<Developer, string>> expression, string value)
        {
            IMongoQuery query = Query<Developer>.EQ(expression, value);
            Developer result = context.Developer.FindOne(query);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Update a developer via the guid.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DeveloperResponse Update(Developer data)
        {
            //initialize response data.
            DeveloperResponse response = new DeveloperResponse();

            //check if email is present
            if (string.IsNullOrWhiteSpace(data.Email))
            {
                response.Status = false;
                response.Message = Constants.Provide_Email;
                return response;
            }

            //check if a guid is included in the data
            if (string.IsNullOrWhiteSpace(data.Guid))
            {
                response.Status = false;
                response.Message = Constants.Provide_Guid;
                return response;
            }

            //check if the developer exists on the system via guid.

            if (!CheckExistence(e => e.Guid, data.Guid))
            {
                response.Status = false;
                response.Message = Constants.Non_Exist;
                return response;
            }

            //Update the Contact
            MongoDB.Driver.IMongoQuery query = Query<Developer>.EQ(d => d.Guid, data.Guid);
            MongoDB.Driver.IMongoUpdate replacement = Update<Developer>.Replace(data);
            context.Developer.Update(query, replacement);

            //prepare response data
            response.Status = true;
            response.Message = Constants.Success;

            //return the newly inserted data from the database.
            response.Data = FetchOne(d => d.Email, data.Email);
            return response;
        }

        /// <summary>
        /// Fetch a contact by email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        internal static DeveloperResponse FetchByEmail_Address(string email)
        {
            //initialize response
            DeveloperResponse response = new DeveloperResponse();

            //Check for existence of unique identifiers (email and phone)
            if (!CheckExistence(e => e.Email, email))
            {
                response.Status = false;
                response.Message = Constants.Non_Exist;
                return response;
            }
            response.Data = FetchOne(d => d.Email, email); 

            //prepare response
            response.Status = true;
            return response;
        }

        /// <summary>
        /// Fetch all developers
        /// </summary>
        /// <returns></returns>
        public static DeveloperResponses FetchAll()
        {
            //prepare responses
            DeveloperResponses responses = new DeveloperResponses();
            MongoCursor<Developer> results = context.Developer.FindAll();

            //test for emptiness
            if (results.Count() == 0)
            {
                responses.Status = true;
                responses.Message = Constants.Empty_List;
                responses.Data = new List<Developer>() { };
                return responses;
            }
            responses.Status = true;

            //return data
            responses.Data = results.ToList();
            return responses;
        }

        /// <summary>
        /// Fetch a developer by Guid property.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static DeveloperResponse FetchById(string guid)
        {
            //prepare response
            DeveloperResponse response = new DeveloperResponse();
            //check for existence
            if (!CheckExistence(e => e.Guid, guid))
            {
                response.Status = false;
                response.Message = Constants.Non_Exist;
                return response;
            }
            response.Data = FetchOne(d => d.Guid, guid);

            //send response
            response.Status = true;
            return response;
        }

        /// <summary>
        /// Delete a Developer via the guid Property.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static GeneralResponse Delete(string guid)
        {
            //prepare response
            GeneralResponse response = new GeneralResponse();

            //check if contact exists
            if (!CheckExistence(e => e.Guid, guid))
            {
                response.Status = false;
                response.Message = Constants.Non_Exist;
                return response;
            }

            //proceed to delete the Developer
            IMongoQuery query = Query<Developer>.EQ(d => d.Guid, guid);
            context.Developer.Remove(query);

            //send response
            response.Status = true;
            response.Message = Constants.Success;
            return response;
        }

        /// <summary>
        /// Fetch Developers that have a similar tech stack.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static DeveloperResponses FetchByCategory(int category)
        {
            //initialize response
            DeveloperResponses responses = new DeveloperResponses();
            IMongoQuery query = Query<Developer>.EQ(d => (int)d.Stack, category);
            responses.Data = context.Developer.Find(query).ToList();

            //prepare response
            responses.Status = true;
            return responses;
        }
    }
}
