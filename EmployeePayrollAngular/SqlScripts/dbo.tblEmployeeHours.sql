IF OBJECT_ID('dbo.tblEmployeeHours', 'U') IS NOT NULL
BEGIN
	DROP TABLE [dbo].[tblEmployeeHours];
END

CREATE TABLE [dbo].[tblEmployeeHours]
(	 
    [Date] DATETIME NOT NULL, 
    HoursWorked DECIMAL(12,2) NOT NULL, 
	EmployeeId INT NOT NULL,
    JobGroup CHAR(1) NOT NULL	
)
GO

ALTER TABLE dbo.tblEmployeeHours ADD CONSTRAINT PK_tblEmployeeHours_EmployeeId PRIMARY KEY CLUSTERED
(
  EmployeeId ASC,
  [Date] ASC,
  JobGroup ASC
)


IF OBJECT_ID('dbo.tblJobGroup', 'U') IS NOT NULL
BEGIN
	DROP TABLE [dbo].[tblJobGroup];
END
CREATE TABLE [dbo].[tblJobGroup]
(	 
    JobGroup CHAR(1) PRIMARY KEY NOT NULL,
	PayRate DECIMAL(12,2) NOT NULL
)
GO

INSERT INTO [dbo].[tblJobGroup](JobGroup, PayRate) VALUES('A', 20);
INSERT INTO [dbo].[tblJobGroup](JobGroup, PayRate) VALUES('B', 30);
GO

IF OBJECT_ID('dbo.tblReportUploadHistory', 'U') IS NOT NULL
BEGIN
	DROP TABLE [dbo].[tblReportUploadHistory]
END
CREATE TABLE [dbo].[tblReportUploadHistory]
(	 
    ReportId INT PRIMARY KEY NOT NULL,
	DateUploaded DATETIME NOT NULL
)
GO

