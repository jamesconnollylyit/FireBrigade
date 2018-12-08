using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class ProcessLogin
    {

        FireDBEntities db = new FireDBEntities("metadata=res://*/FireBrigadeModel.csdl|res://*/FireBrigadeModel.ssdl|res://*/FireBrigadeModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.8.120;initial catalog=FireDB;persist security info=True;user id=fireuser;password=password;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");

        public bool ValidateUser(string userName, string password)
        {
            bool output = false;
            User validatedUser = new User();
                     
            bool credentialsValidated = ValidateUserInput(userName, password);
            if (credentialsValidated)
            {
                validatedUser = GetUserRecord(userName, password);
                if (validatedUser.UserId > 0)
                {
                    output = true;
                }               
            }
            else
            {
                throw new System.ArgumentException("Error with username or password", "credentialsValidated");
            }
            return output;
        }

        public bool ValidateUserInput(string username, string password)
        {
            bool validated = true;
            try
            {
                if (username.Length == 0 || username.Length > 30)
                {
                    validated = false;
                   
                }
                // Check each character in the surname string
                // for a number. This assumes that numbers are
                // not allowed in the username.
                foreach (char ch in username)
                {
                    // Check if the current character in the
                    // string is not a number
                    if (ch >= '0' && ch <= '9')
                    {
                        validated = false;
                    }
                }
                // Password must exists and cannot be longer
                // than 30 characters
                if (password.Length == 0 || password.Length > 30)
                {
                    validated = false;
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Problem validating input", "ValidateUserInput");
            }
            // It is easier to set validated to false
            // inside one of the chesks than it is 
            // to validate each check
            return validated;
        }

        /// <summary>
        /// Validates the user credentials against those in ther SQL database.
        /// </summary>
        /// <param name="username">
        /// Username entered by the user.
        /// </param>
        /// <param name="password">
        /// Password entered by the user.
        /// </param>
        /// <returns>
        /// Validated user.
        /// </returns>
        public User GetUserRecord(string username, string password)
        {         
            User validatedUser = new User();
            try
            {
                // Gets the username and password passed to the method
                // form the Users table in the SQL database               
                foreach (var user in db.Users.Where(t => t.Username == username && t.Password == password))
                {
                    validatedUser = user;
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Problem connecting to the SQL server. Application will now close.", "GetUserRecord");
            }
            return validatedUser;
        }
    }
}
