using System;
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

        private int GetNumOfWorkableEmployeesMon()
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(EmployeeID) FROM workabledays as wd WHERE Monday = 1;";
                

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int employeecount = int.Parse(dr[0].ToString());
                return employeecount;
            }
        }
        private int GetNumOfWorkableEmployeesTues()
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(EmployeeID) FROM workabledays as wd WHERE Tuesday = 1;";


                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int employeecount = int.Parse(dr[0].ToString());
                return employeecount;
            }
        }
        private int GetNumOfWorkableEmployeesWed()
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(EmployeeID) FROM workabledays as wd WHERE Wednesday = 1;";


                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int employeecount = int.Parse(dr[0].ToString());
                return employeecount;
            }
        }
        private int GetNumOfWorkableEmployeesThurs()
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(EmployeeID) FROM workabledays as wd WHERE Thursday = 1;";


                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int employeecount = int.Parse(dr[0].ToString());
                return employeecount;
            }
        }
        private int GetNumOfWorkableEmployeesFri()
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(EmployeeID) FROM workabledays as wd WHERE Friday = 1;";


                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int employeecount = int.Parse(dr[0].ToString());
                return employeecount;
            }
        }
                   
        public int GetNumberOfVacaEmployees(DateTime date)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(EmployeeID) FROM vacation as v WHERE v.StartDate <= @date AND v.EndDate >= @date;";
                cmd.Parameters.AddWithValue("date", date);
                
                MySqlDataReader dr = cmd.ExecuteReader();
                int employeecount = 0;
                if (dr.Read())
                {
                    employeecount = int.Parse(dr[0].ToString());
                }
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
                //SELECT * FROM offdays as o WHERE o.StartDay between '2018-08-29' and '2018-08-29' OR o.EndDay between '2018-08-29' and '2018-08-29';

                cmd.CommandText = "SELECT Count(EmployeeID) FROM offdays as o WHERE o.StartDay <= @date AND o.EndDay >= @date";
                cmd.Parameters.AddWithValue("date", date);

                MySqlDataReader dr = cmd.ExecuteReader();
                int employeecount =  0;
                if(dr.Read())
                {
                    employeecount = int.Parse(dr[0].ToString());
                }
                
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
                cmd.CommandText = "SELECT Count(EmployeeID) FROM sick as s WHERE s.StartDate <= @date AND s.EndDate >= @date;";
                cmd.Parameters.AddWithValue("date", date);

                MySqlDataReader dr = cmd.ExecuteReader();
                int employeecount = 0;
                if (dr.Read())
                {
                    employeecount = int.Parse(dr[0].ToString());
                }
                return employeecount;
            }
        }
        public List<Employee> GetVacationingEmployees(DateTime date)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
            List<Employee> employees = new List<Employee>();
            using (conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT e.EmployeeID, e.LastName, e.FirstName FROM employees as e Join vacation as v on e.EmployeeID = v.EmployeeID Where v.StartDate <= @date AND v.EndDate >= @date;";
                cmd.Parameters.AddWithValue("date", date);

                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Employee employee = new Employee((dr["FirstName"].ToString()), dr["LastName"].ToString());
                    if (!employees.Contains(employee))
                    {
                        employees.Add(employee);
                    }
                    
                }
                return employees;
            }
        }
        public List<Employee> GetSickEmployees(DateTime date)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
            List<Employee> employees = new List<Employee>();
            using (conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT e.EmployeeID, e.LastName, e.FirstName FROM employees as e Join sick as s on e.EmployeeID = s.EmployeeID Where s.StartDate <= @date AND s.EndDate >= @date;";
                cmd.Parameters.AddWithValue("date", date);

                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Employee employee = new Employee((dr["FirstName"].ToString()), dr["LastName"].ToString());
                    employees.Add(employee);
                }
                return employees;
            }
        }
        public List<Employee> GetOffEmployees(DateTime date)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
            List<Employee> employees = new List<Employee>();
            using (conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT e.EmployeeID, e.LastName, e.FirstName FROM employees as e Join offdays as o on e.EmployeeID = o.EmployeeID Where o.StartDay <= @date AND o.EndDay >= @date;";
                cmd.Parameters.AddWithValue("date", date);

                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Employee employee = new Employee((dr["FirstName"].ToString()), dr["LastName"].ToString());
                    employees.Add(employee);
                }
                return employees;
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
        public int GetNumberOfWorkableEmployees(DateTime date)
        {
            if(date.DayOfWeek.ToString() == "Monday")
            {
                return GetNumOfWorkableEmployeesMon();
            }
            else if (date.DayOfWeek.ToString() == "Tuesday")
            {
                return GetNumOfWorkableEmployeesTues();
            }
            else if (date.DayOfWeek.ToString() == "Wednesday")
            {
                return GetNumOfWorkableEmployeesWed();
            }
            else if (date.DayOfWeek.ToString() == "Thursday")
            {
                return GetNumOfWorkableEmployeesThurs();
            }
            else if (date.DayOfWeek.ToString() == "Friday")
            {
                return GetNumOfWorkableEmployeesFri();
            }
            return 0;
        }
        public List<Employee> GetWorkableEmployees(DateTime date)
        {
            if (date.DayOfWeek.ToString() == "Monday")
            {
                MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
                List<Employee> employees = new List<Employee>();
                using (conn)
                {
                    conn.Open();

                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT e.EmployeeID, e.LastName, e.FirstName FROM employees as e Join workabledays as wd on e.EmployeeID = wd.EmployeeID Where wd.Monday = 1;";

                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Employee employee = new Employee((dr["FirstName"].ToString()), dr["LastName"].ToString());
                        employees.Add(employee);
                    }
                    return employees;
                }
                
            }
            else if (date.DayOfWeek.ToString() == "Tuesday")
            {
                MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
                List<Employee> employees = new List<Employee>();
                using (conn)
                {
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT e.EmployeeID, e.LastName, e.FirstName FROM employees as e Join workabledays as wd on e.EmployeeID = wd.EmployeeID Where Tuesday = 1;";


                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Employee employee = new Employee((dr["FirstName"].ToString()), dr["LastName"].ToString());
                        employees.Add(employee);
                    }
                    return employees;
                }
            }
            else if (date.DayOfWeek.ToString() == "Wednesday")
            {
                MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
                List<Employee> employees = new List<Employee>();
                using (conn)
                {
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT e.EmployeeID, e.LastName, e.FirstName FROM employees as e Join workabledays as wd on e.EmployeeID = wd.EmployeeID Where Wednesday = 1;";


                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Employee employee = new Employee((dr["FirstName"].ToString()), dr["LastName"].ToString());
                        employees.Add(employee);
                    }
                    return employees;
                }
            }
            else if (date.DayOfWeek.ToString() == "Thursday")
            {
                MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
                List<Employee> employees = new List<Employee>();
                using (conn)
                {
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT e.EmployeeID, e.LastName, e.FirstName FROM employees as e Join workabledays as wd on e.EmployeeID = wd.EmployeeID Where Thursday = 1;";


                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Employee employee = new Employee((dr["FirstName"].ToString()), dr["LastName"].ToString());
                        employees.Add(employee);
                    }
                    return employees;
                }
            }
            else if (date.DayOfWeek.ToString() == "Friday")
            {
                MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
                List<Employee> employees = new List<Employee>();
                using (conn)
                {
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT e.EmployeeID, e.LastName, e.FirstName FROM employees as e Join workabledays as wd on e.EmployeeID = wd.EmployeeID Where Friday = 1;";


                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Employee employee = new Employee((dr["FirstName"].ToString()), dr["LastName"].ToString());
                        employees.Add(employee);
                    }
                    return employees;
                }
            }
            return new List<Employee>();
        }
        public List<Employee> GetWorkableLateEmployees(DateTime date)
        {
            if (date.DayOfWeek.ToString() == "Monday")
            {
                MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
                List<Employee> employees = new List<Employee>();
                using (conn)
                {
                    conn.Open();

                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT e.EmployeeID, e.LastName, e.FirstName FROM employees as e Join workablelatedays as wd on e.EmployeeID = wd.EmployeeID Where wd.Monday = 1;";

                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Employee employee = new Employee((dr["FirstName"].ToString()), dr["LastName"].ToString());
                        employees.Add(employee);
                    }
                    return employees;
                }

            }
            else if (date.DayOfWeek.ToString() == "Tuesday")
            {
                MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
                List<Employee> employees = new List<Employee>();
                using (conn)
                {
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT e.EmployeeID, e.LastName, e.FirstName FROM employees as e Join workablelatedays as wd on e.EmployeeID = wd.EmployeeID Where Tuesday = 1;";


                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Employee employee = new Employee((dr["FirstName"].ToString()), dr["LastName"].ToString());
                        employees.Add(employee);
                    }
                    return employees;
                }
            }
            else if (date.DayOfWeek.ToString() == "Wednesday")
            {
                MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
                List<Employee> employees = new List<Employee>();
                using (conn)
                {
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT e.EmployeeID, e.LastName, e.FirstName FROM employees as e Join workablelatedays as wd on e.EmployeeID = wd.EmployeeID Where Wednesday = 1;";


                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Employee employee = new Employee((dr["FirstName"].ToString()), dr["LastName"].ToString());
                        employees.Add(employee);
                    }
                    return employees;
                }
            }
            else if (date.DayOfWeek.ToString() == "Thursday")
            {
                MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
                List<Employee> employees = new List<Employee>();
                using (conn)
                {
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT e.EmployeeID, e.LastName, e.FirstName FROM employees as e Join workablelatedays as wd on e.EmployeeID = wd.EmployeeID Where Thursday = 1;";


                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Employee employee = new Employee((dr["FirstName"].ToString()), dr["LastName"].ToString());
                        employees.Add(employee);
                    }
                    return employees;
                }
            }
            else if (date.DayOfWeek.ToString() == "Friday")
            {
                MySqlConnection conn = new MySqlConnection(Repository.ConnStr);
                List<Employee> employees = new List<Employee>();
                using (conn)
                {
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT e.EmployeeID, e.LastName, e.FirstName FROM employees as e Join workablelatedays as wd on e.EmployeeID = wd.EmployeeID Where Friday = 1;";


                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Employee employee = new Employee((dr["FirstName"].ToString()), dr["LastName"].ToString());
                        employees.Add(employee);
                    }
                    return employees;
                }
            }
            return new List<Employee>();
        }
        public int GetNumberOfEmployees()
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(EmployeeID)  FROM employees;";

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int employeecount = int.Parse(dr[0].ToString());
                return employeecount;
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

        public bool DoesVacationExist(int employeeid, DateTime start, DateTime end)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(v.employeeId) AS result FROM vacation v WHERE employeeID = @employeeId AND (StartDate BETWEEN @start and @end OR EndDate BETWEEN @start AND @end);";
                cmd.Parameters.AddWithValue("employeeId", employeeid);
                cmd.Parameters.AddWithValue("start", start);
                cmd.Parameters.AddWithValue("end", end);


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
        public bool DoesOffDayExist(int employeeid, DateTime start, DateTime end)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(o.employeeId) AS result FROM offdays o WHERE employeeID = @employeeId AND (StartDay BETWEEN @start and @end OR EndDay BETWEEN @start AND @end);";
                cmd.Parameters.AddWithValue("employeeId", employeeid);
                cmd.Parameters.AddWithValue("start", start);
                cmd.Parameters.AddWithValue("end", end);


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
        public bool DoesSickDayExist(int employeeid, DateTime start, DateTime end)
        {
            MySqlConnection conn = new MySqlConnection(Repository.ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(s.employeeId) AS result FROM vacation s WHERE employeeID = @employeeId AND (StartDate BETWEEN @start and @end OR EndDate BETWEEN @start AND @end);";
                cmd.Parameters.AddWithValue("employeeId", employeeid);
                cmd.Parameters.AddWithValue("start", start);
                cmd.Parameters.AddWithValue("end", end);


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
    }
}
