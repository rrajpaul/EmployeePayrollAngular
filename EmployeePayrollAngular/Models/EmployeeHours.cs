using System;

namespace EmployeePayrollAngular.Models
{
    public class EmployeeHours
    {
        public DateTime Date
        {
            get;
            set;
        }
        public double HoursWorked
        {
            get;
            set;
        }
        public int EmployeeId
        {
            get;
            set;
        }
        public string JobGroup 
        {
            get;
            set;
        }
    }
}
