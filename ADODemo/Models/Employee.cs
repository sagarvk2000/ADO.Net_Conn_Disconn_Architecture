using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODemo.Models
{
    public class Employee
    {
        public int Eid { get; set; }
        public string Ename { get; set; }
        public int Salary { get; set; }
        public int Did { get; set; }
    }
    public class Department
    {
        public int Did { get; set; }
        public string Dname  { get; set; }
    }
}
