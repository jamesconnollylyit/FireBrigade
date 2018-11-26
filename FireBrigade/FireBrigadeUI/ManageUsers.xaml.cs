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
    /// Interaction logic for ManageUsers.xaml
    /// </summary>
    public partial class ManageUsers : UserControl
    {
        FireDBEntities db = new FireDBEntities("metadata=res://*/FireBrigadeModel.csdl|res://*/FireBrigadeModel.ssdl|res://*/FireBrigadeModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.8.105;initial catalog=FireDB;persist security info=True;user id=fireuser;password=password;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");
        List<Log> logs = new List<Log>();
        public ManageUsers()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            lstLogDetails.ItemsSource = logs;
            foreach (var logRecord in db.Logs)
            {
                logs.Add(logRecord);
            }
        }
    }
}
