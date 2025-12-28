-- Fix SoumissionId identity if needed
USE ProblemSolvingPlatform;

-- Check if SoumissionId has IDENTITY
IF OBJECT_ID('[dbo].[Soumission]') IS NOT NULL
BEGIN
    -- Get column info
    SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Soumission' 
    AND COLUMN_NAME = 'SoumissionId';
END

-- If SoumissionId doesn't have IDENTITY, you may need to recreate the table or use:
-- ALTER TABLE [dbo].[Soumission] 
-- ADD CONSTRAINT PK_Soumission_New PRIMARY KEY (SoumissionId);

PRINT 'Check completed. SoumissionId column details shown above.';
GO
