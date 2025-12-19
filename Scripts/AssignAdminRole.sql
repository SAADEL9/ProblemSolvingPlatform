-- ===================================================
-- ADMIN ROLE ASSIGNMENT SCRIPT
-- ===================================================

-- ===================================================
-- CREATE ADMIN AND USER ROLES
-- ===================================================

-- Step 1: Create Admin Role
IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'Admin')
BEGIN
    INSERT INTO AspNetRoles (Name, NormalizedName, ConcurrencyStamp)
    VALUES ('Admin', 'ADMIN', NEWID());
    PRINT 'Admin role created successfully!';
END
ELSE
BEGIN
    PRINT 'Admin role already exists!';
END

-- Step 2: Create User Role
IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'User')
BEGIN
    INSERT INTO AspNetRoles (Name, NormalizedName, ConcurrencyStamp)
    VALUES ('User', 'USER', NEWID());
    PRINT 'User role created successfully!';
END
ELSE
BEGIN
    PRINT 'User role already exists!';
END

-- Step 3: Verify roles were created
SELECT 'Available Roles:' as Info;
SELECT Id, Name, NormalizedName FROM AspNetRoles;

-- ===================================================
-- VIEW ALL USERS
-- ===================================================
SELECT 'All Users:' as Info;
SELECT Id, UserName, Email, FirstName, LastName FROM AspNetUsers;

-- ===================================================
-- VIEW USER ROLES
-- ===================================================
SELECT 'Current User Roles:' as Info;
SELECT 
    u.Id,
    u.UserName, 
    r.Name as Role
FROM AspNetUsers u
LEFT JOIN AspNetUserRoles ur ON u.Id = ur.UserId
LEFT JOIN AspNetRoles r ON ur.RoleId = r.Id
ORDER BY u.UserName;

-- ===================================================
-- TO ASSIGN ADMIN ROLE TO A USER:
-- ===================================================
-- Uncomment the section below and replace 'admin' with the actual username

/*
DECLARE @UserId INT;
DECLARE @AdminRoleId INT;

-- Get the Admin role ID
SET @AdminRoleId = (SELECT Id FROM AspNetRoles WHERE Name = 'Admin');

-- Get the user ID (change 'admin' to your username)
SET @UserId = (SELECT Id FROM AspNetUsers WHERE UserName = 'admin');

-- Assign the role if the IDs are valid
IF @AdminRoleId IS NOT NULL AND @UserId IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM AspNetUserRoles WHERE UserId = @UserId AND RoleId = @AdminRoleId)
    BEGIN
        INSERT INTO AspNetUserRoles (UserId, RoleId)
        VALUES (@UserId, @AdminRoleId);
        PRINT 'Admin role assigned successfully to user!';
    END
    ELSE
    BEGIN
        PRINT 'User already has Admin role!';
    END
END
ELSE
BEGIN
    PRINT 'Error: User or role not found!';
END
*/

-- ===================================================
-- EXAMPLE: Assign admin role to user with username 'admin'
-- ===================================================
-- UNCOMMENT THIS BLOCK TO EXECUTE:
/*
DECLARE @UserId INT = (SELECT Id FROM AspNetUsers WHERE UserName = 'admin');
DECLARE @AdminRoleId INT = (SELECT Id FROM AspNetRoles WHERE Name = 'Admin');

IF @AdminRoleId IS NOT NULL AND @UserId IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM AspNetUserRoles WHERE UserId = @UserId AND RoleId = @AdminRoleId)
    BEGIN
        INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES (@UserId, @AdminRoleId);
        SELECT 'Admin role assigned!' as Result;
    END
    ELSE
    BEGIN
        SELECT 'User already has Admin role!' as Result;
    END
END
ELSE
BEGIN
    SELECT 'User or Admin role not found!' as Result;
END
*/
