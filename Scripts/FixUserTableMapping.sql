/* 
   SQL Script to fix User and Soumission table relationship
   Run this script if you are getting FOREIGN KEY conflicts with dbo.Users.
   It ensures your AspNetUsers table is named 'Users', the column is 'UserId',
   and the foreign key in the Soumission table is correctly set.
*/

USE ProblemSolvingPlatform;
GO

-- 1. If you have AspNetUsers but no Users table, rename it
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUsers') 
   AND NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    EXEC sp_rename 'AspNetUsers', 'Users';
    PRINT 'Renamed AspNetUsers to Users';
END
GO

-- 2. If the Users table has a column 'Id', rename it to 'UserId'
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'Id')
   AND NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'UserId')
BEGIN
    EXEC sp_rename 'Users.Id', 'UserId', 'COLUMN';
    PRINT 'Renamed Users.Id to Users.UserId';
END
GO

-- 3. Fix Foreign Keys in other tables
-- Soumission
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_sommision_User')
    ALTER TABLE Soumission DROP CONSTRAINT FK_sommision_User;
GO
ALTER TABLE Soumission ADD CONSTRAINT FK_sommision_User FOREIGN KEY (UserId) REFERENCES Users(UserId);
GO

-- Classement
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_classement_User')
    ALTER TABLE [Classement] DROP CONSTRAINT FK_classement_User;
GO
ALTER TABLE [Classement] ADD CONSTRAINT FK_classement_User FOREIGN KEY (UserId) REFERENCES Users(UserId);
GO

-- Commentaire
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_commentaire_User')
    ALTER TABLE [Commentaire] DROP CONSTRAINT FK_commentaire_User;
GO
ALTER TABLE [Commentaire] ADD CONSTRAINT FK_commentaire_User FOREIGN KEY (UserId) REFERENCES Users(UserId);
GO

PRINT 'Database schema and foreign keys updated successfully!';
