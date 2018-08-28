﻿using System;
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
                while (Reader.DoesEmployeeExist(firstname, lastname))
                {
                    Console.WriteLine("Sorry but that employee already exists, try again");
                    firstname = Lawyer.GetResponse("What is the first name of this employee?");
                    lastname = Lawyer.GetResponse("What is the last name of this employee?");
                }
                int vacations = Lawyer.GetInt("How many vacation days does this employee have for the year?");
                if (Lawyer.GetYesNo("Are you sure you want to add " + firstname + " " + lastname + " to the employees"))
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
            do
            {
                Employee employee = GetEmployee();
                int employeeid = Reader.GetEmployeeId(employee.FirstName, employee.LastName);
                List<DateTime> dates = GetDates("off-days");
                DateTime startdate = dates[0];
                DateTime enddate = dates[1];
                DateTime date = startdate;
                bool fact = true;
                do
                {
                    var day = date.DayOfWeek.ToString();
                    int notworkingemployees = Reader.GetNumberOfOffEmployees(date) + Reader.GetNumberOfSickEmployees(date) + Reader.GetNumberOfVacaEmployees(date);
                    int workableemployees = Reader.GetNumberOfWorkableEmployees(date);
                    if ((notworkingemployees - workableemployees) > (GetEmployees() / 2) - 3)
                    {
                        Console.WriteLine("Sorry but there is a conflict with this day:" + date.ToString());
                        fact = false;
                    }
                    int workingemployees = Reader.GetNumberOfOffEmployees(date) + Reader.GetNumberOfSickEmployees(date) + Reader.GetNumberOfVacaEmployees(date);
                    //if(workingemployees - )
                    Console.WriteLine(day + " current day");
                    date = date.AddDays(1.0);
                } while (!(date > enddate) && fact);
                bool conflictfact = DoesConflictExist(employeeid, startdate, enddate);
                if (fact && !(conflictfact))
                {
                    if(Lawyer.GetYesNo("Are you sure you want to add the off day/s of " + employee.PrintName() + " from " + startdate.ToLongDateString() + " to " + enddate.ToLongDateString()))
                    {
                        Creator.AddOffDay(employeeid, startdate, enddate);
                    }
                    
                }
                else if (conflictfact)
                {
                    Console.WriteLine("Sorry but there was a conflict with trying to schedule this employee for the given off days.");

                }
                Console.Clear();
            } while (Lawyer.GetYesNo("Do you want to add another off day for an employee?"));

        }
        /// <summary>
        /// validates inforamtion form user before adding vacation
        /// </summary>

        public void AddVacation()
        {
            do
            {
                Employee employee = GetEmployee();
                int employeeid = Reader.GetEmployeeId(employee.FirstName, employee.LastName);
                List<DateTime> dates = GetDates("vacation");
                DateTime startdate = dates[0];
                DateTime enddate = dates[1];
                DateTime date = startdate;
                int numofvacationdays = 0;
                bool fact = true;
                do
                {
                    var day = date.DayOfWeek.ToString();
                    int notworkingemployees = Reader.GetNumberOfOffEmployees(date) + Reader.GetNumberOfSickEmployees(date) + Reader.GetNumberOfVacaEmployees(date);
                    int workableemployees = Reader.GetNumberOfWorkableEmployees(date);
                    if ((notworkingemployees - workableemployees) > (GetEmployees() / 2) - 3 || numofvacationdays > Reader.GetNumberOfVacations(employeeid))
                    {
                        Console.WriteLine("Sorry but there is a conflict with this day:" + date.ToString());
                        fact = false;
                    }
                    numofvacationdays++;
                    date = date = date.AddDays(1.00);

                } while ((!(date > enddate)) && fact);
                bool conflictfact = DoesConflictExist(employeeid, startdate, enddate);
                if (fact && !(conflictfact))
                {
                    if((Lawyer.GetYesNo("Are you sure you want to add the vacation day/s of " + employee.PrintName() + " from " + startdate.ToLongDateString() + " to " + enddate.ToLongDateString())))
                    {
                        Creator.AddVacation(employeeid, startdate, enddate);
                        Updater.RemoveVacationsByEmployeeID(employeeid, numofvacationdays);
                    }
                    
                }
                else if (conflictfact)
                {
                    Console.WriteLine("Sorry but there was a conflict with trying to schedule this employee for the given vacation days.");
                    Console.ReadLine();
                }
                Console.Clear();
            } while (Lawyer.GetYesNo("Do you want to add another vacation for another employee?"));
        }
        public void AddSickDay()
        {
            do
            {
                Employee employee = GetEmployee();
                int employeeid = Reader.GetEmployeeId(employee);
                List<DateTime> dates = GetDates("sick-leave");
                DateTime startdate = dates[0];
                DateTime enddate = dates[1];
                DateTime date = startdate;
                bool fact = true;
                do
                {
                    var day = date.DayOfWeek.ToString();
                    int notworkingemployees = Reader.GetNumberOfOffEmployees(date) + Reader.GetNumberOfSickEmployees(date) + Reader.GetNumberOfVacaEmployees(date);
                    int workableemployees = Reader.GetNumberOfWorkableEmployees(date);
                    if ((notworkingemployees - workableemployees) > (GetEmployees() / 2) - 3)
                    {
                        Console.WriteLine("Sorry but there is a conflict with this day:" + date.ToString());
                        fact = false;
                    }
                    //int workingemployees = Reader.GetNumberOfOffEmployees(date) + Reader.GetNumberOfSickEmployees(date) + Reader.GetNumberOfVacaEmployees(date);
                    Console.WriteLine(day + " current day");
                    date.AddDays(1.0);
                } while ((!(date > enddate)) && fact);
                bool conflictfact = DoesConflictExist(employeeid, startdate, enddate);
                if (fact && !(conflictfact))
                {
                    if(Lawyer.GetYesNo("Are you sure you want to add the sick day/s of " + employee.PrintName() + " from " + startdate.ToLongDateString() + " to " + enddate.ToLongDateString()))
                    {
                        Creator.AddSickDay(employeeid, startdate, enddate);
                    }
                    
                }
                else if (conflictfact)
                {
                    Console.WriteLine("Sorry but there was a conflict with trying to schedule this employee for the given sick days.");
                    Console.ReadLine();
                }
                Console.Clear();
            } while (Lawyer.GetYesNo("Do you want to add another sick-leave for another employee?"));
        }
        private List<DateTime> GetDates(string word)
        {
            List<DateTime> dates = new List<DateTime>();
            int year = Lawyer.GetYear("What year is this " + word + " be taking place?");
            int month = Lawyer.GetMonth("What numerical month is this " + word +"  be taking place?");
            int startday = Lawyer.GetDay("What day of the month will this " + word +" start?", month, year);
            int endday = Lawyer.GetDay("What day of the month will this " + word + " end?", month, year);
            DateTime startdate = new DateTime(year, month, startday);
            DateTime enddate = new DateTime(year, month, endday);
            dates.Add(startdate);
            dates.Add(enddate);
            return dates;
        }
        private bool DoesConflictExist(int employeeid, DateTime start, DateTime end)
        {
            return (Reader.DoesOffDayExist(employeeid, start, end) || Reader.DoesSickDayExist(employeeid, start, end) || Reader.DoesVacationExist(employeeid, start, end));
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
        public void UpdateVacations()
        {
            do
            {
                Employee employee = GetEmployee();
                int employeeid = Reader.GetEmployeeId(employee.FirstName, employee.LastName);
                if(Lawyer.GetYesNo("Do you want to add vacations?"))
                {
                    int days = Lawyer.GetInt("How many days do you want to add?");
                    if (Lawyer.GetYesNo("Are you sure you want to set vacations from " + Reader.GetNumberOfVacations(employeeid) + " to be " + (Reader.GetNumberOfVacations(employeeid) + days) + "?"))
                    {
                        Updater.AddVacationsByEmployeeID(employeeid, days);
                        Console.Clear();
                    }
                }
                else if(Lawyer.GetYesNo("Do you want to take off vacations?"))
                {
                    int days = Lawyer.GetInt("How may days  do you want to subtract?");
                    if(Lawyer.GetYesNo("Are you sure you want to set vacations from " + Reader.GetNumberOfVacations(employeeid) + " to be " + (Reader.GetNumberOfVacations(employeeid) - days) + "?"))
                    {
                        Updater.RemoveVacationsByEmployeeID(employeeid, days);
                        Console.Clear();
                    }
                }
            } while (Lawyer.GetYesNo("Do you want to update another employees number of vacation days?"));
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
