IF NOT EXISTS (SELECT 1 FROM auth.Users WHERE Username = 'system')
    THROW 50000, 'El usuario system no existe.', 1;
GO

DECLARE 
    @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
    @now DATETIME2 = GETUTCDATE();
MERGE auth.Users AS target
USING (VALUES
    ('jdoe',    'John Doe'),
    ('mgarcia', 'María García'),
    ('student', 'Student'),
    ('creator', 'Creator')
) AS source (Username, DisplayName)
    ON target.Username = source.Username
WHEN NOT MATCHED THEN
    INSERT (
        Uuid, CreatedAt, UpdatedAt, DeletedAt,
        CreatedById, UpdatedById, DeletedById,
        Username, DisplayName,
        IsActive, CanLogin, LastLoginAt
    )
    VALUES (
        NEWID(), @now, @now, NULL,
        @systemUserId, @systemUserId, NULL,
        source.Username, source.DisplayName,
        1, 1, NULL
    );
GO

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
		AND u.Username IN('creator', 'student')
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

/* Add Role for creator */
DECLARE @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
	@creatorUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'creator'),
	@studentUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'student'),
	@creatorRoleId BIGINT = (SELECT Id FROM auth.Roles WHERE Name = 'creator'),
	@studentRoleId BIGINT = (SELECT Id FROM auth.Roles WHERE Name = 'student'),
	@freeCampusOrganizationId BIGINT = (SELECT Id FROM auth.Organizations WHERE Name = 'freeCampus');
MERGE auth.RolesXUsersXOrganizations AS target
USING (VALUES 
		(@creatorRoleId, @creatorUserId, @freeCampusOrganizationId),
		(@studentRoleId, @studentUserId, @freeCampusOrganizationId)
	) AS source(RoleId, UserId, OrganizationId)	
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

/* Add program types */
DECLARE
	@systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
	@freeCampusId BIGINT = (SELECT Id FROM auth.Organizations WHERE Name = 'freeCampus'),
	@now DATETIME2 = GETUTCDATE();
MERGE fc.ProgramTypes AS target
USING (VALUES 
		(@freeCampusId, 'standalone', 1, 'Standalone', 'Programa for standalone courses')
	) AS source(OrganizationId, Name, IsTranslatable, Title, Description)	
    ON target.OrganizationId = source.OrganizationId AND target.Name = source.Name
WHEN NOT MATCHED THEN
    INSERT (
		Uuid, OrganizationId,
        CreatedAt, UpdatedAt, DeletedAt,
        CreatedById, UpdatedById, DeletedById,
        Name, IsTranslatable, Title, Description
    )
    VALUES (
		NEWID(), source.OrganizationId,
        @now, @now, NULL,
        @systemUserId, @systemUserId, NULL,
        source.Name, source.IsTranslatable, source.Title, source.Description
    );
GO

/* Add independiente program */
DECLARE
	@systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
	@freeCampusId BIGINT = (SELECT Id FROM auth.Organizations WHERE Name = 'freeCampus'),
	@standaloneId BIGINT = (SELECT Id FROM fc.ProgramTypes WHERE Name = 'standalone'),
	@now DATETIME2 = GETUTCDATE();
MERGE fc.Programs AS target
USING (VALUES 
		(@freeCampusId, @standaloneId, 'standalone', 'standalone')
	) AS source(OrganizationId, TypeId, Name, Code)	
    ON target.OrganizationId = source.OrganizationId AND target.Name = source.Name
WHEN NOT MATCHED THEN
    INSERT (
		Uuid, OrganizationId, TypeId,
        CreatedAt, UpdatedAt, DeletedAt,
        CreatedById, UpdatedById, DeletedById,
        Name, Code
    )
    VALUES (
		NEWID(), source.OrganizationId, source.TypeId,
        @now, @now, NULL,
        @systemUserId, @systemUserId, NULL,
        source.Name, source.Code
    );
GO

/* Add independiente program version */
DECLARE
	@systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
	@standaloneId BIGINT = (SELECT Id FROM fc.Programs WHERE Name = 'standalone'),
	@now DATETIME2 = GETUTCDATE();
MERGE fc.ProgramVersions AS target
USING (VALUES 
		(@standaloneId, 1, '1.0', NULL, 'Independiente', 'Programa para cursos independientes', NULL)
	) AS source(ProgramId, VersionNumber, VersionLabel, PreviousVersionId, Title, Description, TotalCredits)	
    ON target.ProgramId = source.ProgramId AND target.VersionNumber = source.VersionNumber
WHEN NOT MATCHED THEN
    INSERT (
		Uuid, ProgramId,
		VersionNumber, VersionLabel, PreviousVersionId,
        Title, Description, TotalCredits,
        CreatedAt, UpdatedAt, DeletedAt,
        CreatedById, UpdatedById, DeletedById
    )
    VALUES (
		NEWID(), source.ProgramId,
		source.VersionNumber, source.VersionLabel, source.PreviousVersionId,
        source.Title, source.Description, source.TotalCredits,
        @now, @now, NULL,
        @systemUserId, @systemUserId, NULL
    );
GO

/* Add course types */
DECLARE
	@systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
	@freeCampusId BIGINT = (SELECT Id FROM auth.Organizations WHERE Name = 'freeCampus'),
	@now DATETIME2 = GETUTCDATE();
MERGE fc.CourseTypes AS target
USING (VALUES 
		(@freeCampusId, 'standalone', 1, 'Standalone', 'Standalone course')
	) AS source(OrganizationId, Name, IsTranslatable, Title, Description)	
    ON target.OrganizationId = source.OrganizationId AND target.Name = source.Name
WHEN NOT MATCHED THEN
    INSERT (
		Uuid, OrganizationId,
        CreatedAt, UpdatedAt, DeletedAt,
        CreatedById, UpdatedById, DeletedById,
		Name, IsTranslatable,
        Title, Description
    )
    VALUES (
		NEWID(), source.OrganizationId,
        @now, @now, NULL,
        @systemUserId, @systemUserId, NULL,
		source.Name, source.IsTranslatable,
        source.Title, source.Description
    );
GO

/* Add prueba 01 course */
DECLARE
	@systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
	@freeCampusId BIGINT = (SELECT Id FROM auth.Organizations WHERE Name = 'freeCampus'),
	@courseTypeIndependienteId BIGINT = (SELECT Id FROM fc.CourseTypes WHERE Name = 'standalone'),
	@now DATETIME2 = GETUTCDATE();
MERGE fc.Courses AS target
USING (VALUES 
		(@freeCampusId, @courseTypeIndependienteId, 'prueba01', 'prueba01', 1)
	) AS source(OrganizationId, TypeId, Code, Name, IsStandalone)	
    ON target.OrganizationId = source.OrganizationId AND target.TypeId = source.TypeId AND target.Name = source.Name
WHEN NOT MATCHED THEN
    INSERT (
		Uuid, OrganizationId, TypeId,
        CreatedAt, UpdatedAt, DeletedAt,
        CreatedById, UpdatedById, DeletedById,
        Code, Name, IsStandalone
    )
    VALUES (
		NEWID(), source.OrganizationId, source.TypeId,
        @now, @now, NULL,
        @systemUserId, @systemUserId, NULL,
        source.Code, source.Name, source.IsStandalone
    );
GO

/* Add prueba 01 course version */
DECLARE
	@systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
	@prueba01Id BIGINT = (SELECT Id FROM fc.Courses WHERE Name = 'prueba01'),
	@now DATETIME2 = GETUTCDATE();
MERGE fc.CourseVersions AS target
USING (VALUES 
		(@prueba01Id, 1, '1.0', null, 'Curso de prueba 01', 'Descripción para el curso de prueba 01', 60, 30)
	) AS source(CourseId, VersionNumber, VersionLabel, PreviousVersionId, Title, Description, Credits, Hours)	
    ON target.CourseId = source.CourseId AND target.VersionNumber = source.VersionNumber
WHEN NOT MATCHED THEN
    INSERT (
		Uuid, CourseId,
        VersionNumber, VersionLabel, PreviousVersionId,
		Title, Description, Credits, Hours,
        CreatedAt, UpdatedAt, DeletedAt,
        CreatedById, UpdatedById, DeletedById
    )
    VALUES (
		NEWID(), source.CourseId,
		source.VersionNumber, source.VersionLabel, source.PreviousVersionId,
		source.Title, source.Description, source.Credits, source.Hours,
        @now, @now, NULL,
        @systemUserId, @systemUserId, NULL
    );
GO