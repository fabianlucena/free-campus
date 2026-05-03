/* auth schema */
IF NOT EXISTS (
    SELECT 1
    FROM sys.schemas
    WHERE name = 'auth'
)
BEGIN
    EXEC('CREATE SCHEMA auth');
END
GO

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

		Username NVARCHAR(64) NOT NULL,
		DisplayName NVARCHAR(128) NOT NULL,
		IsActive BIT NOT NULL,
		CanLogin BIT NOT NULL,
		LastLoginAt DATETIME2 NULL
	);

	INSERT INTO auth.Users (
		Uuid, CreatedAt, UpdatedAt, DeletedAt,
		CreatedById, UpdatedById, DeletedById,
		Username, DisplayName,
		IsActive, CanLogin, LastLoginAt
	)
	VALUES (
		NEWID(), GETUTCDATE(), GETUTCDATE(), NULL,
		0, 0, NULL,
		'system', 'System',
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

		Hash NVARCHAR(254) NOT NULL,

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

		Token NVARCHAR(64) NOT NULL,

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

		Token NVARCHAR(64) NOT NULL,
		ExpireAt DATETIME2 NOT NULL,
		AutoLoginToken NVARCHAR(64) NOT NULL,
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

/* Organizations table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'Organizations'
      AND s.name = 'auth'
)
BEGIN
	CREATE TABLE auth.Organizations (
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		Name NVARCHAR(128) NOT NULL,
		IsActive BIT NOT NULL,
		IsTranslatable BIT NOT NULL,
		Title NVARCHAR(128) NOT NULL,
		Description NVARCHAR(256) NULL,

		CONSTRAINT FK_Organizations_CreatedBy FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_Organizations_UpdatedBy FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_Organizations_DeletedBy FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CreatedAt DATETIME2 NOT NULL,
		UpdatedAt DATETIME2 NOT NULL,
		DeletedAt DATETIME2 NULL,

		CreatedById BIGINT NOT NULL,
		UpdatedById BIGINT NOT NULL,
		DeletedById BIGINT NULL,
	);
END
GO

/* SessionOrganizations table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'SessionOrganizations'
      AND s.name = 'auth'
)
BEGIN
	CREATE TABLE auth.SessionOrganizations (
		SessionId BIGINT NOT NULL,
		OrganizationId BIGINT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		UpdatedAt DATETIME2 NOT NULL,
		DeletedAt DATETIME2 NULL,
		
		CreatedById BIGINT NOT NULL,
		UpdatedById BIGINT NOT NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_SessionOrganizations_SessionId FOREIGN KEY (SessionId)
			REFERENCES auth.Sessions(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_SessionOrganizations_OrganizationId FOREIGN KEY (OrganizationId)
			REFERENCES auth.Organizations(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_SessionOrganizations_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_SessionOrganizations_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_SessionOrganizations_DeletedById FOREIGN KEY (DeletedById)
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

		Name NVARCHAR(128) NOT NULL,
		Description NVARCHAR(256) NULL,

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

/* Insert default system Organization */
DECLARE @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system');
MERGE auth.Organizations AS target
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
        source.Name, 'System Organization for administrator purposes'
    );
GO

/* RolesXUsersXOrganizations table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'RolesXUsersXOrganizations'
      AND s.name = 'auth'
)
BEGIN
	CREATE TABLE auth.RolesXUsersXOrganizations (
		RoleId BIGINT NOT NULL,
		UserId BIGINT NOT NULL,
		OrganizationId BIGINT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		DeletedAt DATETIME2 NULL,

		CreatedById BIGINT NOT NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_RolesXUsersXOrganizations_Role FOREIGN KEY (RoleId)
			REFERENCES auth.Roles(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_RolesXUsersXOrganizations_User FOREIGN KEY (UserId)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_RolesXUsersXOrganizations_Organization FOREIGN KEY (OrganizationId)
			REFERENCES auth.Organizations(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_RolesXUsersXOrganizations_CreatedBy FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_RolesXUsersXOrganizations_DeletedBy FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* Insert default admin user's role */
DECLARE @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
	@adminUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'admin'),
	@adminRoleId BIGINT = (SELECT Id FROM auth.Roles WHERE Name = 'admin'),
	@systemOrganizationId BIGINT = (SELECT Id FROM auth.Organizations WHERE Name = 'system');
MERGE auth.RolesXUsersXOrganizations AS target
USING (VALUES (@adminRoleId, @adminUserId, @systemOrganizationId)) AS source(RoleId, UserId, OrganizationId)
    ON target.RoleId = source.RoleId AND target.UserId = source.UserId AND target.OrganizationId = source.OrganizationId
WHEN NOT MATCHED THEN
    INSERT (
        CreatedAt, DeletedAt,
        CreatedById, DeletedById,
        RoleId, UserId, OrganizationId
    )
    VALUES (
        GETUTCDATE(), NULL,
        @systemUserId, NULL,
        source.RoleId, source.UserId, source.OrganizationId
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

		Name NVARCHAR(128) NOT NULL,

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

		OrganizationId BIGINT NOT NULL,

		Name NVARCHAR(256) NOT NULL,
		Title NVARCHAR(256) NOT NULL,
		Description NVARCHAR(MAX) NULL,

		IsTranslatable BIT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_ProgramTypes_OrganizationId FOREIGN KEY (OrganizationId)
			REFERENCES auth.Organizations(Id) ON DELETE NO ACTION,

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

		OrganizationId BIGINT NOT NULL,
		TypeId BIGINT NOT NULL,

		Code NVARCHAR(256) NOT NULL,
		[Name] NVARCHAR(256) NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_Programs_OrganizationId FOREIGN KEY (OrganizationId)
			REFERENCES auth.Organizations(Id) ON DELETE NO ACTION,

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

/* ProgramVersions table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'ProgramVersions'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.ProgramVersions(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		ProgramId BIGINT NOT NULL,

		VersionNumber INT NOT NULL,
		VersionLabel NVARCHAR(256) NOT NULL,
		PreviousVersionId BIGINT NULL,

		Title NVARCHAR(256) NOT NULL,
		Description NVARCHAR(MAX) NULL,
		TotalCredits INT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_ProgramVersions_OrganizationId FOREIGN KEY (ProgramId)
			REFERENCES fc.Programs(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ProgramVersions_PreviousVersionId FOREIGN KEY (PreviousVersionId)
			REFERENCES fc.ProgramVersions(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ProgramVersions_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ProgramVersions_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ProgramVersions_DeletedById FOREIGN KEY (DeletedById)
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

		OrganizationId BIGINT NOT NULL,

		Name NVARCHAR(256) NOT NULL,
		Title NVARCHAR(256) NOT NULL,
		Description NVARCHAR(MAX) NULL,

		IsTranslatable BIT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_CourseTypes_OrganizationId FOREIGN KEY (OrganizationId)
			REFERENCES auth.Organizations(Id) ON DELETE NO ACTION,

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

		OrganizationId BIGINT NOT NULL,
		TypeId BIGINT NOT NULL,

		Code NVARCHAR(256) NOT NULL,
		Name NVARCHAR(256) NOT NULL,
		IsStandalone BIT NULL,
		
		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_Courses_OrganizationId FOREIGN KEY (OrganizationId)
			REFERENCES auth.Organizations(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_Courses_TypeId FOREIGN KEY (TypeId)
			REFERENCES fc.CourseTypes(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_Courses_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_Courses_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_Courses_DeletedById FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* CoursePrerequisites table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'CoursePrerequisites'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.CoursePrerequisites(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		CourseId BIGINT NOT NULL,
		PrerequisiteId BIGINT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_CoursePrerequisites_CourseId FOREIGN KEY (CourseId)
			REFERENCES fc.Courses(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CoursePrerequisites_PrerequisiteId FOREIGN KEY (PrerequisiteId)
			REFERENCES fc.Courses(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CoursePrerequisites_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CoursePrerequisites_DeletedById FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* CourseVersions table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'CourseVersions'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.CourseVersions(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		CourseId BIGINT NOT NULL,

		VersionNumber INT NOT NULL,
		VersionLabel NVARCHAR(256) NOT NULL,
		PreviousVersionId BIGINT NULL,

		Title NVARCHAR(256) NOT NULL,
		Description NVARCHAR(MAX) NULL,
		Credits INT NULL,
		Hours INT NULL,
		
		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_CourseVersions_OrganizationId FOREIGN KEY (CourseId)
			REFERENCES fc.Courses(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CourseVersions_PreviousVersionId FOREIGN KEY (PreviousVersionId)
			REFERENCES fc.CourseVersions(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CourseVersions_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CourseVersions_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CourseVersions_DeletedById FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* LearningItemTypes table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'LearningItemTypes'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.LearningItemTypes(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		OrganizationId BIGINT NOT NULL,

		Name NVARCHAR(256) NOT NULL,
		Title NVARCHAR(256) NOT NULL,
		Description NVARCHAR(MAX) NULL,

		IsTranslatable BIT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_LearningItemTypes_OrganizationId FOREIGN KEY (OrganizationId)
			REFERENCES auth.Organizations(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_LearningItemTypes_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_LearningItemTypes_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_LearningItemTypes_DeletedById FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* LearningItems table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'LearningItems'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.LearningItems(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		OrganizationId BIGINT NOT NULL,
		TypeId BIGINT NOT NULL,
		
		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_LearningItems_OrganizationId FOREIGN KEY (OrganizationId)
			REFERENCES auth.Organizations(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_LearningItems_TypeId FOREIGN KEY (TypeId)
			REFERENCES fc.LearningItemTypes(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_LearningItems_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_LearningItems_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_LearningItems_DeletedById FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* LearningItemVersions table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'LearningItemVersions'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.LearningItemVersions(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		LearningItemId BIGINT NOT NULL,

		VersionNumber INT NOT NULL,
		VersionLabel NVARCHAR(256) NOT NULL,
		PreviousVersionId BIGINT NULL,

		Title NVARCHAR(256) NOT NULL,
		Description NVARCHAR(MAX) NULL,
		IsPublished BIT NOT NULL,
		
		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_LearningItemVersions_OrganizationId FOREIGN KEY (LearningItemId)
			REFERENCES fc.LearningItems(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_LearningItemVersions_TypeId FOREIGN KEY (PreviousVersionId)
			REFERENCES fc.LearningItemVersions(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_LearningItemVersions_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_LearningItemVersions_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_LearningItemVersions_DeletedById FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* ProgramVersionXCourseVersions table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'ProgramVersionXCourseVersions'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.ProgramVersionXCourseVersions(
		ProgramVersionId BIGINT NOT NULL,
		CourseVersionId BIGINT NOT NULL,

		Code NVARCHAR(256) NOT NULL,
		[Order] INT NOT NULL,
		Level INT NOT NULL,

		IsActive BIT NOT NULL DEFAULT 1,
		IsCore BIT NOT NULL DEFAULT 1,
		IsRequired BIT NOT NULL DEFAULT 1,
		IsElective BIT NOT NULL DEFAULT 1,

		CreatedAt DATETIME2 NOT NULL,
		DeletedAt DATETIME2 NULL,

		CreatedById BIGINT NOT NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_ProgramVersionXCourseVersions_ProgramVersionId FOREIGN KEY (ProgramVersionId)
			REFERENCES fc.ProgramVersions(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_ProgramVersionXCourseVersions_CourseVersionId FOREIGN KEY (CourseVersionId)
			REFERENCES fc.CourseVersions(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_ProgramVersionXCourseVersions_CreatedBy FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_ProgramVersionXCourseVersions_DeletedBy FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* TeachingRoles table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'TeachingRoles'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.TeachingRoles(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		Name NVARCHAR(255) NULL,
		Title NVARCHAR(255) NULL,
		IsTranslatable BIT NOT NULL,
		Description NVARCHAR(MAX) NULL,

		CreatedAt DATETIME2 NOT NULL,
		UpdatedAt DATETIME2 NOT NULL,
		DeletedAt DATETIME2 NULL,

		CreatedById BIGINT NOT NULL,
		UpdatedById BIGINT NOT NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_TeachingRoles_CreatedBy FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_TeachingRoles_UpdatedBy FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_TeachingRoles_DeletedBy FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* TeachingPermissions table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'TeachingPermissions'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.TeachingPermissions(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		Name NVARCHAR(255) NULL,
		Title NVARCHAR(255) NULL,
		IsTranslatable BIT NOT NULL,
		Description NVARCHAR(MAX) NULL,

		CreatedAt DATETIME2 NOT NULL,
		UpdatedAt DATETIME2 NOT NULL,
		DeletedAt DATETIME2 NULL,

		CreatedById BIGINT NOT NULL,
		UpdatedById BIGINT NOT NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_TeachingPermissions_CreatedBy FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_TeachingPermissions_UpdatedBy FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_TeachingPermissions_DeletedBy FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* TeachingAssignments table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'TeachingAssignments'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.TeachingAssignments(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		CourseId BIGINT NOT NULL,
		InstructorId BIGINT NOT NULL,
		TeachingRoleId BIGINT NOT NULL,

		AssignedAt DATETIME2 NOT NULL,
		AssignedById BIGINT NOT NULL,

		IsActive BIT NOT NULL DEFAULT 1,
		Notes NVARCHAR(MAX) NULL,

		CreatedAt DATETIME2 NOT NULL,
		UpdatedAt DATETIME2 NOT NULL,
		DeletedAt DATETIME2 NULL,

		CreatedById BIGINT NOT NULL,
		UpdatedById BIGINT NOT NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_TeachingAssignments_CourseId FOREIGN KEY (CourseId)
			REFERENCES fc.Courses(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_TeachingAssignments_InstructorId FOREIGN KEY (InstructorId)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_TeachingAssignments_TeachingRoleId FOREIGN KEY (TeachingRoleId)
			REFERENCES fc.TeachingRoles(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_TeachingAssignments_AssignedById FOREIGN KEY (AssignedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_TeachingAssignments_CreatedBy FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_TeachingAssignments_UpdatedBy FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_TeachingAssignments_DeletedBy FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* TeachingAssignmentXPermissions table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'TeachingAssignmentXPermissions'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.TeachingAssignmentXPermissions(
		TeachingAssignmentId BIGINT NOT NULL,
		PermissionsId BIGINT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		DeletedAt DATETIME2 NULL,

		CreatedById BIGINT NOT NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_TeachingAssignmentXPermissions_ProgramVersionId FOREIGN KEY (TeachingAssignmentId)
			REFERENCES fc.TeachingAssignments(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_TeachingAssignmentXPermissions_CourseVersionId FOREIGN KEY (PermissionsId)
			REFERENCES fc.TeachingPermissions(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_TeachingAssignmentXPermissions_CreatedBy FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_TeachingAssignmentXPermissions_DeletedBy FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* CourseEnrollmentStatuses table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'CourseEnrollmentStatuses'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.CourseEnrollmentStatuses(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		OrganizationId BIGINT NOT NULL,

		[Order] INT NOT NULL,
		Name NVARCHAR(256) NOT NULL,
		IsActive BIT NOT NULL DEFAULT 1,
		Title NVARCHAR(256) NOT NULL,
		Description NVARCHAR(MAX) NULL,

		IsTranslatable BIT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_CourseEnrollmentStatuses_OrganizationId FOREIGN KEY (OrganizationId)
			REFERENCES auth.Organizations(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CourseEnrollmentStatuses_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CourseEnrollmentStatuses_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CourseEnrollmentStatuses_DeletedById FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* CourseEnrollments table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'CourseEnrollments'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.CourseEnrollments(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		CourseVersionId BIGINT NOT NULL,
		StudentId BIGINT NOT NULL,

		EnrolledAt DATETIME2 NOT NULL,
		EnrolledById BIGINT NOT NULL,
		CompletedAt DATETIME2 NULL,
		DroppedAt DATETIME2 NULL,
		StatusId BIGINT NOT NULL,
		FinalGrade DECIMAL(5,2) NULL,
		IsActive BIT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_CourseEnrollments_CourseId FOREIGN KEY (CourseVersionId)
			REFERENCES fc.CourseVersions(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CourseEnrollments_StudentId FOREIGN KEY (StudentId)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CourseEnrollments_EnrolledById FOREIGN KEY (EnrolledById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CourseEnrollments_StatusId FOREIGN KEY (StatusId)
			REFERENCES fc.CourseEnrollmentStatuses(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CourseEnrollments_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CourseEnrollments_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_CourseEnrollments_DeletedById FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* ProgramEnrollmentStatuses table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'ProgramEnrollmentStatuses'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.ProgramEnrollmentStatuses(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		OrganizationId BIGINT NOT NULL,

		[Order] INT NOT NULL,
		Name NVARCHAR(256) NOT NULL,
		IsActive BIT NOT NULL DEFAULT 1,
		Title NVARCHAR(256) NOT NULL,
		Description NVARCHAR(MAX) NULL,

		IsTranslatable BIT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_ProgramEnrollmentStatuses_OrganizationId FOREIGN KEY (OrganizationId)
			REFERENCES auth.Organizations(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ProgramEnrollmentStatuses_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ProgramEnrollmentStatuses_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ProgramEnrollmentStatuses_DeletedById FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO

/* ProgramEnrollments table */
IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'ProgramEnrollments'
      AND s.name = 'fc'
)
BEGIN
	CREATE TABLE fc.ProgramEnrollments(
		Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Uuid UNIQUEIDENTIFIER NOT NULL,

		ProgramVersionId BIGINT NOT NULL,
		StudentId BIGINT NOT NULL,

		EnrolledAt DATETIME2 NOT NULL,
		EnrolledById BIGINT NOT NULL,
		CompletedAt DATETIME2 NULL,
		DroppedAt DATETIME2 NULL,
		StatusId BIGINT NOT NULL,
		FinalGrade DECIMAL(5,2) NULL,
		IsActive BIT NOT NULL,

		CreatedAt DATETIME2 NOT NULL,
		CreatedById BIGINT NOT NULL,

		UpdatedAt DATETIME2 NOT NULL,
		UpdatedById BIGINT NOT NULL,

		DeletedAt DATETIME2 NULL,
		DeletedById BIGINT NULL,

		CONSTRAINT FK_fc_ProgramEnrollments_ProgramId FOREIGN KEY (ProgramVersionId)
			REFERENCES fc.ProgramVersions(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ProgramEnrollments_StudentId FOREIGN KEY (StudentId)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ProgramEnrollments_EnrolledById FOREIGN KEY (EnrolledById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ProgramEnrollments_StatusId FOREIGN KEY (StatusId)
			REFERENCES fc.ProgramEnrollmentStatuses(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ProgramEnrollments_CreatedById FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ProgramEnrollments_UpdatedById FOREIGN KEY (UpdatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_fc_ProgramEnrollments_DeletedById FOREIGN KEY (DeletedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
	);
END
GO