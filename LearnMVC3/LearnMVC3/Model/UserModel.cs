using System;
using System.Data.SqlServerCe;
using System.Dynamic;
using System.Security.Cryptography;
using System.Text;

namespace LearnMVC3.Tests
{
    public class UserModel : DynamicModel
    {
        public UserModel():base("Users", "Users", "Id"){}
        

        public dynamic Register(string email, string password, string confirmPassword)
        {

            dynamic result = new ExpandoObject();
            result.Success = false;
            result.Message = "";

            if (EmailAndPasswordValidationRulesArePassed(email, password, confirmPassword))
            {
                try
                {
                    result.User = this.Insert(new {Email = email, HashedPassword = Hash(password)});
                    result.Success = true;
                    result.Message = "Thanks for signing up!";
                }
                catch (SqlCeException)
                {
                    result.Message = "This email already exist in our sytem";
                }
                catch(Exception ex)
                {
                    result.Message = "Exception: " + ex.Message;
                }
            }

            return result;
        }

        private static bool EmailAndPasswordValidationRulesArePassed(string email, string password, string passwordConfirmation)
        {
            return (email.Length >= 6) && (password == passwordConfirmation) && (password.Length >= 6);
        }

        public void SetToken(string token, dynamic user)
        {
            this.Update(new { Token = token }, user.ID);
        }

        public static string Hash(string userPassword)
        {
            return BitConverter.ToString(SHA1Managed.Create().ComputeHash(Encoding.Default.GetBytes(userPassword))).Replace
                    ("-", "");
        }

        public dynamic Login(string email, string password)
        {
            dynamic result = new ExpandoObject();

            result.User = this.Single("email  = @0 AND hashedpassword = @1", email, Hash(password));
            result.Authenticated = result.User != null;

            if (!result.Authenticated)
                result.Message = "Invalid email or password";

            return result;
        }

        public static dynamic FindByToken(string token)
        {
            var db = new UserModel();
            return db.Single(where: "Token = @0", args: token);
        }

        public static dynamic FindByEmail(string email)
        {
            var db = new UserModel();
            return db.Single(where: "Email = @0", args: email);
        }



        public dynamic FuzzySearch(string query)
        {
            var queryFormatted = string.Format("select ID, email as TITLE, CreatedAt as DESCRIPTION " +
                                      "from Users where email like ('%{0}%')  " +
                                      "or createdat like ('%{0}%') " , query);
            return this.Query(queryFormatted);
        }

    }
}