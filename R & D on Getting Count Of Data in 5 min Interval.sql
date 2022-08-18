select GETDATE()

SELECT DATEADD(MINUTE,5 , cast(GETDATE() as time))
SELECT DATEDIFF(year, '2017/08/25', '2011/08/25') AS DateDiff;
SELECT DATEDIFF(Minute, '2017/08/25 07:05', '2017/08/25 07:00') AS DateDiff
SELECT DATEPART(minute, '2017/08/25 08:36') AS DatePartInt;




SELECT DATEDIFF(MINUTE, '2017/08/25', DATEADD(MINUTE,5 , GETDATE())) AS DateDiff;


SELECT cast(AttDate as time) [time]
FROM yourtable

select cast(DATEPART(minute, GETDATE()) as varchar)

SELECT cast(DATEPART(hour, GETDATE()) as varchar) + ':' + cast(DATEPART(minute, GETDATE()) as varchar)

SELECT  (cast(DATEPART(hour, GETDATE()) as varchar) + ':' + cast(DATEPART(minute, GETDATE()) as varchar))+'-'+(cast(DATEPART(hour, GETDATE()) as varchar) + ':' + cast(DATEPART(minute, DATEADD(MINUTE,5 , cast(GETDATE() as datetime))) as varchar))



SELECT DATEADD(MINUTE,5 , cast(GETDATE() as datetime))
select cast(DATEPART(minute,DATEADD(MINUTE,5 , cast(GETDATE()  as datetime))) as varchar)





create table Employee
(
EmpId int Identity(1,1),
EmpName varchar(20),
DOJ DateTime Default GetDate()
PRIMARY KEY(EmpId)
);

drop table Employee

Insert Into Employee values('Akash',Getdate(),'0:01:40')
select * from Employee
ALTER TABLE Employee
ADD TalkTime time;



alter PROCEDURE dbo.Get5MinDailyReport    
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
    SELECT WithinTime = CONVERT(VARCHAR(5),d.s,108)+' - '+CONVERT(VARCHAR(5),DATEADD(mi,15,d.s),108), CountofEntries = COUNT(s.EmpId)
    FROM dates AS d 
    LEFT OUTER JOIN dbo.Employee AS s
        ON s.DOJ >= d.s AND s.DOJ < d.e 
        where CAST(d.s as time) >= '09:00:00'
        AND CAST(d.e as time) <='18:00:00' AND NOT CAST(d.e as time) ='00:00:00'
        -- AND any filter criteria for dbo.SomeTable?
    GROUP BY d.s
    ORDER BY d.s;
END
GO

EXEC dbo.Get5MinDailyReport;
select * from Employee Order By EmpId  desc


  
SELECT GETDATE() 'Today', DATEPART(hour,GETDATE()) 'Hour Part'


select Count(Distinct(EmpId)) as EmployeCount from Employee


SELECT LTRIM(RIGHT(CONVERT(VARCHAR(5), GETDATE(), 108), 7))

select sum((CONVERT(VARCHAR(5),DOJ,108)) from Employee


create Table TalkTime
(
Id int not null,
WithinTime DateTime,
TalkTime Time
)




 