﻿/*
Deployment script for MovieDatabase

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "MovieDatabase"
:setvar DefaultFilePrefix "MovieDatabase"
:setvar DefaultDataPath "C:\Users\clinton.majors\AppData\Local\Microsoft\VisualStudio\SSDT\MovieLib"
:setvar DefaultLogPath "C:\Users\clinton.majors\AppData\Local\Microsoft\VisualStudio\SSDT\MovieLib"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                CURSOR_DEFAULT LOCAL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE,
                DISABLE_BROKER 
            WITH ROLLBACK IMMEDIATE;
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367)) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
PRINT N'Creating [dbo].[Movies]...';


GO
CREATE TABLE [dbo].[Movies] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Title]       VARCHAR (100) NOT NULL,
    [IsOwned]     BIT           NOT NULL,
    [Description] VARCHAR (MAX) NULL,
    [Length]      INT           NOT NULL,
    [Rating]      INT           NOT NULL,
    [ReleaseYear] SMALLINT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AK_Movies_Title] UNIQUE NONCLUSTERED ([Title] ASC)
);


GO
PRINT N'Creating unnamed constraint on [dbo].[Movies]...';


GO
ALTER TABLE [dbo].[Movies]
    ADD DEFAULT 0 FOR [IsOwned];


GO
PRINT N'Creating unnamed constraint on [dbo].[Movies]...';


GO
ALTER TABLE [dbo].[Movies]
    ADD DEFAULT 0 FOR [Length];


GO
PRINT N'Creating unnamed constraint on [dbo].[Movies]...';


GO
ALTER TABLE [dbo].[Movies]
    ADD DEFAULT 0 FOR [Rating];


GO
PRINT N'Creating unnamed constraint on [dbo].[Movies]...';


GO
ALTER TABLE [dbo].[Movies]
    ADD DEFAULT 1900 FOR [ReleaseYear];


GO
PRINT N'Creating [dbo].[CK_Movies_Length_Positive]...';


GO
ALTER TABLE [dbo].[Movies] WITH NOCHECK
    ADD CONSTRAINT [CK_Movies_Length_Positive] CHECK ([Length] >= 0);


GO
PRINT N'Creating [dbo].[CK_Movies_Title_Set]...';


GO
ALTER TABLE [dbo].[Movies] WITH NOCHECK
    ADD CONSTRAINT [CK_Movies_Title_Set] CHECK (LEN(Title) > 0);


GO
PRINT N'Creating [dbo].[CK_Movies_ReleaseYear]...';


GO
ALTER TABLE [dbo].[Movies] WITH NOCHECK
    ADD CONSTRAINT [CK_Movies_ReleaseYear] CHECK (ReleaseYear BETWEEN 1900 and 2100);


GO
PRINT N'Creating [dbo].[AddMovie]...';


GO
CREATE PROCEDURE [dbo].[AddMovie]
	@title VARCHAR(100),
	@length INT,
	@isOwned BIT,
	@releaseYear SMALLINT,
	@rating INT = 0,
	@description VARCHAR(MAX) = NULL	
AS BEGIN
	SET NOCOUNT ON;

	INSERT INTO Movies (Title, Description, Length, IsOwned, Rating, ReleaseYear) VALUES (@title, @description, @length, @isOwned, @rating, @releaseYear)

	SELECT SCOPE_IDENTITY()
END
GO
PRINT N'Creating [dbo].[GetAllMovies]...';


GO
CREATE PROCEDURE [dbo].[GetAllMovies]	
AS BEGIN
	SET NOCOUNT ON;

	SELECT Id, Title, Description, Length, IsOwned, Rating, ReleaseYear
	FROM Movies
END
GO
PRINT N'Creating [dbo].[GetMovie]...';


GO
CREATE PROCEDURE [dbo].[GetMovie]
	@id INT
AS BEGIN
	SET NOCOUNT ON;

	SELECT Id, Title, Description, Length, IsOwned, Rating, ReleaseYear
	FROM Movies
	WHERE Id = @id
END
GO
PRINT N'Creating [dbo].[RemoveMovie]...';


GO
CREATE PROCEDURE [dbo].[RemoveMovie]
	@id INT
AS BEGIN
	SET NOCOUNT ON;

	DELETE FROM Movies
	WHERE Id = @id
END
GO
PRINT N'Creating [dbo].[UpdateMovie]...';


GO
CREATE PROCEDURE [dbo].[UpdateMovie]
	@id INT,
	@title VARCHAR(100),
	@length INT,
	@isOwned BIT,
	@releaseYear SMALLINT,
	@rating INT = 0,
	@description VARCHAR(MAX) = NULL
AS BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT * FROM Movies WHERE Id = @id)
	BEGIN
		RAISERROR('Movie not found', 16, 1)
		RETURN
	END

	UPDATE Movies
	SET 
		Title = @title, 
		Description = @description, 
		Length = @length, 
		IsOwned = @isOwned,
		Rating = @rating,
		ReleaseYear = @releaseYear
	WHERE Id = @id
END
GO
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

DECLARE @count INT

SELECT @count = COUNT(*) FROM Movies
IF (@count = 0)
BEGIN
	INSERT INTO Movies (Title, Description, Length, IsOwned, Rating, ReleaseYear) VALUES
		('Star Wars', 'Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a Wookiee, and two droids to save the galaxy from...', 121, 1, 2, 1977),
		('Star Trek: The Motion Picture', 'When an alien spacecraft of enormous power is spotted approaching Earth, Admiral Kirk resumes command of the Starship Enterprise...', 132, 0, 1, 1979),
		('Cars', 'A hot-shot race-car named Lightning McQueen gets waylaid in Radiator Springs, where he finds the true meaning of friendship and family.', 117, 1, 1, 2006),
		('E.T. the Extra-Terrestrial', 'A troubled child summons the courage to help a friendly alien escape Earth and return to his home world. ', 115, 0, 2, 1982)
END
GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Movies] WITH CHECK CHECK CONSTRAINT [CK_Movies_Length_Positive];

ALTER TABLE [dbo].[Movies] WITH CHECK CHECK CONSTRAINT [CK_Movies_Title_Set];

ALTER TABLE [dbo].[Movies] WITH CHECK CHECK CONSTRAINT [CK_Movies_ReleaseYear];


GO
PRINT N'Update complete.';


GO
