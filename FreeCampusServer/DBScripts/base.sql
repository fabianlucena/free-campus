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
    ('availableCourses.view')
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
			'student*availableCourses.view'
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

/* Insert template Organization */
DECLARE @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system');
MERGE auth.Organizations AS target
USING (VALUES ('template')) AS source(Name)
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
        source.Name, 'Template'
    );
GO

/* Insert courses statuses */
DECLARE
	@systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system'),
	@templateId BIGINT = (SELECT Id FROM auth.Organizations WHERE Name = 'template');
MERGE fc.CourseStatuses AS target
USING (VALUES 
	( 1, 'Pending',    1, 'Pending',    'The enrollment request has been submitted but is still awaiting review or approval.'),
	( 2, 'Approved',   0, 'Approved',   'The enrollment request has been reviewed and officially approved by the institution or instructor.'),
	( 3, 'Rejected',   0, 'Rejected',   'The enrollment request was reviewed and explicitly denied.'),
	( 4, 'Waitlisted', 0, 'Waitlisted', 'The student is on a waiting list because the course is full. Enrollment will activate if a seat becomes available.'),
	( 5, 'Enrolled',   1, 'Enrolled',   'The student is officially enrolled in the course but has not yet started any activity.'),
	( 6, 'InProgress', 1, 'InProgress', 'The student has begun participating in the course (accessed content, submitted work, or attended sessions).'),
	( 7, 'Completed',  1, 'Completed',  'The student has successfully finished all required activities and met the completion criteria.'),
	( 8, 'Failed',     0, 'Failed',     'The student completed the course but did not meet the minimum requirements to pass.'),
	( 9, 'Dropped',    1, 'Dropped',    'The student voluntarily withdrew from the course after being enrolled.'),
	(10, 'Canceled',   1, 'Canceled',   'The enrollment was invalidated or removed by the institution (administrative error, payment issue, or policy violation).'),
	(11, 'Archived',   1, 'Archived',   'The enrollment is no longer active and has been stored for historical or auditing purposes.')
) AS source([Order], Name, IsActive, Title, Description)
    ON target.Name = source.Name AND target.OrganizationId = @templateId
WHEN NOT MATCHED THEN
    INSERT (
        Uuid, OrganizationId,
		[Order], Name, IsActive, Title, Description,
		CreatedAt, UpdatedAt, DeletedAt,
		CreatedById, UpdatedById, DeletedById
    )
    VALUES (
        NEWID(), @templateId,
		source.[Order], source.Name, source.IsActive, source.Title, source.Description,
		GETUTCDATE(), GETUTCDATE(), NULL,
        @systemUserId, @systemUserId, NULL
    );
GO

/* Insert freeCampus system Organization */
DECLARE @systemUserId BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system');
MERGE auth.Organizations AS target
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