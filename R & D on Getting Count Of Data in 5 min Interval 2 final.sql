create table Employee
(
EmpId int Identity(1,1),
EmpName varchar(20),
DOJ Time Default GetDate()
PRIMARY KEY(EmpId)
);

drop table Employee

Insert Into Employee values('Akash',Getdate())

select * from Employee

select (datepart(mi,DOJ)/5 ) * 5 TheMinute, count(EmpId)as EmployeeCount
from Employee
group by DOJ,datepart(hh,DOJ),(datepart(mi,DOJ)/5 ) * 5
order by DOJ,datepart(hh,DOJ),(datepart(mi,DOJ)/5 ) * 5





------------------------------------------------------------------

;With mycte
As
(

SELECT 1 as Time_ID, RIGHT(CONVERT(VARCHAR(16),DATEADD(day, DATEDIFF(day,0,GETDATE()),0),120),5)  as Time_Slot
UNION ALL
SELECT Time_ID+1 ,RIGHT(CONVERT(VARCHAR(16),DATEADD(minute, Time_ID*5,DATEADD(day, DATEDIFF(day,0,GETDATE()),0) ) ,120),5)
FROM mycte
WHERE DATEADD(minute, Time_ID*5,DATEADD(day, DATEDIFF(day,0,GETDATE()),0))
<DATEADD(day,DATEDIFF(day,0,GETDATE())+1,0)
)
select *
INTO NewTable from mycte

OPTION (MAXRECURSION 300)

 
SELECt * FROM NewTable
select * from  Employee

select * from NewTable where Time_slot Exists(select count(EmpId) as EmployeeCount,CONVERT(VARCHAR(5),DOJ,108) as Time from Employee where Time ='11:31'GROUP BY DOJ)
select count(EmpId) as EmployeeCount,CONVERT(VARCHAR(5),DOJ,108) as Time from Employee where DOJ  Between '11:30'AND '11:35 'GROUP BY DOJ


--------------------------------------------------------------------

CREATE TABLE TestLocation 
(Id INT IDENTITY(1,1),[Time] Time,Location NVARCHAR(50))
 
INSERT INTO TestLocation VALUES
(GETDATE(),'Test Location'),
(GETDATE(),'Test Location'),
(GETDATE(),'Test Location1'),
(GETDATE(),'Test Location1'),
(GETDATE(),'Test Location2')

drop table TestLocation
;with CTE AS
(
	SELECT  cast([Time] AS Time) AS [Time], 1 as RLevel,   ROW_NUMBER() OVER (ORDER BY EmpId DESC) AS Row, Location
	FROM TestLocation

	UNION ALL
	SELECT CAST(dateadd(Minute, 5, [Time]) AS Time), RLevel + 1, Id, Location
	FROM CTE
	WHERE RLevel < 3 -- i.e. 15 minutes divided by the 5 added
)
select Id, [Time], Location
from CTE
WHERE [Time] <=  (SELECT MAX([Time]) FROM TestLocation)
order by [Time]
------------------------------------------------------------------------
CREATE TABLE TestLocation 
(Id INT IDENTITY(1,1),[Time] NVARCHAR(50),Location NVARCHAR(50))
 
INSERT INTO TestLocation VALUES
('08:00','Test Location'),
('08:15','Test Location'),
('08:30','Test Location1'),
('08:45','Test Location1'),
('09:00','Test Location2')

;with CTE AS
(
	SELECT  cast([Time] AS Time) AS [Time], 1 as RLevel, Id, Location
	FROM TestLocation

	UNION ALL
	SELECT CAST(dateadd(Minute, 5, [Time]) AS Time), RLevel + 1, Id, Location
	FROM CTE
	WHERE RLevel < 3 -- i.e. 15 minutes divided by the 5 added
)
select Id, [Time], Location
from CTE
WHERE [Time] <=  (SELECT MAX([Time]) FROM TestLocation)
order by [Time]

---------------------------------------------------------------------------\


CREATE TABLE dbo.SomeTable(dt DATETIME);
GO
SET NOCOUNT ON;
GO
INSERT dbo.SomeTable(dt) SELECT DATEADD(MINUTE, -5, GETDATE());
GO 45
INSERT dbo.SomeTable(dt) SELECT DATEADD(MINUTE, -5, GETDATE());
GO 32
INSERT dbo.SomeTable(dt) SELECT DATEADD(MINUTE, -5, GETDATE());
GO 21
INSERT dbo.SomeTable(dt) SELECT DATEADD(MINUTE, -5, GETDATE());
GO 16
INSERT dbo.SomeTable(dt) SELECT DATEADD(MINUTE, -5, GETDATE());
GO 55
INSERT dbo.SomeTable(dt) SELECT DATEADD(MINUTE, -5, GETDATE());
GO 26
INSERT dbo.SomeTable(dt) SELECT DATEADD(MINUTE, -5, GETDATE());
GO 71
INSERT dbo.SomeTable(dt) SELECT GETDATE();
GO 14
drop table   dbo.SomeTable
select * from dbo.SomeTable


alter PROCEDURE dbo.GetGroupedIntervalsByDay
    
AS
Declare 
	@Date           DATE,
    @MinuteInterval TINYINT = 1
BEGIN
    SET NOCOUNT ON;
	set @Date=GETDATE();
	set @MinuteInterval=15
    DECLARE 
        @IntervalCount INT,
        @StartDate     SMALLDATETIME = @Date;

    SELECT
        @IntervalCount = 1440 / @MinuteInterval;

    ;WITH dates(s,e) AS
    (
        SELECT 
            DATEADD(MINUTE, @MinuteInterval*(n.n-1), @StartDate),
            DATEADD(MINUTE, @MinuteInterval*(n.n),   @StartDate)
        FROM
        (
          SELECT 
            TOP (@IntervalCount) ROW_NUMBER() OVER (ORDER BY o.[object_id])
            FROM sys.all_columns AS o
            ORDER BY o.[object_id]
        ) AS n(n)
    )
    SELECT WithinTime = CONVERT(VARCHAR(5),d.s,108)+' - '+CONVERT(VARCHAR(5),DATEADD(mi,15,d.s),108), CountofEntries = COUNT(s.dt) 
    FROM dates AS d
    LEFT OUTER JOIN dbo.SomeTable AS s
        ON s.dt >= d.s AND s.dt < d.e
        -- AND any filter criteria for dbo.SomeTable?
    GROUP BY d.s
    ORDER BY d.s;
END
GO


drop procedure dbo.GetGroupedIntervalsByDay


EXEC dbo.GetGroupedIntervalsByDay;


select CONVERT(VARCHAR(5),GETDATE(),108)
