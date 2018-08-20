using System;

namespace Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            Schedule schedule = new Schedule();
            Lawyer lawyer = new Lawyer();

            if(lawyer.GetYesNo("Do you want to Add an employee?"))
            {
                schedule.AddEmployee();
            }
            else
            {
                Console.WriteLine("Okay then");
            }
            Console.WriteLine("Program end");
            Console.ReadLine();
        }
    }
}
