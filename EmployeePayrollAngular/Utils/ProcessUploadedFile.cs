using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using FastMember;
using EmployeePayrollAngular.Models;

namespace EmployeePayrollAngular.Utils
{
    public static class ProcessUploadedFile        
    {
        private static bool _isValidReport;
        private static int _reportId;

        public static string GetDataTableFromCSVFile(string csvInput, string connectionString, int headerCount, string endString = "report id")
        {
            DataTable csvData = new DataTable();
            string errorMessage = "OK";
            try
            {               
                char[] delimiters = new char[] { ',' };
                using (StreamReader reader = new StreamReader(csvInput))
                {
                    int rowPos = 0;

                    string[] headerCols = new string[headerCount];
                    string[] parts = new string[headerCount];

                    List<EmployeeHours> employeeHrs = new List<EmployeeHours>();

                    while (true)
                    {
                        string line = reader.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        if (rowPos == 0)
                        {
                            headerCols = line.Split(delimiters);
                        }
                        else
                        {
                            parts = line.Split(delimiters);
                            if((rowPos > 0)  && (parts[0] != endString))
                            {
                                float hoursWorked;
                                var isValid = float.TryParse(parts[1], out hoursWorked);

                                int employeeId;
                                isValid = Int32.TryParse(parts[2], out employeeId);

                                var datePayroll = ValidationDate(parts[0]);

                                employeeHrs.Add(
                                    new EmployeeHours
                                    {
                                        Date = datePayroll,
                                        HoursWorked = hoursWorked,                                        
                                        EmployeeId = employeeId,
                                        JobGroup = parts[3]
                                    }
                                );
                            } else if((parts[0] == endString))
                            {
                                 _reportId = Convert.ToInt32(parts[1]);
                                _isValidReport = IsValidReportToUpload(connectionString);
                            }

                        }

                        rowPos++;
                    }

                    if(_isValidReport)
                    {
                        DataTable _csvDataTable = new DataTable();
                        using (var rd = ObjectReader.Create(employeeHrs, "Date", "HoursWorked", "EmployeeId", "JobGroup"))
                        {
                            _csvDataTable.Load(rd);
                        }
                        InsertDataSQLBulkCopy(_csvDataTable, connectionString, "dbo.tblEmployeeHours");
                        InsertReportOnSuccess(connectionString);
                    } else
                    {
                        errorMessage = string.Format("Time Report with id {0} was previously uploaded", _reportId);                       
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return errorMessage;
        }

        private static DateTime ValidationDate(string datePayroll)
        {

            var tempDate = datePayroll.ToString().Split('/');

            int day = -1;
            var isValidDay = Int32.TryParse(tempDate[0], out day);

            int month = -1;
            var isValidMonth = Int32.TryParse(tempDate[1], out month);

            int year = -1;
            var isValidYear = Int32.TryParse(tempDate[2], out year);

            DateTime result = new DateTime();

            if(isValidDay && isValidMonth && isValidYear)
            {
                result = new DateTime(year, month, day);
            }

            return result;
        }

        public static DataTable ToDataTable<T>(this T[] students)
        {
            if (students == null || students.Length == 0) return null;
            DataTable table = new DataTable();
            var student_tmp = students[0];
            table.Columns.AddRange(student_tmp.GetType().GetFields().Select(field => new DataColumn(field.Name, field.FieldType)).ToArray());
            int fieldCount = student_tmp.GetType().GetFields().Count();
            students.All(student =>
            {
                table.Rows.Add(Enumerable.Range(0, fieldCount).Select(index => student.GetType().GetFields()[index].GetValue(student)).ToArray());
                return true;
            });
            return table;
        }

        private static void InsertDataSQLBulkCopy(DataTable csvFileData, string connString, string tableName)
        {
            using (SqlConnection dbConnection = new SqlConnection(connString))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                {
                    s.DestinationTableName = tableName;
                    foreach (var column in csvFileData.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());
                    s.WriteToServer(csvFileData);
                }
            }
        }

        private static bool IsValidReportToUpload(string connString)
        {
            bool result = false;

            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlCommand command = new SqlCommand("Select ReportId From dbo.tblReportUploadHistory Where ReportId = @Id", conn);
            command.Parameters.AddWithValue("@Id", _reportId);
 
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)                                
                {
                    result = false;
                } else
                {
                    result = true;
                }
                
            }

            conn.Close();

            return result;
        }

        private static void InsertReportOnSuccess(string connString)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlCommand command = new SqlCommand("INSERT INTO dbo.tblReportUploadHistory(ReportId, DateUploaded) VALUES (@id, GETDATE());", conn);
            command.Parameters.AddWithValue("@Id", _reportId);

            command.ExecuteNonQuery();
        }

    }
}
