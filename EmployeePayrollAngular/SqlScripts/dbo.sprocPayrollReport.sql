IF OBJECT_ID('dbo.sprocPayrollReport', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE dbo.sprocPayrollReport;
END
GO


CREATE PROCEDURE dbo.sprocPayrollReport
AS
BEGIN
SELECT [Employee Id], [Pay Period], Sum([Total]) AS [Amount Paid]
FROM
(
SELECT  EmployeeId AS [Employee Id],
            CASE WHEN Day( [Date] ) < 16
            THEN '1/' + CAST(Month([Date]) AS VARCHAR(2)) + '/' + CAST(Year([Date]) AS VARCHAR(4)) + ' - ' + '15/' + CAST(Month([Date]) AS VARCHAR(2)) + '/' + CAST(Year([Date]) AS VARCHAR(4))
            ELSE '16/' + CAST(Month([Date]) AS VARCHAR(2)) + '/' + CAST(Year([Date]) AS VARCHAR(4)) + ' - ' +  CAST(DAY(EOMONTH(GetDate())) AS VARCHAR) + '/' + CAST(Month([Date]) AS VARCHAR(2)) + '/' + CAST(Year([Date]) AS VARCHAR(4))
            END  AS [Pay Period],  (t2.PayRate * t1.HoursWorked) AS [Total]
    FROM 
        dbo.tblEmployeeHours t1
        INNER JOIN  dbo.tblJobGroup t2 ON  t1.JobGroup = t2.JobGroup
    group by
        EmployeeId, [Date], PayRate, HoursWorked
) as t3
GROUP BY [Employee Id], [Pay Period]
ORDER BY [Employee Id], [Pay Period]

RETURN 0
END