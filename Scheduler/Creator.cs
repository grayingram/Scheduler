using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace Scheduler
{/// <summary>
/// Offers specific methods to add records to a database
/// </summary>
    class Creator
    {
        Repository Repository = new Repository();
        public Reader reader = new Reader();
        
        /// <summary>
        /// Adds an employee record to the database
        /// </summary>
        /// <param name="firstname">first name of the employee record to be added</param>
        /// <param name="lastname">last name of the employee record to be added</param>
        /// <param name="vacationdays">number of vacations to be assigned to the employee record being added</param>
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

        /// <summary>
        /// Adds an vacation record of the given employee to the database
        /// </summary>
        /// <param name="employeeid"></param>
        /// <param name="start">The start date of the vacation</param>
        /// <param name="end">The end date of the vacation</param>
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

        /// <summary>
        /// Adds a record of the workable days for a given employee using their id
        /// </summary>
        /// <param name="employeeid">employee id of the employee to be added</param>
        /// <param name="mon">1 or 0 indicating true or false for the day</param>
        /// <param name="tues">1 or 0 indicating true or false for the day</param>
        /// <param name="wed">1 or 0 indicating true or false for the day</param>
        /// <param name="thurs">1 or 0 indicating true or false for the day</param>
        /// <param name="fri">1 or 0 indicating true or false for the day</param>
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

        /// <summary>
        /// Adds a record of the workable late days for a given employee using their id
        /// </summary>
        /// <param name="employeeid">employee id of the employee to be added</param>
        /// <param name="mon">1 or 0 indicating true or false for the day</param>
        /// <param name="tues">1 or 0 indicating true or false for the day</param>
        /// <param name="wed">1 or 0 indicating true or false for the day</param>
        /// <param name="thurs">1 or 0 indicating true or false for the day</param>
        /// <param name="fri">1 or 0 indicating true or false for the day</param>
        public void AddWorkableLateDays(int employeeid, int mon, int tues, int wed, int thurs, int fri)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO workablelatedays (EmployeeId, Monday, Tuesday, Wednesday, Thursday, Friday) VALUES(@employeeid, @mon, @tues, @wed, @thurs, @fri);";
                cmd.Parameters.AddWithValue("employeeid", employeeid);
                cmd.Parameters.AddWithValue("mon", mon);
                cmd.Parameters.AddWithValue("tues", tues);
                cmd.Parameters.AddWithValue("wed", wed);
                cmd.Parameters.AddWithValue("thurs", thurs);
                cmd.Parameters.AddWithValue("fri", fri);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Adds a sick day record of the given employee to the database
        /// </summary>
        /// <param name="employeeid"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void AddSickDay(int employeeid, DateTime start, DateTime end)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO sick (EmployeeID, StartDate, EndDate) VALUES(@employeeid, @start, @end);";
                cmd.Parameters.AddWithValue("employeeid", employeeid);
                cmd.Parameters.AddWithValue("start", start);
                cmd.Parameters.AddWithValue("end", end);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Adds a off day record of the given employee to the database
        /// </summary>
        /// <param name="employeeid"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
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

        /// <summary>
        /// Adds a record of a month of a certain year being scheduled
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        public void AddScheduledMonth(int month, int year)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO scheduled (Month, Year) VALUES(@month, @year);";
                cmd.Parameters.AddWithValue("month", month);
                cmd.Parameters.AddWithValue("year", year);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
