-- This script creates the necessary database tables for ASP.NET Identity
-- Run this script on your database to set up the tables

-- Create AspNetUsers table (maps to your User model)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUsers')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [UserName] [nvarchar](256),
        [NormalizedUserName] [nvarchar](256),
        [Email] [nvarchar](256),
        [NormalizedEmail] [nvarchar](256),
        [EmailConfirmed] [bit] NOT NULL DEFAULT 0,
        [PasswordHash] [nvarchar](max),
        [SecurityStamp] [nvarchar](max),
        [ConcurrencyStamp] [nvarchar](max),
        [PhoneNumber] [nvarchar](max),
        [PhoneNumberConfirmed] [bit] NOT NULL DEFAULT 0,
        [TwoFactorEnabled] [bit] NOT NULL DEFAULT 0,
        [LockoutEnd] [datetimeoffset],
        [LockoutEnabled] [bit] NOT NULL DEFAULT 1,
        [AccessFailedCount] [int] NOT NULL DEFAULT 0,
        [FirstName] [nvarchar](50),
        [LastName] [nvarchar](50),
        [RegistrationDate] [datetime] DEFAULT GETDATE(),
        [ProfilePicture] [nvarchar](255),
        [Role] [int] NOT NULL DEFAULT 0,
        [IsActive] [bit] NOT NULL DEFAULT 1
    );
END;

-- Create AspNetRoles table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetRoles')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Name] [nvarchar](256),
        [NormalizedName] [nvarchar](256),
        [ConcurrencyStamp] [nvarchar](max)
    );
END;

-- Create AspNetUserRoles table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserRoles')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] [int] NOT NULL,
        [RoleId] [int] NOT NULL,
        PRIMARY KEY ([UserId], [RoleId]),
        FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id]) ON DELETE CASCADE,
        FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles]([Id]) ON DELETE CASCADE
    );
END;

-- Create AspNetUserClaims table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserClaims')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [UserId] [int] NOT NULL,
        [ClaimType] [nvarchar](max),
        [ClaimValue] [nvarchar](max),
        FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id]) ON DELETE CASCADE
    );
END;

-- Create AspNetUserLogins table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserLogins')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] [nvarchar](128) NOT NULL,
        [ProviderKey] [nvarchar](128) NOT NULL,
        [ProviderDisplayName] [nvarchar](max),
        [UserId] [int] NOT NULL,
        PRIMARY KEY ([LoginProvider], [ProviderKey]),
        FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id]) ON DELETE CASCADE
    );
END;

-- Create AspNetUserTokens table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserTokens')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] [int] NOT NULL,
        [LoginProvider] [nvarchar](128) NOT NULL,
        [Name] [nvarchar](128) NOT NULL,
        [Value] [nvarchar](max),
        PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id]) ON DELETE CASCADE
    );
END;

-- Create AspNetRoleClaims table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetRoleClaims')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [RoleId] [int] NOT NULL,
        [ClaimType] [nvarchar](max),
        [ClaimValue] [nvarchar](max),
        FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles]([Id]) ON DELETE CASCADE
    );
END;

-- Create indices
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'UserNameIndex')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers]([NormalizedUserName]);
END;

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'EmailIndex')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers]([NormalizedEmail]);
END;

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'RoleNameIndex')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles]([NormalizedName]);
END;

PRINT 'ASP.NET Identity tables created successfully';
