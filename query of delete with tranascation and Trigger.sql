create Table Data
(
Id int NOT NULL,
Name varchar(10),
PRIMARY KEY(Id)
)


Insert Into Data values(2,'Akash')

create Table DataLog
(
LogId int NOT NULL identity(1,1),
id int,
Name varchar(20),
Operation varchar(5),
DeleteTime DateTime
PRIMARY KEY(LogId)
)


select * from Data
select * from DataLog
delete from Data where Id=2

alter TRIGGER trg_Student ON Data
after Delete
as 
begin
begin tran
	declare @name varchar(20),@tablename varchar(10)
	begin try
	set @tablename='Data'
	insert into DataLog
	select Id,Name,'D',getdate() from deleted
	end try
	begin catch
	Rollback tran
end catch
	--insert into history
	--select 'U',getdate(),StudentId,FirstName,LastName,Age,city from updated don't need this statment
end



master table is used


