/* Add missing columns to Soumission table to match EF model
   Run this script against your database (e.g., in SSMS) to fix the "Invalid column name" errors when loading the Profile page.
   This script is safe to run repeatedly (checks if columns exist before adding them).
*/
use ProblemSolvingPlatform;
IF COL_LENGTH('Soumission', 'ProbId') IS NULL
BEGIN
    ALTER TABLE [Soumission]
    ADD [ProbId] INT NULL;
END

IF COL_LENGTH('Soumission', 'IsPassed') IS NULL
BEGIN
    ALTER TABLE [Soumission]
    ADD [IsPassed] BIT NULL;
END

IF COL_LENGTH('Soumission', 'TestsPassed') IS NULL
BEGIN
    ALTER TABLE [Soumission]
    ADD [TestsPassed] INT NULL;
END

IF COL_LENGTH('Soumission', 'TestsTotal') IS NULL
BEGIN
    ALTER TABLE [Soumission]
    ADD [TestsTotal] INT NULL;
END

IF COL_LENGTH('Soumission', 'PointsEarned') IS NULL
BEGIN
    ALTER TABLE [Soumission]
    ADD [PointsEarned] INT NULL;
END

IF COL_LENGTH('Soumission', 'SubmittedAt') IS NULL
BEGIN
    ALTER TABLE [Soumission]
    ADD [SubmittedAt] DATETIME NULL;
END

-- Optional: set SubmittedAt to current date for existing NULL rows
-- UPDATE [Soumission] SET SubmittedAt = GETDATE() WHERE SubmittedAt IS NULL;
