$server = "(localdb)\mssqllocaldb"
$database = "ProblemSolvingPlatform"
$scriptsDir = "c:\Users\samia\OneDrive\Bureau\saad\leetCodeClone\ProblemSolvingPlatform\Scripts"

# Helper function to run sqlcmd
function Run-SqlScript {
    param(
        [string]$scriptPath
    )
    Write-Host "Running script: $scriptPath"
    sqlcmd -S $server -d $database -i "$scriptPath"
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Error running script $scriptPath"
    } else {
        Write-Host "Success!" -ForegroundColor Green
    }
}

# 1. Run problemes.sql (Seeds problems)
Run-SqlScript -scriptPath "$scriptsDir\problemes.sql"

# 2. Run SetupAdminCorrected.sql (Creates roles and admin)
# Note: This requires a user with ID=1 to exist if using the default value.
# But since the DB is fresh, no users exist yet unless created.
# Wait! SetupAdminCorrected.sql tries to assign admin role to User ID 1.
# If no users exist, it will just create roles and fail to assign.
# The user will need to register first, then run part of this script?
# Or we can insert a default admin user?

# Reading SetupAdminCorrected.sql content...
# It checks IF EXISTS (SELECT 1 FROM Users WHERE UserId = @UserIdToMakeAdmin).
# If the user doesn't exist, it prints "? User not found!".
# So running this on an empty "Users" table will only create Roles.
# This is fine. The roles are the most important part.
# The user can assign admin role later or I can create a default user here.

# Ideally, we should create a default admin user if one doesn't exist.
# But inserting a user into AspNetUsers (Users) requires PasswordHash etc.
# Typically we rely on the App's registration to create a user properly (hashing password).
# So for now, just creating roles is good enough.
Run-SqlScript -scriptPath "$scriptsDir\SetupAdminCorrected.sql"
