-- =====================================================================
-- COMPLETE ADMIN ROLE SETUP SCRIPT
-- =====================================================================
-- RUN THIS ENTIRE SCRIPT TO SET UP ADMIN ROLE AND ASSIGN TO A USER
-- =====================================================================

PRINT '===== ADMIN ROLE SETUP ====='

-- =====================================================================
-- STEP 1: CREATE ADMIN AND USER ROLES
-- =====================================================================
PRINT 'Step 1: Creating roles...';

IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'Admin')
BEGIN
    INSERT INTO AspNetRoles (Name, NormalizedName, ConcurrencyStamp)
    VALUES ('Admin', 'ADMIN', NEWID());
    PRINT '? Admin role created';
END
ELSE
BEGIN
    PRINT '? Admin role already exists';
END

IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'User')
BEGIN
    INSERT INTO AspNetRoles (Name, NormalizedName, ConcurrencyStamp)
    VALUES ('User', 'USER', NEWID());
    PRINT '? User role created';
END
ELSE
BEGIN
    PRINT '? User role already exists';
END

-- =====================================================================
-- STEP 2: DISPLAY ALL ROLES
-- =====================================================================
PRINT '';
PRINT 'Step 2: Available roles:';
SELECT Id, Name FROM AspNetRoles;

-- =====================================================================
-- STEP 3: DISPLAY ALL USERS
-- =====================================================================
PRINT '';
PRINT 'Step 3: Available users (pick one to make admin):';
SELECT Id, UserName, Email FROM AspNetUsers ORDER BY Id;

-- =====================================================================
-- STEP 4: ASSIGN ADMIN ROLE
-- =====================================================================
-- CHANGE THIS LINE TO YOUR USER ID (from Step 3)
DECLARE @UserIdToMakeAdmin INT = 1;  -- ? CHANGE THIS TO YOUR USER ID

PRINT '';
PRINT 'Step 4: Assigning Admin role...';

DECLARE @UserId INT = @UserIdToMakeAdmin;
DECLARE @AdminRoleId INT = (SELECT Id FROM AspNetRoles WHERE Name = 'Admin');
DECLARE @UserExists BIT = 0;

-- Check if user exists
IF EXISTS (SELECT 1 FROM AspNetUsers WHERE Id = @UserId)
BEGIN
    SET @UserExists = 1;
END

IF @AdminRoleId IS NOT NULL AND @UserExists = 1
BEGIN
    IF NOT EXISTS (SELECT 1 FROM AspNetUserRoles WHERE UserId = @UserId AND RoleId = @AdminRoleId)
    BEGIN
        INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES (@UserId, @AdminRoleId);
        PRINT '? Admin role assigned to user!';
    END
    ELSE
    BEGIN
        PRINT '? User already has Admin role!';
    END
END
ELSE
BEGIN
    PRINT '? Error: User or Admin role not found!';
    PRINT '  Make sure you changed @UserIdToMakeAdmin to a valid user ID';
END

-- =====================================================================
-- STEP 5: VERIFY RESULTS
-- =====================================================================
PRINT '';
PRINT 'Step 5: Verification - User roles:';
SELECT 
    u.Id,
    u.UserName, 
    ISNULL(r.Name, 'No Role') as Role
FROM AspNetUsers u
LEFT JOIN AspNetUserRoles ur ON u.Id = ur.UserId
LEFT JOIN AspNetRoles r ON ur.RoleId = r.Id
ORDER BY u.Id;

PRINT '';
PRINT '===== SETUP COMPLETE ====='
PRINT 'Next steps:'
PRINT '1. Restart your ASP.NET application'
PRINT '2. Login with the user you made admin'
PRINT '3. You should now see Create/Edit/Delete buttons on Problems page'
