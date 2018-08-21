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
        public Lawyer Lawyer { get; set; } = new Lawyer();
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
            string firstname = Lawyer.GetResponse("What is the first name of the employee whose days you want to establish?");
            string lastname = Lawyer.GetResponse("What is the last name  of the employee whose days you want to establish?");
            while(!(Reader.DoesEmployeeExist(firstname, lastname)))
            {
                Console.WriteLine("Sorry no such employee exist, try again");
                firstname = Lawyer.GetResponse("What is the first name of the employee whose days you want to establish?");
                lastname = Lawyer.GetResponse("What is the last name  of the employee whose days you want to establish?");
            }
            if(Reader.DoesWorkablebyEIDExist(Reader.GetEmployeeId(firstname, lastname)))
            {
                Console.WriteLine("Sorry but that employee already has their workable days established.");
            }
            int employeeid = Reader.GetEmployeeId(firstname, lastname);
            int mon = 0;
            int tues = 0;
            int wed = 0;
            int thurs = 0;
            int fri = 0;
            for(int i = 0; i < 5; i++)
            {
                string day = GetDay(i +1);
                if(i == 0)
                {
                    if(Lawyer.GetYesNo("Is the employee " + firstname + " " + lastname + " able to work " + day))
                    {
                        mon = 1;
                    }
                }
                else if(i == 1)
                {
                    if(Lawyer.GetYesNo("Is the employee " + firstname + " " + lastname + " able to work " + day))
                    {
                        tues = 1;
                    }

                }
                else if (i == 2)
                {
                    if(Lawyer.GetYesNo("Is the employee " + firstname + " " + lastname + " able to work " + day))
                    {
                        wed = 1;
                    }
                }
                else if (i == 3)
                {
                    if(Lawyer.GetYesNo("Is the employee " + firstname + " " + lastname + " able to work " + day))
                    {
                        thurs = 1;
                    }
                }
                else if (i == 4)
                {
                    if(Lawyer.GetYesNo("Is the employee " + firstname + " " + lastname + " able to work " + day))
                    {
                        fri = 1;
                    }
                }
            }
            if(Lawyer.GetYesNo("Are you sure you want to set the days for " + firstname + " " + lastname + " and their workable late days to be Monday: " + mon + " Tuesday:" + tues + " Wednesday: " + wed + " Thursday: " + thurs + " Friday: " + fri))
            {
                Creator.AddWorkableDays(employeeid, mon, tues, wed, thurs, fri);
                Console.Clear();
            }
            Console.Clear();


        }
        public void AddOffDay()
        {
            string firstname = Lawyer.GetResponse("What is the first name of the employee whose off days you want to set?");
            string lastname = Lawyer.GetResponse("What is the last name  of the employee whose off days you want to set?");
            while (!(Reader.DoesEmployeeExist(firstname, lastname)))
            {
                Console.WriteLine("Sorry no such employee exist, try again");
                firstname = Lawyer.GetResponse("What is the first name of the employee whose off days you want to set?");
                lastname = Lawyer.GetResponse("What is the last name  of the employee whose off days you want to set?");
            }
            int employeeid = Reader.GetEmployeeId(firstname, lastname);
            int year = Lawyer.GetYear("What year is this vacation taking place?");
            int month = Lawyer.GetMonth("What numerical month is this vacation taking place?");
            int startday = Lawyer.GetDay("What day of the month will this vacation start?", month, year);
            int endday = Lawyer.GetDay("What day of the month will this vacation end?", month, year);
            DateTime startdate = new DateTime(year, month, startday);
            DateTime enddate = new DateTime(year, month, endday);
            Creator.AddVacation(employeeid, startdate, enddate);
            Console.Clear();

        }
        public void AddVacation()
        {
            string firstname = Lawyer.GetResponse("What is the first name of the employee whose vacation do you want to schedule?");
            string lastname = Lawyer.GetResponse("What is the last name  of the employee whose vacation do you want to schedule?");
            while (!(Reader.DoesEmployeeExist(firstname, lastname)))
            {
                Console.WriteLine("Sorry no such employee exist, try again");
                firstname = Lawyer.GetResponse("What is the first name of the employee whose vacation do you want to schedule?");
                lastname = Lawyer.GetResponse("What is the last name  of the employee whose vacation do you want to schedule?");
            }
            int employeeid = Reader.GetEmployeeId(firstname, lastname);
            int year = Lawyer.GetYear("What year is this off days be taking place?");
            int month = Lawyer.GetMonth("What numerical month is this off days be taking place?");
            int startday = Lawyer.GetDay("What day of the month will this off days start?", month, year);
            int endday = Lawyer.GetDay("What day of the month will this off days end?", month, year);
            DateTime startdate = new DateTime(year, month, startday);
            DateTime enddate = new DateTime(year, month, endday);
            Creator.AddOffDay(employeeid, startdate, enddate);
            Console.Clear();
        }
        public void AddSickDay()
        {
            string firstname = Lawyer.GetResponse("What is the first name of the employee whose sick days you want to set?");
            string lastname = Lawyer.GetResponse("What is the last name  of the employee whose sick days you want to set?");
            while (!(Reader.DoesEmployeeExist(firstname, lastname)))
            {
                Console.WriteLine("Sorry no such employee exist, try again");
                firstname = Lawyer.GetResponse("What is the first name of the employee whose sick days you want to set?");
                lastname = Lawyer.GetResponse("What is the last name  of the employee whose sick days you want to set?");
            }
            int employeeid = Reader.GetEmployeeId(firstname, lastname);
            int year = Lawyer.GetYear("What year is this sick/recovery be taking place?");
            int month = Lawyer.GetMonth("What numerical month is this sick/recovery be taking place?");
            int startday = Lawyer.GetDay("What day of the month will this sick/recovery start?", month, year);
            int endday = Lawyer.GetDay("What day of the month will this sick/recovery end?", month, year);
            DateTime startdate = new DateTime(year, month, startday);
            DateTime enddate = new DateTime(year, month, endday);
            Creator.AddSickDay(employeeid, startdate, enddate);
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
