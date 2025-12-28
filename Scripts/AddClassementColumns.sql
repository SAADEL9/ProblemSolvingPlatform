/* Add missing columns to Classement table to match EF model
   Run this script against your database (e.g., in SSMS) to fix the "Invalid column name" errors.
*/

IF COL_LENGTH('Classement', 'TotalPoints') IS NULL
BEGIN
    ALTER TABLE [Classement]
    ADD [TotalPoints] INT NOT NULL CONSTRAINT DF_Classement_TotalPoints DEFAULT(0);
END

IF COL_LENGTH('Classement', 'ProblemsSolved') IS NULL
BEGIN
    ALTER TABLE [Classement]
    ADD [ProblemsSolved] INT NOT NULL CONSTRAINT DF_Classement_ProblemsSolved DEFAULT(0);
END

IF COL_LENGTH('Classement', 'LastUpdated') IS NULL
BEGIN
    ALTER TABLE [Classement]
    ADD [LastUpdated] DATETIME NULL;
END

-- Optional: update existing rows' LastUpdated to current date if desired
-- UPDATE [Classement] SET LastUpdated = GETDATE() WHERE LastUpdated IS NULL;
