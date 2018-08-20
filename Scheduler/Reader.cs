using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace Scheduler
{
    class Reader
    {
        Repository Repository = new Repository();

        public int GetEmployeeId(string firstname, string lastname)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT EmployeeID FROM employees as e WHERE e.FirstName = @firstname AND e.LastName = @lastname;";
                cmd.Parameters.AddWithValue("firstname", firstname);
                cmd.Parameters.AddWithValue("lastname", lastname);

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int employeeid = int.Parse(dr[0].ToString());
                return employeeid;
            }
        }

        public bool DoesEmployeeExist(string firstname, string lastname)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(c.firstname) AS result FROM employees c WHERE firstname = @firstname AND lastname = @lastname;";
                cmd.Parameters.AddWithValue("firstname", firstname);
                cmd.Parameters.AddWithValue("lastname", lastname);

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int count = int.Parse(dr[0].ToString());
                if (count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool DoesEmployeebyIdExist(int employeeid)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(e.employeeId) AS result FROM employees e WHERE employeeID = @employeeId;";
                cmd.Parameters.AddWithValue("employeeId", employeeid);


                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int count = int.Parse(dr[0].ToString());
                if (count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool DoesWorkablebyEIDExist(int employeeid)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(w.employeeId) AS result FROM workabledays w WHERE employeeID = @employeeId;";
                cmd.Parameters.AddWithValue("employeeId", employeeid);


                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int count = int.Parse(dr[0].ToString());
                if (count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
