-------------------------------CREATE TABLES-------------------------------
--------Non_Profit_Organizations--------
create table Non_Profit_Organizations (OrganizationID int primary key identity,
	OrganizationName nvarchar(50), Email nvarchar(50), LinkToWebsite nvarchar(50), Description nvarchar(500), DeleteAnswer nvarchar (30))

----------------Campaigns---------------
create table Campaigns (CampaignID int primary key identity, 
	CampaignName nvarchar(50), LinkToLandingPage nvarchar(50), Hashtag nvarchar(30), 
	OrganizationID int foreign key references Non_Profit_Organizations (OrganizationID) on delete cascade,
	DeleteAnswer nvarchar (30))

------------Social_Activists------------
create table Social_Activists (ActivistID int primary key identity, 
	FullName nvarchar(40), Email nvarchar(50), Address nvarchar(100), PhoneNumber nvarchar(15), DeleteAnswer nvarchar (30))

-----------Business_Companies-----------
create table Business_Companies (BusinessID int primary key identity,
	BusinessName nvarchar(50), Email nvarchar(50), DeleteAnswer nvarchar (30))

------------Donated_Products------------
create table Donated_Products (ProductID int primary key identity,
	ProductName nvarchar(30), Price money, 
	BusinessID int foreign key references Business_Companies (BusinessID) on delete cascade,
	CampaignID int foreign key references Campaigns (CampaignID) on delete cascade,
	Bought nvarchar(5), Shipped nvarchar (5))

------------Active_Campaigns------------
create table Active_Campaigns (ActiveCampID int primary key identity,
	ActivistID int foreign key references Social_Activists (ActivistID) on delete cascade,
	CampaignID int foreign key references Campaigns (CampaignID) on delete cascade,
	TwitterUserName nvarchar(50), Hashtag nvarchar(30), MoneyEarned int, CampaignName nvarchar(50), TweetsNumber int)

------------------Users-----------------
create table Users (UserID int primary key identity, Email nvarchar(50))

---------------Contact_Us---------------
create table Contact_Us(MessageID int primary key identity, 
Name nvarchar(100), Email nvarchar(30), UserMessage nvarchar(500))

----------------Twitter-----------------
create table Twitter (TwitterID int primary key identity, 
TwitterUserName nvarchar(50), Hashtag nvarchar(30), TweetID nvarchar(300), TweetDate DateTime)

----------------Logger------------------
create table Logger (LoggerID int primary key identity, 
EventMessage nvarchar(max), ErrorMessage nvarchar(max), Exception nvarchar(max), ExceptionMessage nvarchar(max), LogDate DateTime)

declare @loggerID int, @eventMessage nvarchar(max), @errorMessage nvarchar(max),
	@exception nvarchar(max), @exceptionMessage nvarchar(max), @dateTime DateTime
	
insert into Logger values(@eventMessage, @errorMessage, @exception, @exceptionMessage, @dateTime)
select @loggerID = @@IDENTITY
delete from Logger
where LogDate < dateadd(month, -3, getdate())

---------------------------------------------------------------------------
----------------------------------SELECT-----------------------------------
select * from Non_Profit_Organizations
select * from Campaigns
select * from Social_Activists
select * from Donated_Products
select * from Active_Campaigns
select * from Business_Companies
select * from Users
select * from Contact_Us
select * from Twitter
select * from Logger

---------------------------------------------------------------------------
---------------------------Querys for the Tables---------------------------
--------------------------Non_Profit_Organizations-------------------------
--Insert into Non_Profit_Organizations only when the email not exists in the table
declare @organizationID int, @organizationName nvarchar(50) = 'Make A Wish', @linkToWebsite nvarchar(50) = 'www.wish.com', 
	@email nvarchar(50) = '345@gmail.com', @description nvarchar(50) = 'Make a wish', @deleteAnswer nvarchar(30) = '1'

if not exists (select Email from Non_Profit_Organizations where Email = @email)
begin
	insert into Non_Profit_Organizations values(@organizationName, @email, @linkToWebsite, @description, @deleteAnswer) 
	select @organizationID = @@IDENTITY
end
---------------------------------------------------------------------------
--Insert into Non_Profit_Organizations only when the email not exists in the table using Stored Procedure
---------------------------------------------------------------------------
create procedure InsertOrganizationToDB (@organizationName nvarchar(50), @email nvarchar(50), @linkToWebsite nvarchar(50), @description nvarchar(50), @deleteAnswer nvarchar(30))
as
begin
	declare @organizationID int
	if not exists (select Email from Non_Profit_Organizations where Email = @email)
	begin
		insert into Non_Profit_Organizations values(@organizationName, @email, @linkToWebsite, @description, @deleteAnswer) 
		select @organizationID = @@IDENTITY
	end
end

drop procedure InsertOrganizationToDB
---------------------------------------
--Delete from Non_Profit_Organizations, and its deleting from all the tables where there is a foreign key reference
declare @organizationID int = 16
delete from Non_Profit_Organizations where OrganizationID = @organizationID
---------------------------------------------------------------------------
--Delete from Non_Profit_Organizations, and its deleting from all the tables where there is a foreign key reference using Stored Procedure
---------------------------------------------------------------------------
create procedure DeleteOrganizationByID (@organizationID int)
as
begin
   declare @email nvarchar(50)

   select @email = Users.Email from Non_Profit_Organizations
   inner join Users on Non_Profit_Organizations.Email = Users.Email
   where Non_Profit_Organizations.OrganizationID = @organizationID
   
   delete from Users where Email = @email
   delete from Non_Profit_Organizations where OrganizationID = @organizationID
end

drop procedure DeleteOrganizationByID
---------------------------------------
--Find organization by email
declare  @email nvarchar(50) = 'toma1212@gmail.com'
select * from Non_Profit_Organizations where Email =  @email
---------------------------------------------------------------------------
--Find organization by email using Stored Procedure
---------------------------------------------------------------------------
create procedure GetOneOrganizationByEmail (@email nvarchar(50))
as
begin
    select * from Non_Profit_Organizations where Email =  @email
end

drop procedure GetOneOrganizationByEmail
---------------------------------------
--Select Non_Profit_Organizations and update DeleteAnswer if found in another tables
update Non_Profit_Organizations set DeleteAnswer = 'Has more records'
where exists (
    select * from Campaigns
    where Campaigns.OrganizationID = Non_Profit_Organizations.OrganizationID
)
select * from Non_Profit_Organizations
---------------------------------------------------------------------------
--Select Non_Profit_Organizations and update DeleteAnswer if found in another tables using Stored Procedure
---------------------------------------------------------------------------
create procedure GetAllOrganizations
as
begin
    update Non_Profit_Organizations set DeleteAnswer = 'Has more records'
	where exists (
    select * from Campaigns
    where Campaigns.OrganizationID = Non_Profit_Organizations.OrganizationID
	)
	select * from Non_Profit_Organizations
end

drop procedure GetAllOrganizations
---------------------------------------
---------------------------------------------------------------------------
--Update Organizations using Stored Procedure
---------------------------------------------------------------------------
create procedure UpdateOrganizationByID (@organizationID int, @organizationName nvarchar(50), @linkToWebsite nvarchar(50), @description nvarchar(50))
as
begin
   update Non_Profit_Organizations set OrganizationName = @organizationName, LinkToWebsite = @linkToWebsite, Description = @description 
   where OrganizationID = @organizationID
end

drop procedure UpdateOrganizationByID
---------------------------------------------------------------------------
---------------------------------Campaigns---------------------------------
--Insert into Campaigns 
declare @campaignID int, @campaignName nvarchar(50) = 'Animals', @linkToLandingPage nvarchar(50) = 'www.animals.com', 
	@hashtag nvarchar(30) = '#animals', @organizationID int = 11, @deleteAnswer nvarchar(30) = '1'

insert into Campaigns values(@campaignName, @linkToLandingPage, @hashtag, 
	(select OrganizationID from Non_Profit_Organizations where OrganizationID = @organizationID), @deleteAnswer)
select @campaignID = @@IDENTITY
---------------------------------------------------------------------------
--Insert into Campaigns using Stored Procedure
---------------------------------------------------------------------------
create procedure InsertCampaignToDB (@organizationID int, @campaignName nvarchar(50), @linkToLandingPage nvarchar(50), @hashtag nvarchar(30), @deleteAnswer nvarchar(30))
as
begin
    declare @campaignID int

    insert into Campaigns values(@campaignName, @linkToLandingPage, @hashtag, 
	(select OrganizationID from Non_Profit_Organizations where OrganizationID = @organizationID), @deleteAnswer)
	select @campaignID = @@IDENTITY
end

exec InsertCampaignToDB @organizationID = 1, @campaignName = 'Toma', @linkToLandingPage = 'toma.com', @hashtag = '#toma', @deleteAnswer = '1'
select * from Campaigns

drop procedure InsertCampaignToDB
---------------------------------------
---------------------------------------------------------------------------
--Update Campaign using Stored Procedure
---------------------------------------------------------------------------
create procedure UpdateCampaignByID (@campaignID int, @campaignName nvarchar(50), @linkToLandingPage nvarchar(50), @hashtag nvarchar(30))
as
begin
   update Campaigns set CampaignName = @campaignName, LinkToLandingPage = @linkToLandingPage, Hashtag = @hashtag 
   where CampaignID = @campaignID
end

drop procedure UpdateCampaignByID
---------------------------------------
--Delete from Campaigns, and its deleting from all the tables where there is a foreign key reference
declare @campaignID int = 4
delete from Campaigns where CampaignID = @campaignID
---------------------------------------------------------------------------
--Delete from Campaigns, and its deleting from all the tables where there is a foreign key reference using Stored Procedure
---------------------------------------------------------------------------
create procedure DeleteCampaignByID (@campaignID int)
as
begin
   delete from Campaigns where CampaignID = @campaignID
end

drop procedure DeleteCampaignByID
---------------------------------------
--Select Campaigns and update DeleteAnswer if found in another tables
update Campaigns set DeleteAnswer = 'Has more records'
where exists (
    select * from Donated_Products
    where Donated_Products.CampaignID = Campaigns.CampaignID
)
select * from Campaigns
---------------------------------------------------------------------------
--Select Campaigns and update DeleteAnswer if found in another tables using Stored Procedure
---------------------------------------------------------------------------
create procedure GetAllCampaigns
as
begin
    update Campaigns set DeleteAnswer = 'Has more records'
	where exists (
    select * from Donated_Products
    where Donated_Products.CampaignID = Campaigns.CampaignID
	)
	select * from Campaigns
end

exec GetAllCampaigns 
drop procedure GetAllCampaigns
---------------------------------------
--Find campaigns with inner join with the Non_Profit_Organizations table where the email is the same as the parameter 
declare @email nvarchar(50) = 'toma1212@gmail.com'
update Campaigns set DeleteAnswer = 'Has more records'
where exists (
    select * from Donated_Products
    where Donated_Products.CampaignID = Campaigns.CampaignID
)
select * from Campaigns 
inner join Non_Profit_Organizations on Non_Profit_Organizations.OrganizationID = Campaigns.OrganizationID
where Non_Profit_Organizations.Email = @email
---------------------------------------------------------------------------
--Find campaigns with inner join with the Non_Profit_Organizations table where the email is the same as the parameter using Stored Procedure
---------------------------------------------------------------------------
create procedure GetAllCampaignsByEmail (@email nvarchar(50))
as
begin
    update Campaigns set DeleteAnswer = 'Has more records'
	where exists (
    select * from Donated_Products
    where Donated_Products.CampaignID = Campaigns.CampaignID
	)
	select * from Campaigns 
	inner join Non_Profit_Organizations on Non_Profit_Organizations.OrganizationID = Campaigns.OrganizationID
	where Non_Profit_Organizations.Email = @email
end

exec GetAllCampaignsByEmail  @email = 'toma1212@gmail.com'
drop procedure GetAllCampaignsByEmail
---------------------------------------
--Find campaigns with inner join with the Non_Profit_Organizations table 
select Campaigns.*, Non_Profit_Organizations.OrganizationName, Non_Profit_Organizations.Description from Campaigns 
inner join Non_Profit_Organizations on Non_Profit_Organizations.OrganizationID = Campaigns.OrganizationID
---------------------------------------------------------------------------
--Find campaigns with inner join with the Non_Profit_Organizations table using Stored Procedure
---------------------------------------------------------------------------
create procedure GetAllCampaignsOfOrganization
as
begin
    select Campaigns.*, Non_Profit_Organizations.OrganizationName, Non_Profit_Organizations.Description from Campaigns 
	inner join Non_Profit_Organizations on Non_Profit_Organizations.OrganizationID = Campaigns.OrganizationID
end

drop procedure GetAllCampaignsOfOrganization
---------------------------------------------------------------------------
------------------------------Donated_Products-----------------------------
--Insert into Donated_Products 
declare @productID int, @productName nvarchar(30) = 'iphone', @price money = 2, 
	@businessID int = 2, @campaignID int = 2, @bought nvarchar(5) = 'NO', @shipped nvarchar(5) = 'NO'

insert into Donated_Products values(@productName, @price,
	(select BusinessID from Business_Companies where BusinessID = @businessID),
	(select CampaignID from Campaigns where CampaignID = @campaignID),
	 @bought, @shipped)
select @productID = @@IDENTITY
---------------------------------------------------------------------------
--Insert into Donated_Products using Stored Procedure
---------------------------------------------------------------------------
create procedure InsertDonatedProductToDB (@productName nvarchar(30), @price money, @businessID int, @campaignID int, @bought nvarchar(5), @shipped nvarchar(5))
as
begin
    declare @productID int

    insert into Donated_Products values(@productName, @price,
	(select BusinessID from Business_Companies where BusinessID = @businessID),
	(select CampaignID from Campaigns where CampaignID = @campaignID),
	 @bought, @shipped)
	select @productID = @@IDENTITY
end

drop procedure InsertDonatedProductToDB
---------------------------------------
--Delete from Donated_Products
declare @productID int = 25
delete from Donated_Products where ProductID = @productID
---------------------------------------
--Update Donated_Products Bought Status
declare @productID int, @bought nvarchar(5)

update Donated_Products set Bought = @bought
where ProductID = @productID
---------------------------------------------------------------------------
--Update Donated_Products Bought Status using Stored Procedure
---------------------------------------------------------------------------
create procedure UpdateDonatedProductBoughtStatus (@productID int, @bought nvarchar(5))
as
begin
	update Donated_Products set Bought = @bought
	where ProductID = @productID
end

drop procedure UpdateDonatedProductBoughtStatus
---------------------------------------
--Update Donated_Products Shipped Status
declare @productID int, @shipped nvarchar(5)

update Donated_Products set Shipped = @shipped
where ProductID = @productID
---------------------------------------------------------------------------
--Update Donated_Products Shipped Status using Stored Procedure
---------------------------------------------------------------------------
create procedure UpdateDonatedProductShippedStatus (@productID int, @shipped nvarchar(5))
as
begin
	update Donated_Products set Shipped = @shipped
	where ProductID = @productID
end

drop procedure UpdateDonatedProductShippedStatus
---------------------------------------------------------------------------
------------------------------Social_Activists-----------------------------
--Insert into Social_Activists 
declare @activistID int, @fullName nvarchar(40) = 'Tomi', @email nvarchar(50) = '9@gmail.com', 
	@address nvarchar(100) = 'Tel-Aviv', @phoneNumber nvarchar(15) = '050000', @deleteAnswer nvarchar(30) = '1'

if not exists (select Email from Social_Activists where Email = @email)
begin
insert into Social_Activists values(@fullName, @email, @address, @phoneNumber, @deleteAnswer)
select @activistID = @@IDENTITY
end
---------------------------------------------------------------------------
--Insert into Social_Activists using Stored Procedure
---------------------------------------------------------------------------
create procedure InsertSocialActivistToDB (@fullName nvarchar(40), @email nvarchar(50), @address nvarchar(100), @phoneNumber nvarchar(15), @deleteAnswer nvarchar(30))
as
begin
    declare @activistID int

    if not exists (select Email from Social_Activists where Email = @email)
	begin
	insert into Social_Activists values(@fullName, @email, @address, @phoneNumber, @deleteAnswer)
	select @activistID = @@IDENTITY
	end
end

drop procedure InsertSocialActivistToDB
---------------------------------------
--Delete from Social_Activists, and its deleting from all the tables where there is a foreign key reference
declare @activistID int = 3
delete from Social_Activists where ActivistID = @activistID
---------------------------------------------------------------------------
--Delete from Social_Activists, and its deleting from all the tables where there is a foreign key reference using Stored Procedure
---------------------------------------------------------------------------
create procedure DeleteSocialActivistByID (@activistID int)
as
begin
   declare @email nvarchar(50)

   select @email = Users.Email from Social_Activists
   inner join Users on Social_Activists.Email = Users.Email
   where Social_Activists.ActivistID = @activistID
   
   delete from Users where Email = @email
   delete from Social_Activists where ActivistID = @activistID
end

drop procedure DeleteSocialActivistByID
---------------------------------------
--Find social activist by email
declare  @email nvarchar(50) = '123@gmail.com'

select * from Social_Activists where Email = @email
---------------------------------------------------------------------------
--Find social activist by email using Stored Procedure
---------------------------------------------------------------------------
create procedure GetOneSocialActivistByEmail (@email nvarchar(50))
as
begin
    select * from Social_Activists where Email = @email
end

drop procedure GetOneSocialActivistByEmail
---------------------------------------
--Update social activist 
declare @activistID int, @fullName nvarchar(40) = 'Toma', @email nvarchar(50) = '123@gmail.com', 
	@address nvarchar(100) = 'Tel-Aviv', @phoneNumber nvarchar(15) = '050000'

update Social_Activists set FullName = @fullName, Address = @address, PhoneNumber = @phoneNumber 
where ActivistID = @activistID
---------------------------------------------------------------------------
--Update social activist using Stored Procedure
---------------------------------------------------------------------------
create procedure UpdateSocialActivistByID (@activistID int, @fullName nvarchar(40),@address nvarchar(100), @phoneNumber nvarchar(15))
as
begin
	update Social_Activists set FullName = @fullName, Address = @address, PhoneNumber = @phoneNumber 
	where ActivistID = @activistID
end

drop procedure UpdateSocialActivistByID
---------------------------------------
--Select Social_Activists and update DeleteAnswer if found in another tables
update Social_Activists set DeleteAnswer = 'Has more records'
where exists (
    select * from Active_Campaigns
    where Active_Campaigns.ActivistID = Social_Activists.ActivistID
)
select * from Social_Activists
---------------------------------------------------------------------------
--Select Social_Activists and update DeleteAnswer if found in another tables using Stored Procedure
---------------------------------------------------------------------------
create procedure GetAllSocialActivists
as
begin
    update Social_Activists set DeleteAnswer = 'Has more records'
	where exists (
    select * from Active_Campaigns
    where Active_Campaigns.ActivistID = Social_Activists.ActivistID
	)
	select * from Social_Activists
end

drop procedure GetAllSocialActivists
---------------------------------------------------------------------------
-----------------------------Business_Companies----------------------------
--Insert into Business_Companies 
declare @businessID int, @businessName nvarchar(50) = 'APPLE', @email nvarchar(50) = '123@gmail.com', @deleteAnswer nvarchar(30) = '1'

if not exists (select Email from Business_Companies where Email = @email)
begin
insert into Business_Companies values(@businessName, @email, @deleteAnswer)
select @businessID = @@IDENTITY
end
---------------------------------------------------------------------------
--Insert into Business_Companies using Stored Procedure
---------------------------------------------------------------------------
create procedure InsertBusinessCompanyToDB (@businessName nvarchar(50), @email nvarchar(50), @deleteAnswer nvarchar(30))
as
begin
    declare @businessID int

	if not exists (select Email from Business_Companies where Email = @email)
	begin
	insert into Business_Companies values(@businessName, @email, @deleteAnswer)
	select @businessID = @@IDENTITY
	end
end

drop procedure InsertBusinessCompanyToDB
---------------------------------------
--Select Business_Companies and update DeleteAnswer if found in another tables
update Business_Companies set DeleteAnswer = 'Has more records'
where exists (
    select * from Donated_Products
    where Donated_Products.BusinessID = Business_Companies.BusinessID
)
select * from Business_Companies
---------------------------------------------------------------------------
--Select Business_Companies and update DeleteAnswer if found in another tables using Stored Procedure
---------------------------------------------------------------------------
create procedure GetAllBusinessCompanies
as
begin
    update Business_Companies set DeleteAnswer = 'Has more records'
	where exists (
    select * from Donated_Products
    where Donated_Products.BusinessID = Business_Companies.BusinessID
	)
	select * from Business_Companies
end

drop procedure GetAllBusinessCompanies
---------------------------------------
--Delete from Business_Companies, and its deleting from all the tables where there is a foreign key reference
declare @businessID int = 4
delete from Business_Companies where BusinessID = @businessID
---------------------------------------------------------------------------
--Delete from Business_Companies, and its deleting from all the tables where there is a foreign key reference using Stored Procedure
---------------------------------------------------------------------------
create procedure DeleteBusinessCompanyByID (@businessID int)
as
begin
  declare @email nvarchar(50)

   select @email = Users.Email from Business_Companies
   inner join Users on Business_Companies.Email = Users.Email
   where Business_Companies.BusinessID = @businessID
   
   delete from Users where Email = @email
   delete from Business_Companies where BusinessID = @businessID
end

drop procedure DeleteBusinessCompanyByID
---------------------------------------
--Find business company by email
declare  @email nvarchar(50) = '123@gmail.com'
select * from Business_Companies where Email =  @email
---------------------------------------------------------------------------
--Find business company by email using Stored Procedure
---------------------------------------------------------------------------
create procedure GetOneBusinessCompanyByEmail (@email nvarchar(50))
as
begin
    select * from Business_Companies where Email =  @email
end

drop procedure GetOneBusinessCompanyByEmail
---------------------------------------
--Update business company
declare @businessID int, @businessName nvarchar(50) 

update Business_Companies set BusinessName = @businessName
where BusinessID = @businessID
---------------------------------------------------------------------------
--Update business company using Stored Procedure
---------------------------------------------------------------------------
create procedure UpdateBusinessCompanyByID (@businessID int, @businessName nvarchar(50))
as
begin
	update Business_Companies set BusinessName = @businessName
	where BusinessID = @businessID
end

drop procedure UpdateBusinessCompanyByID
---------------------------------------
--Find donated products with inner join with the Campaigns, and Non_Profit_Organizations tables 
declare @businessID int = 1

select Donated_Products.*, Campaigns.CampaignName, Non_Profit_Organizations.OrganizationName from Donated_Products 
inner join Campaigns on Campaigns.CampaignID = Donated_Products.CampaignID
inner join Non_Profit_Organizations on Non_Profit_Organizations.OrganizationID = Campaigns.OrganizationID
where BusinessID = @businessID
---------------------------------------------------------------------------
--Find donated products with inner join with the Campaigns, and Non_Profit_Organizations tables using Stored Procedure
---------------------------------------------------------------------------
create procedure GetAllDonatedProductsAndCampaignsAndOrganizationsForBusinessID (@businessID int)
as
begin
	select Donated_Products.*, Campaigns.CampaignName, Non_Profit_Organizations.OrganizationName from Donated_Products 
	inner join Campaigns on Campaigns.CampaignID = Donated_Products .CampaignID
	inner join Non_Profit_Organizations on Non_Profit_Organizations.OrganizationID = Campaigns.OrganizationID
	where BusinessID = @businessID
end

drop procedure GetAllDonatedProductsAndCampaignsAndOrganizationsForBusinessID
---------------------------------------
--Find donated products with inner join with the Campaigns, and Non_Profit_Organizations tables 
select Donated_Products.*, Campaigns.CampaignName,Campaigns.Hashtag, Non_Profit_Organizations.OrganizationName, Non_Profit_Organizations.OrganizationID from Donated_Products 
inner join Campaigns on Campaigns.CampaignID = Donated_Products .CampaignID
inner join Non_Profit_Organizations on Non_Profit_Organizations.OrganizationID = Campaigns.OrganizationID
---------------------------------------------------------------------------
--Find donated products with inner join with the Campaigns, and Non_Profit_Organizations tables using Stored Procedure
---------------------------------------------------------------------------
create procedure GetAllDonatedProductsAndCampaignsAndOrganizations
as
begin
    select Donated_Products.*, Campaigns.CampaignName, Campaigns.Hashtag, Non_Profit_Organizations.OrganizationName, Non_Profit_Organizations.OrganizationID from Donated_Products
	inner join Campaigns on Campaigns.CampaignID = Donated_Products .CampaignID
	inner join Non_Profit_Organizations on Non_Profit_Organizations.OrganizationID = Campaigns.OrganizationID
end

drop procedure GetAllDonatedProductsAndCampaignsAndOrganizations
---------------------------------------
--Find donated products, and active campaigns with inner join with the Campaigns, and Active_Campaigns tables 
declare @activistID int = 9

select Donated_Products.*, Active_Campaigns.*  from Donated_Products
inner join Campaigns on Campaigns.CampaignID = Donated_Products.CampaignID
inner join Active_Campaigns on Active_Campaigns.CampaignID = Campaigns.CampaignID
where ActivistID = @activistID and Donated_Products.Bought = 'NO'
---------------------------------------------------------------------------
--Find  donated products, and active campaigns with inner join with the Campaigns, and Active_Campaigns tables using Stored Procedure
---------------------------------------------------------------------------
create procedure GetDonatedProductsAndActiveCampaignsForActivist (@activistID int)
as
begin
    select Donated_Products.*, Active_Campaigns.*  from Donated_Products
	inner join Campaigns on Campaigns.CampaignID = Donated_Products.CampaignID
	inner join Active_Campaigns on Active_Campaigns.CampaignID = Campaigns.CampaignID
	where ActivistID = @activistID and Donated_Products.Bought = 'NO'
end

drop procedure GetDonatedProductsAndActiveCampaignsForActivist
---------------------------------------
--Find the business company shipments with inner join with the Campaigns, Active_Campaigns, and Social_Activists tables 
select Donated_Products.*, Social_Activists.FullName, Social_Activists.Email, 
Social_Activists.Address, Social_Activists.PhoneNumber from Donated_Products
inner join Campaigns on Campaigns.CampaignID = Donated_Products.CampaignID
inner join Active_Campaigns on Active_Campaigns.CampaignID = Campaigns.CampaignID
inner join Social_Activists on Social_Activists.ActivistID = Active_Campaigns.ActivistID
where Donated_Products.Shipped = 'NO' and  Donated_Products.Bought = 'YES'
---------------------------------------------------------------------------
--Find the business company shipments with inner join with the Campaigns, Active_Campaigns, and Social_Activists tables using Stored Procedure
---------------------------------------------------------------------------
create procedure GetBusinessCompaniesShipments
as
begin
    select Donated_Products.*, Social_Activists.FullName, Social_Activists.Email,
	Social_Activists.Address, Social_Activists.PhoneNumber from Donated_Products
	inner join Campaigns on Campaigns.CampaignID = Donated_Products.CampaignID
	inner join Active_Campaigns on Active_Campaigns.CampaignID = Campaigns.CampaignID
	inner join Social_Activists on Social_Activists.ActivistID = Active_Campaigns.ActivistID
	where Donated_Products.Shipped = 'NO' and  Donated_Products.Bought = 'YES'
end

drop procedure GetBusinessCompaniesShipments
---------------------------------------
--Find the business company shipments with inner join with the Campaigns, Active_Campaigns, and Social_Activists tables by BusinessID
declare @businessID int = 4

select Donated_Products.*, Social_Activists.FullName, Social_Activists.Email, 
Social_Activists.Address, Social_Activists.PhoneNumber from Donated_Products
inner join Campaigns on Campaigns.CampaignID = Donated_Products.CampaignID
inner join Active_Campaigns on Active_Campaigns.CampaignID = Campaigns.CampaignID
inner join Social_Activists on Social_Activists.ActivistID = Active_Campaigns.ActivistID
where Donated_Products.Shipped = 'NO' and  Donated_Products.Bought = 'YES' and Donated_Products.BusinessID = @businessID
---------------------------------------------------------------------------
--Find the business company shipments with inner join with the Campaigns, Active_Campaigns, and Social_Activists tables by BusinessID using Stored Procedure
---------------------------------------------------------------------------
create procedure BusinessCompanyShipmentsByBusinessID (@businessID int)
as
begin
	select Donated_Products.*, Social_Activists.FullName, Social_Activists.Email,
	Social_Activists.Address, Social_Activists.PhoneNumber from Donated_Products
	inner join Campaigns on Campaigns.CampaignID = Donated_Products.CampaignID
	inner join Active_Campaigns on Active_Campaigns.CampaignID = Campaigns.CampaignID
	inner join Social_Activists on Social_Activists.ActivistID = Active_Campaigns.ActivistID
	where Donated_Products.Shipped = 'NO' and  Donated_Products.Bought = 'YES' and Donated_Products.BusinessID = @businessID
end

drop procedure BusinessCompanyShipmentsByBusinessID
---------------------------------------------------------------------------
------------------------------Active_Campaigns-----------------------------
--Insert into Active_Campaigns 
declare @activeCampID int, @activistID int = 3, @campaignID int = 6, 
	@twitterUserName nvarchar(50) = 'tomao360', @hashtag nvarchar(30) = '#animals', @moneyEarned money = 0, @campaignName nvarchar(50) = 'Wish', @tweetsNumber int = 0

if not exists (select ActivistID, CampaignID from Active_Campaigns where ActivistID = @activistID and CampaignID = @campaignID)
begin
	insert into Active_Campaigns values(
		(select ActivistID from Social_Activists where ActivistID = @activistID),
		(select CampaignID from Campaigns where CampaignID = @campaignID),
		@twitterUserName, @hashtag, @moneyEarned, @campaignName, @tweetsNumber)
	select @activeCampID = @@IDENTITY
end
---------------------------------------------------------------------------
--Insert into Active_Campaigns using Stored Procedure
---------------------------------------------------------------------------
create procedure InsertActiveCampaignToDB (@activistID int, @campaignID int, @twitterUserName nvarchar(50), 
	@hashtag nvarchar(30), @moneyEarned money, @campaignName nvarchar(50), @tweetsNumber int)
as
begin
    declare @activeCampID int

	if not exists (select ActivistID, CampaignID from Active_Campaigns where ActivistID = @activistID and CampaignID = @campaignID)
	begin
		insert into Active_Campaigns values(
			(select ActivistID from Social_Activists where ActivistID = @activistID),
			(select CampaignID from Campaigns where CampaignID = @campaignID),
			@twitterUserName, @hashtag, @moneyEarned, @campaignName, @tweetsNumber)
		select @activeCampID = @@IDENTITY
	end
end

drop procedure InsertActiveCampaignToDB
---------------------------------------
--Find active campaign by social activist ID
declare  @activistID int = 1

select * from Active_Campaigns where ActivistID = @activistID
---------------------------------------------------------------------------
--Find active campaign by social activist ID using Stored Procedure
---------------------------------------------------------------------------
create procedure GetActiveCampaignsForActivist (@activistID int)
as
begin
    select * from Active_Campaigns where ActivistID = @activistID
end

drop procedure GetActiveCampaignsForActivist
---------------------------------------
--Update Active_Campaigns -> ADD MONEY and ADD TWEETS
declare @activeCampID int = 1, @moneyEarned money = 20, @tweetsNumber int = 600

update Active_Campaigns set MoneyEarned = MoneyEarned + @moneyEarned, TweetsNumber = TweetsNumber + @tweetsNumber
where ActiveCampID = @activeCampID
---------------------------------------------------------------------------
--Update Active_Campaigns -> ADD MONEY and ADD TWEETS using Stored Procedure
---------------------------------------------------------------------------
create procedure UpdateActiveCampaignAddMoneyByID (@activeCampID int, @moneyEarned money, @tweetsNumber int)
as
begin
	update Active_Campaigns set MoneyEarned = MoneyEarned + @moneyEarned, TweetsNumber = TweetsNumber + @tweetsNumber
	where ActiveCampID = @activeCampID
end

drop procedure UpdateActiveCampaignAddMoneyByID
---------------------------------------
--Update Active_Campaigns -> SUBTRACT MONEY
declare @activeCampID int, @moneyEarned money

update Active_Campaigns set MoneyEarned = MoneyEarned - @moneyEarned
where ActiveCampID = @activeCampID
---------------------------------------------------------------------------
--Update Active_Campaigns -> SUBTRACT MONEY using Stored Procedure
---------------------------------------------------------------------------
create procedure UpdateActiveCampaignSubtractMoneyByID (@activeCampID int, @moneyEarned money)
as
begin
	update Active_Campaigns set MoneyEarned = MoneyEarned - @moneyEarned
	where ActiveCampID = @activeCampID
end

drop procedure UpdateActiveCampaignSubtractMoneyByID
---------------------------------------------------------------------------
-----------------------------------Users-----------------------------------
--Insert into Users only when the email not exists in the table
declare @userID int, @email nvarchar(50) = '345@gmail.com'

if not exists (select Email from Users where Email = @email)
begin
	insert into Users values(@email)
	select @userID = @@IDENTITY
end
---------------------------------------------------------------------------
--Insert into Users only when the email not exists in the table using Stored Procedure
---------------------------------------------------------------------------
create procedure InsertUserToDB (@email nvarchar(50))
as
begin
    declare @userID int

	if not exists (select Email from Users where Email = @email)
	begin
		insert into Users values(@email)
		select @userID = @@IDENTITY
	end
end

drop procedure InsertUserToDB
---------------------------------------------------------------------------
---------------------------------Contact_Us--------------------------------
--Insert into Contact_Us
declare @name nvarchar(100) = 'toma', @email nvarchar(30) = 'xxx', @userMessage nvarchar(500) = 'ddd', @messageID int

insert into Contact_Us values(@name, @email, @userMessage)
select @messageID = @@IDENTITY
---------------------------------------------------------------------------
--Insert into Contact_Us using Stored Procedure
---------------------------------------------------------------------------
create procedure InsertContactUsMessageToDB (@name nvarchar(100), @email nvarchar(30), @userMessage nvarchar(500))
as
begin
    declare @messageID int

	insert into Contact_Us values(@name, @email, @userMessage)
	select @messageID = @@IDENTITY
end

drop procedure InsertContactUsMessageToDB
---------------------------------------
--Delete from Contact_Us
delete from Contact_Us where MessageID = @messageID
---------------------------------------------------------------------------
--Delete from Contact_Us using Stored Procedure
---------------------------------------------------------------------------
create procedure DeleteContactUsMessageByID (@messageID int)
as
begin
   delete from Contact_Us where MessageID = @messageID
end

drop procedure DeleteContactUsMessageByID
---------------------------------------------------------------------------
----------------------------------Twitter----------------------------------
--Insert into Twitter
declare @twitterID int, @twitterUserName nvarchar(50) = 'tomao360', @hashtag nvarchar(30) = 'animals', @tweetID nvarchar(300) = '222', @tweetDate DateTime = '2023-01-16 10:00:000'
	
if not exists (select TweetID from Twitter where TweetID = @tweetID)
begin
	insert into Twitter values(@twitterUserName, @hashtag, @tweetID, @tweetDate) 
	select @twitterID = @@IDENTITY
end
---------------------------------------------------------------------------
--Insert into Twitter using Stored Procedure
---------------------------------------------------------------------------
create procedure InsertTweetToDB (@twitterUserName nvarchar(50), @hashtag nvarchar(30), @tweetID nvarchar(300), @tweetDate DateTime)
as
begin
    declare @twitterID int

	if not exists (select TweetID from Twitter where TweetID = @tweetID)
	begin
		insert into Twitter values(@twitterUserName, @hashtag, @tweetID, @tweetDate) 
		select @twitterID = @@IDENTITY
	end
end

drop procedure InsertTweetToDB
---------------------------------------
--Get the last updated Tweet
select top 1 *from Twitter
order by TweetDate desc
---------------------------------------------------------------------------
--Get the last updated Tweet using Stored Procedure
---------------------------------------------------------------------------
create procedure GetTheLastUpdatedTweet
as
begin
    select top 1 *from Twitter
	order by TweetDate desc
end

drop procedure GetTheLastUpdatedTweet
---------------------------------------------------------------------------
----------------------------Queries For Reports----------------------------
--Find campaign popularity high to low with inner join with the Campaigns, Donated_Products, and Active_Campaigns tables 
select Non_Profit_Organizations.OrganizationName, Campaigns.CampaignID, Campaigns.CampaignName, Campaigns.LinkToLandingPage, Campaigns.Hashtag,
count(distinct Active_Campaigns.ActivistID) as TotalActivists, sum(distinct Active_Campaigns.TweetsNumber) as TotalTweets, count(distinct Donated_Products.ProductID) as TotalProducts
from Non_Profit_Organizations 
inner join Campaigns on Campaigns.OrganizationID = Non_Profit_Organizations.OrganizationID
inner join Donated_Products on Donated_Products.CampaignID = Campaigns.CampaignID
inner join Active_Campaigns on Active_Campaigns.CampaignID = Donated_Products.CampaignID
group by Campaigns.CampaignID, Campaigns.CampaignName, Campaigns.Hashtag, Campaigns.LinkToLandingPage, Non_Profit_Organizations.OrganizationName
order by TotalActivists desc,  TotalTweets desc, TotalProducts desc
---------------------------------------------------------------------------
--Find campaign popularity high to low with inner join with the Campaigns, Donated_Products, and Active_Campaigns tables using Stored Procedure 
---------------------------------------------------------------------------
create procedure GetTheMostPopularCampaign
as
begin
    select Non_Profit_Organizations.OrganizationName, Campaigns.CampaignID, Campaigns.CampaignName, Campaigns.LinkToLandingPage, Campaigns.Hashtag,
	count(distinct Active_Campaigns.ActivistID) as TotalActivists, sum(distinct Active_Campaigns.TweetsNumber) as TotalTweets, count(distinct Donated_Products.ProductID) as TotalProducts
	from Non_Profit_Organizations
	inner join Campaigns on Campaigns.OrganizationID = Non_Profit_Organizations.OrganizationID
	inner join Donated_Products on Donated_Products.CampaignID = Campaigns.CampaignID
	inner join Active_Campaigns on Active_Campaigns.CampaignID = Donated_Products.CampaignID
	group by Campaigns.CampaignID, Campaigns.CampaignName, Campaigns.Hashtag, Campaigns.LinkToLandingPage, Non_Profit_Organizations.OrganizationName
	order by TotalActivists desc,  TotalTweets desc, TotalProducts desc
end

drop procedure GetTheMostPopularCampaign
---------------------------------------
--Find campaign profitability high to low with inner join with the Campaigns, Donated_Products, and Active_Campaigns tables 
select Non_Profit_Organizations.OrganizationName, Campaigns.CampaignID, Campaigns.CampaignName, Campaigns.LinkToLandingPage, Campaigns.Hashtag,
sum(distinct Active_Campaigns.TweetsNumber) as TotalMoney
from Non_Profit_Organizations 
inner join Campaigns on Campaigns.OrganizationID = Non_Profit_Organizations.OrganizationID
inner join Donated_Products on Donated_Products.CampaignID = Campaigns.CampaignID
inner join Active_Campaigns on Active_Campaigns.CampaignID = Donated_Products.CampaignID
group by Campaigns.CampaignID, Campaigns.CampaignName, Campaigns.Hashtag, Campaigns.LinkToLandingPage, Non_Profit_Organizations.OrganizationName
order by TotalMoney desc
---------------------------------------------------------------------------
--Find campaign profitability high to low with inner join with the Campaigns, Donated_Products, and Active_Campaigns tables using Stored Procedure 
---------------------------------------------------------------------------
create procedure GetTheMostProfitableCampaign
as
begin
    select Non_Profit_Organizations.OrganizationName, Campaigns.CampaignID, Campaigns.CampaignName, Campaigns.LinkToLandingPage, Campaigns.Hashtag,
	sum(distinct Active_Campaigns.TweetsNumber) as TotalMoney
	from Non_Profit_Organizations
	inner join Campaigns on Campaigns.OrganizationID = Non_Profit_Organizations.OrganizationID
	inner join Donated_Products on Donated_Products.CampaignID = Campaigns.CampaignID
	inner join Active_Campaigns on Active_Campaigns.CampaignID = Donated_Products.CampaignID
	group by Campaigns.CampaignID, Campaigns.CampaignName, Campaigns.Hashtag, Campaigns.LinkToLandingPage, Non_Profit_Organizations.OrganizationName
	order by TotalMoney desc
end

drop procedure GetTheMostProfitableCampaign
---------------------------------------
--Find organization's amount of campaigns high to low with inner join with the Campaigns table
select Non_Profit_Organizations.OrganizationID, Non_Profit_Organizations.OrganizationName, Non_Profit_Organizations.Email, Non_Profit_Organizations.LinkToWebsite,
Non_Profit_Organizations.Description, count(distinct Campaigns.CampaignID) as TotalCampaigns
from Non_Profit_Organizations 
inner join Campaigns on Campaigns.OrganizationID = Non_Profit_Organizations.OrganizationID
group by  Non_Profit_Organizations.OrganizationID, Non_Profit_Organizations.OrganizationName, Non_Profit_Organizations.Email, Non_Profit_Organizations.LinkToWebsite,
Non_Profit_Organizations.Description
order by TotalCampaigns desc
---------------------------------------
--Find organization's amount of campaigns, and donated products high to low with inner join with the Campaigns, and Donated_Products tables
select Non_Profit_Organizations.OrganizationID, Non_Profit_Organizations.OrganizationName, Non_Profit_Organizations.Email, Non_Profit_Organizations.LinkToWebsite,
Non_Profit_Organizations.Description, count(distinct Campaigns.CampaignID) as TotalCampaigns, count(distinct Donated_Products.ProductID) as TotalProducts
from Non_Profit_Organizations 
inner join Campaigns on Campaigns.OrganizationID = Non_Profit_Organizations.OrganizationID
left join Donated_Products on Donated_Products.CampaignID = Campaigns.CampaignID
group by  Non_Profit_Organizations.OrganizationID, Non_Profit_Organizations.OrganizationName, Non_Profit_Organizations.Email, Non_Profit_Organizations.LinkToWebsite,
Non_Profit_Organizations.Description
order by TotalCampaigns desc, TotalProducts desc
---------------------------------------------------------------------------
--Find organization's amount of campaigns, and donated products high to low with inner join with the Campaigns, and Donated_Products tables using Stored Procedure 
---------------------------------------------------------------------------
create procedure GetCountCampaignAndProductsToOrg
as
begin
    select Non_Profit_Organizations.OrganizationID, Non_Profit_Organizations.OrganizationName, Non_Profit_Organizations.Email, Non_Profit_Organizations.LinkToWebsite,
	Non_Profit_Organizations.Description, count(distinct Campaigns.CampaignID) as TotalCampaigns, count(distinct Donated_Products.ProductID) as TotalProducts
	from Non_Profit_Organizations
	inner join Campaigns on Campaigns.OrganizationID = Non_Profit_Organizations.OrganizationID
	left join Donated_Products on Donated_Products.CampaignID = Campaigns.CampaignID
	group by  Non_Profit_Organizations.OrganizationID, Non_Profit_Organizations.OrganizationName, Non_Profit_Organizations.Email, Non_Profit_Organizations.LinkToWebsite,
	Non_Profit_Organizations.Description
	order by TotalCampaigns desc, TotalProducts desc
end

drop procedure GetCountCampaignAndProductsToOrg
---------------------------------------
--Find business company's amount of donated products high to low with inner join with the Donated_Products table
select Business_Companies.BusinessID, Business_Companies.BusinessName, Business_Companies.Email, 
count(distinct Donated_Products.ProductID) as TotalProducts
from Business_Companies 
inner join Donated_Products on Donated_Products.BusinessID = Business_Companies.BusinessID
group by  Business_Companies.BusinessID, Business_Companies.BusinessName, Business_Companies.Email
order by TotalProducts desc
---------------------------------------------------------------------------
--Find business company's amount of donated products high to low with inner join with the Donated_Products table using Stored Procedure 
---------------------------------------------------------------------------
create procedure GetAmountOfDonatedProducts
as
begin
    select Business_Companies.BusinessID, Business_Companies.BusinessName, Business_Companies.Email,
	count(distinct Donated_Products.ProductID) as TotalProducts
	from Business_Companies
	inner join Donated_Products on Donated_Products.BusinessID = Business_Companies.BusinessID
	group by  Business_Companies.BusinessID, Business_Companies.BusinessName, Business_Companies.Email
	order by TotalProducts desc
end

drop procedure GetAmountOfDonatedProducts
---------------------------------------
--Find social activist's amount of money earned high to low with inner join with the Active_Campaigns table
select Social_Activists.ActivistID, Social_Activists.FullName, Social_Activists.Email,
sum(distinct Active_Campaigns.TweetsNumber) as TotalMoney
from Social_Activists 
inner join Active_Campaigns on Active_Campaigns.ActivistID = Social_Activists.ActivistID
group by  Social_Activists.ActivistID, Social_Activists.FullName, Social_Activists.Email
order by TotalMoney desc
---------------------------------------------------------------------------
--Find social activist's amount of money earned high to low with inner join with the Active_Campaigns table using Stored Procedure 
---------------------------------------------------------------------------
create procedure GetActivistsThatEarnedMostMoneyByOrder
as
begin
    select Social_Activists.ActivistID, Social_Activists.FullName, Social_Activists.Email,
	sum(distinct Active_Campaigns.TweetsNumber) as TotalMoney
	from Social_Activists 
	inner join Active_Campaigns on Active_Campaigns.ActivistID = Social_Activists.ActivistID
	group by  Social_Activists.ActivistID, Social_Activists.FullName, Social_Activists.Email
	order by TotalMoney desc
end

drop procedure GetActivistsThatEarnedMostMoneyByOrder
---------------------------------------
--Find social activist's amount of promoted campaigns high to low with inner join with the Active_Campaigns table
select Social_Activists.ActivistID, Social_Activists.FullName, Social_Activists.Email,
count(distinct Active_Campaigns.CampaignID) as TotalCampaigns
from Social_Activists 
inner join Active_Campaigns on Active_Campaigns.ActivistID = Social_Activists.ActivistID
group by  Social_Activists.ActivistID, Social_Activists.FullName, Social_Activists.Email
order by TotalCampaigns desc
---------------------------------------------------------------------------
--Find social activist's amount of promoted campaigns high to low with inner join with the Active_Campaigns table using Stored Procedure 
---------------------------------------------------------------------------
create procedure GetActivistsThatPromotedMostCampaignsByOrder
as
begin
    select Social_Activists.ActivistID, Social_Activists.FullName, Social_Activists.Email,
	count(distinct Active_Campaigns.CampaignID) as TotalCampaigns
	from Social_Activists 
	inner join Active_Campaigns on Active_Campaigns.ActivistID = Social_Activists.ActivistID
	group by  Social_Activists.ActivistID, Social_Activists.FullName, Social_Activists.Email
	order by TotalCampaigns desc
end

drop procedure GetActivistsThatPromotedMostCampaignsByOrder

---------------------------------------------------------------------------
--------------------------------DROP TABLES--------------------------------
drop table Non_Profit_Organizations
drop table Campaigns
drop table Social_Activists
drop table Donated_Products
drop table Active_Campaigns
drop table Business_Companies
drop table Users
drop table Contact_Us
drop table Twitter
drop table Logger

---------------------------------------------------------------------------

