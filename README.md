## Project Description

A client is going to use our web
app to determine how much each employee should be paid in each _pay period_, so
it is critical that we get our numbers right.

The partner in question only pays its employees by the hour (there are no
salaried employees.) Employees belong to one of two _job groups_ which
determine their wages; job group A is paid $20/hr, and job group B is paid
$30/hr. Each employee is identified by a string called an "employee id" that is
globally unique in their system.

Hours are tracked per employee, per day in comma-separated value files (CSV).
Each individual CSV file is known as a "time report", and will contain:

1. A header, denoting the columns in the sheet (`date`, `hours worked`,
   `employee id`, `job group`)
2. 0 or more data rows
3. A footer row where the first cell contains the string `report id`, and the
   second cell contains a unique identifier for this report.

Our partner has guaranteed that:

1. Columns will always be in that order.
2. There will always be data in each column.
3. There will always be a well-formed header line.
4. There will always be a well-formed footer line.

An example input file named `sample.csv` is included in this repo.

### Web Application functionality:

We've agreed to build the following web-based prototype for our partner.

1. Your app must accept (via a form) a comma separated file with the schema
   described in the previous section.
2. Your app must parse the given file, and store the timekeeping information in
   a relational database for archival reasons.
3. After upload, your application should display a _payroll report_. This
   report should also be accessible to the user without them having to upload a
   file first.
4. If an attempt is made to upload two files with the same report id, the
   second upload should fail with an error message indicating that this is not
   allowed.

The payroll report should be structured as follows:

1. There should be 3 columns in the report: `Employee Id`, `Pay Period`, `Amount Paid`

## Deployment Instructions

1. Extract bundle and open in VS 2017 community or Pro version on Windows 10 Pro 64 bit, should work on MacOS X and linux but will need Mono and its setup for the choosen environment
2. Change the connection string information (server, database and username and password) in appsettings.json which is in the root of the project
3. Run my sql scripts
4. Build the solution and run in VS 2017 or commandline tools

## Design descisions

1. Tables were chosen based on what action was being taken for example Time Report upload the table is called tblEmployeeHours and the controller is called UploadHours
   and the tblJobGroup was created for lookup and for creating the Payroll report
2. Rest API the intended action by the user in the fron end
3. Angular 5 based solution based on .Net Core 2.1 template for angular 5.x
4. I did not use EF as my time was limited but for complex web application EF will enhance productivity but might affect performance.
5. In Visual Studio 2017/2019 you may need to update the web app port in angular files employee-list/employee-list.component
   and services/upload.service.ts and services/employee.service.ts
