
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
            string currentUser = tbxUsername.Text;
            string currentPassword = tbxPassword.Password;
            foreach (var user in db.Users)
            {
                if (user.Username == currentUser && user.Password == currentPassword)
                {
                    Dashboard dashboard = new Dashboard();
                    dashboard.user = user;
                    dashboard.ShowDialog();
                    this.Hide();
                }
                else
                {
                    lblErrorMessage.Content = "Please check your username and password";
                }
                
            }

        }
    }
}
