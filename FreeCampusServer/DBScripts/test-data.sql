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
		(@freeCampusId, 'Independiente', 'Programa para cursos independientes')
	) AS source(OrganizationId, Title, Description)	
    ON target.OrganizationId = source.OrganizationId AND target.Title = source.Title AND target.Description = source.Description
WHEN NOT MATCHED THEN
    INSERT (
		Uuid, OrganizationId,
        CreatedAt, UpdatedAt, DeletedAt,
        CreatedById, UpdatedById, DeletedById,
        Title, Description
    )
    VALUES (
		NEWID(), source.OrganizationId,
        @now, @now, NULL,
        @systemUserId, @systemUserId, NULL,
        source.Title, source.Description
    );
GO

/* Add independiente program */
DECLARE
	@systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
	@freeCampusId BIGINT = (SELECT Id FROM auth.Organizations WHERE Name = 'freeCampus'),
	@independienteId BIGINT = (SELECT Id FROM fc.ProgramTypes WHERE Title = 'Independiente'),
	@now DATETIME2 = GETUTCDATE();
MERGE fc.Programs AS target
USING (VALUES 
		(@freeCampusId, @independienteId, 'Independiente', 'Programa para cursos independientes')
	) AS source(OrganizationId, TypeId, Title, Description)	
    ON target.OrganizationId = source.OrganizationId AND target.TypeId = source.TypeId AND target.Title = source.Title AND target.Description = source.Description
WHEN NOT MATCHED THEN
    INSERT (
		Uuid, source.OrganizationId, TypeId,
        CreatedAt, UpdatedAt, DeletedAt,
        CreatedById, UpdatedById, DeletedById,
        Title, Description
    )
    VALUES (
		NEWID(), source.OrganizationId, source.TypeId,
        @now, @now, NULL,
        @systemUserId, @systemUserId, NULL,
        source.Title, source.Description
    );
GO

/* Add course types */
DECLARE
	@systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
	@freeCampusId BIGINT = (SELECT Id FROM auth.Organizations WHERE Name = 'freeCampus'),
	@now DATETIME2 = GETUTCDATE();
MERGE fc.CourseTypes AS target
USING (VALUES 
		(@freeCampusId, 'Independiente', 'Cursos independiente')
	) AS source(OrganizationId, Title, Description)	
    ON target.OrganizationId = source.OrganizationId, target.Title = source.Title AND target.Description = source.Description
WHEN NOT MATCHED THEN
    INSERT (
		Uuid, OrganizationId,
        CreatedAt, UpdatedAt, DeletedAt,
        CreatedById, UpdatedById, DeletedById,
        Title, Description
    )
    VALUES (
		NEWID(), source.OrganizationId,
        @now, @now, NULL,
        @systemUserId, @systemUserId, NULL,
        source.Title, source.Description
    );
GO

/* Add prueba 1 course */
DECLARE
	@systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
	@freeCampusId BIGINT = (SELECT Id FROM auth.Organizations WHERE Name = 'freeCampus'),
	@courseTypeIndependienteId BIGINT = (SELECT Id FROM fc.CourseTypes WHERE Title = 'Independiente'),
	@now DATETIME2 = GETUTCDATE();
MERGE fc.Courses AS target
USING (VALUES 
		(@freeCampusId, @courseTypeIndependienteId, 'Prueba 1', 'Curso de prueba 1')
	) AS source(Title, Description, TypeId, ProgramId)	
    ON target.OrganizationId = source.OrganizationId AND target.TypeId = source.TypeId AND target.Title = source.Title AND target.Description = source.Description
WHEN NOT MATCHED THEN
    INSERT (
		Uuid, OrganizationId, TypeId,
        CreatedAt, UpdatedAt, DeletedAt,
        CreatedById, UpdatedById, DeletedById,
        Title, Description
    )
    VALUES (
		NEWID(), source.OrganizationId, source.TypeId,
        @now, @now, NULL,
        @systemUserId, @systemUserId, NULL,
        source.Title, source.Description
    );
GO