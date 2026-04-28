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
	@freeCampusCompanyId BIGINT = (SELECT Id FROM auth.Companies WHERE Name = 'freeCampus');
MERGE auth.RolesXUsersXCompanies AS target
USING (VALUES 
		(@creatorRoleId, @creatorUserId, @freeCampusCompanyId),
		(@studentRoleId, @studentUserId, @freeCampusCompanyId)
	) AS source(RoleId, UserId, CompanyId)	
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

/* Add programs types */
DECLARE @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system');
MERGE fc.ProgramTypes AS target
USING (VALUES 
		('Independiente', 'Programa para cursos independientes')
	) AS source(RoleId, UserId, CompanyId)	
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