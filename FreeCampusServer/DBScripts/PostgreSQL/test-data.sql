DO $$ -- Check system user
BEGIN
	IF NOT EXISTS (SELECT 1 FROM auth.users WHERE username = 'system') THEN
		RAISE EXCEPTION 'El usuario system no existe.';
	END IF;
END $$;

WITH sys_user AS (
    SELECT id AS sys_user_id
    FROM auth.users
    WHERE username = 'system'
),
now_utc AS (
    SELECT NOW() AT TIME ZONE 'UTC' AS now
),
source AS (
    SELECT *
    FROM (
        VALUES
            ('jdoe',    'John Doe'),
            ('mgarcia', 'María García'),
            ('student', 'Student'),
            ('creator', 'Creator')
    ) AS v(username, display_name)
)
INSERT INTO auth.users (
    uuid,
    created_at, updated_at, deleted_at,
    created_by_id, updated_by_id, deleted_by_id,
    username, display_name,
    is_active, can_login, last_login_at
)
SELECT
    gen_random_uuid(),
    n.now, n.now, NULL,
    su.sys_user_id, su.sys_user_id, NULL,
    s.username, s.display_name,
    TRUE, TRUE, NULL
FROM source s
CROSS JOIN sys_user su
CROSS JOIN now_utc n
ON CONFLICT (username) DO NOTHING;

WITH sys_user AS (
        SELECT id AS sys_user_id
        FROM auth.users
        WHERE username = 'system'
    ),
    hash_value AS (
        SELECT '100000.He2nIoHKO5PDiudeF3GV1Q==.OXZML34kQ8gPcsX01odwNpaNmNMkMzlggv5pLKqzekg=' AS hash
    ),
    now_utc AS (
        SELECT NOW() AT TIME ZONE 'UTC' AS now
    ),
    source AS (
        SELECT u.id AS user_id
        FROM auth.users u
        WHERE u.username IN ('creator', 'student')
        AND NOT EXISTS (
                SELECT 1
                FROM auth.user_passwords p
                WHERE p.user_id = u.id
        )
    )
INSERT INTO auth.user_passwords (
    user_id,
    created_at, updated_at, deleted_at,
    created_by_id, updated_by_id, deleted_by_id,
    password_hash
)
SELECT
    s.user_id,
    n.now, n.now, NULL,
    su.sys_user_id, su.sys_user_id, NULL,
    hv.hash
FROM source s
CROSS JOIN sys_user su
CROSS JOIN now_utc n
CROSS JOIN hash_value hv
ON CONFLICT (user_id) DO NOTHING;

/* Add Role for creator */
WITH
sys_user AS (
	    SELECT id AS sys_user_id
	    FROM auth.users
	    WHERE username = 'system'
	),
	creator_user AS (
	    SELECT id AS creator_user_id
	    FROM auth.users
	    WHERE username = 'creator'
	),
	student_user AS (
	    SELECT id AS student_user_id
	    FROM auth.users
	    WHERE username = 'student'
	),
	creator_role AS (
	    SELECT id AS creator_role_id
	    FROM auth.roles
	    WHERE name = 'creator'
	),
	student_role AS (
	    SELECT id AS student_role_id
	    FROM auth.roles
	    WHERE name = 'student'
	),
	freecampus_org AS (
	    SELECT id AS freecampus_id
	    FROM auth.organizations
	    WHERE name = 'freeCampus'
	),
	now_utc AS (
	    SELECT NOW() AT TIME ZONE 'UTC' AS now
	),
	source AS (
	    SELECT *
	    FROM (
	        VALUES
	            ((SELECT creator_role_id FROM creator_role),
	             (SELECT creator_user_id FROM creator_user),
	             (SELECT freecampus_id FROM freecampus_org)),
	
	            ((SELECT student_role_id FROM student_role),
	             (SELECT student_user_id FROM student_user),
	             (SELECT freecampus_id FROM freecampus_org))
	    ) AS v(role_id, user_id, organization_id)
	)
INSERT INTO auth.roles_x_users_x_organizations (
    created_at, deleted_at,
    created_by_id, deleted_by_id,
    role_id, user_id, organization_id
)
SELECT
    n.now, NULL,
    su.sys_user_id, NULL,
    s.role_id, s.user_id, s.organization_id
FROM source s
CROSS JOIN sys_user su
CROSS JOIN now_utc n
ON CONFLICT (role_id, user_id, organization_id) DO NOTHING;

/* Add program types */
WITH sys_user AS (
	    SELECT id AS sys_user_id
	    FROM auth.users
	    WHERE username = 'system'
	),
	freecampus_org AS (
	    SELECT id AS freecampus_id
	    FROM auth.organizations
	    WHERE name = 'freeCampus'
	),
	now_utc AS (
	    SELECT NOW() AT TIME ZONE 'UTC' AS now
	),
	source AS (
	    SELECT *
	    FROM (
	        VALUES
	            ((SELECT freecampus_id FROM freecampus_org),
	             'standalone', TRUE, 'Standalone', 'Programa for standalone courses')
	    ) AS v(organization_id, name, is_translatable, title, description)
	)
INSERT INTO fc.program_types (
    uuid, organization_id,
    created_at, updated_at, deleted_at,
    created_by_id, updated_by_id, deleted_by_id,
    name, is_translatable, title, description
)
SELECT
    gen_random_uuid(),
    s.organization_id,
    n.now, n.now, NULL,
    su.sys_user_id, su.sys_user_id, NULL,
    s.name, s.is_translatable, s.title, s.description
FROM source s
CROSS JOIN sys_user su
CROSS JOIN now_utc n
ON CONFLICT (organization_id, name) DO NOTHING;

/* Add independiente program */
WITH
	sys_user AS (
	    SELECT id AS sys_user_id
	    FROM auth.users
	    WHERE username = 'system'
	),
	freecampus_org AS (
	    SELECT id AS freecampus_id
	    FROM auth.organizations
	    WHERE name = 'freeCampus'
	),
	standalone_type AS (
	    SELECT id AS standalone_id
	    FROM fc.program_types
	    WHERE name = 'standalone'
	),
	now_utc AS (
	    SELECT NOW() AT TIME ZONE 'UTC' AS now
	),
	source AS (
	    SELECT *
	    FROM (
	        VALUES
	            (
	                (SELECT freecampus_id FROM freecampus_org),
	                (SELECT standalone_id FROM standalone_type),
	                'standalone',
	                'standalone'
	            )
	    ) AS v(organization_id, type_id, name, code)
	)
INSERT INTO fc.programs (
    uuid, organization_id, type_id,
    created_at, updated_at, deleted_at,
    created_by_id, updated_by_id, deleted_by_id,
    name, code
)
SELECT
    gen_random_uuid(),
    s.organization_id,
    s.type_id,
    n.now, n.now, NULL,
    su.sys_user_id, su.sys_user_id, NULL,
    s.name, s.code
FROM source s
CROSS JOIN sys_user su
CROSS JOIN now_utc n
ON CONFLICT (organization_id, name) DO NOTHING;

/* Add independiente program version */
WITH
	sys_user AS (
	    SELECT id AS sys_user_id
	    FROM auth.users
	    WHERE username = 'system'
	),
	standalone_program AS (
	    SELECT id AS standalone_id
	    FROM fc.programs
	    WHERE name = 'standalone'
	),
	now_utc AS (
	    SELECT NOW() AT TIME ZONE 'UTC' AS now
	),
	source AS (
	    SELECT *
	    FROM (
	        VALUES
	            (
	                (SELECT standalone_id FROM standalone_program),
	                1,
	                '1.0',
	                null::BIGINT,
	                'Independiente',
	                'Programa para cursos independientes',
	                null::INT
	            )
	    ) AS v(program_id, version_number, version_label, previous_version_id, title, description, total_credits)
	)
INSERT INTO fc.program_versions (
    uuid, program_id,
    version_number, version_label, previous_version_id,
    title, description, total_credits,
    created_at, updated_at, deleted_at,
    created_by_id, updated_by_id, deleted_by_id
)
SELECT
    gen_random_uuid(),
    s.program_id,
    s.version_number, s.version_label, s.previous_version_id,
    s.title, s.description, s.total_credits,
    n.now, n.now, NULL,
    su.sys_user_id, su.sys_user_id, NULL
FROM source s
CROSS JOIN sys_user su
CROSS JOIN now_utc n
ON CONFLICT (program_id, version_number) DO NOTHING;

/* Add course types */
WITH
	sys_user AS (
	    SELECT id AS sys_user_id
	    FROM auth.users
	    WHERE username = 'system'
	),
	freecampus_org AS (
	    SELECT id AS freecampus_id
	    FROM auth.organizations
	    WHERE name = 'freeCampus'
	),
	now_utc AS (
	    SELECT NOW() AT TIME ZONE 'UTC' AS now
	),
	source AS (
	    SELECT *
	    FROM (
	        VALUES
	            (
	                (SELECT freecampus_id FROM freecampus_org),
	                'standalone',
	                TRUE,
	                'Standalone',
	                'Standalone course'
	            )
	    ) AS v(organization_id, name, is_translatable, title, description)
	)
INSERT INTO fc.course_types (
    uuid, organization_id,
    created_at, updated_at, deleted_at,
    created_by_id, updated_by_id, deleted_by_id,
    name, is_translatable,
    title, description
)
SELECT
    gen_random_uuid(),
    s.organization_id,
    n.now, n.now, NULL,
    su.sys_user_id, su.sys_user_id, NULL,
    s.name, s.is_translatable,
    s.title, s.description
FROM source s
CROSS JOIN sys_user su
CROSS JOIN now_utc n
ON CONFLICT (organization_id, name) DO NOTHING;

/* Add prueba 01 course */
WITH
	sys_user AS (
	    SELECT id AS sys_user_id
	    FROM auth.users
	    WHERE username = 'system'
	),
	freecampus_org AS (
	    SELECT id AS freecampus_id
	    FROM auth.organizations
	    WHERE name = 'freeCampus'
	),
	standalone_type AS (
	    SELECT id AS standalone_type_id
	    FROM fc.course_types
	    WHERE name = 'standalone'
	),
	now_utc AS (
	    SELECT NOW() AT TIME ZONE 'UTC' AS now
	),
	source AS (
	    SELECT *
	    FROM (
	        VALUES
	            (
	                (SELECT freecampus_id FROM freecampus_org),
	                (SELECT standalone_type_id FROM standalone_type),
	                'prueba01',
	                'prueba01',
	                TRUE
	            )
	    ) AS v(organization_id, type_id, code, name, is_standalone)
	)
INSERT INTO fc.courses (
    uuid, organization_id, type_id,
    created_at, updated_at, deleted_at,
    created_by_id, updated_by_id, deleted_by_id,
    code, name, is_standalone
)
SELECT
    gen_random_uuid(),
    s.organization_id,
    s.type_id,
    n.now, n.now, NULL,
    su.sys_user_id, su.sys_user_id, NULL,
    s.code, s.name, s.is_standalone
FROM source s
CROSS JOIN sys_user su
CROSS JOIN now_utc n
ON CONFLICT (organization_id, code) DO NOTHING;

/* Add prueba 01 course version */
WITH
	sys_user AS (
	    SELECT id AS sys_user_id
	    FROM auth.users
	    WHERE username = 'system'
	),
	course_prueba01 AS (
	    SELECT id AS course_id
	    FROM fc.courses
	    WHERE name = 'prueba01'
	),
	now_utc AS (
	    SELECT NOW() AT TIME ZONE 'UTC' AS now
	),
	source AS (
	    SELECT *
	    FROM (
	        VALUES
	            (
	                (SELECT course_id FROM course_prueba01),
	                1,
	                '1.0',
	                null::BIGINT,
	                'Curso de prueba 01',
	                'Descripción para el curso de prueba 01',
	                60,
	                30
	            )
	    ) AS v(course_id, version_number, version_label, previous_version_id, title, description, credits, hours)
	)
INSERT INTO fc.course_versions (
    uuid, course_id,
    version_number, version_label, previous_version_id,
    title, description, credits, hours,
    created_at, updated_at, deleted_at,
    created_by_id, updated_by_id, deleted_by_id
)
SELECT
    gen_random_uuid(),
    s.course_id,
    s.version_number, s.version_label, s.previous_version_id,
    s.title, s.description, s.credits, s.hours,
    n.now, n.now, NULL,
    su.sys_user_id, su.sys_user_id, NULL
FROM source s
CROSS JOIN sys_user su
CROSS JOIN now_utc n
ON CONFLICT (course_id, version_number) DO NOTHING;
