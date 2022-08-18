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

select * from [dbo].[User]
