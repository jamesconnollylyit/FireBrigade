using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DBLibrary
{
    public class Logic
    {
        public bool ValidateInput(TextBox userForename, TextBox userSurname, TextBox userName, TextBox password, ComboBox cboAccessLevel )
        {
            bool validated = true;

            if (userForename.Text.Length == 0 || userForename.Text.Length > 30)
            {
                validated = false;
            }

            if (userSurname.Text.Length == 0 || userSurname.Text.Length > 30)
            {
                validated = false;
            }

            if (userName.Text.Length == 0 || userName.Text.Length > 30)
            {
                validated = false;
            }

            if (password.Text.Length == 0 || password.Text.Length > 30)
            {
                validated = false;
            }

            //Check to see if an item greater than 0 and less than 2 has been selected.
            // Not checking for <0 as the first choice in the combobox is the message
            // to "please select" which is in position 0 index value
            if (cboAccessLevel.SelectedIndex < 1 || cboAccessLevel.SelectedIndex > 2)
            {
                validated = false;
            }
            return validated;
        }
    }
}
