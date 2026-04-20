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
		Email VARCHAR(256) NOT NULL,
		PasswordHash VARCHAR(256) NOT NULL,
		IsActive BIT NOT NULL,
		CanLogin BIT NOT NULL,
		LastLoginAt DATETIME2 NULL
	);

	INSERT INTO auth.Users (
		Uuid, CreatedAt, UpdatedAt, DeletedAt,
		CreatedById, UpdatedById, DeletedById,
		Username, DisplayName, Email, PasswordHash,
		IsActive, CanLogin, LastLogin
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

		UserId BIGINT NOT NULL,
		DeviceId BIGINT NOT NULL,

		CONSTRAINT FK_Sessions_CreatedBy FOREIGN KEY (CreatedById)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
		
		CONSTRAINT FK_Sessions_User FOREIGN KEY (UserId)
			REFERENCES auth.Users(Id) ON DELETE NO ACTION,
			
		CONSTRAINT FK_Sessions_Device FOREIGN KEY (DeviceId)
			REFERENCES auth.Devices(Id) ON DELETE NO ACTION,
	);
END
GO

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

IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'RolesXUsers'
      AND s.name = 'auth'
)
BEGIN
	CREATE TABLE auth.RolesXUsers (
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