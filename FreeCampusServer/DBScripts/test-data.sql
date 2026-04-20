IF NOT EXISTS (SELECT 1 FROM auth.Users WHERE Username = 'system')
    THROW 50000, 'El usuario system no existe.', 1;
GO

DECLARE 
    @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
    @now DATETIME2 = GETUTCDATE(),
    @1234hash NVARCHAR(255) = '100000.He2nIoHKO5PDiudeF3GV1Q==.OXZML34kQ8gPcsX01odwNpaNmNMkMzlggv5pLKqzekg=';
MERGE auth.Users AS target
USING (VALUES
    ('jdoe',    'John Doe',   'jdoe@example.com'),
    ('mgarcia', 'María García','mgarcia@example.com')
) AS source (Username, DisplayName, Email)
    ON target.Username = source.Username
WHEN NOT MATCHED THEN
    INSERT (
        Uuid, CreatedAt, UpdatedAt, DeletedAt,
        CreatedById, UpdatedById, DeletedById,
        Username, DisplayName, Email, PasswordHash,
        IsActive, CanLogin, LastLoginAt
    )
    VALUES (
        NEWID(), @now, @now, NULL,
        @systemUserId, @systemUserId, NULL,
        source.Username, source.DisplayName, source.Email, @1234hash,
        1, 1, NULL
    );
GO

DECLARE 
    @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
    @now DATETIME2 = GETUTCDATE();
MERGE auth.Roles AS target
USING (VALUES
    ('role1', 'Role 1'),
    ('role2', 'Role 2'),
    ('role3', 'Role 3'),
    ('role4', 'Role 4'),
    ('role5', 'Role 5'),
    ('role1-1', 'Subrole 1 of role 1'),
    ('role1-2', 'Subrole 2 of role 1'),
    ('role1-3', 'Subrole 3 of role 1'),
    ('role1-2-1', 'Subrole 1 of subrole 2 of role 1'),
    ('role1-2-2', 'Subrole 2 of subrole 2 of role 1')
) AS source (Name, Description)
    ON target.Name = source.Name
WHEN NOT MATCHED THEN
    INSERT (
        Uuid, CreatedAt, UpdatedAt, DeletedAt,
        CreatedById, UpdatedById, DeletedById,
        Name, Description
    )
    VALUES (
        NEWID(), @now, @now, NULL,
        @systemUserId, @systemUserId, NULL,
        source.Name, source.Description
    );
GO

DECLARE 
    @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
    @now DATETIME2 = GETUTCDATE();
MERGE auth.RolesIncludes AS target
USING (SELECT r1.Id, r2.Id
	FROM auth.Roles r1,
		auth.Roles r2
	WHERE CONCAT(r1.Name, '*', r2.Name)
		IN(
		'role1-1*role1',
		'role1-2*role1',
		'role1-3*role1',
		'role1-2-1*role1-2',
		'role1-2-2*role1-2')
) AS source (RoleId, IncludeId)
    ON target.RoleId = source.RoleId AND target.IncludeId = source.IncludeId
WHEN NOT MATCHED THEN
    INSERT (
		RoleId, IncludeId,
        CreatedAt, DeletedAt,
        CreatedById, DeletedById
    )
    VALUES (
		source.RoleId, source.IncludeId,
        @now, @now,
        @systemUserId, @systemUserId
    );
GO