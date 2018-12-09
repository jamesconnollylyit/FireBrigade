using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class LoginProcess
    {

        /// <summary>
        /// Validates the user credentials against those in the SQL database.
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
                    if (ch >= '0' && ch <= '9' || ch == '-')
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
                validated = false;
            }
            // It is easier to set validated to false
            // inside one of the chesks than it is 
            // to validate each check           
            return validated;
        }

    }
}
