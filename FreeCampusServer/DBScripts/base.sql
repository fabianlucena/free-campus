IF NOT EXISTS (SELECT 1 FROM auth.Users WHERE Username = 'system')
    THROW 50000, 'El usuario system no existe.', 1;
GO

DECLARE 
    @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
    @now DATETIME2 = GETUTCDATE();
MERGE auth.Roles AS target
USING (VALUES
    ('student', 'Student'),
    ('creator', 'Creator')
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
			''
		)
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

DECLARE 
    @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
    @now DATETIME2 = GETUTCDATE();
MERGE auth.Permissions AS target
USING (VALUES
    ('courses.view')
) AS source (Name)
    ON target.Name = source.Name
WHEN NOT MATCHED THEN
    INSERT (
        Uuid, CreatedAt, DeletedAt,
        CreatedById, DeletedById,
        Name
    )
    VALUES (
        NEWID(), @now, NULL,
        @systemUserId, NULL,
        source.Name
    );
GO

DECLARE 
    @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
    @now DATETIME2 = GETUTCDATE();
MERGE auth.PermissionsXRoles AS target
USING (SELECT r.Id, p.Id
	FROM auth.Roles r,
		auth.Permissions p
	WHERE CONCAT(r.Name, '*', p.Name)
		IN(
			'student*courses.view'
		)
) AS source (RoleId, PermissionId)
    ON target.RoleId = source.RoleId AND target.PermissionId = source.PermissionId
WHEN NOT MATCHED THEN
    INSERT (
		RoleId, PermissionId,
        CreatedAt, DeletedAt,
        CreatedById, DeletedById
    )
    VALUES (
		source.RoleId, source.PermissionId,
        @now, @now,
        @systemUserId, @systemUserId
    );
GO

/* Insert freeCampus system Organization */
DECLARE @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system');
MERGE auth.Companies AS target
USING (VALUES ('freeCampus')) AS source(Name)
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
        source.Name, 'Free campus academy'
    );
GO