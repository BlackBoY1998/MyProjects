select * from Orders

--monthly
select YEAR(OrderDate)[Year],MONTH(OrderDate)[Month],
DATENAME(MONTH,OrderDate)[MONTH NAME],COUNT(1)[Orders COUNT]
FROM Orders
GROUP BY YEAR(OrderDate),MONTH(OrderDate),DATENAME(MONTH,OrderDate)
ORDER BY 1,2

--yearly
select YEAR(OrderDate)[YEAR],COUNT(1)[Orders COUNT]
From Orders
GROUP BY YEAR(OrderDate)
ORDER BY 1

--weekly
SELECT
  DATEPART(week, OrderDate) AS Week,
  COUNT(1)[Orders COUNT]
FROM Orders
GROUP BY DATEPART(week,OrderDate)
ORDER BY DATEPART(week,OrderDate)

---hourly
SELECT DATEPART(hour,OrderDate) From Orders

select CAST(OrderDate as DATE) as DATE,DATEPART(hour,OrderDate)as Hours,COUNT(1) [Order Count] From Orders
GROUP BY CAST(OrderDate as DATE),DATEPART(hour,OrderDate)
ORDER BY 1,2



---- R & D on UserLog

select * from UserLog

--weekly
SELECT
  DATEPART(week, LoginDateTime) AS Week,
  COUNT(1)[Log COUNT]
FROM UserLog WHERE '20210702' <= LoginDateTime
  AND LoginDateTime < getdate()
GROUP BY DATEPART(week,LoginDateTime),DATEName(DW,LoginDateTime)
ORDER BY DATEPART(week,LoginDateTime)

---monthly
select YEAR(LoginDateTime)[Year],MONTH(LoginDateTime)[Month],
DATENAME(MONTH,LoginDateTime)[MONTH NAME],COUNT(1)[Logs COUNT]
FROM UserLog where DATEPART(MONTH,'20210623')=DATEPART(MONTH,DATEADD(MONTH,-1,GETDATE()))
GROUP BY YEAR(LoginDateTime),MONTH(LoginDateTime),DATENAME(MONTH,LoginDateTime)
ORDER BY 1,2


-- yearly
select YEAR(LoginDateTime)[YEAR],COUNT(1)[Log COUNT]
From UserLog where DATEPART(YEAR,'20200709')=DATEPART(YEAR,DATEADD(YEAR,-1,GETDATE()))
GROUP BY YEAR(LoginDateTime)
ORDER BY 1

--Financial year
select YEAR(LoginDateTime)[YEAR],COUNT(1)[Log COUNT]
From UserLog where DATEPART(YEAR,'20200401')=DATEPART(YEAR,DATEADD(YEAR,-1,'20210331'))
GROUP BY YEAR(LoginDateTime)
ORDER BY 1

--Hourly Data
select CAST(LoginDateTime as DATE) as DATE,DATEPART(hour,LoginDateTime)as Hours,COUNT(1) [Log Count] From UserLog
Where LoginDateTime > DATEADD(HOUR, -1, GETDATE())
GROUP BY CAST(LoginDateTime as DATE),DATEPART(hour,LoginDateTime)
ORDER BY 1,2


SELECT number, DATENAME(MONTH, '2012-' + CAST(number as varchar(2)) + '-1') monthname
FROM master..spt_values
WHERE Type = 'P' and number between 1 and 12
ORDER BY Number


---weekly
DECLARE @Date Date;
SET @Date=GETDATE(); 
SELECT 
	[Sunday],
	[Monday],
	[Tuesday],
	[Wednesday],
	[Thursday],
	[Friday],
	[Saturday]
FROM 
(
	SELECT [Day]=Datename(dw, LoginDateTime), LoginDateTime
	FROM UserLog 
	where DATEADD(week, -1, GETDATE()) <= LoginDateTime
  AND LoginDateTime < @Date
	
) As Src 
PIVOT 
(
COUNT(LoginDateTime) FOR [Day] in ([Sunday],[Monday],[Tuesday],[Wednesday],[Thursday],[Friday],[Saturday])
) As pvt OPTION (MAXRECURSION 0)

---monthly
SELECT *
FROM (SELECT YEAR(LoginDateTime) [Year], 
       DATENAME(MONTH, LoginDateTime) [Month], 
       COUNT(1) [Sales Count]
      FROM UserLog
      GROUP BY YEAR(LoginDateTime), 
      DATENAME(MONTH, LoginDateTime)) AS MontlySalesData
PIVOT(  SUM([Sales Count])   
    FOR Month IN ([January],[February],[March],[April],[May],
    [June],[July],[August],[September],[October],[November],
    [December])) AS MNamePivot
    
    
    
  ---yearly  
DECLARE @DynamicPivotQuery AS NVARCHAR(MAX)
DECLARE @ColumnName AS NVARCHAR(MAX)
 
SELECT YEAR(LoginDateTime) [Year],  Count(1)  [Sales Count]   
    INTO #PivotLogData
FROM UserLog
GROUP BY YEAR(LoginDateTime) 
--Get distinct values of the PIVOT Column 
SELECT @ColumnName= ISNULL(@ColumnName + ',','') 
       + QUOTENAME([Year])
FROM (SELECT DISTINCT [Year] FROM #PivotLogData) AS Years
--Prepare the PIVOT query using the dynamic 
SET @DynamicPivotQuery = 
  N'SELECT ' + @ColumnName + '
    FROM #PivotLogData
    PIVOT(SUM( [Sales Count]   ) 
          FOR [Year] IN (' + @ColumnName + ')) AS PVTTable'
--Execute the Dynamic Pivot Query
EXEC sp_executesql @DynamicPivotQuery


select * from #PivotLogData
drop table #PivotLogData