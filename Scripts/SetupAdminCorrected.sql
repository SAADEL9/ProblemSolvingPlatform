SET QUOTED_IDENTIFIER ON;
GO

-- =====================================================================
-- COMPLETE ADMIN ROLE SETUP SCRIPT (CORRECTED FOR SCHEMA)
-- =====================================================================

PRINT '===== ADMIN ROLE SETUP (CORRECTED) ====='

-- Step 1: Create Roles
IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'Admin')
BEGIN
    INSERT INTO AspNetRoles (Name, NormalizedName, ConcurrencyStamp)
    VALUES ('Admin', 'ADMIN', NEWID());
    PRINT '? Admin role created';
END

IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'User')
BEGIN
    INSERT INTO AspNetRoles (Name, NormalizedName, ConcurrencyStamp)
    VALUES ('User', 'USER', NEWID());
    PRINT '? User role created';
END

-- Step 2: Show Users (using correct table name 'Users' and column 'UserId')
PRINT '';
PRINT 'Step 3: Available users:';
-- Select from Users table (mapped from User model)
SELECT UserId, UserName, Email FROM Users ORDER BY UserId;

-- Step 3: Assign Admin Role
DECLARE @UserIdToMakeAdmin INT = 1;  -- ? CHANGE THIS TO YOUR USER ID IF NEEDED

PRINT '';
PRINT 'Step 4: Assigning Admin role...';

DECLARE @AdminRoleId INT = (SELECT Id FROM AspNetRoles WHERE Name = 'Admin');

IF EXISTS (SELECT 1 FROM Users WHERE UserId = @UserIdToMakeAdmin)
BEGIN
    IF NOT EXISTS (SELECT 1 FROM AspNetUserRoles WHERE UserId = @UserIdToMakeAdmin AND RoleId = @AdminRoleId)
    BEGIN
        INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES (@UserIdToMakeAdmin, @AdminRoleId);
        PRINT '? Admin role assigned to user!';
    END
    ELSE
    BEGIN
        PRINT '? User already has Admin role!';
    END
END
ELSE
BEGIN
    PRINT '? User not found! Please check the UserId.';
END
