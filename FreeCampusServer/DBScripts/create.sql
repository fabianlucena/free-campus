/* Users table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'Users'
      AND s.name = 'auth'
)
BEGIN
	CREATE TABLE auth.Users (
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,
		CreatedAt DATETIME2 NOT NULL,
		UpdatedAt DATETIME2 NOT NULL,
		DeletedAt DATETIME2 NULL,
		CreatedById BIGINT NOT NULL,
		UpdatedById BIGINT NOT NULL,
		DeletedById BIGINT NULL,

		Username VARCHAR(64) NOT NULL,
		DisplayName VARCHAR(128) NOT NULL,
		IsActive BIT NOT NULL,
		CanLogin BIT NOT NULL,
		LastLoginAt DATETIME2 NULL
	);

	INSERT INTO auth.Users (
		Uuid, CreatedAt, UpdatedAt, DeletedAt,
		CreatedById, UpdatedById, DeletedById,
		Username, DisplayName, Email, PasswordHash,
		IsActive, CanLogin, LastLoginAt
	)
	VALUES (
		NEWID(), GETUTCDATE(), GETUTCDATE(), NULL,
		0, 0, NULL,
		'system', 'System', '', '',
		1, 1, NULL
	);

	DECLARE @Id BIGINT = SCOPE_IDENTITY();
	UPDATE auth.Users
	SET CreatedById = @Id,
		UpdatedById = @Id
	WHERE Id = @Id;

	ALTER TABLE auth.Users
	ADD CONSTRAINT FK_Users_CreatedBy
		FOREIGN KEY (CreatedById) REFERENCES auth.Users(Id) ON DELETE NO ACTION;

	ALTER TABLE auth.Users
	ADD CONSTRAINT FK_Users_UpdatedBy
		FOREIGN KEY (UpdatedById) REFERENCES auth.Users(Id) ON DELETE NO ACTION;

	ALTER TABLE auth.Users
	ADD CONSTRAINT FK_Users_DeletedBy
		FOREIGN KEY (DeletedById) REFERENCES auth.Users(Id) ON DELETE NO ACTION;
END
GO

/* UserPasswords table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'UserPasswords'
      AND s.name = 'auth'
)
BEGIN
	CREATE TABLE auth.UserPasswords (
		UserId BIGINT NOT NULL PRIMARY KEY,

		CreatedAt DATETIME2 NOT NULL,
		UpdatedAt DATETIME2 NOT NULL,
		DeletedAt DATETIME2 NULL,

		CreatedById BIGINT NOT NULL,
		UpdatedById BIGINT NOT NULL,
		DeletedById BIGINT NULL,

		Hash VARCHAR(64) NOT NULL,

		CONSTRAINT FK_UserPasswords_UserId
			FOREIGN KEY (UserId) REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_UserPasswords_CreatedById
			FOREIGN KEY (CreatedById) REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_UserPasswords_UpdatedById
			FOREIGN KEY (UpdatedById) REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_UserPasswords_DeletedById
			FOREIGN KEY (DeletedById) REFERENCES auth.Users(Id) ON DELETE NO ACTION
	);
END
GO

IF NOT EXISTS (SELECT 1 FROM auth.Users WHERE Username = 'system')
    THROW 50000, 'El usuario system no existe.', 1;
GO

/* Insert default admin user */
DECLARE @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
	@1234hash NVARCHAR(255) = '100000.He2nIoHKO5PDiudeF3GV1Q==.OXZML34kQ8gPcsX01odwNpaNmNMkMzlggv5pLKqzekg=';
MERGE auth.Users AS target
USING (VALUES ('admin')) AS source(Username)
    ON target.Username = source.Username
WHEN NOT MATCHED THEN
    INSERT (
        Uuid, CreatedAt, UpdatedAt, DeletedAt,
        CreatedById, UpdatedById, DeletedById,
        Username, DisplayName,
        IsActive, CanLogin, LastLoginAt
    )
    VALUES (
        NEWID(), GETUTCDATE(), GETUTCDATE(), NULL,
        @systemUserId, @systemUserId, NULL,
        source.Username, 'Administrator',
        1, 1, NULL
    );
GO

/* Insert default admin user password */
DECLARE @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
	@1234hash NVARCHAR(255) = '100000.He2nIoHKO5PDiudeF3GV1Q==.OXZML34kQ8gPcsX01odwNpaNmNMkMzlggv5pLKqzekg=';
MERGE auth.UserPasswords AS target
USING (SELECT u.Id
	FROM auth.Users u
	WHERE NOT EXISTS(
			SELECT *
			FROM auth.UserPasswords p
			WHERE p.UserId = u.Id
		)
		AND u.Username = 'admin'
	) AS source(UserId)
    ON target.UserId = source.UserId
WHEN NOT MATCHED THEN
    INSERT (
		UserId,
        CreatedAt, UpdatedAt, DeletedAt,
        CreatedById, UpdatedById, DeletedById,
        Hash
    )
    VALUES (
        source.UserId,
        GETUTCDATE(), GETUTCDATE(), NULL,
        @systemUserId, @systemUserId, NULL,
		@1234hash
    );
GO

/* Devices table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'Devices'
      AND s.name = 'auth'
)
BEGIN
	CREATE TABLE auth.Devices (
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		Token VARCHAR(64) NOT NULL,

		CONSTRAINT FK_Devices_CreatedBy FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* Sessions table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'Sessions'
      AND s.name = 'auth'
)
BEGIN
	CREATE TABLE auth.Sessions (
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		Token VARCHAR(64) NOT NULL,
		ExpireAt DATETIME2 NOT NULL,
		AutoLoginToken VARCHAR(64) NOT NULL,
		LastUsedAt DATETIME2 NOT NULL,
		ClosedAt DATETIME2 NULL,

		UserId BIGINT NOT NULL,
		DeviceId BIGINT NOT NULL,
		DataJson NVARCHAR(MAX) NULL,

		CONSTRAINT FK_Sessions_CreatedBy FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
		
		CONSTRAINT FK_Sessions_User FOREIGN KEY (UserId)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
			
		CONSTRAINT FK_Sessions_Device FOREIGN KEY (DeviceId)
			REFERENCES auth.Devices(Id) ON DELETE NO ACTION,
	);
END
GO

/* Companies table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'Companies'
      AND s.name = 'auth'
)
BEGIN
	CREATE TABLE auth.Companies (
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		UpdatedAt DATETIME2 NOT NULL,
		DeletedAt DATETIME2 NULL,

		CreatedById BIGINT NOT NULL,
		UpdatedById BIGINT NOT NULL,
		DeletedById BIGINT NULL,

		Name VARCHAR(128) NOT NULL,
		Description VARCHAR(256) NULL,

		CONSTRAINT FK_Companies_CreatedBy FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_Companies_UpdatedBy FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_Companies_DeletedBy FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* SessionCompanies table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'SessionCompanies'
      AND s.name = 'auth'
)
BEGIN
	CREATE TABLE auth.SessionCompanies (
		SessionId BIGINT NOT NULL,
		CompanyId BIGINT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		UpdatedAt DATETIME2 NOT NULL,
		DeletedAt DATETIME2 NULL,
		
		CreatedById BIGINT NOT NULL,
		UpdatedById BIGINT NOT NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_SessionCompanies_SessionId FOREIGN KEY (SessionId)
			REFERENCES auth.Sessions(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_SessionCompanies_CompanyId FOREIGN KEY (CompanyId)
			REFERENCES auth.Companies(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_SessionCompanies_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_SessionCompanies_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_SessionCompanies_DeletedById FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* Roles table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'Roles'
      AND s.name = 'auth'
)
BEGIN
	CREATE TABLE auth.Roles (
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		UpdatedAt DATETIME2 NOT NULL,
		DeletedAt DATETIME2 NULL,

		CreatedById BIGINT NOT NULL,
		UpdatedById BIGINT NOT NULL,
		DeletedById BIGINT NULL,

		Name VARCHAR(128) NOT NULL,
		Description VARCHAR(256) NULL,

		CONSTRAINT FK_Roles_CreatedBy FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_Roles_UpdatedBy FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_Roles_DeletedBy FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* Insert default admin role */
DECLARE @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system' );
MERGE auth.Roles AS target
USING (VALUES ('admin')) AS source(Name)
    ON target.Name = source.Name
WHEN NOT MATCHED THEN
    INSERT (
        Uuid, CreatedAt, UpdatedAt, DeletedAt,
        CreatedById, UpdatedById, DeletedById,
        Name, Description
    )
    VALUES (
        NEWID(), GETUTCDATE(), GETUTCDATE(), NULL,
        @systemUserId, @systemUserId, NULL,
        source.Name, 'Administrator role with full privileges'
    );
GO

/* Insert default system company */
DECLARE @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system');
MERGE auth.Companies AS target
USING (VALUES ('system')) AS source(Name)
    ON target.Name = source.Name
WHEN NOT MATCHED THEN
    INSERT (
        Uuid,
		CreatedAt, UpdatedAt, DeletedAt,
		CreatedById, UpdatedById, DeletedById,
        Name, Description
    )
    VALUES (
        NEWID(), GETUTCDATE(), GETUTCDATE(), NULL,
        @systemUserId, @systemUserId, NULL,
        source.Name, 'System company for administrator purposes'
    );
GO

/* RolesXUsersXCompanies table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'RolesXUsersXCompanies'
      AND s.name = 'auth'
)
BEGIN
	CREATE TABLE auth.RolesXUsersXCompanies (
		RoleId BIGINT NOT NULL,
		UserId BIGINT NOT NULL,
		CompanyId BIGINT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		DeletedAt DATETIME2 NULL,

		CreatedById BIGINT NOT NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_RolesXUsersXCompanies_Role FOREIGN KEY (RoleId)
			REFERENCES auth.Roles(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_RolesXUsersXCompanies_User FOREIGN KEY (UserId)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_RolesXUsersXCompanies_Company FOREIGN KEY (CompanyId)
			REFERENCES auth.Companies(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_RolesXUsersXCompanies_CreatedBy FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_RolesXUsersXCompanies_DeletedBy FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* Insert default admin user's role */
DECLARE @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
	@adminUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'admin'),
	@adminRoleId BIGINT = (SELECT Id FROM auth.Roles WHERE Name = 'admin'),
	@systemCompanyId BIGINT = (SELECT Id FROM auth.Companies WHERE Name = 'system');
MERGE auth.RolesXUsersXCompanies AS target
USING (VALUES (@adminRoleId, @adminUserId, @systemCompanyId)) AS source(RoleId, UserId, CompanyId)
    ON target.RoleId = source.RoleId AND target.UserId = source.UserId AND target.CompanyId = source.CompanyId
WHEN NOT MATCHED THEN
    INSERT (
        CreatedAt, DeletedAt,
        CreatedById, DeletedById,
        RoleId, UserId, CompanyId
    )
    VALUES (
        GETUTCDATE(), NULL,
        @systemUserId, NULL,
        source.RoleId, source.UserId, source.CompanyId
    );
GO

/* RolesIncludes table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'RolesIncludes'
      AND s.name = 'auth'
)
BEGIN
	CREATE TABLE auth.RolesIncludes (
		RoleId BIGINT NOT NULL,
		IncludeId BIGINT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		DeletedAt DATETIME2 NULL,

		CreatedById BIGINT NOT NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_RolesIncludes_Role FOREIGN KEY (RoleId)
			REFERENCES auth.Roles(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_RolesIncludes_Include FOREIGN KEY (IncludeId)
			REFERENCES auth.Roles(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_RolesIncludes_CreatedBy FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_RolesIncludes_DeletedBy FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* Permissions table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'Permissions'
      AND s.name = 'auth'
)
BEGIN
	CREATE TABLE auth.Permissions (
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		DeletedAt DATETIME2 NULL,

		CreatedById BIGINT NOT NULL,
		DeletedById BIGINT NULL,

		Name VARCHAR(128) NOT NULL,

		CONSTRAINT FK_Permissions_CreatedBy FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_Permissions_DeletedBy FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* PermissionsXRoles table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'PermissionsXRoles'
      AND s.name = 'auth'
)
BEGIN
	CREATE TABLE auth.PermissionsXRoles (
		PermissionId BIGINT NOT NULL,
		RoleId BIGINT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		DeletedAt DATETIME2 NULL,

		CreatedById BIGINT NOT NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_PermissionsXRoles_Permission FOREIGN KEY (PermissionId)
			REFERENCES auth.Permissions(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_PermissionsXRoles_Role FOREIGN KEY (RoleId)
			REFERENCES auth.Roles(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_PermissionsXRoles_CreatedBy FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_PermissionsXRoles_DeletedBy FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* fc schema */
IF NOT EXISTS (
    SELECT 1
    FROM sys.schemas
    WHERE name = 'fc'
)
BEGIN
    EXEC('CREATE SCHEMA fc');
END
GO

/* ProgramTypes table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'ProgramTypes'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.ProgramTypes(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		Title VARCHAR(256) NOT NULL,
		Description VARCHAR(MAX) NULL,

		CompanyId BIGINT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_ProgramTypes_CompanyId FOREIGN KEY (CompanyId)
			REFERENCES auth.Companies(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ProgramTypes_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ProgramTypes_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ProgramTypes_DeletedById FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* Programs table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'Programs'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.Programs(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		Title VARCHAR(256) NOT NULL,
		Description VARCHAR(MAX) NULL,

		TypeId BIGINT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_Programs_TypeId FOREIGN KEY (TypeId)
			REFERENCES fc.ProgramTypes(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_Programs_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_Programs_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_Programs_DeletedById FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* CourseTypes table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'CourseTypes'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.CourseTypes(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		Title VARCHAR(256) NOT NULL,
		Description VARCHAR(MAX) NULL,

		CompanyId BIGINT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_CourseTypes_CompanyId FOREIGN KEY (CompanyId)
			REFERENCES auth.Companies(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CourseTypes_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CourseTypes_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CourseTypes_DeletedById FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* Courses table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'Courses'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.Courses(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		Title VARCHAR(256) NOT NULL,
		Description VARCHAR(MAX) NULL,

		TypeId BIGINT NOT NULL,
		ProgramId BIGINT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_Courses_TypeId FOREIGN KEY (TypeId)
			REFERENCES fc.CourseTypes(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_Courses_ProgramId FOREIGN KEY (ProgramId)
			REFERENCES fc.Programs(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_Courses_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_Courses_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_Courses_DeletedById FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* ModuleTypes table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'ModuleTypes'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.ModuleTypes(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		Title VARCHAR(256) NOT NULL,
		Description VARCHAR(MAX) NULL,

		CompanyId BIGINT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_ModuleTypes_CompanyId FOREIGN KEY (CompanyId)
			REFERENCES auth.Companies(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ModuleTypes_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ModuleTypes_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ModuleTypes_DeletedById FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* Modules table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'Modules'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.Modules(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		Title VARCHAR(256) NOT NULL,
		Description VARCHAR(MAX) NULL,

		TypeId BIGINT NOT NULL,
		CourseId BIGINT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_Modules_TypeId FOREIGN KEY (TypeId)
			REFERENCES fc.ModuleTypes(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_Modules_CourseId FOREIGN KEY (CourseId)
			REFERENCES fc.Courses(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_Modules_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_Modules_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_Modules_DeletedById FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO