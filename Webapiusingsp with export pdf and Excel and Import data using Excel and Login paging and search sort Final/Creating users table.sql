CREATE TABLE [dbo].[User](
[Id] [int] NOT NULL Primary key,
[Name] [varchar](50) NULL,
[UserName] [varchar](50) NULL,
[Password] [varchar](50) NULL,
[Gender] [varchar](50) NULL,
[DOB] [datetime] NULL,
[EmailID] [varchar](50) NULL,
[Mobile] [varchar](20) NULL,
[Landline] [varchar](20) NULL,
[Address] [varchar](1024) NULL,
[PinCode] [varchar](20) NULL
)

select * from User
select * from [dbo].[User]

create procedure Usertsp(@Id int,@Name nvarchar(50),@UserName varchar(50),@Password varchar(50),@Gender varchar(50),@DOB datetime,@EmailId varchar(150),@Mobile varchar(20),@Landline varchar(20),@Address varchar(1024),@PinCode varchar(20),@Operation nvarchar(100))
as
begin
	if(@Operation='Insert')---for inserting
	begin
		Insert into [dbo].[User] values(@Name,@UserName,@Password,@Gender,@DOB,@EmailId,@Mobile,@Landline,@Address,@PinCode)
	end
	if(@Operation='Update') --for updating
	begin
		Update [dbo].[User] set Name=@Name,UserName=@UserName,Password=@Password,Gender=@Gender,DOB=@DOB,EmailID=@EmailId,Mobile=@Mobile,Landline=@Landline,Address=@Address,PinCode=@PinCode where Id=@Id
	end
	if(@Operation='Delete')--for deleting
	begin
		Delete from [dbo].[User] where Id=@Id
	end
	if(@operation='Select')--for select for getting perticuler student
	begin
		select * from [dbo].[User] where Id=@Id
	end
	select * from [dbo].[User]---if noting in this condition then all students shouold display
End


create procedure selectUsers
as 
begin
select * from  [dbo].[User]
end

create procedure InsertupdateUsers(@Id int,@Name nvarchar(50),@UserName varchar(50),@Password varchar(50),@Gender varchar(50),@DOB datetime,@EmailId varchar(150),@Mobile varchar(20),@Landline varchar(20),@Address varchar(1024),@PinCode varchar(20),@Operation nvarchar(100))
as
begin
	if(@Operation='Insert')
	begin
		Insert into [dbo].[User] values(@Name,@UserName,@Password,@Gender,@DOB,@EmailId,@Mobile,@Landline,@Address,@PinCode)
	end
	if(@Operation='Update')
	begin
		Update [dbo].[User] set Name=@Name,UserName=@UserName,Password=@Password,Gender=@Gender,DOB=@DOB,EmailID=@EmailId,Mobile=@Mobile,Landline=@Landline,Address=@Address,PinCode=@PinCode where Id=@Id
	end
End

create procedure DeleteEmployee(@Id int)
as
begin
	Delete from [dbo].[User] where Id=@Id
end
