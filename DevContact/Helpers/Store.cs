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
