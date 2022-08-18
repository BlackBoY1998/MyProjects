CREATE TABLE [dbo].[Template]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1), 
    [Name] NVARCHAR(50) NOT NULL, 
    [Template] NVARCHAR(MAX) NOT NULL,
	[DOC] DateTime NOT NULL
)

create procedure AddTemplate(@Name NVARCHAR(50), @Template NVARCHAR(MAX),@DOC DateTime)
as
begin 
	Insert into [dbo].[Template] values(@Name,@Template,@DOC)
End

create Procedure SelectTemplate
as
begin
select * from Template
end