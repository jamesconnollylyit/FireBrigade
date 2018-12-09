
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

        LoginProcess loginProcess = new LoginProcess();

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
            credentialsValidated = loginProcess.ValidateUserInput(currentUser, currentPassword);
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
