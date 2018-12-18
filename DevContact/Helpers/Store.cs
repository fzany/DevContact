using DevContact.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

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

            //check if guid is present
            if (!string.IsNullOrEmpty(data.Guid))
            {
                response.Status = false;
                response.Message = Constants.Remove_Guid;
                return response;
            }

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
            IMongoQuery query = Query<Developer>.EQ(expression, value.ToLower());
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
            IMongoQuery query = Query<Developer>.EQ(expression, value.ToLower());
            Developer result = context.Developer.FindOne(query);
            if (result == null)
            {
                return false;
            }
            return true;
        }    


    
    }
}
