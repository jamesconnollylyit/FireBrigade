using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Xunit;
using DBLibrary;

namespace DBLibrary.Texts
{
    public class AdminTests
    {
        [Theory]
        [InlineData("James", "Connolly", "jconnolly", "password", 1)]
        public void AdminValidateInput_ShouldWork(string foreName, string surName, string userName, string password, int accessLevel)
        {
            Logic logic = new Logic();
            
            TextBox tbxForename = new TextBox();
            tbxForename.Text = foreName;

            TextBox tbxSurname = new TextBox();
            tbxForename.Text = surName;

            TextBox tbxUserName = new TextBox();
            tbxForename.Text = userName;

            TextBox tbxPassword = new TextBox();
            tbxForename.Text = password;

            ComboBox cboAccessLevel = new ComboBox();
            cboAccessLevel.SelectedIndex = accessLevel;

            bool actual = logic.ValidateInput(tbxForename, tbxSurname, tbxUserName, tbxPassword, cboAccessLevel);

            Assert.True(actual = true);
        }

    }
}
