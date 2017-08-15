ALTER PROCEDURE [dbo].[Save_Registration]
	@id				int
	,@companyname	nvarchar(255)
	,@qqusername	nvarchar(255)
	,@qqpassword	nvarchar(255)
	,@apiprovider	nvarchar(25)
	,@isActive		bit
AS
BEGIN

if(@id = -1)
begin
	insert into Client_Registration(CompanyName,QQUsername,QQPassword,APIProvider)
	values(@companyname,@qqusername,@qqpassword,@apiprovider);
end
else
begin
	
	update Client_Registration
	set CompanyName	=	@companyname
	,QQUsername		=	@qqusername
	,QQPassword		=	@qqpassword
	,IsActive		=	@isActive
	where Id		=	@id
end
END
GO


--update current records
update Client_Registration
set API_Provider  = 'QQSOLUTION'
GO