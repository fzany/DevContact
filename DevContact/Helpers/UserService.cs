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
    public interface IUserService
    {
        AuthenticateResponse Authenticate(string email, string password);
        UserResponses GetAll();
    }
    public class UserService : IUserService
    {
        private static readonly DataContext context = new DataContext();

        private readonly AppSettings _appSettings;
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        /// <summary>
        /// Method to Authenticate User request and return token.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AuthenticateResponse Authenticate(string email, string password)
        {
            email = email.ToLower();

            AuthenticateResponse response = new AuthenticateResponse();
            //check for email and password emptiness
            if (string.IsNullOrWhiteSpace(email))
            {
                response.Message = Constants.Provide_Email;
                response.Status = false;
                return response;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                response.Message = Constants.Provide_Password;
                response.Status = false;
                return response;
            }

            //check if user is in the database
            bool IsUserExist = CheckUserExistence(u => u.Email, email);
            if (!IsUserExist)
            {
                response.Message = Constants.Non_Exist;
                response.Status = false;
                return response;
            }

            //verify Password match
            User user = FetchOneUser(h => h.Email, email);

            if (!password.Equals(user.Password))
            {
                response.Message = Constants.Invalid_Password;
                response.Status = false;
                return response;
            }

            //generate jwt token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Guid)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            //fill data
            response.Token = tokenHandler.WriteToken(token);
            response.Status = true;
            user.Password = string.Empty;
            response.Data = user;

            return response;
        }


        public bool CheckUserExistence(Expression<Func<User, string>> expression, string value)
        {
            IMongoQuery query = Query<User>.EQ(expression, value.ToLower());
            User result = context.User.FindOne(query);
            if (result == null)
            {
                return false;
            }
            return true;
        }
        public User FetchOneUser(Expression<Func<User, string>> expression, string value)
        {
            IMongoQuery query = Query<User>.EQ(expression, value.ToLower());
            return context.User.FindOne(query);
        }

        public UserResponses GetAll()
        {
            UserResponses responses = new UserResponses();
            List<User> users = context.User.FindAll().ToList();
            responses.Data = users;
            if (users.Count() == 0)
            {
                responses.Message = "Empty List";
            }
            responses.Status = true;

            return responses;
        }
    }
}
