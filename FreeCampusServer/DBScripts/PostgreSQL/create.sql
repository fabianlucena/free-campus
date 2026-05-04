CREATE SCHEMA IF NOT EXISTS auth;

DO $$ -- users table
BEGIN
	CREATE TABLE IF NOT EXISTS auth.users(
		id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
		uuid UUID NOT NULL DEFAULT gen_random_uuid(),

		username VARCHAR(64) NOT NULL,
		display_name VARCHAR(128) NOT NULL,
		is_active BOOLEAN NOT NULL,
		can_login BOOLEAN NOT NULL,
		last_login_at TIMESTAMP NULL,

		created_at TIMESTAMP NOT NULL DEFAULT NOW(),
		created_by_id BIGINT NOT NULL,

		updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
		updated_by_id BIGINT NOT NULL,

		deleted_at TIMESTAMP NULL,
		deleted_by_id BIGINT NULL,

		CONSTRAINT uk_auth_users_username UNIQUE (username)
	);

	INSERT INTO auth.users (
		uuid,
		username, display_name,
		is_active, can_login, last_login_at,
		created_at, updated_at, deleted_at,
		created_by_id, updated_by_id, deleted_by_id
	)
	VALUES (
		gen_random_uuid(),
		'system', 'System',
		TRUE, TRUE, NULL,
		NOW(), NOW(), NULL,
		1, 1, NULL
	)
	ON CONFLICT (username) DO NOTHING;

	WITH sys AS (SELECT id FROM auth.users WHERE username = 'system')
	UPDATE auth.users u
	SET created_by_id = sys.id,
			updated_by_id = sys.id
	FROM sys
	WHERE u.id = sys.id;
	
	IF NOT EXISTS (
		SELECT 1
		FROM pg_constraint
		WHERE conname = 'fk_auth_users_created_by_id'
	) THEN
		ALTER TABLE auth.users
			ADD CONSTRAINT fk_auth_users_created_by_id
				FOREIGN KEY (created_by_id)
				REFERENCES auth.users(id)
				ON DELETE restrict;
	END IF;
	
	IF NOT EXISTS (
		SELECT 1
		FROM pg_constraint
		WHERE conname = 'fk_auth_users_updated_by_id'
	) THEN
		ALTER TABLE auth.users
			ADD CONSTRAINT fk_auth_users_updated_by_id
				FOREIGN KEY (updated_by_id)
				REFERENCES auth.users(id)
				ON DELETE restrict;
	END IF;
	
	IF NOT EXISTS (
		SELECT 1
		FROM pg_constraint
		WHERE conname = 'fk_auth_users_deleted_by_id'
	) THEN
		ALTER TABLE auth.users
			ADD CONSTRAINT fk_auth_users_deleted_by_id
				FOREIGN KEY (deleted_by_id)
				REFERENCES auth.users(id)
				ON DELETE restrict;
	END IF;
END $$;

-- user_passwords table
CREATE TABLE IF NOT EXISTS auth.user_passwords (
	user_id BIGINT NOT NULL PRIMARY KEY,

	password_hash VARCHAR(256) NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT fk_auth_user_passwords_user_id
		FOREIGN KEY (user_id) REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_user_passwords_created_by_id
		FOREIGN KEY (created_by_id) REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_user_passwords_updated_by_id
		FOREIGN KEY (updated_by_id) REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_user_passwords_deleted_by_id
		FOREIGN KEY (deleted_by_id) REFERENCES auth.users(id) ON DELETE RESTRICT
);

DO $$ -- Check system user
BEGIN
	IF NOT EXISTS (SELECT 1 FROM auth.users WHERE username = 'system') THEN
		RAISE EXCEPTION 'El usuario system no existe.';
	END IF;
END $$;

-- Insert default admin user
INSERT INTO auth.users (
	uuid,
	username, display_name,
	is_active, can_login, last_login_at,
	created_at, updated_at, deleted_at,
	created_by_id, updated_by_id, deleted_by_id
)
VALUES (
	gen_random_uuid(),
	'admin', 'Administrator',
	TRUE, TRUE, NULL,
	NOW(), NOW(), NULL,
	1, 1, NULL
)
ON CONFLICT (username) DO NOTHING;

-- Insert default admin user pasword (only if admin does not have password) 1234
INSERT INTO auth.user_passwords (
	user_id,
	password_hash,
	created_at, updated_at, deleted_at,
	created_by_id, updated_by_id, deleted_by_id
)
SELECT admin.id, 
	'100000.He2nIoHKO5PDiudeF3GV1Q==.OXZML34kQ8gPcsX01odwNpaNmNMkMzlggv5pLKqzekg=',
	NOW(), NOW(), NULL,
	system.id, system.id, null
FROM auth.users admin, auth.users system
where admin.username = 'admin'
	and system.username = 'system'
ON CONFLICT (user_id) DO NOTHING;

-- devices table
CREATE TABLE IF NOT EXISTS auth.devices (
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	token VARCHAR(64) NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	CONSTRAINT uk_auth_devices_token UNIQUE (token),

	CONSTRAINT fk_auth_devices_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- sessions table
CREATE TABLE IF NOT EXISTS auth.sessions (
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	token VARCHAR(64) NOT NULL,
	expire_at TIMESTAMP NOT NULL,
	auto_login_token VARCHAR(64) NOT NULL,
	last_used_at TIMESTAMP NOT NULL,
	closed_at TIMESTAMP NULL,

	user_id BIGINT NOT NULL,
	device_id BIGINT NOT NULL,
	data_json TEXT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	CONSTRAINT uk_auth_sessions_token UNIQUE (token),
	
	CONSTRAINT fk_auth_sessions_User FOREIGN KEY (user_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,
		
	CONSTRAINT fk_auth_sessions_Device FOREIGN KEY (device_id)
		REFERENCES auth.devices(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_sessions_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- organizations table
CREATE TABLE IF NOT EXISTS auth.organizations (
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	name VARCHAR(256) NOT NULL,
	is_active BOOLEAN NOT NULL,
	is_translatable BOOLEAN NOT NULL,
	title VARCHAR(256) NOT NULL,
	description VARCHAR(256) NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_auth_organizations_name UNIQUE (name),

	CONSTRAINT fk_auth_organizations_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_organizations_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_organizations_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- session_organizations table
CREATE TABLE IF NOT EXISTS auth.session_organizations (
	session_id BIGINT NOT NULL PRIMARY KEY,
	organization_id BIGINT NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT fk_auth_session_organizations_session_id FOREIGN KEY (session_id)
		REFERENCES auth.sessions(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_session_organizations_organization_id FOREIGN KEY (organization_id)
		REFERENCES auth.organizations(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_session_organizations_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_session_organizations_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_session_organizations_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- roles table
CREATE TABLE IF NOT EXISTS auth.roles (
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	name VARCHAR(256) NOT NULL,
	description TEXT NULL,

	CONSTRAINT uk_auth_roles_name UNIQUE (name),

	CONSTRAINT fk_auth_roles_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_roles_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_roles_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- Insert default admin role
INSERT INTO auth.roles (
	uuid,
	name, description,
	created_at, updated_at, deleted_at,
	created_by_id, updated_by_id, deleted_by_id
) SELECT 
		gen_random_uuid(),
		'admin', 'Administrator role with full privileges',
		NOW(), NOW(), NULL,
		system.id, system.id, NULL
	FROM auth.users system
	WHERE system.username = 'system'
ON CONFLICT (name) DO NOTHING;

-- Insert default system Organization
INSERT INTO auth.organizations (
	uuid,
	name, title, description, is_active, is_translatable,
	created_at, updated_at, deleted_at,
	created_by_id, updated_by_id, deleted_by_id
) SELECT 
		gen_random_uuid(),
		'system', 'System', 'System Organization for administrator purposes', TRUE, FALSE,
		NOW(), NOW(), NULL,
		system.id, system.id, NULL
	FROM auth.users system
	WHERE system.username = 'system'
ON CONFLICT (name) DO NOTHING;

-- roles_x_users_x_organizations table
CREATE TABLE IF NOT EXISTS auth.roles_x_users_x_organizations (
	role_id BIGINT NOT NULL,
	user_id BIGINT NOT NULL,
	organization_id BIGINT NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT pk_auth_roles_x_users_x_organizations PRIMARY KEY (role_id,user_id,organization_id),

	CONSTRAINT fk_auth_roles_x_users_x_organizations_role FOREIGN KEY (role_id)
		REFERENCES auth.roles(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_roles_x_users_x_organizations_User FOREIGN KEY (user_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_roles_x_users_x_organizations_Organization FOREIGN KEY (organization_id)
		REFERENCES auth.organizations(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_roles_x_users_x_organizations_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_roles_x_users_x_organizations_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- Insert default admin user's role
INSERT INTO auth.roles_x_users_x_organizations (
	role_id, user_id, organization_id,
	created_at, deleted_at,
	created_by_id, deleted_by_id
) SELECT 
		role_admin.id, user_admin.id, organization_system.id,
		NOW(), NULL,
		user_system.id, NULL
	FROM auth.roles role_admin,
		auth.users user_admin,
		auth.organizations organization_system,
		auth.users user_system
	WHERE role_admin.name = 'admin' 
		AND user_admin.username = 'admin' 
		AND organization_system.name = 'system'
		AND user_system.username = 'system'
ON CONFLICT (role_id, user_id, organization_id) DO NOTHING;

-- roles_includes table
CREATE TABLE IF NOT EXISTS auth.roles_includes (
	role_id BIGINT NOT NULL,
	include_id BIGINT NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT pk_auth_roles_includes PRIMARY KEY (role_id,include_id),

	CONSTRAINT fk_auth_roles_includes_role FOREIGN KEY (role_id)
		REFERENCES auth.roles(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_roles_includes_include FOREIGN KEY (include_id)
		REFERENCES auth.roles(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_roles_includes_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_roles_includes_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- permissions table
CREATE TABLE IF NOT EXISTS auth.permissions (
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	name VARCHAR(255) NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_auth_permissions_name UNIQUE (name),

	CONSTRAINT fk_auth_permissions_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_permissions_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- permissions_x_roles table
CREATE TABLE IF NOT EXISTS auth.permissions_x_roles (
	permission_id BIGINT NOT NULL,
	role_id BIGINT NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT pk_auth_permissions_x_roles PRIMARY KEY (permission_id,role_id),

	CONSTRAINT fk_auth_permissions_x_roles_Permission FOREIGN KEY (permission_id)
		REFERENCES auth.permissions(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_permissions_x_roles_role FOREIGN KEY (role_id)
		REFERENCES auth.roles(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_permissions_x_roles_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_permissions_x_roles_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

CREATE SCHEMA IF NOT EXISTS fc;

-- program_types table
CREATE TABLE IF NOT EXISTS fc.program_types(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	organization_id BIGINT NOT NULL,

	name VARCHAR(256) NOT NULL,
	title VARCHAR(256) NOT NULL,
	description TEXT NULL,

	is_translatable BOOLEAN NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_fc_pprogram_types UNIQUE (organization_id, name),

	CONSTRAINT fk_fc_program_types_organization_id FOREIGN KEY (organization_id)
		REFERENCES auth.organizations(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_program_types_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_program_types_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_program_types_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- programs table
CREATE TABLE IF NOT EXISTS fc.programs(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	organization_id BIGINT NOT NULL,
	type_id BIGINT NOT NULL,

	code VARCHAR(256) NOT NULL,
	name VARCHAR(256) NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_fc_programs UNIQUE (organization_id, name),

	CONSTRAINT fk_fc_programs_organization_id FOREIGN KEY (organization_id)
		REFERENCES auth.organizations(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_programs_type_id FOREIGN KEY (type_id)
		REFERENCES fc.program_types(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_programs_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_programs_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_programs_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- program_versions table
CREATE TABLE IF NOT EXISTS fc.program_versions(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	program_id BIGINT NOT NULL,

	version_number INT NOT NULL,
	version_label VARCHAR(256) NOT NULL,
	previous_version_id BIGINT NULL,

	title VARCHAR(256) NOT NULL,
	description TEXT NULL,
	total_credits INT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_fc_program_versions UNIQUE (program_id, version_number),

	CONSTRAINT fk_fc_program_versions_organization_id FOREIGN KEY (program_id)
		REFERENCES fc.programs(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_program_versions_previous_version_id FOREIGN KEY (previous_version_id)
		REFERENCES fc.program_versions(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_program_versions_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_program_versions_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_program_versions_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- course_types table
CREATE TABLE IF NOT EXISTS fc.course_types(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	organization_id BIGINT NOT NULL,

	name VARCHAR(256) NOT NULL,
	title VARCHAR(256) NOT NULL,
	description TEXT NULL,

	is_translatable BOOLEAN NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_fc_course_types UNIQUE (organization_id, name),

	CONSTRAINT fk_fc_course_types_organization_id FOREIGN KEY (organization_id)
		REFERENCES auth.organizations(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_types_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_types_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_types_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- courses table
CREATE TABLE IF NOT EXISTS fc.courses(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	organization_id BIGINT NOT NULL,
	type_id BIGINT NOT NULL,

	code VARCHAR(256) NOT NULL,
	name VARCHAR(256) NOT NULL,
	is_standalone BOOLEAN NULL,
	
	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_fc_courses UNIQUE (organization_id, code),

	CONSTRAINT fk_fc_courses_organization_id FOREIGN KEY (organization_id)
		REFERENCES auth.organizations(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_courses_type_id FOREIGN KEY (type_id)
		REFERENCES fc.course_types(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_courses_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_courses_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_courses_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- course_prerequisites table
CREATE TABLE IF NOT EXISTS fc.course_prerequisites(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	course_id BIGINT NOT NULL,
	prerequisite_id BIGINT NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_fc_course_prerequisites UNIQUE (course_id, prerequisite_id),

	CONSTRAINT fk_fc_course_prerequisites_course_id FOREIGN KEY (course_id)
		REFERENCES fc.courses(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_prerequisites_prerequisite_id FOREIGN KEY (prerequisite_id)
		REFERENCES fc.courses(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_prerequisites_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_prerequisites_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- course_versions table
CREATE TABLE IF NOT EXISTS fc.course_versions(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	course_id BIGINT NOT NULL,

	version_number INT NOT NULL,
	version_label VARCHAR(256) NOT NULL,
	previous_version_id BIGINT NULL,

	title VARCHAR(256) NOT NULL,
	description TEXT NULL,
	credits INT NULL,
	hours INT NULL,
	
	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_fc_course_versions UNIQUE (course_id, version_number),

	CONSTRAINT fk_fc_course_versions_organization_id FOREIGN KEY (course_id)
		REFERENCES fc.courses(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_versions_previous_version_id FOREIGN KEY (previous_version_id)
		REFERENCES fc.course_versions(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_versions_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_versions_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_versions_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- learning_item_types table
CREATE TABLE IF NOT EXISTS fc.learning_item_types(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	organization_id BIGINT NOT NULL,

	name VARCHAR(256) NOT NULL,
	title VARCHAR(256) NOT NULL,
	description TEXT NULL,

	is_translatable BOOLEAN NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_fc_learning_item_types UNIQUE (organization_id, name),

	CONSTRAINT fk_fc_learning_item_types_organization_id FOREIGN KEY (organization_id)
		REFERENCES auth.organizations(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_learning_item_types_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_learning_item_types_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_learning_item_types_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- learning_items table
CREATE TABLE IF NOT EXISTS fc.learning_items(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	organization_id BIGINT NOT NULL,
	type_id BIGINT NOT NULL,
	
	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT fk_fc_learning_items_organization_id FOREIGN KEY (organization_id)
		REFERENCES auth.organizations(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_learning_items_type_id FOREIGN KEY (type_id)
		REFERENCES fc.learning_item_types(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_learning_items_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_learning_items_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_learning_items_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- learning_item_versions table
CREATE TABLE IF NOT EXISTS fc.learning_item_versions(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	learning_item_id BIGINT NOT NULL,

	version_number INT NOT NULL,
	version_label VARCHAR(256) NOT NULL,
	previous_version_id BIGINT NULL,

	title VARCHAR(256) NOT NULL,
	description TEXT NULL,
	is_published BOOLEAN NOT NULL,
	
	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_fc_learning_item_versions UNIQUE (learning_item_id, version_number),

	CONSTRAINT fk_fc_learning_item_versions_organization_id FOREIGN KEY (learning_item_id)
		REFERENCES fc.learning_items(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_learning_item_versions_type_id FOREIGN KEY (previous_version_id)
		REFERENCES fc.learning_item_versions(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_learning_item_versions_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_learning_item_versions_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_learning_item_versions_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- program_version_x_course_versions table
CREATE TABLE IF NOT EXISTS fc.program_version_x_course_versions(
	program_version_id BIGINT NOT NULL,
	course_version_id BIGINT NOT NULL,

	code VARCHAR(256) NOT NULL,
	sequence_number INT NOT NULL,
	level INT NOT NULL,

	is_active BOOLEAN NOT NULL DEFAULT TRUE,
	is_core BOOLEAN NOT NULL DEFAULT TRUE,
	is_required BOOLEAN NOT NULL DEFAULT TRUE,
	is_elective BOOLEAN NOT NULL DEFAULT TRUE,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT pk_fc_program_version_x_course_versions PRIMARY KEY (program_version_id, course_version_id),

	CONSTRAINT fk_auth_program_version_x_course_versions_program_version_id FOREIGN KEY (program_version_id)
		REFERENCES fc.program_versions(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_program_version_x_course_versions_course_version_id FOREIGN KEY (course_version_id)
		REFERENCES fc.course_versions(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_program_version_x_course_versions_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_auth_program_version_x_course_versions_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- teaching_roles table
CREATE TABLE IF NOT EXISTS fc.teaching_roles(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	name VARCHAR(255) NULL,
	title VARCHAR(255) NULL,
	is_translatable BOOLEAN NOT NULL,
	description TEXT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_fc_teaching_roles UNIQUE (name),

	CONSTRAINT fk_fc_teaching_roles_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_teaching_roles_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_teaching_roles_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- teaching_permissions table
CREATE TABLE IF NOT EXISTS fc.teaching_permissions(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	name VARCHAR(255) NULL,
	title VARCHAR(255) NULL,
	is_translatable BOOLEAN NOT NULL,
	description TEXT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_fc_teaching_permissions UNIQUE (name),

	CONSTRAINT fk_fc_teaching_permissions_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_teaching_permissions_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_teaching_permissions_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- teaching_assignments table
CREATE TABLE IF NOT EXISTS fc.teaching_assignments(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	course_id BIGINT NOT NULL,
	instructor_id BIGINT NOT NULL,
	teaching_role_id BIGINT NOT NULL,

	assigned_at TIMESTAMP NOT NULL,
	assigned_by_id BIGINT NOT NULL,

	is_active BOOLEAN NOT NULL DEFAULT TRUE,
	notes TEXT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_fc_teaching_assignments UNIQUE (course_id, instructor_id, teaching_role_id, assigned_at),

	CONSTRAINT fk_fc_teaching_assignments_course_id FOREIGN KEY (course_id)
		REFERENCES fc.courses(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_teaching_assignments_instructor_id FOREIGN KEY (instructor_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_teaching_assignments_teaching_role_id FOREIGN KEY (teaching_role_id)
		REFERENCES fc.teaching_roles(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_teaching_assignments_assigned_by_id FOREIGN KEY (assigned_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_teaching_assignments_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_teaching_assignments_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_teaching_assignments_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- teaching_assignment_x_permissions table
CREATE TABLE IF NOT EXISTS fc.teaching_assignment_x_permissions(
	teaching_assignment_id BIGINT NOT NULL,
	permissions_id BIGINT NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT pk_fc_teaching_assignment_x_permissions PRIMARY KEY (teaching_assignment_id, permissions_id),

	CONSTRAINT fk_fc_teaching_assignment_x_permissions_program_version_id FOREIGN KEY (teaching_assignment_id)
		REFERENCES fc.teaching_assignments(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_teaching_assignment_x_permissions_course_version_id FOREIGN KEY (permissions_id)
		REFERENCES fc.teaching_permissions(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_teaching_assignment_x_permissions_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_teaching_assignment_x_permissions_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

-- course_enrollment_statuses table
CREATE TABLE IF NOT EXISTS fc.course_enrollment_statuses(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	organization_id BIGINT NOT NULL,

	display_order INT NOT NULL,
	name VARCHAR(256) NOT NULL,
	is_active BOOLEAN NOT NULL DEFAULT TRUE,
	title VARCHAR(256) NOT NULL,
	description TEXT NULL,

	is_translatable BOOLEAN NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_fc_course_enrollment_statuses UNIQUE (organization_id, name),

	CONSTRAINT fk_fc_course_enrollment_statuses_organization_id FOREIGN KEY (organization_id)
		REFERENCES auth.organizations(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_enrollment_statuses_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_enrollment_statuses_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_enrollment_statuses_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

/* course_enrollments table */
CREATE TABLE IF NOT EXISTS fc.course_enrollments(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	course_version_id BIGINT NOT NULL,
	student_id BIGINT NOT NULL,

	enrolled_at TIMESTAMP NOT NULL,
	enrolled_by_id BIGINT NOT NULL,
	completed_at TIMESTAMP NULL,
	dropped_at TIMESTAMP NULL,
	status_id BIGINT NOT NULL,
	final_grade NUMERIC(5, 2) NULL,
	is_active BOOLEAN NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_fc_course_enrollments UNIQUE (student_id,course_version_id,enrolled_at),

	CONSTRAINT fk_fc_course_enrollments_course_id FOREIGN KEY (course_version_id)
		REFERENCES fc.course_versions(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_enrollments_student_id FOREIGN KEY (student_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_enrollments_enrolled_by_id FOREIGN KEY (enrolled_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_enrollments_status_id FOREIGN KEY (status_id)
		REFERENCES fc.course_enrollment_statuses(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_enrollments_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_enrollments_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_course_enrollments_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

/* program_enrollment_statuses table */
CREATE TABLE IF NOT EXISTS fc.program_enrollment_statuses(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	organization_id BIGINT NOT NULL,

	display_order INT NOT NULL,
	name VARCHAR(256) NOT NULL,
	is_active BOOLEAN NOT NULL DEFAULT TRUE,
	title VARCHAR(256) NOT NULL,
	description TEXT NULL,

	is_translatable BOOLEAN NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_fc_program_enrollment_statuses UNIQUE (organization_id, name),

	CONSTRAINT fk_fc_program_enrollment_statuses_organization_id FOREIGN KEY (organization_id)
		REFERENCES auth.organizations(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_program_enrollment_statuses_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_program_enrollment_statuses_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_program_enrollment_statuses_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);

/* program_enrollments table */
CREATE TABLE IF NOT EXISTS fc.program_enrollments(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	uuid UUID NOT NULL DEFAULT gen_random_uuid(),

	program_version_id BIGINT NOT NULL,
	student_id BIGINT NOT NULL,

	enrolled_at TIMESTAMP NOT NULL,
	enrolled_by_id BIGINT NOT NULL,
	completed_at TIMESTAMP NULL,
	dropped_at TIMESTAMP NULL,
	status_id BIGINT NOT NULL,
	final_grade NUMERIC(5, 2) NULL,
	is_active BOOLEAN NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT NOW(),
	created_by_id BIGINT NOT NULL,

	updated_at TIMESTAMP NOT NULL DEFAULT NOW(),
	updated_by_id BIGINT NOT NULL,

	deleted_at TIMESTAMP NULL,
	deleted_by_id BIGINT NULL,

	CONSTRAINT uk_fc_program_enrollments UNIQUE (program_version_id, student_id, enrolled_at),

	CONSTRAINT fk_fc_program_enrollments_program_id FOREIGN KEY (program_version_id)
		REFERENCES fc.program_versions(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_program_enrollments_student_id FOREIGN KEY (student_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_program_enrollments_enrolled_by_id FOREIGN KEY (enrolled_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_program_enrollments_status_id FOREIGN KEY (status_id)
		REFERENCES fc.program_enrollment_statuses(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_program_enrollments_created_by_id FOREIGN KEY (created_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_program_enrollments_updated_by_id FOREIGN KEY (updated_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT,

	CONSTRAINT fk_fc_program_enrollments_deleted_by_id FOREIGN KEY (deleted_by_id)
		REFERENCES auth.users(id) ON DELETE RESTRICT
);