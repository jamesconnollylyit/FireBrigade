
using DBLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Data.SqlClient;
using System.Data.Entity.Core;

namespace FireBrigadeUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FireDBEntities db = new FireDBEntities("metadata=res://*/FireBrigadeModel.csdl|res://*/FireBrigadeModel.ssdl|res://*/FireBrigadeModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.8.120;initial catalog=FireDB;persist security info=True;user id=fireuser;password=password;pooling=True;MultipleActiveResultSets=True;App=EntityFramework'");

        public MainWindow()
        {
            InitializeComponent();
                     
        }

        /// <summary>
        /// Validates the users credentials entered into the 
        /// username and password text boxes.
        /// </summary>
        /// <param name="username">current username from textbox.</param>
        /// <param name="password">current password from textbox.</param>
        /// <returns>Whether credentials are valid or not.</returns>
        private bool ValidateUserInput(string username, string password)
        {
            // It is easier to set validated to false
            // inside one of the checks than it is
            // to validate each check
            bool validated = true;
            if (username.Length ==0 || username.Length>30)
            {
                validated = false;
            }
            // Check each character in the username string 
            // for a number. This assumes that numbers are
            // not allowed in the username.
            foreach (char ch in username)
            {
                // Check if the current character in the 
                // string is not a number.
                if (ch >= '0' && ch <= '9')
                {
                    validated = false;
                }
            }
            // Password must exist and cannot be longer than
            // 30 characters
            if (password.Length ==0 || password.Length>30)
            {
                validated = false;
            }
            return validated;
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
                CreateLogEntry("Login", "User logged in.", validatedUser.UserId, validatedUser.Username);
                Dashboard dashboard = new Dashboard();
                dashboard.user = validatedUser;
                dashboard.Owner = this;
                dashboard.ShowDialog();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Error with your username or password. Please check and try again.", "User login", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //catch (EntityException ex)
            //{
            //    MessageBox.Show("Problem connecting to the SQL server. Application will now close.", "Connecting to Server", MessageBoxButton.OK, MessageBoxImage.Error);
            //    this.Close();
            //    Environment.Exit(0);
            //}
            //finally
            //{
            //    MessageBox.Show("Problem connecting to the SQL server. Application will now close.", "Connecting to Server", MessageBoxButton.OK, MessageBoxImage.Error);
            //}        
        }

        private User GetUserRecord(string username, string password)
        {
            List<User> u = new List<User>();
            User validatedUser = new User();
            try
            {
                //foreach (var user in db.Users.Where(t => t.Username == username && t.Password == password))
                //{
                //    validatedUser = user;
                //}
                foreach (var users in db.Users)
                {
                    u.Add(users);
                }
            }
            catch (EntityException ex)
            {
                MessageBox.Show("Problem connecting to the SQL server. Application will now close.", "Connecting to Server", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                Environment.Exit(0);               
            }
            return validatedUser;
        }

        

        private void CreateLogEntry(string category, string description, int userID, string userName)
        {
            string comment = $"{description} user credentials = {userName}";
            Log log = new Log();
            log.UserID = userID;
            log.Category = category;
            log.Description = comment;
            log.Date = DateTime.Now;
            SaveLog(log);
        }

        private void SaveLog(Log log)
        {
            db.Entry(log).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }
        

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }
    }
}
