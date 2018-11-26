
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

namespace FireBrigadeUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FireDBEntities db = new FireDBEntities("metadata=res://*/FireBrigadeModel.csdl|res://*/FireBrigadeModel.ssdl|res://*/FireBrigadeModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.8.105;initial catalog=FireDB;persist security info=True;user id=fireuser;password=password;pooling=True;MultipleActiveResultSets=True;App=EntityFramework'");

        public MainWindow()
        {
            InitializeComponent();
                     
        }


        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            User validatedUser = new User();
            bool login = false;
            string currentUser = tbxUsername.Text;
            string currentPassword = tbxPassword.Password;
            foreach (var user in db.Users)
            {
                if (user.Username == currentUser && user.Password == currentPassword)
                {                     
                    login = true;
                    validatedUser = user;  
                }
                else
                {
                    lblErrorMessage.Content = "Please check your username and password";
                }             
            }
            if (login)
            {
                CreateLogEntry("Login", "User logged in.", validatedUser.UserId, validatedUser.Username);
                Dashboard dashboard = new Dashboard();
                dashboard.user = validatedUser;
                dashboard.Owner = this;
                dashboard.ShowDialog();
                this.Hide();
            }          
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
