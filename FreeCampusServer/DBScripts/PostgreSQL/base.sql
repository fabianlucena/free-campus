DO $$ -- Check system user
BEGIN
	IF NOT EXISTS (SELECT 1 FROM auth.users WHERE username = 'system') THEN
		RAISE EXCEPTION 'El usuario system no existe.';
	END IF;
END $$;

WITH sys_user AS (SELECT id FROM auth.users WHERE username = 'system')
INSERT INTO auth.roles (
    uuid,
    name, description, 
    created_at, created_by_id, updated_at, updated_by_id
) VALUES
    (gen_random_uuid(), 'teacher', 'Teacher', now(), (SELECT id FROM sys_user), now(), (SELECT id FROM sys_user)),
    (gen_random_uuid(), 'creator', 'Creator', now(), (SELECT id FROM sys_user), now(), (SELECT id FROM sys_user)),
    (gen_random_uuid(), 'student', 'Student', now(), (SELECT id FROM sys_user), now(), (SELECT id FROM sys_user))
ON CONFLICT (name) DO NOTHING;

WITH sys_user AS (SELECT id FROM auth.users WHERE username = 'system'),
    now_utc AS (SELECT NOW() AT TIME ZONE 'UTC' AS now),
    source AS (
        SELECT r1.id AS role_id, r2.id AS include_id
        FROM auth.roles r1
        CROSS JOIN auth.roles r2
        WHERE CONCAT(r1.name, '*', r2.name) IN (
            ''
        )
    )
INSERT INTO auth.roles_includes (
    role_id, include_id,
    created_at, created_by_id,
    deleted_at, deleted_by_id
)
SELECT 
    s.role_id,
    s.include_id,
    n.now,
    su.id,
    n.now,
    su.id
FROM source s
CROSS JOIN sys_user su
CROSS JOIN now_utc n
ON CONFLICT (role_id, include_id) DO NOTHING;

WITH sys_user AS (SELECT id AS sys_user_id FROM auth.users WHERE username = 'system'),
    now_utc AS (SELECT NOW() AT TIME ZONE 'UTC' AS now),
    source AS (SELECT unnest(ARRAY[
            'availableCourses.view',
            'myCourses.view'
        ]) AS name
    )
INSERT INTO auth.permissions (
    uuid, name,
    created_at, created_by_id,
    deleted_at, deleted_by_id
)
SELECT 
    gen_random_uuid(),
    s.name,
    n.now,
    u.sys_user_id,
    NULL,
    NULL
FROM source s
CROSS JOIN sys_user u
CROSS JOIN now_utc n
ON CONFLICT (name) DO NOTHING;

WITH sys_user AS (SELECT id AS sys_user_id FROM auth.users WHERE username = 'system'),
    now_utc AS (SELECT NOW() AT TIME ZONE 'UTC' AS now),
    source AS (
        SELECT r.id AS role_id, p.id AS permission_id
        FROM auth.roles r
        CROSS JOIN auth.permissions p
        WHERE CONCAT(r.name, '*', p.name) IN (
            'student*availableCourses.view',
            'student*myCourses.view'
        )
    )
INSERT INTO auth.permissions_x_roles (
    role_id, permission_id,
    created_at, created_by_id,
    deleted_at, deleted_by_id
)
SELECT 
    s.role_id,
    s.permission_id,
    n.now,
    u.sys_user_id,
    NULL,
    NULL
FROM source s
CROSS JOIN sys_user u
CROSS JOIN now_utc n
ON CONFLICT (role_id, permission_id) DO NOTHING;

/* Insert template Organization */
WITH sys_user AS (SELECT id AS sys_user_id FROM auth.users WHERE username = 'system'),
    now_utc AS (SELECT NOW() AT TIME ZONE 'UTC' AS now),
    source AS (
        SELECT *
        FROM (
            VALUES 
                ('template', FALSE, TRUE, 'Template', 'For use as template only.')
        ) AS v(name, is_active, is_translatable, title, description)
    )
INSERT INTO auth.organizations (
    uuid,
    name, is_active, is_translatable, title, description,
    created_at, created_by_id, 
    updated_at, updated_by_id,
    deleted_at, deleted_by_id
)
SELECT 
    gen_random_uuid(),
    s.name, s.is_active, s.is_translatable, s.title, s.description,
    n.now, u.sys_user_id,
    n.now, u.sys_user_id,
    NULL, NULL
FROM source s
CROSS JOIN sys_user u
CROSS JOIN now_utc n
ON CONFLICT (name) DO NOTHING;

/* Insert courses statuses */
WITH sys_user AS (SELECT id AS sys_user_id FROM auth.users WHERE username = 'system'),
    template_org AS (SELECT id AS template_id FROM auth.organizations WHERE name = 'template'),
    now_utc AS (SELECT NOW() AT TIME ZONE 'UTC' AS now),
    source AS (
        SELECT *
        FROM (
            VALUES
                ( 1, 'Pending',    TRUE,  'Pending',    'The enrollment request has been submitted but is still awaiting review or approval.'),
                ( 2, 'Approved',   FALSE, 'Approved',   'The enrollment request has been reviewed and officially approved by the institution or instructor.'),
                ( 3, 'Rejected',   FALSE, 'Rejected',   'The enrollment request was reviewed and explicitly denied.'),
                ( 4, 'Waitlisted', FALSE, 'Waitlisted', 'The student is on a waiting list because the course is full. Enrollment will activate if a seat becomes available.'),
                ( 5, 'Enrolled',   TRUE,  'Enrolled',   'The student is officially enrolled in the course but has not yet started any activity.'),
                ( 6, 'InProgress', TRUE,  'InProgress', 'The student has begun participating in the course (accessed content, submitted work, or attended sessions).'),
                ( 7, 'Completed',  TRUE,  'Completed',  'The student has successfully finished all required activities and met the completion criteria.'),
                ( 8, 'Failed',     FALSE, 'Failed',     'The student completed the course but did not meet the minimum requirements to pass.'),
                ( 9, 'Dropped',    TRUE,  'Dropped',    'The student voluntarily withdrew from the course after being enrolled.'),
                (10, 'Canceled',   TRUE,  'Canceled',   'The enrollment was invalidated or removed by the institution (administrative error, payment issue, or policy violation).'),
                (11, 'Archived',   TRUE,  'Archived',   'The enrollment is no longer active and has been stored for historical or auditing purposes.')
        ) AS v(display_order, name, is_active, title, description)
    )
INSERT INTO fc.course_enrollment_statuses (
    uuid, organization_id, is_translatable,
    display_order, name, is_active, title, description,
    created_at, created_by_id,
    updated_at, updated_by_id,
    deleted_at, deleted_by_id
)
SELECT
    gen_random_uuid(),
    t.template_id, TRUE,
    s.display_order, s.name, s.is_active, s.title, s.description,
    n.now,
    u.sys_user_id,
    n.now,
    u.sys_user_id,
    NULL,
    NULL
FROM source s
CROSS JOIN sys_user u
CROSS JOIN template_org t
CROSS JOIN now_utc n
ON CONFLICT (name, organization_id) DO NOTHING;

/* Insert freeCampus system Organization */
WITH sys_user AS (SELECT id AS sys_user_id FROM auth.users WHERE username = 'system'),
    now_utc AS (SELECT NOW() AT TIME ZONE 'UTC' AS now),
    source AS (
        SELECT *
        FROM (
            VALUES 
                ('freeCampus', FALSE, TRUE, 'Free Campus Academy', 'Free campus academy.')
        ) AS v(name, is_active, is_translatable, title, description)
    )
INSERT INTO auth.organizations (
    uuid,
    name, is_active, is_translatable, title, description,
    created_at,
    created_by_id,
    updated_at,
    updated_by_id,
    deleted_at,
    deleted_by_id
)
SELECT
    gen_random_uuid(),
    s.name, s.is_active, s.is_translatable, s.title, s.description,
    n.now,
    u.sys_user_id,
    n.now,
    u.sys_user_id,
    NULL,
    NULL
FROM source s
CROSS JOIN sys_user u
CROSS JOIN now_utc n
ON CONFLICT (name) DO NOTHING;

/* Insert courses statuses for freeCampus */
WITH sys_user AS (SELECT id AS sys_user_id FROM auth.users WHERE username = 'system'),
    template_org AS (SELECT id AS template_id FROM auth.organizations WHERE name = 'template'),
    freecampus_org AS (SELECT id AS freecampus_id FROM auth.organizations WHERE name = 'freeCampus'),
    now_utc AS (SELECT NOW() AT TIME ZONE 'UTC' AS now),
    source AS (
        SELECT 
            cs.display_order,
            cs.name,
            cs.is_active,
            cs.is_translatable,
            cs.title,
            cs.description
        FROM fc.course_enrollment_statuses cs
        JOIN template_org t ON cs.organization_id = t.template_id
    )
INSERT INTO fc.course_enrollment_statuses (
    uuid, organization_id,
    display_order, name, is_active, is_translatable, title, description,
    created_at,
    created_by_id,
    updated_at,
    updated_by_id,
    deleted_at,
    deleted_by_id
)
SELECT
    gen_random_uuid(),
    f.freecampus_id,
    s.display_order, s.name, s.is_active, s.is_translatable, s.title, s.description,
    n.now,
    u.sys_user_id,
    n.now,
    u.sys_user_id,
    NULL,
    NULL
FROM source s
CROSS JOIN sys_user u
CROSS JOIN freecampus_org f
CROSS JOIN now_utc n
ON CONFLICT (name, organization_id) DO NOTHING;
