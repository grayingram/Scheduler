using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
    class Employee
    {
        public string FirstName { get;private set; }
        public string LastName { get;private set; }
        public int ID { get; private set; }

        public Employee(string firstname, string lastname, int id)
        {
            FirstName = firstname;
            LastName = lastname;
            ID = id;
        }
        public Employee()
        {
            FirstName = "";
            LastName = "";
        }

        public string PrintName()
        {
            return FirstName + " " + LastName;
        }
        //public List<string> DaysWorkable { get; set; } = new List<string>();
        //public bool IsFullTime { get; set; }
    }
}
