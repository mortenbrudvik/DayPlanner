CREATE TABLE [dbo].[TaskItems]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
    [IsCompleted] BIT NOT NULL DEFAULT 0, 
    [Name] NVARCHAR(200) NULL, 
    [Created] DATETIME NOT NULL, 
    [Updated] DATETIME NULL
)
