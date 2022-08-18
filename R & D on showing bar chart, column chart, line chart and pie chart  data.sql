select * from Customers where Country='UK'

select * from Customers where Country='USA'
select * from Categories
select * from Products
select * from [Order Details]
select COUNT(CategoryID) from Products 


SELECT Categories.CategoryName, COUNT(Categories.CategoryID)[ Product count] 
FROM Categories LEFT JOIN Products ON Categories.CategoryID = Products.CategoryID
GROUP BY Categories.CategoryName 

SELECT Products.ProductName, SUM([Order Details].Quantity)[ Product count] 
FROM [Order Details] LEFT JOIN Products ON [Order Details].ProductID = Products.ProductID
GROUP BY Products.ProductName

select * from Employees
select COUNT(EmployeeID)from Employees where City='London'


SELECT Products.ProductName, SUM([Order Details].Quantity)[ Product count] 
FROM [Order Details] LEFT JOIN Products ON [Order Details].ProductID = Products.ProductID
GROUP BY Products.ProductName

select * from [Order Details] where ProductID=65
select * from Products
select * from Employees
