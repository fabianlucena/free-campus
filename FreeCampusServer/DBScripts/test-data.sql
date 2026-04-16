INSERT INTO auth.Users (
    Id, Uuid, CreatedAt, UpdatedAt, DeletedAt,
    CreatedById, UpdatedById, DeletedById,
    Username, DisplayName, Email, PasswordHash,
    IsActive, CanLogin, LastLogin
)
VALUES
(1, NEWID(), GETUTCDATE(), GETUTCDATE(), NULL,
 1, 1, NULL,
 'admin', 'Administrador', 'admin@example.com', 'HASH_ADMIN',
 1, 1, NULL),

(2, NEWID(), GETUTCDATE(), GETUTCDATE(), NULL,
 1, 1, NULL,
 'jdoe', 'John Doe', 'jdoe@example.com', 'HASH_JDOE',
 1, 1, NULL),

(3, NEWID(), GETUTCDATE(), GETUTCDATE(), NULL,
 1, 1, NULL,
 'mgarcia', 'María García', 'mgarcia@example.com', 'HASH_MGARCIA',
 1, 1, NULL);
