using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary;
using Xunit;

namespace DBLibrary.Tests
{
    public class LoginProcessTests
    {
        [Theory]
        [InlineData ("j", "c", true)]
        [InlineData ("ttt", "ppp", true)]
        [InlineData ("1", "password", false)]
        [InlineData ("", "", false)]
        [InlineData ("qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq", "password", false)]
        [InlineData ("user", "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq", false)]
        [InlineData ("-user", "password", false)]
        public void ValidateUserInput_StringsShouldVerify(string username, string password, bool expected)
        {
            // Arrange 
            LoginProcess loginProcess = new LoginProcess();         

            // Act 
            bool actual = loginProcess.ValidateUserInput(username, password);

            // Assert
            Assert.Equal(expected, actual);
        }

    }
}
