-- =====================================================================
-- ADD FUNCTION TEMPLATE AND TEST CASES COLUMNS TO PROBLEME TABLE
-- =====================================================================

-- Add new columns to Probleme table if they don't exist
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Probleme' AND COLUMN_NAME = 'FunctionTemplate')
BEGIN
    ALTER TABLE Probleme ADD FunctionTemplate NVARCHAR(MAX) NULL;
    PRINT 'FunctionTemplate column added';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Probleme' AND COLUMN_NAME = 'TestCases')
BEGIN
    ALTER TABLE Probleme ADD TestCases NVARCHAR(MAX) NULL;
    PRINT 'TestCases column added';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Probleme' AND COLUMN_NAME = 'Language')
BEGIN
    ALTER TABLE Probleme ADD Language NVARCHAR(50) DEFAULT 'python' NULL;
    PRINT 'Language column added';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Probleme' AND COLUMN_NAME = 'CreatedAt')
BEGIN
    ALTER TABLE Probleme ADD CreatedAt DATETIME DEFAULT GETDATE() NULL;
    PRINT 'CreatedAt column added';
END

SELECT 'Migration completed successfully!' AS Result;
