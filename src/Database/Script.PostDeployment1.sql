/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


IF NOT EXISTS( SELECT TOP 1 1 FROM dbo.[Value])
BEGIN
	INSERT INTO dbo.[Value](Description) VALUES('Value1')
	INSERT INTO dbo.[Value](Description) VALUES('Value2')
	INSERT INTO dbo.[Value](Description) VALUES('Value3')
END

IF NOT EXISTS( SELECT TOP 1 1 FROM dbo.[Name])
BEGIN
	INSERT INTO dbo.[Name]([Name]) VALUES('Name1')
	INSERT INTO dbo.[Name]([Name]) VALUES('Name2')
END