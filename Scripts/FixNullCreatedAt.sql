-- =====================================================================
-- OPTIONAL: Fix NULL CreatedAt values in existing problems
-- =====================================================================
-- Run this if you want to set CreatedAt for all existing problems
-- This will set all NULL CreatedAt values to current date

UPDATE Probleme
SET CreatedAt = GETDATE()
WHERE CreatedAt IS NULL;

PRINT 'Updated all problems with NULL CreatedAt to current date';

-- Verify the update
SELECT 'Problems with timestamps:' as Info;
SELECT ProbId, Title, Difficulte, CreatedAt 
FROM Probleme 
ORDER BY CreatedAt DESC;
