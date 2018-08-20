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
                cmd.CommandText = "INSERT INTO employees (FirstName, LastName, Vacations) VALUES(@firstname, @lastname, @vacations);";
                cmd.Parameters.AddWithValue("firstname", firstname);
                cmd.Parameters.AddWithValue("lastname", lastname);
                cmd.Parameters.AddWithValue("vacations", vacationdays);
                cmd.ExecuteNonQuery();
            }
        }
        public void AddVacation(int employeeid, DateTime start, DateTime end)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO vacation (EmployeeID, StartDate, EndDate) VALUES(@employee, @start, @end);";
                cmd.Parameters.AddWithValue("employee", employeeid);
                cmd.Parameters.AddWithValue("start", start);
                cmd.Parameters.AddWithValue("end", end);
                cmd.ExecuteNonQuery();
            }
        }
        public void AddWorkableDays(int employeeid, int mon, int tues, int wed, int thurs, int fri)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO workabledays (EmployeeId, Monday, Tuesday, Wednesday, Thursday, Friday) VALUES(@employeeid, @mon, @tues, @wed, @thurs, @fri);";
                cmd.Parameters.AddWithValue("employeeid", employeeid);
                cmd.Parameters.AddWithValue("mon", mon);
                cmd.Parameters.AddWithValue("tues", tues);
                cmd.Parameters.AddWithValue("wed", wed);
                cmd.Parameters.AddWithValue("thurs", thurs);
                cmd.Parameters.AddWithValue("fri", fri);
                cmd.ExecuteNonQuery();
            }
        }
        public void AddSickDay(int employeeid, DateTime sickday)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO sick (EmployeeID, SickDay) VALUES(@employeeid, @sickday);";
                cmd.Parameters.AddWithValue("employeeid", employeeid);
                cmd.Parameters.AddWithValue("sickday", sickday);
                cmd.ExecuteNonQuery();
            }
        }
        public void AddOffDay(int employeeid, DateTime start, DateTime end)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO offdays (EmployeeID, StartDay, EndDay) VALUES(@employeeid, @start, @end);";
                cmd.Parameters.AddWithValue("employeeid", employeeid);
                cmd.Parameters.AddWithValue("start", start);
                cmd.Parameters.AddWithValue("end", end);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
