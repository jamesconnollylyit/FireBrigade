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
using System.Windows.Shapes;

namespace FireBrigadeUI
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public User user = new User();
        public Dashboard()
        {
            InitializeComponent();

        }


        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            frmMain.Navigate(admin);
        }

        private void btnExitApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mnuShowGroups_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Menu option is checked");
        }


        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void CheckUserAccess(User user)
        {
            if (user.LevelID == 2)
            {
                mnuBuildingMenu.Visibility = Visibility.Visible;
            }

            if (user.LevelID == 3)
            {
                mnuAdminMenu.Visibility = Visibility.Visible;
                mnuBuildingMenu.Visibility = Visibility.Visible;
                mnuToolsMenu.Visibility = Visibility.Visible;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckUserAccess(user);
        }

        private void mnuManageUsers_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            frmMain.Navigate(admin);

        }
    }
}
