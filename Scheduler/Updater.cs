using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace Scheduler
{
    class Updater
    {
        Repository Repository = new Repository();
        public Reader Reader = new Reader();
        public void UpdateVactionsByEmployeeID(int employeeid, int numberofdays)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();
                int change = Reader.GetNumberOfVacations(employeeid) - numberofdays;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Update employees SET vacations = @change WHERE employeeid = @employeeid";
                cmd.Parameters.AddWithValue("employeeid", employeeid);
                cmd.Parameters.AddWithValue("change", change);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
