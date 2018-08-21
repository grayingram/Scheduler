using System;

namespace Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            Schedule schedule = new Schedule();
            Lawyer lawyer = new Lawyer();
            do
            {
                if (lawyer.GetYesNo("Do you want to Add an employee?"))
                {
                    schedule.AddEmployee();
                }
                else if(lawyer.GetYesNo("Do you want to set workable days for an employee?"))
                {
                    schedule.SetWorkableDays();
                }
                else
                {
                    Console.WriteLine("Okay then");
                }
            } while (lawyer.GetYesNo("Do you want to add anything else to the tables"));
            Console.WriteLine("Program end");
            Console.ReadLine();
        }
    }
}
