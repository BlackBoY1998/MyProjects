alter Procedure GetReport(@Option int)
as
Begin
if(@Option=1)--Hourly
	begin
	select CAST(OrderDate as DATE) as DATE,DATEPART(hour,OrderDate)as Hours,COUNT(1) [Order Count] From Orders
	GROUP BY CAST(OrderDate as DATE),DATEPART(hour,OrderDate)
	ORDER BY 1,2
	end
else if(@Option=2)--weekly
	begin
	SELECT
	DATEPART(week, OrderDate) AS Week,
	COUNT(1)[Orders COUNT]
	FROM Orders
	GROUP BY DATEPART(week,OrderDate)
	ORDER BY DATEPART(week,OrderDate)
	end
else if(@Option=3)--monthly
	begin
	select YEAR(OrderDate)[Year],MONTH(OrderDate)[Month],
	DATENAME(MONTH,OrderDate)[MONTH NAME],COUNT(1)[Orders COUNT]
	FROM Orders
	GROUP BY YEAR(OrderDate),MONTH(OrderDate),DATENAME(MONTH,OrderDate)
	ORDER BY 1,2
	end
else if(@Option=4)--yearly
	begin
	select YEAR(OrderDate)[YEAR],COUNT(1)[Orders COUNT]
	From Orders
	GROUP BY YEAR(OrderDate)
	ORDER BY 1
	end
End