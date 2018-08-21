using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
    class Schedule
    {
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public static int NumberOfEmployees { get; private set; }//debateable/deletable
        public static int DaysWorkable { get; private set; }
        private Lawyer Lawyer { get; set; } = new Lawyer();
        private Creator Creator { get; set; } = new Creator();
        private Reader Reader { get; set; } = new Reader();

        public Schedule()
        {            
            //DaysWorkable = Lawyer.GetInt("How many days of the week is this company open for?");
            //NumberOfEmployees = Lawyer.GetInt("How many employees work in this company?");
        }
        public void AddEmployee()
        {
            do
            {
                if (Lawyer.GetYesNo("Do you want to see all existing employees?"))
                {
                    ReadEmployees(Reader);
                }
                string firstname = Lawyer.GetResponse("What is the first name of this employee?");
                string lastname = Lawyer.GetResponse("What is the last name of this employee?");
                while(Reader.DoesEmployeeExist(firstname, lastname))
                {
                    Console.WriteLine("Sorry but that employee already exists, try again");
                    firstname = Lawyer.GetResponse("What is the first name of this employee?");
                    lastname = Lawyer.GetResponse("What is the last name of this employee?");
                }
                int vacations = Lawyer.GetInt("How many vacation days does this employee have for the year?");
                if(Lawyer.GetYesNo("Are you sure you want to add " + firstname + " " + lastname + " to the employees"))
                {
                    Creator.AddEmployee(firstname, lastname, vacations);
                    NumberOfEmployees++;
                    Console.Clear();
                }
                Console.Clear();
            } while (Lawyer.GetYesNo("Do you want to add another employee?"));
        }
        public void SetWorkableDays()
        {
            do
            {
                Employee employee = GetEmployee(Reader);
                int employeeid = Reader.GetEmployeeId(employee.FirstName, employee.LastName);
                int mon = 0;
                int tues = 0;
                int wed = 0;
                int thurs = 0;
                int fri = 0;
                bool monFact = false, tuesFact = false, wedFact = false, thursFact = false, friFact = false;
                for (int i = 0; i < 5; i++)
                {
                    string day = GetDay(i + 1);
                    if (i == 0)
                    {
                        if (Lawyer.GetYesNo("Is the employee " + employee.FirstName + " " + employee.LastName + " able to work " + day))
                        {
                            mon = 1;
                            monFact = true;
                        }
                    }
                    else if (i == 1)
                    {
                        if (Lawyer.GetYesNo("Is the employee " + employee.FirstName + " " + employee.LastName + " able to work " + day))
                        {
                            tues = 1;
                            tuesFact = true;
                        }

                    }
                    else if (i == 2)
                    {
                        if (Lawyer.GetYesNo("Is the employee " + employee.FirstName + " " + employee.LastName + " able to work " + day))
                        {
                            wed = 1;
                            wedFact = true;
                        }
                    }
                    else if (i == 3)
                    {
                        if (Lawyer.GetYesNo("Is the employee " + employee.FirstName + " " + employee.LastName + " able to work " + day))
                        {
                            thurs = 1;
                            thursFact = true;
                        }
                    }
                    else if (i == 4)
                    {
                        if (Lawyer.GetYesNo("Is the employee " + employee.FirstName + " " + employee.LastName + " able to work " + day))
                        {
                            fri = 1;
                            friFact = true;
                        }
                    }
                }
                if (Lawyer.GetYesNo("Are you sure you want to set the days for " + employee.FirstName + " " + employee.LastName + " and their workable late days to be Monday: " + monFact + " Tuesday:" + tuesFact + " Wednesday: " + wedFact + " Thursday: " + thursFact + " Friday: " + friFact))
                {
                    Creator.AddWorkableDays(employeeid, mon, tues, wed, thurs, fri);
                    Console.Clear();
                }
                Console.Clear();
            } while (Lawyer.GetYesNo("Do you want to set the workable days for another employee"));


        }
        public void AddOffDay()
        {
            Employee employee = GetEmployee(Reader);
            int employeeid = Reader.GetEmployeeId(employee.FirstName, employee.LastName);
            int year = Lawyer.GetYear("What year is this vacation taking place?");
            int month = Lawyer.GetMonth("What numerical month is this vacation taking place?");
            int startday = Lawyer.GetDay("What day of the month will this vacation start?", month, year);
            int endday = Lawyer.GetDay("What day of the month will this vacation end?", month, year);
            DateTime startdate = new DateTime(year, month, startday);
            DateTime enddate = new DateTime(year, month, endday);
            if(Lawyer.GetYesNo("Are you sure you want to add the vacation of " + employee.PrintName() + " from " + startdate.ToLongDateString() + " to " + enddate.ToLongDateString()))
            {
                Creator.AddVacation(employeeid, startdate, enddate);
            }
            Console.Clear();

        }
        public void AddVacation()
        {
            Employee employee = GetEmployee(Reader);
            int employeeid = Reader.GetEmployeeId(employee.FirstName, employee.LastName);
            int year = Lawyer.GetYear("What year is this off days be taking place?");
            int month = Lawyer.GetMonth("What numerical month is this off days be taking place?");
            int startday = Lawyer.GetDay("What day of the month will this off days start?", month, year);
            int endday = Lawyer.GetDay("What day of the month will this off days end?", month, year);
            DateTime startdate = new DateTime(year, month, startday);
            DateTime enddate = new DateTime(year, month, endday);
            if (Lawyer.GetYesNo("Are you sure you want to add the off day/s of " + employee.PrintName() + " from " + startdate.ToLongDateString() + " to " + enddate.ToLongDateString()))
            {
                Creator.AddOffDay(employeeid, startdate, enddate);
            }
            Console.Clear();
        }
        public void AddSickDay()
        {
            Employee employee = GetEmployee(Reader);
            int employeeid = Reader.GetEmployeeId(employee);
            int year = Lawyer.GetYear("What year is this sick/recovery be taking place?");
            int month = Lawyer.GetMonth("What numerical month is this sick/recovery be taking place?");
            int startday = Lawyer.GetDay("What day of the month will this sick/recovery start?", month, year);
            int endday = Lawyer.GetDay("What day of the month will this sick/recovery end?", month, year);
            DateTime startdate = new DateTime(year, month, startday);
            DateTime enddate = new DateTime(year, month, endday);
            if (Lawyer.GetYesNo("Are you sure you want to add the sick day/s of " + employee.PrintName() + " from " + startdate.ToLongDateString() + " to " + enddate.ToLongDateString()))
            {
                Creator.AddSickDay(employeeid, startdate, enddate);
            }
            Console.Clear();
        }

        private string GetDay(int day)
        {
            if(day == 1)
            {
                return "Monday";
            }
            else if(day == 2)
            {
                return "Tuesday";
            }
            else if(day == 3)
            {
                return "Wednesday";
            }
            else if(day == 4)
            {
                return "Thursday";
            }
            return "Friday";
        }
        private void ReadEmployees(Reader reader)
        {
            foreach (var employee in reader.Employees)
            {
                Console.Write("First Name: " + employee.FirstName + " Last Name: " + employee.LastName + " ");
                if (!(Lawyer.GetYesNo("Do you want see another employee?")))
                {
                    break;
                }
                Console.Clear();
            }
        }
        private Employee GetEmployee(Reader reader)
        {
            //string firstname = Lawyer.GetResponse("What is the first name of this employee?");
            //string lastname = Lawyer.GetResponse("What is the last name of this employee?");
            //while (!(Reader.DoesEmployeeExist(firstname, lastname)))
            //{
            //    Console.WriteLine("Sorry but that employee does not exist, try again");
            //    firstname = Lawyer.GetResponse("What is the first name of this employee?");
            //    lastname = Lawyer.GetResponse("What is the last name of this employee?");
            //}
            foreach (var employee in reader.Employees)
            {
                Console.Write("First Name: " + employee.FirstName + " \nLast Name: " + employee.LastName + " ");
                if((Lawyer.GetYesNo("Is this the employee you want to use?")))
                {
                    return employee;
                }
                Console.Clear();              
            }
            Employee bob = new Employee("Bob", "Bob");
            return bob;
        }
        //for(int i = 0; i<NumberOfEmployees; i++)
        //    {
        //        Employee tempemployee = new Employee();
        //Console.WriteLine("For Employee #:" + (i + 1));
        //        tempemployee.FirstName = lawyer.GetResponse("What is the first name of this employee?");
        //        tempemployee.LastName = lawyer.GetResponse("What is the first name of this employee?");
        //       // tempemployee.DaysWorkable = lawyer.GetResponse("Blah", DaysWorkable);
        //    }
    }
}
