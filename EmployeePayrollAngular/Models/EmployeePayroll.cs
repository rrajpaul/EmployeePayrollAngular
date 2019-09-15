using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePayrollAngular.Models
{
    public class EmployeePayroll
    {

        public int EmployeeId
        {
            get;
            set;
        }

        public string PayPeriod
        {
            get;
            set;
        }

        public double AmountPaid
        {
            get;
            set;
        }
    }
}
