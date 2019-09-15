using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EmployeePayrollAngular.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePayrollAngular.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : Controller
    {

        private readonly string _sprocPayroll = "sprocPayrollReport";

        IConfiguration _iconfiguration;
        public EmployeeController(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        [HttpGet]
        public IEnumerable<EmployeePayroll> GetAllEmployees()
        {
            List<EmployeePayroll> employeesPayoll = new List<EmployeePayroll>();

            string conStr = _iconfiguration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(_sprocPayroll, con)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlDataAdapter da = new SqlDataAdapter
            {
                SelectCommand = cmd
            };
            DataSet ds = new DataSet();
            da.Fill(ds);
 
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                employeesPayoll.Add(new EmployeePayroll
                {
                    EmployeeId = Convert.ToInt32(dr["Employee Id"]),
                    PayPeriod = dr["Pay Period"].ToString(),
                    AmountPaid = Convert.ToDouble(dr["Amount Paid"])
                });
            }
            
            return employeesPayoll;
        }
    }
}