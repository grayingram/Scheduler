using System;

namespace Scheduler
{
    class Program
    {
        private static Reader Reader { get; set; } = new Reader();
        static void Main(string[] args)
        {
            Schedule schedule = new Schedule();
            Lawyer lawyer = new Lawyer();
            Manager manager = new Manager();
            
            if(lawyer.GetYesNo("Do you want to build the schedule?"))
            {
                manager.MakeSchedule();
            }
            do
            {
                if (lawyer.GetYesNo("Do you want to Add a record?"))
                {
                    Create(schedule, lawyer);
                }

                else if (lawyer.GetYesNo("Do you want to update any records?"))
                {
                    Update(schedule, lawyer);
                }
                else
                {
                    Console.WriteLine("Okay then");
                }
            } while (lawyer.GetYesNo("Do you want to add anything else to the tables"));
            Console.WriteLine("Program end");
            Console.ReadLine();
        }
        public static void Create(Schedule schedule, Lawyer lawyer)
        {
            do
            {
                if (lawyer.GetYesNo("Do you want to Add an employee?"))
                {
                    schedule.AddEmployee();
                }
                else if (lawyer.GetYesNo("Do you want to add vacation days for an employee?"))
                {
                    Employee employee = GetEmployee(lawyer);
                    schedule.AddVacation();
                }
                else if (lawyer.GetYesNo("Do you want to add off days for an employee?"))
                {
                    Employee employee = GetEmployee(lawyer);
                    schedule.AddOffDay(employee);
                }
                else if (lawyer.GetYesNo("Do you want to add sick days for an employee?"))
                {
                    Employee employee = GetEmployee(lawyer);
                    schedule.AddSickDay();
                }
                else if (lawyer.GetYesNo("Do you want to set workable days for an employee?"))
                {
                    schedule.SetWorkableDays();
                }
                else if (lawyer.GetYesNo("Do you want to set workable late days for an employee"))
                {
                    schedule.SetWorkableLateDays();
                }
            } while (lawyer.GetYesNo("Do you want to add more records?"));
        }
        public static void Update(Schedule schedule, Lawyer lawyer)
        {
            do
            {
                if (lawyer.GetYesNo("Do you want to update workable days for an employee?"))
                {
                    schedule.UpdateWorkableDays();
                }


                else if (lawyer.GetYesNo("Do you want to update number of vacation days for an employee?"))
                {
                    schedule.UpdateVacations();
                }
            } while (lawyer.GetYesNo("Do you want to update any other records?"));
        }
        public static void Read(Schedule schedule, Lawyer lawyer)
        {

        }
        public static void Delete(Schedule schedule, Lawyer lawyer)
        {

        }

        private static Employee GetEmployee(Lawyer lawyer)
        {
            if (lawyer.GetYesNo("Do you know the last name of the employee?"))
            {
                string lastname = lawyer.GetResponse("What is the last name of the employee?");
                while (!(Reader.DoesEmployeeExist(lastname)))
                {
                    lastname = lawyer.GetResponse("Sorry no employee under that last name exist.\nWhat is the name of the employee?");
                }
                foreach (var employee in Reader.Employees)
                {
                    if (employee.LastName == lastname && Reader.GetNumberOfEmployeeWSameName(lastname) == 1)
                    {
                        return employee;
                    }
                    else if (employee.LastName == lastname)
                    {
                        string firstletter = lawyer.GetResponse("Sorry there are two or more employees with that last name.\nWhat is the first letter of the employees name");
                        if (employee.FirstName[0].ToString() == firstletter)
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
                if ((lawyer.GetYesNo("Is this the employee you want to use?")))
                {
                    return employee;
                }
                Console.Clear();
            }

            Employee bob = new Employee("Bob", "Bob", 10000);
            return bob;
        }
    }
}
