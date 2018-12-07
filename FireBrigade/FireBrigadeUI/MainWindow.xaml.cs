
using DBLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FireBrigadeUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FireDBEntities db = new FireDBEntities("metadata=res://*/FireBrigadeModel.csdl|res://*/FireBrigadeModel.ssdl|res://*/FireBrigadeModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.8.120;initial catalog=FireDB;persist security info=True;user id=fireuser;password=password;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            User validatedUser = new User();
            bool login = false;
            bool credentialsValidated = false;
            string currentUser = tbxUsername.Text;
            string currentPassword = tbxPassword.Password;
            credentialsValidated = ValidateUserInput(currentUser, currentPassword);
            if (credentialsValidated)
            {
                validatedUser = GetUserRecord(currentUser, currentPassword);
                if (validatedUser.UserId >0)
                {
                    Dashboard dashboard = new Dashboard();
                    dashboard.user = validatedUser;
                    dashboard.Owner = this;
                    dashboard.ShowDialog();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("The credentials you entered do not exist in the database. Please check and try again.", "User login", MessageBoxButton.OK, MessageBoxImage.Error);
                }             
            }
            else
            {
                MessageBox.Show("Error with your username or password. Please check and try again.", "User login", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }                    
        }

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
        private bool ValidateUserInput(string username, string password)
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
                validated = false;
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
        private User GetUserRecord(string username, string password)
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
            catch (EntityException ex)
            {
                MessageBox.Show("Problem connecting to the SQL server. Application will now close. See exception " + ex.InnerException, "Connect to Database", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                Environment.Exit(0);               
            }        
            return validatedUser;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

       
    }
}
