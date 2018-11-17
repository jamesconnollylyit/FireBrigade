using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library1
{
    public class Logic
    {
        readonly string myconnectionstring = "metadata = res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.8.105;initial catalog=FireDB;persist security info=True;user id=fireuser;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'";
        FireDBEntities db = new FireDBEntities("metadata = res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.8.105;initial catalog=FireDB;persist security info=True;user id=fireuser;password=password;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");





        public User GetUserDetails(string username)
        {


            User u = new User();

            foreach (var user in db.Users.Where(t => t.Username == username))
            {
               
                   u = user;
                
            }
            return u;
        }
    }
}
