﻿using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace Scheduler
{
    class Reader
    {
        Repository Repository = new Repository();
        public List<Employee> Employees { get; private set; }

        public Reader()
        {
            Employees = ReadEmployees();
        }
        public List<Employee> ReadEmployees()
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
            List<Employee> employees = new List<Employee>();
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM employees;";

                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Employee employee = new Employee((dr["FirstName"].ToString()), dr["LastName"].ToString());
                    employees.Add(employee);
                }
                return employees;
            }
        }

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
        public int GetEmployeeId(Employee employee)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT EmployeeID FROM employees as e WHERE e.FirstName = @firstname AND e.LastName = @lastname;";
                cmd.Parameters.AddWithValue("firstname", employee.FirstName);
                cmd.Parameters.AddWithValue("lastname", employee.LastName);

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int employeeid = int.Parse(dr[0].ToString());
                return employeeid;
            }
        }
        public int GetNumberOfVacations(int employeeid)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Vacations FROM employees as e WHERE employeeid = @employeeid";
                cmd.Parameters.AddWithValue("employeeid", employeeid);
                
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int vacations = int.Parse(dr[0].ToString());
                return vacations;
            }
        }

        //public int GetNumberOfWorkableEmployees(DateTime date)
        //{
        //    MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

        //    using (conn)
        //    {
        //        conn.Open();
        //        var day = date.DayOfWeek.ToString();
        //        MySqlCommand cmd = conn.CreateCommand();
        //        cmd.CommandText = "SELECT Count(EmployeeID) FROM workabledays as wd WHERE ;";
        //        cmd.Parameters.AddWithValue("date", date);

        //        MySqlDataReader dr = cmd.ExecuteReader();
        //        dr.Read();
        //        int employeecount = int.Parse(dr[0].ToString());
        //        return employeecount;
        //    }
        //}
        
        public int GetNumberOfVacaEmployees(DateTime date)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(EmployeeID) FROM vacation as v WHERE @date BETWEEN StartDate AND EndDate;";
                cmd.Parameters.AddWithValue("date", date);
                
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int employeecount = int.Parse(dr[0].ToString());
                return employeecount;
            }
        }
        public int GetNumberOfOffEmployees(DateTime date)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(EmployeeID) FROM offdays as o WHERE @date BETWEEN StartDate AND EndDate;";
                cmd.Parameters.AddWithValue("date", date);

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int employeecount = int.Parse(dr[0].ToString());
                return employeecount;
            }
        }
        public int GetNumberOfSickEmployees(DateTime date)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(EmployeeID) FROM sick as o WHERE @date BETWEEN StartDate AND EndDate;";
                cmd.Parameters.AddWithValue("date", date);

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int employeecount = int.Parse(dr[0].ToString());
                return employeecount;
            }
        }
        public int GetNumberOfEmployeeWSameName(string lastname)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
            //int product = GetCategoryID(categoryId);
            using (conn)
            {
                conn.Open();
                int count = 0;
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(EmployeeID) as EmployeeID FROM employees as e WHERE e.LastName = @lastname;";
                cmd.Parameters.AddWithValue("lastname", lastname);

                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                   count = (int.Parse(dr["EmployeeID"].ToString()));
                }
                return count;
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
        public bool DoesEmployeeExist(string lastname)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(c.firstname) AS result FROM employees c WHERE lastname = @lastname;";
                cmd.Parameters.AddWithValue("lastname", lastname);

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int count = int.Parse(dr[0].ToString());
                if (count >= 1)
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
        public bool DoesWorkableLatebyEIDExist(int employeeid)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(w.employeeId) AS result FROM workablelatedays w WHERE employeeID = @employeeId;";
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
