using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
    class Employee
    {
        public string Name { get; set; }
        public List<string> DaysWorkable { get; set; } = new List<string>();
        public bool IsFullTime { get; set; }
    }
}
