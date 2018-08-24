using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
    class Schedule
    {
        public static int NumberOfEmployees { get; private set; }//debateable/deletable
        public static int DaysWorkable { get; private set; }
        private Lawyer Lawyer { get; set; } = new Lawyer();
        private Creator Creator { get; set; } = new Creator();
        private Reader Reader { get; set; } = new Reader();
        private Updater Updater { get; set; } = new Updater();

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
                    ReadEmployees();
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
        public int GetEmployees()
        {
            return Reader.GetNumberOfEmployees();
        }
        public void SetWorkableDays()
        {
            do
            {
                Employee employee = GetEmployee();
                int employeeid = Reader.GetEmployeeId(employee.FirstName, employee.LastName);
                while (Reader.DoesWorkablebyEIDExist(employeeid))
                {
                    Console.WriteLine("Sorry but that employee's workable days have been set, try a different employee.");
                    employee = GetEmployee();
                    employeeid = Reader.GetEmployeeId(employee.FirstName, employee.LastName);
                }
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
        public void SetWorkableLateDays()
        {
            do
            {
                Employee employee = GetEmployee();
                int employeeid = Reader.GetEmployeeId(employee.FirstName, employee.LastName);
                while (Reader.DoesWorkableLatebyEIDExist(employeeid))
                {
                    Console.WriteLine("Sorry but that employee's workable late days have been set, try a different employee.");
                    employee = GetEmployee();
                    employeeid = Reader.GetEmployeeId(employee.FirstName, employee.LastName);
                }
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
                        if (Lawyer.GetYesNo("Is the employee " + employee.FirstName + " " + employee.LastName + " able to work late on " + day + "?"))
                        {
                            mon = 1;
                            monFact = true;
                        }
                    }
                    else if (i == 1)
                    {
                        if (Lawyer.GetYesNo("Is the employee " + employee.FirstName + " " + employee.LastName + " able to work late on " + day + "?"))
                        {
                            tues = 1;
                            tuesFact = true;
                        }

                    }
                    else if (i == 2)
                    {
                        if (Lawyer.GetYesNo("Is the employee " + employee.FirstName + " " + employee.LastName + " able to work late on " + day + "?"))
                        {
                            wed = 1;
                            wedFact = true;
                        }
                    }
                    else if (i == 3)
                    {
                        if (Lawyer.GetYesNo("Is the employee " + employee.FirstName + " " + employee.LastName + " able to work late on " + day + "?"))
                        {
                            thurs = 1;
                            thursFact = true;
                        }
                    }
                    else if (i == 4)
                    {
                        if (Lawyer.GetYesNo("Is the employee " + employee.FirstName + " " + employee.LastName + " able to work late on " + day + "?"))
                        {
                            fri = 1;
                            friFact = true;
                        }
                    }
                }
                if (Lawyer.GetYesNo("Are you sure you want to set the days for " + employee.FirstName + " " + employee.LastName + " and their workable late days to be Monday: " + monFact + " Tuesday:" + tuesFact + " Wednesday: " + wedFact + " Thursday: " + thursFact + " Friday: " + friFact))
                {
                    Creator.AddWorkableLateDays(employeeid, mon, tues, wed, thurs, fri);
                    Console.Clear();
                }
                Console.Clear();
            } while (Lawyer.GetYesNo("Do you want to set the workable days for another employee"));
        }
        public void AddOffDay()
        {
            Employee employee = GetEmployee();
            int employeeid = Reader.GetEmployeeId(employee.FirstName, employee.LastName);
            int year = Lawyer.GetYear("What year is these off days taking place?");
            int month = Lawyer.GetMonth("What numerical month is these off days taking place?");
            int startday = Lawyer.GetDay("What day of the month will these off days start?", month, year);
            int endday = Lawyer.GetDay("What day of the month will these off days end?", month, year);
            DateTime startdate = new DateTime(year, month, startday);
            DateTime enddate = new DateTime(year, month, endday);
            DateTime date = startdate;
            bool fact = true;
            do
            {
                var day = date.DayOfWeek.ToString();
                int notworkingemployees = Reader.GetNumberOfOffEmployees(date) + Reader.GetNumberOfSickEmployees(date) + Reader.GetNumberOfVacaEmployees(date);
                int workableemployees = Reader.GetNumberOfWorkableEmployees(date);
                if (Math.Abs(notworkingemployees - workableemployees) > (GetEmployees() / 2) - 3)
                {
                    Console.WriteLine("Sorry but there is a conflict with this day:" + date.ToString());
                    fact = false;
                }
                int workingemployees = Reader.GetNumberOfOffEmployees(date) + Reader.GetNumberOfSickEmployees(date) + Reader.GetNumberOfVacaEmployees(date);
                //if(workingemployees - )
                Console.WriteLine(day + " current day");
                date = date.AddDays(1.0);
            } while (!(date > enddate)&& fact);
            if((Lawyer.GetYesNo("Are you sure you want to add the vacation of " + employee.PrintName() + " from " + startdate.ToLongDateString() + " to " + enddate.ToLongDateString())) && fact)
            {
                Creator.AddVacation(employeeid, startdate, enddate);
            }
            Console.Clear();

        }
        public void AddVacation()
        {
            Employee employee = GetEmployee();
            int employeeid = Reader.GetEmployeeId(employee.FirstName, employee.LastName);
            int year = Lawyer.GetYear("What year is this vacation be taking place?");
            int month = Lawyer.GetMonth("What numerical month is this vacation be taking place?");
            int startday = Lawyer.GetDay("What day of the month will this vacation start?", month, year);
            int endday = Lawyer.GetDay("What day of the month will this vacation end?", month, year);
            DateTime startdate = new DateTime(year, month, startday);
            DateTime enddate = new DateTime(year, month, endday);
            DateTime date = startdate;
            bool fact = true;
            do
            {
                var day = date.DayOfWeek.ToString();
                int notworkingemployees = Reader.GetNumberOfOffEmployees(date) + Reader.GetNumberOfSickEmployees(date) + Reader.GetNumberOfVacaEmployees(date);
                int workableemployees = Reader.GetNumberOfWorkableEmployees(date);
                if(Math.Abs(notworkingemployees - workableemployees) > (GetEmployees() / 2) - 3)
                {
                    Console.WriteLine("Sorry but there is a conflict with this day:" + date.ToString());
                    fact = false;
                }
                //list of Employees that are not able to work via, sick, off, vacation, & workable tables + count
                //int workableemployees = Reader.
                //if(notworkingemployees - workableemployees)
                Console.WriteLine(day + " current day");
                date = date = date.AddDays(1.00);
                
            } while ((!(date > enddate )) && fact);
            if ((Lawyer.GetYesNo("Are you sure you want to add the off day/s of " + employee.PrintName() + " from " + startdate.ToLongDateString() + " to " + enddate.ToLongDateString())) && fact)
            {
                Creator.AddOffDay(employeeid, startdate, enddate);
                int numofvacationdays = DateTime.Compare(startdate, enddate);
                Updater.UpdateVactionsByEmployeeID(employeeid, numofvacationdays);
            }
            Console.Clear();
        }
        public void AddSickDay()
        {
            Employee employee = GetEmployee();
            int employeeid = Reader.GetEmployeeId(employee);
            int year = Lawyer.GetYear("What year is this sick/recovery be taking place?");
            int month = Lawyer.GetMonth("What numerical month is this sick/recovery be taking place?");
            int startday = Lawyer.GetDay("What day of the month will this sick/recovery start?", month, year);
            int endday = Lawyer.GetDay("What day of the month will this sick/recovery end?", month, year);
            DateTime startdate = new DateTime(year, month, startday);
            DateTime enddate = new DateTime(year, month, endday);
            DateTime date = startdate;
            bool fact = true;
            do
            {
                var day = date.DayOfWeek.ToString();
                int notworkingemployees = Reader.GetNumberOfOffEmployees(date) + Reader.GetNumberOfSickEmployees(date) + Reader.GetNumberOfVacaEmployees(date);
                int workableemployees = Reader.GetNumberOfWorkableEmployees(date);
                if (Math.Abs(notworkingemployees - workableemployees) > (GetEmployees() / 2) - 3)
                {
                    Console.WriteLine("Sorry but there is a conflict with this day:" + date.ToString());
                    fact = false;
                }
                //int workingemployees = Reader.GetNumberOfOffEmployees(date) + Reader.GetNumberOfSickEmployees(date) + Reader.GetNumberOfVacaEmployees(date);
                Console.WriteLine(day + " current day");
                date.AddDays(1.0);
            } while ((!(date > enddate)) && fact);
            if ((Lawyer.GetYesNo("Are you sure you want to add the sick day/s of " + employee.PrintName() + " from " + startdate.ToLongDateString() + " to " + enddate.ToLongDateString())) && fact)
            {
                Creator.AddSickDay(employeeid, startdate, enddate);
            }
            Console.Clear();
        }

        public void UpdateWorkableDays()
        {
            do
            {
                Employee employee = GetEmployee();
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
                    Updater.UpdateWorkableDays(employeeid, mon, tues, wed, thurs, fri);
                    Console.Clear();
                }
                Console.Clear();
            } while (Lawyer.GetYesNo("Do you want to update the workable days for another employee"));
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
        private void ReadEmployees()
        {
            foreach (var employee in Reader.Employees)
            {
                Console.Write("First Name: " + employee.FirstName + " Last Name: " + employee.LastName + " ");
                if (!(Lawyer.GetYesNo("Do you want see another employee?")))
                {
                    break;
                }
                Console.Clear();
            }
        }
        private Employee GetEmployee()
        {
            if (Lawyer.GetYesNo("Do you know the last name of the employee?"))
            {
                string lastname = Lawyer.GetResponse("What is the last name of the employee?");
                while (!(Reader.DoesEmployeeExist(lastname)))
                {
                    lastname = Lawyer.GetResponse("Sorry no employee under that last name exist.\nWhat is the name of the employee?");
                }
                foreach (var employee in Reader.Employees)
                {
                    if (employee.LastName == lastname && Reader.GetNumberOfEmployeeWSameName(lastname) == 1)
                    {
                        return employee;
                    }
                    else if(employee.LastName == lastname)
                    {
                        string firstletter = Lawyer.GetResponse("Sorry there are two or more employees with that last name.\nWhat is the first letter of the employees name");
                        if(employee.FirstName[0].ToString() == firstletter)
                        {
                            return employee;
                        }
                        else
                        {
                            Console.WriteLine("Guess you are looking for the other");
                            continue;
                        }
                    }
                }
            }
            foreach (var employee in Reader.Employees)
            {
                Console.Write("First Name: " + employee.FirstName + " \nLast Name: " + employee.LastName + " ");
                if ((Lawyer.GetYesNo("Is this the employee you want to use?")))
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
