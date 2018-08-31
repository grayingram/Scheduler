using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{/// <summary>
/// AN employee with a last and first name and id
/// </summary>
    class Employee
    {
        public string FirstName { get;private set; }
        public string LastName { get;private set; }
        
        public int ID { get; private set; }
        /// <summary>
        /// Initializes an employee using given firstname, lastname, and id
        /// </summary>
        /// <param name="firstname">first name of an employee</param>
        /// <param name="lastname"> last name of an employee</param>
        /// <param name="id">id of an employee</param>
        public Employee(string firstname, string lastname, int id)
        {
            FirstName = firstname;
            LastName = lastname;
            ID = id;
        }
        /// <summary>
        /// Default constructor
        /// </summary>
        public Employee()
        {
            FirstName = "";
            LastName = "";
        }

        /// <summary>
        /// Prints the name of an employee
        /// </summary>
        /// <returns>The employee object's Firstname value and Lastname value</returns>
        public string PrintName()
        {
            return FirstName + " " + LastName;
        }
        
    }
}
