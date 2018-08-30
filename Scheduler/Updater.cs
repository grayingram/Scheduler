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
        public void RemoveVacationsByEmployeeID(int employeeid, int numberofdays)
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
        public void AddVacationsByEmployeeID(int employeeid, int numberofdays)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();
                int change = Reader.GetNumberOfVacations(employeeid) + numberofdays;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Update employees SET vacations = @change WHERE employeeid = @employeeid";
                cmd.Parameters.AddWithValue("employeeid", employeeid);
                cmd.Parameters.AddWithValue("change", change);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateWorkableDays(int employeeid, int mon, int tues, int wed, int thurs, int fri)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
            using (conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Update workabledays SET Monday = @mon, Tuesday = @tues, Wednesday =@wed, Thursday= @thurs, Friday = @fri WHERE employeeid=@employeeid;";
                cmd.Parameters.AddWithValue("mon", mon);
                cmd.Parameters.AddWithValue("tues", tues);
                cmd.Parameters.AddWithValue("wed", wed);
                cmd.Parameters.AddWithValue("thurs", thurs);
                cmd.Parameters.AddWithValue("fri", fri);
                cmd.Parameters.AddWithValue("employeeid", employeeid);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateWorkedLateDays(int fact, int employeeid)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
            using (conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Update workedlate SET WorkedLateForWeek=@fact WHERE employeeid=@employeeid;";
                cmd.Parameters.AddWithValue("fact", fact);
                cmd.Parameters.AddWithValue("employeeid", employeeid);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateWorkedLateDays(int employeeid)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
            using (conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Update workedlate SET WorkedLateForWeek= 0 WHERE employeeid=@employeeid;";
                cmd.Parameters.AddWithValue("employeeid", employeeid);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
