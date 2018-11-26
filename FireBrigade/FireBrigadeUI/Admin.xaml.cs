using DBLibrary;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Admin.xaml
    /// </summary>
    /// 
    
    public partial class Admin : Page
    {
        FireDBEntities db = new FireDBEntities("metadata=res://*/FireBrigadeModel.csdl|res://*/FireBrigadeModel.ssdl|res://*/FireBrigadeModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.8.105;initial catalog=FireDB;persist security info=True;user id=fireuser;password=password;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");

        List<User> users = new List<User>();
        List<Log> logs = new List<Log>();
        User selectedUser = new User();

        enum DBOperation
        {
            Add,
            Modify,
            Delete
        }

        DBOperation dbOperation = new DBOperation();

        public Admin()
        {
            InitializeComponent();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUserList();
            lstLogList.ItemsSource = logs;
            
            foreach (var log in db.Logs)
            {
                logs.Add(log);
            }
        }

        private void submnuAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            stkUserPanel.Visibility = Visibility.Visible;
        }
        
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (dbOperation == DBOperation.Add)
            {
                User user = new User();
                user.Forename = tbxUserForename.Text.Trim();
                user.Surname = tbxUserSurname.Text.Trim();
                user.Username = tbxUsername.Text.Trim();
                user.Password = tbxUserPassword.Text.Trim();
                user.LevelID = cboAccessLevel.SelectedIndex;
                int saveSuccess = SaveUser(user);
                
                if (saveSuccess == 1)
                {
                    MessageBox.Show("User saved successfully.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshUserList();
                    ClearUserDetails();
                    stkUserPanel.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Problem saving user record.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            if (dbOperation == DBOperation.Modify)
            {
                foreach (var user in db.Users.Where(t => t.UserId == selectedUser.UserId))
                {
                    user.Forename = tbxUserForename.Text.Trim();
                    user.Surname = tbxUserSurname.Text.Trim();
                    user.Password = tbxUserPassword.Text.Trim();
                    user.Username = tbxUsername.Text.Trim();
                    user.LevelID = cboAccessLevel.SelectedIndex;
                }
                int saveSuccess = db.SaveChanges();
                if (saveSuccess == 1)
                {
                    MessageBox.Show("User modified successfully.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshUserList();
                    ClearUserDetails();
                    stkUserPanel.Visibility = Visibility.Collapsed;
                }
            }
            
        }

        /// <summary>
        /// Saves user information to the SQL database.
        /// </summary>
        /// <param name="user">
        /// represents a user record to save to the database.
        /// </param>
        /// <returns>
        /// An integer to indicate save success.
        /// </returns>
        public int SaveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;            
        }

        /// <summary>
        /// Refreshes the user list after a user update.
        /// </summary>
        private void RefreshUserList()
        {
            lstUserList.ItemsSource = users;
            users.Clear();
            foreach (var user in db.Users)
            {
                users.Add(user);
            }
            lstUserList.Items.Refresh();            
        }

        private void ClearUserDetails()
        {
            tbxUserForename.Text = "";
            tbxUserSurname.Text = "";
            tbxUsername.Text = "";
            tbxUserPassword.Text = "";
            cboAccessLevel.SelectedIndex = 0;
        }

        private void lstUserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUserList.SelectedIndex > 0)
            {
                selectedUser = users.ElementAt(lstUserList.SelectedIndex);
                submnuModifySelectedUser.IsEnabled = true;
                submnuDeleteSelectedUser.IsEnabled = true;
                if (dbOperation == DBOperation.Add)
                {
                    ClearUserDetails();
                }
                if (dbOperation == DBOperation.Modify)
                {                   
                    tbxUserForename.Text = selectedUser.Forename;
                    tbxUserSurname.Text = selectedUser.Surname;
                    tbxUserPassword.Text = selectedUser.Password;
                    tbxUsername.Text = selectedUser.Username;
                    cboAccessLevel.SelectedIndex = selectedUser.LevelID;
                }             
            }                                       
        }

        private void submnuModifySelectedUser_Click(object sender, RoutedEventArgs e)
        {
            stkUserPanel.Visibility = Visibility.Visible;
            dbOperation = DBOperation.Modify;
        }

        private void submnuDeleteSelectedUser_Click(object sender, RoutedEventArgs e)
        {
            db.Users.RemoveRange(db.Users.Where(t => t.UserId == selectedUser.UserId));
            int saveSuccess = db.SaveChanges();
            if (saveSuccess == 1)
            {               
                MessageBox.Show("User modified successfully.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshUserList();
                ClearUserDetails();
                stkUserPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Problem deleting user record.", "Delete from database", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
