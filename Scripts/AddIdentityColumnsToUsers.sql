/* 
   SQL Script to add missing ASP.NET Identity columns to the Users table.
   Run this script to resolve 'Invalid column name' SqlExceptions (e.g. AccessFailedCount).
*/

USE ProblemSolvingPlatform;
GO

-- Add missing columns to Users table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'NormalizedUserName')
    ALTER TABLE Users ADD NormalizedUserName NVARCHAR(256) NULL;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'NormalizedEmail')
    ALTER TABLE Users ADD NormalizedEmail NVARCHAR(256) NULL;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'EmailConfirmed')
    ALTER TABLE Users ADD EmailConfirmed BIT NOT NULL DEFAULT 0;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'SecurityStamp')
    ALTER TABLE Users ADD SecurityStamp NVARCHAR(MAX) NULL;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'ConcurrencyStamp')
    ALTER TABLE Users ADD ConcurrencyStamp NVARCHAR(MAX) NULL;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'PhoneNumber')
    ALTER TABLE Users ADD PhoneNumber NVARCHAR(MAX) NULL;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'PhoneNumberConfirmed')
    ALTER TABLE Users ADD PhoneNumberConfirmed BIT NOT NULL DEFAULT 0;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'TwoFactorEnabled')
    ALTER TABLE Users ADD TwoFactorEnabled BIT NOT NULL DEFAULT 0;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'LockoutEnd')
    ALTER TABLE Users ADD LockoutEnd DATETIMEOFFSET(7) NULL;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'LockoutEnabled')
    ALTER TABLE Users ADD LockoutEnabled BIT NOT NULL DEFAULT 1;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'AccessFailedCount')
    ALTER TABLE Users ADD AccessFailedCount INT NOT NULL DEFAULT 0;

-- Ensure UserName column exists (if not already there)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'UserName')
    ALTER TABLE Users ADD UserName NVARCHAR(256) NULL;

-- Optional: Populate Normalized columns for existing users
-- UPDATE Users SET NormalizedUserName = UPPER(UserName), NormalizedEmail = UPPER(Email);

PRINT 'Missing Identity columns added to Users table successfully!';
GO
