using System;

namespace Scheduler
{
    class Program
    {
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
                    schedule.AddVacation();
                }
                else if (lawyer.GetYesNo("Do you want to add off days for an employee?"))
                {
                    schedule.AddOffDay();
                }
                else if (lawyer.GetYesNo("Do you want to add sick days for an employee?"))
                {
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
    }
}
