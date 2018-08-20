using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
    class Schedule
    {
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public int NumberOfEmployees { get; private set; }//debateable/deletable
        public int DaysWorkable { get; private set; }
        public Lawyer Lawyer { get; set; } = new Lawyer();
        private Creator Creator { get; set; } = new Creator();
        private Reader Reader { get; set; } = new Reader();
        public Schedule()
        {            
            DaysWorkable = Lawyer.GetInt("How many days of the week is this company open for?");
            NumberOfEmployees = Lawyer.GetInt("How many employees work in this company?");
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
                if(Lawyer.GetYesNo("Are you sure you wnat to add " + firstname + " " + lastname + " to the employees"))
                {
                    Creator.AddEmployee(firstname, lastname, vacations);
                }
            } while (Lawyer.GetYesNo("Do you want to add another employee?"));
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
