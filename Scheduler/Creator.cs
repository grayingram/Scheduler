using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace Scheduler
{
    class Creator
    {
        Repository Repository = new Repository();
        public Reader reader = new Reader();

        public void AddEmployee(string firstname, string lastname, int vacationdays)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO categories (FirstName, LastName, Vacations) VALUES(@firstname, @lastname, @vacations);";
                cmd.Parameters.AddWithValue("firstname", firstname);
                cmd.Parameters.AddWithValue("lastname", lastname);
                cmd.Parameters.AddWithValue("vacations", vacationdays);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
