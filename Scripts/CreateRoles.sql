-- ===================================================
-- CREATE ADMIN AND USER ROLES IN DATABASE
-- ===================================================
-- Run this script first to create the roles!

-- Create Admin Role
INSERT INTO AspNetRoles (Name, NormalizedName, ConcurrencyStamp)
VALUES ('Admin', 'ADMIN', NEWID());

-- Create User Role
INSERT INTO AspNetRoles (Name, NormalizedName, ConcurrencyStamp)
VALUES ('User', 'USER', NEWID());

-- Verify roles were created
SELECT 'Roles Created:' as Info;
SELECT Id, Name FROM AspNetRoles;
