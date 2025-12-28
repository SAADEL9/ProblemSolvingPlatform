-- Migration Script: Add Submission Tracking and Leaderboard Features
-- Run this script to update your database with the new fields

-- Update Soumission table
use ProblemSolvingPlatform;
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Soumission]') AND name = 'ProbId')
BEGIN
    ALTER TABLE [dbo].[Soumission]
    ADD [ProbId] INT NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Soumission]') AND name = 'IsPassed')
BEGIN
    ALTER TABLE [dbo].[Soumission]
    ADD [IsPassed] BIT NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Soumission]') AND name = 'TestsPassed')
BEGIN
    ALTER TABLE [dbo].[Soumission]
    ADD [TestsPassed] INT NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Soumission]') AND name = 'TestsTotal')
BEGIN
    ALTER TABLE [dbo].[Soumission]
    ADD [TestsTotal] INT NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Soumission]') AND name = 'PointsEarned')
BEGIN
    ALTER TABLE [dbo].[Soumission]
    ADD [PointsEarned] INT NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Soumission]') AND name = 'SubmittedAt')
BEGIN
    ALTER TABLE [dbo].[Soumission]
    ADD [SubmittedAt] DATETIME2 NOT NULL DEFAULT GETDATE();
END
GO

-- Update Classement table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Classement]') AND name = 'TotalPoints')
BEGIN
    ALTER TABLE [dbo].[Classement]
    ADD [TotalPoints] INT NOT NULL DEFAULT 0;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Classement]') AND name = 'ProblemsSolved')
BEGIN
    ALTER TABLE [dbo].[Classement]
    ADD [ProblemsSolved] INT NOT NULL DEFAULT 0;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Classement]') AND name = 'LastUpdated')
BEGIN
    ALTER TABLE [dbo].[Classement]
    ADD [LastUpdated] DATETIME2 NULL;
END
GO

PRINT 'Migration completed successfully!';
PRINT 'New fields added:';
PRINT '  - Soumission: ProbId, IsPassed, TestsPassed, TestsTotal, PointsEarned, SubmittedAt';
PRINT '  - Classement: TotalPoints, ProblemsSolved, LastUpdated';
GO
